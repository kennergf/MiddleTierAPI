using System.Collections.Generic;
using MiddleTier.API.Models;

namespace MiddleTier.API.Interfaces
{
    public interface INotifier
    {
        bool HasNotification();

        List<Notification> GetNotifications();

        void Add(Notification notification);
    }
}