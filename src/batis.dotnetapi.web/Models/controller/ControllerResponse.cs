using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace batis.dotnetapi.web.Models
{
  public class ControllerResponse
  {
    protected object _data;
    protected ControllerResponseStatusType _status;
    protected List<ValidationError> _validationErrors;

    public ControllerResponse()
    {
      _status = ControllerResponseStatusType.Failure;
    }

    public string Status
    {
      get { return _status.ToString(); }
      set { }
    }
    
    public object Data
    {
      get {return _data; }
      set { }
    }

    public List<ValidationError> ValidationErrors
    {
      get { return _validationErrors; }
      set { }
    }

    public void AddData(object data)
    {
      _status = ControllerResponseStatusType.Success;
      _data = data;
    }

    public void AddException(System.Exception exception)
    {
      _status = ControllerResponseStatusType.Failure;
      AddValidationError(ValidationErrorTypes.Exception, "", exception);
    }

    public void AddValidationError(ValidationErrorTypes errorType, string reference, object message)
    {
      _status = ControllerResponseStatusType.Failure;
      if (_validationErrors == null) _validationErrors = new List<ValidationError>();
      _validationErrors.Add(new ValidationError() { ErrorType = errorType, Message = message, Reference = reference });
    }
  }
}