using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using My_Final_Project.Models;

namespace My_Final_Project.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        [Column("Country_ID")]
        public int? CountryId { get; set; }

        [PersonalData]
        [Required(ErrorMessage = "Обязательно для заполнения")]
        [Column(TypeName = "nvarchar(100)")]
        public string? FirstName { get; set; }

        [PersonalData]
        [Required(ErrorMessage = "Обязательно для заполнения")]
        [Column(TypeName = "nvarchar(100)")]
        public string? LastName { get; set; }

        [PersonalData]
        [Required(ErrorMessage = "Обязательно для щаполнения")]
        [Column("Date_of_Birth", TypeName = "date")]
        public DateTime? DateOfBirth { get; set; }

        [PersonalData]
        [Column("Registration_Date", TypeName = "date")]
        public DateTime? RegistrationDate { get; set; }

        [PersonalData]
        [ForeignKey("CityId")]
        [InverseProperty("Users")]
        public virtual Country? Country { get; set; }

        [PersonalData]
        [InverseProperty("User")]
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

        [PersonalData]
        [InverseProperty("User")]
        public virtual ICollection<Download> Downloads { get; set; } = new List<Download>();

        [PersonalData]
        [InverseProperty("User")]
        public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

        [PersonalData]
        [InverseProperty("User")]
        public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

        [PersonalData]
        [InverseProperty("User")]
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();


        [PersonalData]
        [Column(TypeName = "nvarchar(255)")]
        public string? userPhoto { get; set; }
    }
}
