using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace My_Final_Project.Models
{
    public partial class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Movie_ID")]
        public int MovieId { get; set; }

        [Column("Country_ID")]
        public int? CountryId { get; set; }

        [Column("Content_Type_ID")]
        public int? ContentTypeId { get; set; }


        [Required(ErrorMessage = "Обязательно для заполнения")]
        [Column("Title", TypeName = "NVARCHAR(255)")]
        [StringLength(255)]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [Column("Start_Date", TypeName = "date")]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [Column("End_Date", TypeName = "date")]
        public DateTime? EndDate { get; set; }

        [Column("Date_Added", TypeName = "date")]
        public DateTime? DateAdded { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [Column("Currency", TypeName = "NVARCHAR(255)")]
        [StringLength(255)]
        public string? Currency { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        public double? Price { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [Column("Cover_Image_URL", TypeName = "NVARCHAR(255)")]
        [StringLength(255)]
        [ValidRelativePath("wwwroot/Posters/")]
        public string? CoverImageUrl { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [Column("Short_Description", TypeName = "NVARCHAR(255)")]
        [StringLength(255)]
        public string? ShortDescription { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [Column("Released_Episodes")]
        public int? ReleasedEpisodes { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [Column("Total_Episodes")]
        public int? TotalEpisodes { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [Column("IsFinished", TypeName = "BIT")]
        public bool? IsFinished { get; set; } = false;

        [InverseProperty("Movie")]
        public virtual ICollection<Announcement> Announcements { get; set; } = new List<Announcement>();

        [InverseProperty("Movie")]
        public virtual ICollection<Award> Awards { get; set; } = new List<Award>();

        [InverseProperty("Movie")]
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

        [ForeignKey("CountryId")]
        [InverseProperty("Movies")]
        public virtual Country? Country { get; set; }

        [InverseProperty("Movie")]
        public virtual ICollection<Download> Downloads { get; set; } = new List<Download>();

        [InverseProperty("Movie")]
        public virtual ICollection<Episode> Episodes { get; set; } = new List<Episode>();

        [InverseProperty("Movie")]
        public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

        [InverseProperty("Movie")]
        public virtual ICollection<MovieFragment> MovieFragments { get; set; } = new List<MovieFragment>();

        [InverseProperty("Movie")]
        public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

        [InverseProperty("Movie")]
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

        [InverseProperty("Movie")]
        public virtual ICollection<RolesActor> RolesActors { get; set; } = new List<RolesActor>();

        [ForeignKey("MovieId")]
        [InverseProperty("Movies")]
        public virtual ICollection<Actor> Actors { get; set; } = new List<Actor>();

        [ForeignKey("MovieId")]
        [InverseProperty("Movies")]
        public virtual ICollection<Director> Directors { get; set; } = new List<Director>();

        [ForeignKey("MovieId")]
        [InverseProperty("Movies")]
        public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();

        [ForeignKey("MovieId")]
        [InverseProperty("Movies")]
        public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
        
        [ForeignKey("ContentTypeId")]
        [InverseProperty("Movies")]
        public virtual ContentType? ContentType { get; set; }
    }
}