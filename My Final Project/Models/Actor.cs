using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace My_Final_Project.Models
{
    public partial class Actor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Actor_ID")]
        public int ActorId { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [Column("Full_Name", TypeName = "NVARCHAR(255)")]
        [StringLength(255)]
        public string? FullName { get; set; }

        [Column("Country_ID")]
        public int? CountryId { get; set; }

        [ForeignKey("CountryId")]
        [InverseProperty("Actors")]
        public virtual Country? Country { get; set; }

        [InverseProperty("Actor")]
        public virtual ICollection<RolesActor> RolesActors { get; set; } = new List<RolesActor>();

        [ForeignKey("ActorId")]
        [InverseProperty("Actors")]
        public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}
