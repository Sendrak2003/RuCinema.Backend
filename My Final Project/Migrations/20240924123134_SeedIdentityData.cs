using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Collections.Generic;

#nullable disable

namespace My_Final_Project.Migrations
{
    /// <inheritdoc />
    public partial class SeedIdentityData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var passwordHasher = new PasswordHasher<IdentityUser>();

            migrationBuilder.Sql(@"
        INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [Date_of_Birth], [userPhoto])
        VALUES (N'80b27df5-1519-4177-b7d3-aae75f58e655', N'admin123', N'ADMIN123', N'admin@ya.ru', N'ADMIN@YA.RU', 1, '" + passwordHasher.HashPassword(null, "password") + @"', N'KJHGFDSPOIUYTREWQASDFGHJKLZXCVBNM', N'QWERTYUIOPASDFGHJKLZXCVBNMQWERTYU', NULL, 0, 0, NULL, 1, 0, N'Admin', N'One', '2000-01-01', N'/User Photo/defultPhoto.jpg');

        INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [Date_of_Birth], [userPhoto])
        VALUES (N'f1f118bd-76c4-4ccc-bd63-5986dc9c65ac', N'user123', N'USER123', N'user@ya.ru', N'USER@YA.RU', 1, '" + passwordHasher.HashPassword(null, "password") + @"', N'ZXCVBNMQWERTYUIOPASDFGHJKLQWERTYU', N'ASDFGHJKLZXCVBNMQWERTYUIOPASDFGHJK', NULL, 0, 0, NULL, 1, 0, N'User', N'One', '2001-02-02', N'/User Photo/user1.jpg');

        INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp])
        VALUES (N'ec178006-e20d-4297-808d-c9dd2437a3b3', N'User', N'USER', NULL);

        INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp])
        VALUES (N'9c4a8b2d-0f5a-4e60-9cf1-2a6d8c1e2f03', N'Admin', N'ADMIN', NULL);

        INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId])
        VALUES (N'f1f118bd-76c4-4ccc-bd63-5986dc9c65ac', N'ec178006-e20d-4297-808d-c9dd2437a3b3');

        INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId])
        VALUES (N'80b27df5-1519-4177-b7d3-aae75f58e655', N'9c4a8b2d-0f5a-4e60-9cf1-2a6d8c1e2f03');

        INSERT INTO [dbo].[AspNetRoleClaims] ([RoleId], [ClaimType], [ClaimValue])
        VALUES (N'ec178006-e20d-4297-808d-c9dd2437a3b3', N'role', N'Admin');

        INSERT INTO [dbo].[AspNetRoleClaims] ([RoleId], [ClaimType], [ClaimValue])
        VALUES (N'9c4a8b2d-0f5a-4e60-9cf1-2a6d8c1e2f03', N'role', N'User');

        INSERT INTO [dbo].[AspNetUserClaims] ([UserId], [ClaimType], [ClaimValue])
        VALUES (N'80b27df5-1519-4177-b7d3-aae75f58e655', N'Position', N'Admin');

        INSERT INTO [dbo].[AspNetUserClaims] ([UserId], [ClaimType], [ClaimValue])
        VALUES (N'f1f118bd-76c4-4ccc-bd63-5986dc9c65ac', N'Position', N'User');

        ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
        DELETE FROM AspNetUserRoles WHERE UserId IN ('f1f118bd-76c4-4ccc-bd63-5986dc9c65ac', '80b27df5-1519-4177-b7d3-aae75f58e655');
        DELETE FROM AspNetRoles WHERE Id IN ('ec178006-e20d-4297-808d-c9dd2437a3b3', '9c4a8b2d-0f5a-4e60-9cf1-2a6d8c1e2f03');
        DELETE FROM AspNetUsers WHERE Id IN ('f1f118bd-76c4-4ccc-bd63-5986dc9c65ac', '80b27df5-1519-4177-b7d3-aae75f58e655');
        DELETE FROM AspNetUserClaims WHERE UserId IN ('80b27df5-1519-4177-b7d3-aae75f58e655', 'f1f118bd-76c4-4ccc-bd63-5986dc9c65ac')

        DELETE FROM [dbo].[AspNetRoleClaims]
        WHERE [RoleId] = N'ec178006-e20d-4297-808d-c9dd2437a3b3' AND [ClaimType] = N'role' AND [ClaimValue] = N'Admin';

        DELETE FROM [dbo].[AspNetRoleClaims]
        WHERE [RoleId] = N'9c4a8b2d-0f5a-4e60-9cf1-2a6d8c1e2f03' AND [ClaimType] = N'role' AND [ClaimValue] = N'User';
    ");
        }
    }

}
