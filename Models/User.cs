using BaltaBlog.Interfaces;
using Dapper.Contrib.Extensions;

namespace BaltaBlog.Models;

[Table("[User]")]
public class User : IModel
{
    /* 
        Se o atributo se chama Id ou UserId, o Dapper
        sabe que é a PK, mas existe a anotação [Key]
        para deixar bem claro, nesse caso era opcional
    */
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Bio { get; set; }
    public string Image { get; set; }
    public string Slug { get; set; }
    [Write(false)] // Essa propriedade não será incluida no INSERT ou UPDATE
    public List<Role> Roles { get; set; }

    public User()
        => Roles = new List<Role>();


}