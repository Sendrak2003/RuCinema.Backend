using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace My_Final_Project.Models
{
    public partial class Tag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Tag_ID")]
        public int TagId { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [Column("Tag_Name", TypeName = "NVARCHAR(255)")]
        [StringLength(255)]
        [RegularExpression(@"^#[А-Яа-я]+$", ErrorMessage = "Строка должна начинаться с символа # и содержать кириллические буквы")]
        public string? TagName { get; set; }

        [ForeignKey("TagId")]
        [InverseProperty("Tags")]
        public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}