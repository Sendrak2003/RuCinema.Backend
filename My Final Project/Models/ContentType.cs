using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace My_Final_Project.Models;

[Table("Content_Type")]
public partial class ContentType
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Content_Type_ID")]
    public int ContentTypeId { get; set; }

    [Column("Content_Type_Name", TypeName = "NVARCHAR(255)")]
    [RegularExpression(@"^[a-zA-Zа-яА-Я]+(?:[\s-][a-zA-Zа-яА-Я]+)*$", ErrorMessage = "Название типа контента должно содержать только буквы (кириллица и/или латиница), пробелы, дефисы.")]
    public string? ContentTypeName { get; set; }

    [InverseProperty("ContentType")]
    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
