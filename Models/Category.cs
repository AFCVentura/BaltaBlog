
using BaltaBlog.Interfaces;
using Dapper.Contrib.Extensions;

namespace BaltaBlog.Models;

[Table("[Category]")]
public class Category : IModel
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public List<Post> Posts { get; set; }

    public Category()
    {
        Posts = new List<Post>();
    }
}