using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using MiniTodo.Data;
using MiniTodo.DTOs;
using MiniTodo.Models;
using MiniTodo.ViewModel;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.MapGet("/example", () => Results.Ok(new Todo(Guid.NewGuid(), "Ir a academia", false)));


app.MapGet("/v1/todos", (AppDbContext context) =>
{
    var todos = context.Todos;
    return todos is not null ? Results.Ok(todos) : Results.NotFound();
});

app.MapPost("/v1/todos", (AppDbContext context, CreateTodoViewModel model) =>
{
    var todo = model.MapTo();
    if (!model.IsValid)
        return Results.BadRequest(model.Notifications);

    context.Todos.Add(todo);
    context.SaveChanges();

    return Results.Created($"/v1/todos/{todo.Id}", todo);
});

app.MapGet("/v2/todos", (AppDbContext context) =>
{
    var todosEntity = context.TodosV2;
    var todo = new List<TodoV2DTO>();
    foreach (var todos in todosEntity)
    {
        todo.Add(new TodoV2DTO(todos.Guid, todos.Title, todos.Done));
    }
    
    return todo is not null ? Results.Ok(todo) : Results.NotFound();
});

app.MapPost("/v2/todos", (AppDbContext context, CreateTodoViewModelV2 model) =>
{
    var todo = model.MapTo();
    if (!model.IsValid)
        return Results.BadRequest(model.Notifications);

    context.TodosV2.Add(new TodoV2()
    {
        Guid = todo.Guid,
        Title = todo.Title,
        Done = todo.Done,
    });
    context.SaveChanges();
    return Results.Created($"/v2/todos/{todo.Guid}", todo);
});

app.MapPut("/v2/todos/{todoGuid}", (AppDbContext context, Guid todoGuid) =>{
    var todo = context.TodosV2.FirstOrDefault(t => t.Guid == todoGuid);
    if(todo == default){
        return Results.NotFound("Todo not foud");
    }
    todo.Done = true;
    context.SaveChangesAsync();
    return Results.Ok(todo);
});

app.MapDelete("/v2/todos/{todoGuid}", (AppDbContext context, Guid todoGuid) =>{
    var todo = context.TodosV2.FirstOrDefault(t => t.Guid == todoGuid);
    if(todo == default){
        return Results.NotFound("Todo not foud");
    }
    context.TodosV2.Remove(todo);
    context.SaveChangesAsync();
    return Results.Accepted();
});

app.Run();