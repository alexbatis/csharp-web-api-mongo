using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace batis.dotnetapi.web.Models
{
  public class ValidationError
  {
    public ValidationErrorTypes ErrorType { get; set; }
    public string ErrorTypeDescription { get { return ErrorType.ToString();  } }
    public string Reference { get; set; }
    public object Message { get; set; }
  }
}