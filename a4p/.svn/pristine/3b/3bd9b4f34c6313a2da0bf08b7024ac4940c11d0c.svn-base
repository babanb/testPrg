using ADOPets.Web.Common.Helpers;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADOPets.Web.ViewModels.NewsFeed
{
    public class NewsFeedNotificationsViewModel
    {
        public NewsFeedNotificationsViewModel(string id, string userName, string notifyType, DateTime? notifyDate, int userId, string message,int? petId)
        {
            Id = id;
            FromUserName = userName;
            TypeOfNotification = notifyType;
            NotificationDate = TimeZoneHelper.ConvertDateTimeByUserTimeZoneId(Convert.ToDateTime(notifyDate));
            HoursAgo = TimeZoneHelper.TimeAgo(Convert.ToDateTime(notifyDate));
            AlbumId = 0;
            UserId = userId;
            Message = userName + " " + message;
            PetId = Convert.ToInt32(petId);
        }
        public NewsFeedNotificationsViewModel(string id, string userName, string notifyType, DateTime? notifyDate, int? albumId, int userId, string message, int? petId)
        {
            Id = id;
            FromUserName = userName;
            TypeOfNotification = notifyType;
            NotificationDate = TimeZoneHelper.ConvertDateTimeByUserTimeZoneId(Convert.ToDateTime(notifyDate));
            HoursAgo = TimeZoneHelper.TimeAgo(Convert.ToDateTime(notifyDate));
            AlbumId = Convert.ToInt32(albumId);
            UserId = userId;
            Message = userName + " " + message;
            PetId = Convert.ToInt32(petId);
        }

        public NewsFeedNotificationsViewModel(Model.VideoGallery gallery, ICollection<User> users, string notifyType, string messagee, int? petId)
        {
            if (users != null)
            {
                Id = gallery.Id.ToString();
                FromUserName = users.FirstOrDefault().FirstName + " " + users.FirstOrDefault().LastName;
                TypeOfNotification = notifyType;
                NotificationDate = TimeZoneHelper.ConvertDateTimeByUserTimeZoneId(Convert.ToDateTime(gallery.CreatedDate));
                HoursAgo = TimeZoneHelper.TimeAgo(Convert.ToDateTime(gallery.CreatedDate));
                AlbumId = 0;
                UserId = users.FirstOrDefault().Id;
                Message = users.FirstOrDefault().FirstName + " " + users.FirstOrDefault().LastName + " " + messagee;
                PetId = Convert.ToInt32(petId);
            }
        }

        public NewsFeedNotificationsViewModel(Gallery gallery, ICollection<User> users, string notifyType, int? albumId, string message, int? petId)
        {
            if (users != null)
            {
                Id = gallery.Id.ToString();
                FromUserName = users.FirstOrDefault().FirstName + " " + users.FirstOrDefault().LastName;
                TypeOfNotification = notifyType;
                NotificationDate = TimeZoneHelper.ConvertDateTimeByUserTimeZoneId(Convert.ToDateTime(gallery.CreatedDate));
                HoursAgo = TimeZoneHelper.TimeAgo(Convert.ToDateTime(gallery.CreatedDate));
                AlbumId = Convert.ToInt32(albumId);
                UserId = users.FirstOrDefault().Id;
                Message = users.FirstOrDefault().FirstName + " " + users.FirstOrDefault().LastName + " " + message;
                PetId = Convert.ToInt32(petId);
            }
        }

        public NewsFeedNotificationsViewModel(Gallery gallery, ICollection<User> users, string notifyType, string message, int? petId)
        {
            if (users != null)
            {
                Id = gallery.Id.ToString();
                FromUserName = users.FirstOrDefault().FirstName + " " + users.FirstOrDefault().LastName;
                TypeOfNotification = notifyType;
                NotificationDate = TimeZoneHelper.ConvertDateTimeByUserTimeZoneId(Convert.ToDateTime(gallery.CreatedDate));
                HoursAgo = TimeZoneHelper.TimeAgo(Convert.ToDateTime(gallery.CreatedDate));
                AlbumId = 0;
                UserId = users.FirstOrDefault().Id;
                Message = users.FirstOrDefault().FirstName + " " + users.FirstOrDefault().LastName + " " + message;
                PetId = Convert.ToInt32(petId);
            }
        }

        public string Id { get; set; }
        public string FromUserName { get; set; }
        public string Title { get; set; }
        public string TypeOfNotification { get; set; }
        public DateTime NotificationDate { get; set; }
        public string HoursAgo { get; set; }
        public int UserId { get; set; }
        public int AlbumId { get; set; }
        public int PetId { get; set; }
        public string Message { get; set; }
    }
}