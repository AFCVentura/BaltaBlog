using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaltaBlog.Models;

[Table("[Role]")]
public class Role
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }

}