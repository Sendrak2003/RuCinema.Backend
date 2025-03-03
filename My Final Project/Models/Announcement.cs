using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace My_Final_Project.Models
{
    public partial class Announcement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Announcement_ID")]
        public int AnnouncementId { get; set; }

        [Column("Movie_ID")]
        public int? MovieId { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [Column("Title", TypeName = "NVARCHAR(255)")]
        [StringLength(255)]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        //[RegularExpression(@"^[a-zA-Zа-яА-Я]+(?:[\s.,:;!?()-—""][a-zA-Zа-яА-Я]+)*$", ErrorMessage = "Введите корректное краткое описание.")]
        [Column("Short_Description", TypeName = "NVARCHAR(255)")]
        [StringLength(255)]
        public string? ShortDescription { get; set; }

        [Column("Announcement_Date", TypeName = "date")]
        public DateTime? AnnouncementDate { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [Column("Trailer_URL", TypeName = "NVARCHAR(255)")]
        [StringLength(255)]
        public string? TrailerUrl { get; set; }

        [ForeignKey("MovieId")]
        [InverseProperty("Announcements")]
        public virtual Movie? Movie { get; set; }
    }
}