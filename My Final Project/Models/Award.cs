using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace My_Final_Project.Models
{

    public partial class Award
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Award_ID")]
        public int AwardId { get; set; }

        [Column("Movie_ID")]
        public int? MovieId { get; set; }

        [Required(ErrorMessage = "Обязательно для щаполнения")]
        [Column("Award_Name", TypeName = "NVARCHAR(255)")]
        [StringLength(255)]
        public string? AwardName { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [Column("Award_Year")]
        public int? AwardYear { get; set; }

        [Column("Award_Photo_URL", TypeName = "NVARCHAR(255)")]
        [StringLength(255)]
        public string? AwardPhotoUrl { get; set; }

        [ForeignKey("MovieId")]
        [InverseProperty("Awards")]
        public virtual Movie? Movie { get; set; }
    }
}