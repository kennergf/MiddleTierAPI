using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MiddleTier.API.Interfaces;
using MiddleTier.API.Models;

namespace MiddleTier.API.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly INotifier _notifier;
        //public readonly IUser _APIUser;

        public MainController(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected bool OperationValid()
        {
            return !_notifier.HasNotification();
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if (OperationValid())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = _notifier.GetNotifications().Select(n => n.Message)
            });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if(!modelState.IsValid) NotifyErroModelInvalid(modelState);
            return CustomResponse();
        }

        protected void NotifyErroModelInvalid(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotifyErro(errorMsg);
            }
        }

        protected void NotifyErro(string message)
        {
            _notifier.Add(new Notification(message));
        }
    }
}
