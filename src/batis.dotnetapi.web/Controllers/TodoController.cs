using batis.dotnetapi.web.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using MongoDB.Driver;
using System.Web.Http.Results;
using MongoDB.Driver.Builders;
using MongoDB.Bson;

namespace batis.dotnetapi.web.Controllers
{
  [RoutePrefix("api")]
  public class TodoController : ApiController
  {

    TodoDAO db = new TodoDAO();

    [Route("todos")]
    [HttpGet]
    public ControllerResponse getTodoList()
    {
      ControllerResponse response = new ControllerResponse();
      response.AddData(db.getAllTodos());
      return response;
    }

    [Route("todos/{id}")]
    [HttpGet]
    public ControllerResponse getTodoItem(string id)
    {
      ControllerResponse response = new ControllerResponse();
      try
      {
        ObjectId formattedId = new ObjectId(id);
        var todo = db.getTodo(formattedId);
        if(todo == null)
        {
          response.AddValidationError(ValidationErrorTypes.Data, "", "No Todo item found for ID :" + id);
        }
        else
        {
          response.AddData(todo);
        }
      }
      catch (System.FormatException e1)
      {
        response.AddException(e1);
      }
      return response;
    }

    [Route("todos/{id}")]
    [HttpDelete]
    public ControllerResponse deleteTodoItem (string id)
    {
      ControllerResponse response = new ControllerResponse();
      try
      {
        ObjectId formattedId = new ObjectId(id);
        var documentsAffected = db.deleteTodo(formattedId);
        if (documentsAffected != 1)
        {
          response.AddValidationError(ValidationErrorTypes.Data, "", "No Todo item found for ID : " + id);
        }
        else
        {
          response.AddData("Item " + id + " was removed.");
        }
      }
      catch (System.FormatException e1)
      {
        response.AddException(e1);
      }
      return response;
    }

    [Route("todos/{id}")]
    [HttpPut]
    public ControllerResponse updateTodoItem(string id, Todo updatedTodo)
    {
      ControllerResponse response = new ControllerResponse();
      try
      {
        ObjectId formattedId = new ObjectId(id);
        var todo = db.updateTodo(formattedId, updatedTodo);
        if (todo == null)
        {
          response.AddValidationError(ValidationErrorTypes.Data, "", "No Todo item found for ID : " + id);
        }
        else
        {
          response.AddData(todo);
        }
      }
      catch (System.FormatException e1)
      {
        response.AddException(e1);
      }
      return response;
    }

    [Route("todos")]
    [HttpPost]
    public ControllerResponse postTodo(Todo todoToInsert)
    {
      ControllerResponse response = new ControllerResponse();
      try
      {
        var todo = db.insertTodo(todoToInsert);
        if (todo == null)
        {
          response.AddValidationError(ValidationErrorTypes.Data, "", "No Todo item found for ID : " + todoToInsert._id);
        }
        else
        {
          response.AddData(todo);
        }
      }
      catch (System.Exception e1)
      {
        response.AddException(e1);
      }
      return response;
    }
 
  }
}