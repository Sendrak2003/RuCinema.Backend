using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace My_Final_Project.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        //    migrationBuilder.CreateTable(
        //        name: "ActorMovie",
        //        columns: table => new
        //        {
        //            ActorId = table.Column<int>(type: "int", nullable: false),
        //            MovieId = table.Column<int>(type: "int", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_ActorMovie", x => new { x.ActorId, x.MovieId });
        //        });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Content_Type",
                columns: table => new
                {
                    Content_Type_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content_Type_Name = table.Column<string>(type: "NVARCHAR(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Content___33E2D62219ACA25C", x => x.Content_Type_ID);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Country_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country_Name = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: true),
                    Flag_Image = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Countrie__8036CB4EEFC4EE01", x => x.Country_ID);
                });

            //migrationBuilder.CreateTable(
            //    name: "DirectorMovie",
            //    columns: table => new
            //    {
            //        DirectorId = table.Column<int>(type: "int", nullable: false),
            //        MovieId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_DirectorMovie", x => new { x.DirectorId, x.MovieId });
            //    });

            migrationBuilder.CreateTable(
                name: "Directors",
                columns: table => new
                {
                    Director_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Full_Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Director__3939BCE1696F064B", x => x.Director_ID);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Genre_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Genre_Name = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Genres__964A2006799799D3", x => x.Genre_ID);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Tag_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tag_Name = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tags__D0AC5C33A5E8C8DB", x => x.Tag_ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Actors",
                columns: table => new
                {
                    Actor_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Full_Name = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    Country_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Actors__E57403ED5B0C4E2A", x => x.Actor_ID);
                    table.ForeignKey(
                        name: "FK__Actors__Country___2A4B4B5E",
                        column: x => x.Country_ID,
                        principalTable: "Countries",
                        principalColumn: "Country_ID");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Country_ID = table.Column<int>(type: "int", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Date_of_Birth = table.Column<DateTime>(type: "date", nullable: false),
                    Registration_Date = table.Column<DateTime>(type: "date", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    userPhoto = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__206D91905A81CD68", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Users__Country_I__300424B4",
                        column: x => x.CityId,
                        principalTable: "Countries",
                        principalColumn: "Country_ID");
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    City_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country_ID = table.Column<int>(type: "int", nullable: true),
                    City_Name = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cities__DE9DE0209382E0F6", x => x.City_ID);
                    table.ForeignKey(
                        name: "FK__Cities__Country___2D27B809",
                        column: x => x.Country_ID,
                        principalTable: "Countries",
                        principalColumn: "Country_ID");
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Movie_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country_ID = table.Column<int>(type: "int", nullable: true),
                    Content_Type_ID = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    Start_Date = table.Column<DateTime>(type: "date", nullable: false),
                    End_Date = table.Column<DateTime>(type: "date", nullable: false),
                    Date_Added = table.Column<DateTime>(type: "date", nullable: true),
                    Currency = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Cover_Image_URL = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    Short_Description = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    Released_Episodes = table.Column<int>(type: "int", nullable: false),
                    Total_Episodes = table.Column<int>(type: "int", nullable: false),
                    IsFinished = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Movies__7A880405F20AA26B", x => x.Movie_ID);
                    table.ForeignKey(
                        name: "FK__Movies__Content___37A5467C",
                        column: x => x.Content_Type_ID,
                        principalTable: "Content_Type",
                        principalColumn: "Content_Type_ID");
                    table.ForeignKey(
                        name: "FK__Movies__Country__36B12243",
                        column: x => x.Country_ID,
                        principalTable: "Countries",
                        principalColumn: "Country_ID");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Announcements",
                columns: table => new
                {
                    Announcement_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Movie_ID = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    Short_Description = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    Announcement_Date = table.Column<DateTime>(type: "date", nullable: true),
                    Trailer_URL = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Announce__853AB7CF91279941", x => x.Announcement_ID);
                    table.ForeignKey(
                        name: "FK__Announcem__Movie__3C69FB99",
                        column: x => x.Movie_ID,
                        principalTable: "Movies",
                        principalColumn: "Movie_ID");
                });

            migrationBuilder.CreateTable(
                name: "Awards",
                columns: table => new
                {
                    Award_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Movie_ID = table.Column<int>(type: "int", nullable: true),
                    Award_Name = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    Award_Year = table.Column<int>(type: "int", nullable: false),
                    Award_Photo_URL = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Awards__30C01BCD528A7834", x => x.Award_ID);
                    table.ForeignKey(
                        name: "FK__Awards__Movie_ID__693CA210",
                        column: x => x.Movie_ID,
                        principalTable: "Movies",
                        principalColumn: "Movie_ID");
                });

            migrationBuilder.CreateTable(
                name: "Downloads",
                columns: table => new
                {
                    Download_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Movie_ID = table.Column<int>(type: "int", nullable: true),
                    User_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Download_Date = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Download__423297A7C412C268", x => x.Download_ID);
                    table.ForeignKey(
                        name: "FK__Downloads__Movie__46E78A0C",
                        column: x => x.Movie_ID,
                        principalTable: "Movies",
                        principalColumn: "Movie_ID");
                    table.ForeignKey(
                        name: "FK__Downloads__User___47DBAE45",
                        column: x => x.User_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Episodes",
                columns: table => new
                {
                    Episode_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Movie_ID = table.Column<int>(type: "int", nullable: true),
                    Episode_Number = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    Title = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    Short_Description = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    Release_Date = table.Column<DateTime>(type: "date", nullable: true),
                    File_URL = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Episodes__5ABD44031620DA0D", x => x.Episode_ID);
                    table.ForeignKey(
                        name: "FK__Episodes__Movie___398D8EEE",
                        column: x => x.Movie_ID,
                        principalTable: "Movies",
                        principalColumn: "Movie_ID");
                });

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    Favorite_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Movie_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Favorite__749FA5A71BA754A0", x => x.Favorite_ID);
                    table.ForeignKey(
                        name: "FK__Favorites__Movie__5812160E",
                        column: x => x.Movie_ID,
                        principalTable: "Movies",
                        principalColumn: "Movie_ID");
                    table.ForeignKey(
                        name: "FK__Favorites__User___571DF1D5",
                        column: x => x.User_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Genre_Movie",
                columns: table => new
                {
                    Movie_ID = table.Column<int>(type: "int", nullable: false),
                    Genre_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Genre_Mo__03ECA605F3AF6449", x => new { x.Movie_ID, x.Genre_ID });
                    table.ForeignKey(
                        name: "FK__Genre_Mov__Genre__5FB337D6",
                        column: x => x.Genre_ID,
                        principalTable: "Genres",
                        principalColumn: "Genre_ID");
                    table.ForeignKey(
                        name: "FK__Genre_Mov__Movie__5EBF139D",
                        column: x => x.Movie_ID,
                        principalTable: "Movies",
                        principalColumn: "Movie_ID");
                });

            migrationBuilder.CreateTable(
                name: "Movie_Actor",
                columns: table => new
                {
                    Movie_ID = table.Column<int>(type: "int", nullable: false),
                    Actor_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Movie_Ac__B4DF443BA52AC0A3", x => new { x.Movie_ID, x.Actor_ID });
                    table.ForeignKey(
                        name: "FK__Movie_Act__Actor__403A8C7D",
                        column: x => x.Actor_ID,
                        principalTable: "Actors",
                        principalColumn: "Actor_ID");
                    table.ForeignKey(
                        name: "FK__Movie_Act__Movie__3F466844",
                        column: x => x.Movie_ID,
                        principalTable: "Movies",
                        principalColumn: "Movie_ID");
                });

            migrationBuilder.CreateTable(
                name: "Movie_Director",
                columns: table => new
                {
                    Movie_ID = table.Column<int>(type: "int", nullable: false),
                    Director_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Movie_Di__791B9FCB5E6F89FE", x => new { x.Movie_ID, x.Director_ID });
                    table.ForeignKey(
                        name: "FK__Movie_Dir__Direc__440B1D61",
                        column: x => x.Director_ID,
                        principalTable: "Directors",
                        principalColumn: "Director_ID");
                    table.ForeignKey(
                        name: "FK__Movie_Dir__Movie__4316F928",
                        column: x => x.Movie_ID,
                        principalTable: "Movies",
                        principalColumn: "Movie_ID");
                });

            migrationBuilder.CreateTable(
                name: "Movie_Fragments",
                columns: table => new
                {
                    Fragment_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Movie_ID = table.Column<int>(type: "int", nullable: true),
                    Image_URL = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Movie_Fr__D36055D2EC26CF7B", x => x.Fragment_ID);
                    table.ForeignKey(
                        name: "FK__Movie_Fra__Movie__628FA481",
                        column: x => x.Movie_ID,
                        principalTable: "Movies",
                        principalColumn: "Movie_ID");
                });

            migrationBuilder.CreateTable(
                name: "Movie_Tag",
                columns: table => new
                {
                    Movie_ID = table.Column<int>(type: "int", nullable: false),
                    Tag_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Movie_Ta__5782C1C6B3634117", x => new { x.Movie_ID, x.Tag_ID });
                    table.ForeignKey(
                        name: "FK__Movie_Tag__Movie__5AEE82B9",
                        column: x => x.Movie_ID,
                        principalTable: "Movies",
                        principalColumn: "Movie_ID");
                    table.ForeignKey(
                        name: "FK__Movie_Tag__Tag_I__5BE2A6F2",
                        column: x => x.Tag_ID,
                        principalTable: "Tags",
                        principalColumn: "Tag_ID");
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Rating_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Movie_ID = table.Column<int>(type: "int", nullable: true),
                    User_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Rating_Count = table.Column<double>(type: "float", nullable: false),
                    Rating_Date = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Ratings__BE48C8253983175E", x => x.Rating_ID);
                    table.ForeignKey(
                        name: "FK__Ratings__Movie_I__4AB81AF0",
                        column: x => x.Movie_ID,
                        principalTable: "Movies",
                        principalColumn: "Movie_ID");
                    table.ForeignKey(
                        name: "FK__Ratings__User_ID__4BAC3F29",
                        column: x => x.User_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Review_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Movie_ID = table.Column<int>(type: "int", nullable: true),
                    User_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Likes = table.Column<int>(type: "int", nullable: false),
                    Review_Text = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    Publication_Date = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Reviews__F85DA7EB44FDD2ED", x => x.Review_ID);
                    table.ForeignKey(
                        name: "FK__Reviews__Movie_I__4F7CD00D",
                        column: x => x.Movie_ID,
                        principalTable: "Movies",
                        principalColumn: "Movie_ID");
                    table.ForeignKey(
                        name: "FK__Reviews__User_ID__5070F446",
                        column: x => x.User_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Roles_Actors",
                columns: table => new
                {
                    Role_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role_Name = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    Actor_ID = table.Column<int>(type: "int", nullable: true),
                    Movie_ID = table.Column<int>(type: "int", nullable: true),
                    Actor_Photo_URL = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Roles_Ac__D80AB49B70635F5D", x => x.Role_ID);
                    table.ForeignKey(
                        name: "FK__Roles_Act__Actor__656C112C",
                        column: x => x.Actor_ID,
                        principalTable: "Actors",
                        principalColumn: "Actor_ID");
                    table.ForeignKey(
                        name: "FK__Roles_Act__Movie__66603565",
                        column: x => x.Movie_ID,
                        principalTable: "Movies",
                        principalColumn: "Movie_ID");
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Comment_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Review_ID = table.Column<int>(type: "int", nullable: true),
                    Parent_Comment_ID = table.Column<int>(type: "int", nullable: true),
                    Movie_ID = table.Column<int>(type: "int", nullable: true),
                    User_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Likes = table.Column<int>(type: "int", nullable: false),
                    Comment_Text = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    Publication_Date = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Comments__99FC143B9CCE4679", x => x.Comment_ID);
                    table.ForeignKey(
                        name: "FK__Comments__Movie___534D60F1",
                        column: x => x.Movie_ID,
                        principalTable: "Movies",
                        principalColumn: "Movie_ID");
                    table.ForeignKey(
                        name: "FK__Comments__Parent__5535A963",
                        column: x => x.Parent_Comment_ID,
                        principalTable: "Comments",
                        principalColumn: "Comment_ID");
                    table.ForeignKey(
                        name: "FK__Comments__Review__5629CD9C",
                        column: x => x.Review_ID,
                        principalTable: "Reviews",
                        principalColumn: "Review_ID");
                    table.ForeignKey(
                        name: "FK__Comments__User_I__5441852A",
                        column: x => x.User_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actors_Country_ID",
                table: "Actors",
                column: "Country_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_Movie_ID",
                table: "Announcements",
                column: "Movie_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CityId",
                table: "AspNetUsers",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Awards_Movie_ID",
                table: "Awards",
                column: "Movie_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Country_ID",
                table: "Cities",
                column: "Country_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_Movie_ID",
                table: "Comments",
                column: "Movie_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_Parent_Comment_ID",
                table: "Comments",
                column: "Parent_Comment_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_Review_ID",
                table: "Comments",
                column: "Review_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_User_ID",
                table: "Comments",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Downloads_Movie_ID",
                table: "Downloads",
                column: "Movie_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Downloads_User_ID",
                table: "Downloads",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_Movie_ID",
                table: "Episodes",
                column: "Movie_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_Movie_ID",
                table: "Favorites",
                column: "Movie_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_User_ID",
                table: "Favorites",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Genre_Movie_Genre_ID",
                table: "Genre_Movie",
                column: "Genre_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Movie_Actor_Actor_ID",
                table: "Movie_Actor",
                column: "Actor_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Movie_Director_Director_ID",
                table: "Movie_Director",
                column: "Director_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Movie_Fragments_Movie_ID",
                table: "Movie_Fragments",
                column: "Movie_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Movie_Tag_Tag_ID",
                table: "Movie_Tag",
                column: "Tag_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_Content_Type_ID",
                table: "Movies",
                column: "Content_Type_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_Country_ID",
                table: "Movies",
                column: "Country_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_Movie_ID",
                table: "Ratings",
                column: "Movie_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_User_ID",
                table: "Ratings",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_Movie_ID",
                table: "Reviews",
                column: "Movie_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_User_ID",
                table: "Reviews",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Actors_Actor_ID",
                table: "Roles_Actors",
                column: "Actor_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Actors_Movie_ID",
                table: "Roles_Actors",
                column: "Movie_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        //    migrationBuilder.DropTable(
        //        name: "ActorMovie");

            migrationBuilder.DropTable(
                name: "Announcements");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Awards");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Comments");

            //migrationBuilder.DropTable(
            //    name: "DirectorMovie");

            migrationBuilder.DropTable(
                name: "Downloads");

            migrationBuilder.DropTable(
                name: "Episodes");

            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropTable(
                name: "Genre_Movie");

            migrationBuilder.DropTable(
                name: "Movie_Actor");

            migrationBuilder.DropTable(
                name: "Movie_Director");

            migrationBuilder.DropTable(
                name: "Movie_Fragments");

            migrationBuilder.DropTable(
                name: "Movie_Tag");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Roles_Actors");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Directors");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Actors");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Content_Type");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
