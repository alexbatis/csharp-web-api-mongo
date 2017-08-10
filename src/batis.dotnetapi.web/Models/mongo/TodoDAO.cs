using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace batis.dotnetapi.web.Models
{
  public class TodoDAO
  {
    private MongoDatabase database;
    private MongoCollection<Todo> collection;

    public TodoDAO()
    {
      var connectionstring = "mongodb://localhost";
      var client = new MongoClient(connectionstring);
      var server = client.GetServer();
      database = server.GetDatabase("dotnet");
      collection = database.GetCollection<Todo>("todos");
    }

    public IList<Todo> getAllTodos()
    {
      IList<Todo> todos = new List<Todo>();
      var getTodos = collection.FindAs(typeof(Todo), Query.NE("Name", "null"));

      foreach (Todo todo in getTodos)
      {
        todos.Add(todo);
      }
      return todos;
    }

    public Todo getTodo(ObjectId id)
    {
      return collection.FindOneById(id);
    }


    public Todo insertTodo(Todo todoToInsert)
    {
      collection.Insert(todoToInsert);
      return collection.FindOneById(todoToInsert._id);
    }

    public long deleteTodo(ObjectId id)
    {
      return collection.Remove(Query.EQ("_id", id)).DocumentsAffected;
    }

    public Todo updateTodo(ObjectId id, Todo updatedTodo)
    {
      var query = Query.EQ("_id", id);
      var update = Update.Set("title", updatedTodo.title).Set("description", updatedTodo.description).Set("completed", updatedTodo.completed);
      collection.Update(query, update);
      return collection.FindOneById(id);
    }


  }
}