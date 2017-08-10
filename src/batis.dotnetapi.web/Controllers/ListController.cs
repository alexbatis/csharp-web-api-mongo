using batis.dotnetapi.web.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;


namespace batis.dotnetapi.web.Controllers
{
  [RoutePrefix("api")]
  public class ListController : ApiController
  {

    public List<object> theList = new List<object>();
    //public static List<TodoItem> todoList = new List<TodoItem>();
    public static Dictionary<string, TodoItem> todoList= new Dictionary<string, TodoItem>();

    [Route("list")]
    [HttpGet]
    public ControllerResponse getList()
    {
      ControllerResponse response = new ControllerResponse();
      response.AddData(todoList);
      return response;
    }

    [Route("list/{id}")]
    [HttpGet]
    public ControllerResponse getListItem(string id)
    {
      ControllerResponse response = new ControllerResponse();
      TodoItem item;
      if (todoList.TryGetValue(id, out item)) //item exists in todoList hashmap
      {
        response.AddData(item);
      }
      else
      {
        response.AddValidationError(ValidationErrorTypes.Exception,"","No item found with id : " + id);
      }
      return response;
    }

    [Route("list")]
    [HttpPost]
    public ControllerResponse addToList(TodoItem itemToPost)
    {
      ControllerResponse response = new ControllerResponse();
      if(itemToPost != null)
      {
        todoList.Add(itemToPost._id, itemToPost);
        response.AddData(itemToPost);
      }
      else
      {
        response.AddValidationError(ValidationErrorTypes.Exception, "", "Could not post item. Make sure the payload is valid.");
      }

      return response;
    }


    [Route("list/{id}")]
    [HttpPut]
    public ControllerResponse updateListItem(string id, TodoItem updatedItem)
    {
      ControllerResponse response = new ControllerResponse();
      if (updatedItem != null)
      {
        TodoItem item;
        if (todoList.TryGetValue(id, out item)) //item exists in todoList hashmap
        {
          todoList[id].title = updatedItem.title;
          todoList[id].description = updatedItem.description;
          response.AddData(item);
        }
        else
        {
          response.AddValidationError(ValidationErrorTypes.Exception, "", "No item found with id : " + id);
        }
      }
      else
      {
        response.AddValidationError(ValidationErrorTypes.Exception, "", "Could not post item. Make sure the payload is valid.");
      }
      return response;
    }

    [Route("list/{id}")]
    [HttpDelete]
    public ControllerResponse deleteListItem(string id)
    {
      ControllerResponse response = new ControllerResponse();

        TodoItem item;
        if (todoList.TryGetValue(id, out item)) //item exists in todoList hashmap
        {
          todoList.Remove(id);
          response.AddData(todoList);
        }
        else
        {
          response.AddValidationError(ValidationErrorTypes.Exception, "", "No item found with id : " + id);
        }
      

      return response;
    }
  }
}