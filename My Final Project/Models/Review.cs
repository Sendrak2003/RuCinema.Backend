using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using My_Final_Project.Areas.Identity.Data;

namespace My_Final_Project.Models
{
    public partial class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Review_ID")]
        public int ReviewId { get; set; }

        [Column("Movie_ID")]
        public int? MovieId { get; set; }

        [Column("User_ID")]
        public string? UserId { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        public int? Likes { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [Column("Review_Text", TypeName = "NVARCHAR(255)")]
        [StringLength(255)]
        public string? ReviewText { get; set; }

        [Column("Publication_Date", TypeName = "date")]
        public DateTime? PublicationDate { get; set; }

        [InverseProperty("Review")]
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

        [ForeignKey("MovieId")]
        [InverseProperty("Reviews")]
        public virtual Movie? Movie { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Reviews")]
        public virtual ApplicationUser? User { get; set; }

    }
}