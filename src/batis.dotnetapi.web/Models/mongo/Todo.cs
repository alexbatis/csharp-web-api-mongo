using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace batis.dotnetapi.web.Models
{
  public class Todo
  {
    [BsonId]
    public ObjectId _id { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public Boolean completed { get; set; }
    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    public DateTime Created { get; set; }

    public Todo()
    {
      completed = false;
      Created = DateTime.Now;
    }

    public override string ToString()
    {
      return "_id=" + _id + " title=" + title + " description=" + description;
    }

  }
}