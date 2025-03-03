using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using My_Final_Project.Areas.Identity.Data;

namespace My_Final_Project.Models
{
    public partial class Rating
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Rating_ID")]
        public int RatingId { get; set; }

        [Column("Movie_ID")]
        public int? MovieId { get; set; }
       
        [Column("User_ID")]
        public string? UserId { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [Column("Rating_Count")]
        public double? RatingCount { get; set; }

        [Column("Rating_Date", TypeName = "date")]
        public DateTime? RatingDate { get; set; }

        [ForeignKey("MovieId")]
        [InverseProperty("Ratings")]
        public virtual Movie? Movie { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Ratings")]
        public virtual ApplicationUser? User { get; set; }
    }
}