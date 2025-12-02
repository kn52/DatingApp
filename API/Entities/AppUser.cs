using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities;

[Table("Users")]
public class AppUser
{
    [Key]
    public string  Id { get; set;} = Guid.NewGuid().ToString();
    public string  DisplayName  { get; set;}
    public string  Email  { get; set;}
}
