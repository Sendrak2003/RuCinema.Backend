using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using My_Final_Project.Areas.Identity.Data;

namespace My_Final_Project.Models
{
    public partial class Download
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Download_ID")]
        public int DownloadId { get; set; }

        [Column("Movie_ID")]
        public int? MovieId { get; set; }

        [Column("User_ID")]
        public string? UserId { get; set; }

        [Column("Download_Date", TypeName = "date")]
        public DateTime? DownloadDate { get; set; }

        [ForeignKey("MovieId")]
        [InverseProperty("Downloads")]
        public virtual Movie? Movie { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Downloads")]
        public virtual ApplicationUser? User { get; set; }
    }
}