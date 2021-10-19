using System.Collections.Generic;
using MiddleTier.API.Models;

namespace MiddleTier.API.Interfaces
{
    public interface INotifier
    {
        /// <summary>
        /// Check if there is any notification
        /// </summary>
        /// <returns></returns>
        bool HasNotification();

        /// <summary>
        /// Get all notifications available
        /// </summary>
        /// <returns></returns>
        List<Notification> GetNotifications();

        /// <summary>
        /// Add new Notification
        /// </summary>
        /// <param name="notification"></param>
        void Add(Notification notification);
    }
}