using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADOPets.Web.ViewModels.GalleryComment
{
    public class CommentDeleteModel
    {
        public int GalleryId { get; set; }
        public int CommentId { get; set; }
        public int GalleryType { get; set; }
    }
}