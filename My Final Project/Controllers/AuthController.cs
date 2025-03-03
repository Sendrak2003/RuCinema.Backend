using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using My_Final_Project.Areas.Identity.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using My_Final_Project.Models;
using Microsoft.AspNetCore.Authorization;

namespace My_Final_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        public AuthController(UserManager<ApplicationUser> userManager, IConfiguration configuration, IWebHostEnvironment environment)
        {
            _userManager = userManager;
            _configuration = configuration;
            _environment = environment;
        }

        [HttpPost("login")]
        public async Task<JsonResult> Login([FromBody] LoginModel login)
        {
            if (string.IsNullOrEmpty(login.Email) && string.IsNullOrEmpty(login.Username))
            {
                return new JsonResult(new { message = "Не указаны email или имя пользователя." }) { StatusCode = StatusCodes.Status400BadRequest };
            }

            ApplicationUser user = null;

            if (!string.IsNullOrEmpty(login.Email))
            {
                user = await _userManager.FindByEmailAsync(login.Email);
            }

            if (user == null && !string.IsNullOrEmpty(login.Username))
            {
                user = await _userManager.FindByNameAsync(login.Username);
            }

            if (user == null || !await _userManager.CheckPasswordAsync(user, login.Password))
            {
                return new JsonResult(new { message = "Неверные учетные данные." }) { StatusCode = StatusCodes.Status401Unauthorized };
            }

            var token = await GenerateJwtToken(user);
            var tokenExpiry = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiryInMinutes"]));
            var baseUrl = $"{Request.Scheme}://{Request.Host.Value}";

            return new JsonResult(new
            {
                Token = token,
                TokenExpiry = tokenExpiry,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Photo = string.IsNullOrEmpty(user.userPhoto) ? "" : baseUrl + user.userPhoto,
            })
            { StatusCode = StatusCodes.Status200OK };
        }

        [HttpPost("register")]
        public async Task<JsonResult> Register([FromBody] RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(ModelState) { StatusCode = StatusCodes.Status400BadRequest };
            }

            if (await _userManager.FindByEmailAsync(registerModel.Email) != null)
            {
                return new JsonResult(new { message = "Email уже используется." }) { StatusCode = StatusCodes.Status400BadRequest };
            }
            if (await _userManager.FindByNameAsync(registerModel.Username) != null)
            {
                return new JsonResult(new { message = "Логин уже используется." }) { StatusCode = StatusCodes.Status400BadRequest };
            }

            // Перемещение последнего загруженного фото
            if (!string.IsNullOrEmpty(registerModel.PhotoPath) && System.IO.File.Exists(registerModel.PhotoPath))
            {
                var targetPath = Path.Combine(_environment.WebRootPath, "User Photo", Path.GetFileName(registerModel.PhotoPath));

                try
                {
                    System.IO.File.Move(registerModel.PhotoPath, targetPath);
                    registerModel.PhotoPath = targetPath;
                }
                catch (IOException ioEx)
                {
                    return new JsonResult(new { message = "Ошибка при перемещении файла: " + ioEx.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
                }
                catch (Exception ex)
                {
                    return new JsonResult(new { message = "Произошла непредвиденная ошибка: " + ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
                }
            }

            var user = new ApplicationUser
            {
                UserName = registerModel.Username,
                Email = registerModel.Email,
                FirstName = registerModel.FirstName,
                LastName = registerModel.LastName,
                DateOfBirth = registerModel.DateOfBirth,
                CountryId = registerModel.CountryId,
                RegistrationDate = DateTime.UtcNow,
                userPhoto = Path.Combine("User Photo", Path.GetFileName(registerModel.PhotoPath))
            };

            var result = await _userManager.CreateAsync(user, registerModel.Password);

            if (result.Succeeded)
            {
                var token = await GenerateJwtToken(user);
                var tokenExpiry = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiryInMinutes"]));
                var baseUrl = $"{Request.Scheme}://{Request.Host.Value}";

                return new JsonResult(new
                {
                    Token = token,
                    TokenExpiry = tokenExpiry,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Photo = string.IsNullOrEmpty(user.userPhoto) ? "" : baseUrl + user.userPhoto,
                })
                { StatusCode = StatusCodes.Status200OK };
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return new JsonResult(ModelState) { StatusCode = StatusCodes.Status400BadRequest };
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<JsonResult> GetCurrentUser()
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;

            if (claimsIdentity == null)
            {
                return new JsonResult(new { message = "Не удалось аутентифицировать пользователя." }) { StatusCode = StatusCodes.Status401Unauthorized };
            }

            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return new JsonResult(new { message = "Идентификатор пользователя не найден." }) { StatusCode = StatusCodes.Status401Unauthorized };
            }

            var user = await _userManager.FindByEmailAsync(userId);
            if (user == null)
            {
                return new JsonResult(new { message = "Пользователь не найден." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            var baseUrl = $"{Request.Scheme}://{Request.Host.Value}";

            return new JsonResult(new
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Photo = string.IsNullOrEmpty(user.userPhoto) ? "" : baseUrl + user.userPhoto,
                DateOfBirth = user.DateOfBirth
            })
            { StatusCode = StatusCodes.Status200OK };
        }


        private async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:SigningKey"]);

            // Создаем список claims
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), 
                new Claim(JwtRegisteredClaimNames.NameId, user.Id)
            };

            // Получаем роли пользователя асинхронно
            var roles = await _userManager.GetRolesAsync(user); // Теперь используется await
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role)); // Добавление роли в claims
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiryInMinutes"])), // Время действия токена
                Issuer = _configuration["Jwt:Site"],
                Audience = _configuration["Jwt:Site"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256) // Подпись
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token); 
        }


        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:SigningKey"]);

            try
            {
                return tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["Jwt:Issuer"], 
                    ValidateAudience = true,
                    ValidAudience = _configuration["Jwt:Site"],
                    ValidateLifetime = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
            }
            catch (SecurityTokenExpiredException)
            {
                Console.WriteLine("Токен истек.");
                return new ClaimsPrincipal();
            }
            catch (SecurityTokenValidationException ex)
            {
                Console.WriteLine($"Ошибка валидации токена: {ex.Message}");
                return new ClaimsPrincipal(); // Возвращаем пустой ClaimsPrincipal
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                return new ClaimsPrincipal(); // Возвращаем пустой ClaimsPrincipal
            }

        }


        [HttpPost("upload")]
        public async Task<JsonResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return new JsonResult(new { message = "Файл не был загружен." }) { StatusCode = StatusCodes.Status400BadRequest };
            }

            if (file.ContentType != "image/jpeg" && file.ContentType != "image/png")
            {
                return new JsonResult(new { message = "Недопустимый тип файла. Допустимые типы: image/jpeg, image/png." }) { StatusCode = StatusCodes.Status400BadRequest };
            }

            string filename = $"user_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var uploadDir = Path.Combine(_environment.WebRootPath, "Upload", "Files");

            if (!Directory.Exists(uploadDir))
            {
                Directory.CreateDirectory(uploadDir);
            }

            var uploadPath = Path.Combine(uploadDir, filename);

            try
            {
                using (var stream = new FileStream(uploadPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return new JsonResult(new { FilePath = uploadPath }) { StatusCode = StatusCodes.Status200OK };
            }
            catch (Exception ex)
            {
                return new JsonResult(new { message = "Ошибка при загрузке файла: " + ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }



    }
}
