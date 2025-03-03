using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using My_Final_Project.Areas.Identity.Data;

namespace My_Final_Project.Models
{
    public partial class Country
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Country_ID")]
        public int CountryId { get; set; }

        [RegularExpression(@"^[А-ЯЁа-яё]+$", ErrorMessage = "Название страгы должно начинаться с заглвной русской буквы")]
        [Column("Country_Name", TypeName = "NVARCHAR(255)")]
        [StringLength(255)]
        public string? CountryName { get; set; }

        [Required(ErrorMessage = "Обязательно для щаполнения")]
        [Column("Flag_Image", TypeName = "NVARCHAR(255)")]
        [StringLength(255)]
        [ValidRelativePath("wwwroot/Flags/")]
        public string? FlagImage { get; set; }

        [InverseProperty("Country")]
        public virtual ICollection<Actor> Actors { get; set; } = new List<Actor>();

        [InverseProperty("Country")]
        public virtual ICollection<City> Cities { get; set; } = new List<City>();

        [InverseProperty("Country")]
        public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();

        [InverseProperty("Country")]
        public virtual ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
    }
}