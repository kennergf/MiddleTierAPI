using MiddleTier.API.Interfaces;
using MiddleTier.API.Models;

namespace MiddleTier.API.Services
{
    public abstract class BaseService
    {
        private readonly INotifier _notifier;

        protected BaseService(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected void Notify(string message)
        {
            _notifier.Add(new Notification(message));
        }
    }
}