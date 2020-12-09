using EcommerceDemo.Utilities.Enums;

namespace EcommerceDemo.Models.Model
{
    public class AlertNotificationModel
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public NotificationTypes Type { get; set; }
    }
}
