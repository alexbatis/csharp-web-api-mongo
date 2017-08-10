using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace batis.dotnetapi.web.Models
{
  public class TodoItem
  {
    public string _id { get; }
    public string title { get; set; }
    public string description { get; set; }

    public TodoItem()
    {
      _id = Guid.NewGuid().ToString("N");
    }

    public override string ToString()
    {
      return "_id=" + _id + " title=" + title + " description=" + description;
    }
  }
}