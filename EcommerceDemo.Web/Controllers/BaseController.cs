using EcommerceDemo.Models.Model;
using EcommerceDemo.Utilities.Enums;
using System.Web.Mvc;

namespace EcommerceDemo.Web.Controllers
{
    public class BaseController : Controller
    {
        protected void SetNotification(string message, NotificationTypes type, string title = "")
        {
            TempData["Notification"] = new AlertNotificationModel()
            {
                Message = message,
                Title = title,
                Type = type
            };
        }
    }
}