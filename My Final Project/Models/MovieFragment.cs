using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace My_Final_Project.Models
{
    [Table("Movie_Fragments")]
    public partial class MovieFragment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Fragment_ID")]
        public int FragmentId { get; set; }

        [Column("Movie_ID")]
        public int? MovieId { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [Column("Image_URL", TypeName = "NVARCHAR(255)")]
        [StringLength(255)]
        [ValidRelativePath("wwwroot/Fragments/")]
        public string? ImageUrl { get; set; }

        [ForeignKey("MovieId")]
        [InverseProperty("MovieFragments")]
        public virtual Movie? Movie { get; set; }
    }
}
