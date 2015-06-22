using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model;
using System.Globalization;


namespace ADOPets.Web.ViewModels.Calender
{
    public class IndexViewModel
    {
        public IndexViewModel() { }

        public IndexViewModel(Model.Calendar calendar)
        {
            Id = calendar.Id;
            Date = calendar.Date.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
            Physician = calendar.Physician;
            Comment = calendar.Comment;
            Reason = calendar.Reason;
        }

        public int Id { get; set; }
        public string Date { get; set; }
        public string Physician { get; set; }
        public string Comment { get; set; }
        public string Reason { get; set; }


    }
}