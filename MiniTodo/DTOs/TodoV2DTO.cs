
using System;

namespace MiniTodo.DTOs;
public class TodoV2DTO
{
    public TodoV2DTO(Guid guid, string title, bool done)
    {
        Guid = guid;
        Title = title;
        Done = done;
    }
    public Guid Guid { get; set; }
    public string Title { get; set; }
    public bool Done { get; set; }
}
