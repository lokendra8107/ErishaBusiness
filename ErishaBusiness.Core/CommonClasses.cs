using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ErishaBusiness.Core
{
    #region [START : USERROLES]
    public struct Role
    {
        public const string Admin = "Admin";
        public const string Teacher = "Teacher";
        public const string Student = "Student";
    }
    #endregion

    public class Notification
    {
        public string Heading { get; set; }
        public string Message { get; set; }
        public MessageType Type { get; set; }
        public string Icon {
            get {
                switch (this.Type)
                {
                    case MessageType.Warning:
                        return "icon-warning-sign";
                    case MessageType.Success:
                        return "icon-check-sign";
                    case MessageType.Danger:
                        return "icon-remove-sign";
                    case MessageType.Info:
                        return "icon-info-sign";
                    default:
                        return "icon-info-sign";
                }
            }
        }
    }

    public class Message
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string DisplayName { get; set; }
        public string FontAwesomeIcon { get; set; }
        public string AvatarURL { get; set; }
        public string URLPath { get; set; }
        public string ShortDesc { get; set; }
        public string TimeSpan { get; set; }
        public int Percentage { get; set; }
        public string Type { get; set; }
    }

    public class Calenderview
    {
        public string day { get; set; }
        public string date { get; set; }
        public string mnth { get; set; }
        public string year { get; set; }

        public string datestring { get; set; }
    }
}
