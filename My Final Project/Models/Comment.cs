using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using My_Final_Project.Areas.Identity.Data;

namespace My_Final_Project.Models
{
    public partial class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Comment_ID")]
        public int CommentId { get; set; }
        
        [Column("Review_ID")]
        public int? ReviewId { get; set; }

        [Column("Parent_Comment_ID")]
        public int? ParentCommentId { get; set; }

        [Column("Movie_ID")]
        public int? MovieId { get; set; }

        [Column("User_ID")]
        public string? UserId { get; set; }

        [Required(ErrorMessage = "Обязательно для щаполнения")]
        public int? Likes { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [Column("Comment_Text", TypeName = "NVARCHAR(255)")]
        [StringLength(255)]
        public string? CommentText { get; set; }

        [Column("Publication_Date", TypeName = "date")]
        public DateTime? PublicationDate { get; set; }

        [InverseProperty("ParentComment")]
        public virtual ICollection<Comment> InverseParentComment { get; set; } = new List<Comment>();

        [ForeignKey("MovieId")]
        [InverseProperty("Comments")]
        public virtual Movie? Movie { get; set; }

        [ForeignKey("ParentCommentId")]
        [InverseProperty("InverseParentComment")]
        public virtual Comment? ParentComment { get; set; }

        [ForeignKey("ReviewId")]
        [InverseProperty("Comments")]
        public virtual Review? Review { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Comments")]
        public virtual ApplicationUser? User { get; set; }
        
    }
}