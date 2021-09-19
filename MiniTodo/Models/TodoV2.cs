
using System.ComponentModel.DataAnnotations;

namespace MiniTodo.Models;
public class TodoV2
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public string Title { get; set; }
    public bool Done { get; set; }

}
