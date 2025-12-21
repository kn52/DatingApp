using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities;

[Table("Users")]
public class AppUser
{
    [Key]
    public string  Id { get; set;} = Guid.NewGuid().ToString();
    [Column("DisplayName")]
    public string  Name  { get; set;}
    [Column("Email")]
    public string  Email  { get; set;}
}
