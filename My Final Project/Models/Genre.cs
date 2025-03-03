using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace My_Final_Project.Models
{
    public partial class Genre
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Genre_ID")]
        public int GenreId { get; set; }

        [RegularExpression(@"^[А-ЯЁа-яё]+$", ErrorMessage = "Название жанра должно начинаться с заглвной русской буквы")]
        [Column("Genre_Name", TypeName = "NVARCHAR(255)")]
        [StringLength(255)]
        public string? GenreName { get; set; }

        [ForeignKey("GenreId")]
        [InverseProperty("Genres")]
        public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}