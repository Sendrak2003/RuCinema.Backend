using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using My_Final_Project.Areas.Identity.Data;

namespace My_Final_Project.Models
{
    public partial class Favorite
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Favorite_ID")]
        public int FavoriteId { get; set; }

        [Column("User_ID")]
        public string? UserId { get; set; }

        [Column("Movie_ID")]
        public int? MovieId { get; set; }

        [ForeignKey("MovieId")]
        [InverseProperty("Favorites")]
        public virtual Movie? Movie { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Favorites")]
        public virtual ApplicationUser? User { get; set; }
    }
}