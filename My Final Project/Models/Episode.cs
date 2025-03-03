using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace My_Final_Project.Models
{
    public partial class Episode
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Episode_ID")]
        public int EpisodeId { get; set; }

        [Column("Movie_ID")]
        public int? MovieId { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [Column("Episode_Number")]
        public int? EpisodeNumber { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [Column(TypeName = "NVARCHAR(255)")]
        [StringLength(255)]
        public string? Duration { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [Column(TypeName = "NVARCHAR(255)")]
        [StringLength(255)]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [Column("Short_Description", TypeName = "NVARCHAR(255)")]
        [StringLength(255)]
        public string? ShortDescription { get; set; }

        [Column("Release_Date", TypeName = "date")]
        public DateTime? ReleaseDate { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [Column("File_URL", TypeName = "NVARCHAR(255)")]
        [StringLength(255)]
        public string? FileUrl { get; set; }

        [ForeignKey("MovieId")]
        [InverseProperty("Episodes")]
        public virtual Movie? Movie { get; set; }
    }
}