using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace My_Final_Project.Models
{
    public partial class Director
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Director_ID")]
        public int DirectorId { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [Column("Full_Name")]
        [StringLength(255)]
        public string? FullName { get; set; }

        [ForeignKey("DirectorId")]
        [InverseProperty("Directors")]
        public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}