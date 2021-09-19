using System;
using Flunt.Notifications;
using Flunt.Validations;
using MiniTodo.DTOs;
using MiniTodo.Models;

namespace MiniTodo.ViewModel
{
    public class CreateTodoViewModelV2 : Notifiable<Notification>
    {
        public Guid Guid { get; set; }
        public string Title { get; set; }

        public TodoV2DTO MapTo()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNull(Title, "Informe o título da tarefa")
                .IsGreaterThan(Title, 5, "O título deve conter mais de 5 caracteres"));

            return new TodoV2DTO(Guid.NewGuid(), Title, false);
        }
    }
}