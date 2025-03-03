using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace My_Final_Project.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Логин обязателен для заполнения.")]
        [StringLength(50, ErrorMessage = "Логин должен быть не более 50 символов.")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Логин может содержать только буквы и цифры.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email обязателен для заполнения.")]
        [EmailAddress(ErrorMessage = "Введите корректный адрес Email.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Имя обязательно для заполнения.")]
        [StringLength(50, ErrorMessage = "Имя должно быть не более 50 символов.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Фамилия обязательна для заполнения.")]
        [StringLength(50, ErrorMessage = "Фамилия должна быть не более 50 символов.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Пароль обязателен для заполнения.")]
        [DataType(DataType.Password)]
        [StringLength(32, MinimumLength = 6, ErrorMessage = "Пароль должен быть от 6 до 32 символов.")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[_#?!@$%^&*-]).{8,32}$", ErrorMessage = "Пароль должен содержать: заглавную букву, строчную букву, цифру, символ.")]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Путь к фото обязателен для заполнения.")]
        [StringLength(255, ErrorMessage = "Путь к фото не может быть длиннее 255 символов.")]
        public string PhotoPath { get; set; } = string.Empty;

        [Required(ErrorMessage = "Обязательно для щаполнения")]
        public int CountryId { get; set; }

        [Required(ErrorMessage = "Обязательно для щаполнения")]
        public DateTime DateOfBirth { get; set; }
    }
}
