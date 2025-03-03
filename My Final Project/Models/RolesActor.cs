using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace My_Final_Project.Models
{
    [Table("Roles_Actors")]
    public partial class RolesActor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Role_ID")]
        public int RoleId { get; set; }

        [Required (ErrorMessage = "Обязательно для заполнения")]
        [Column("Role_Name", TypeName = "NVARCHAR(255)")]
        [StringLength(255)]
        public string? RoleName { get; set; }

        [Column("Actor_ID")]
        public int? ActorId { get; set; }

        [Column("Movie_ID")]
        public int? MovieId { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [Column("Actor_Photo_URL", TypeName = "NVARCHAR(255)")]
        [StringLength(255)]
        [ValidRelativePath("wwwroot/Actor Photo/")]
        public string? ActorPhotoUrl { get; set; }

        [ForeignKey("ActorId")]
        [InverseProperty("RolesActors")]
        public virtual Actor? Actor { get; set; }

        [ForeignKey("MovieId")]
        [InverseProperty("RolesActors")]
        public virtual Movie? Movie { get; set; }
    }
}