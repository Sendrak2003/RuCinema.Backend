using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace My_Final_Project.Models
{
    public partial class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("City_ID")]
        public int CityId { get; set; }

        [Column("Country_ID")]
        public int? CountryId { get; set; }

        [RegularExpression(@"^[А-ЯЁа-яё]+$", ErrorMessage = "Название города должно начинаться с заглвной русской буквы")]
        [Column("City_Name", TypeName = "NVARCHAR(255)")]
        [StringLength(255)]
        public string? CityName { get; set; }

        [ForeignKey("CountryId")]
        [InverseProperty("Cities")]
        public virtual Country? Country { get; set; }
    }
}