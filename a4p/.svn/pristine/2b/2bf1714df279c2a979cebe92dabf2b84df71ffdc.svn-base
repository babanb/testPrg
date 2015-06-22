using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADOPets.Web.ViewModels.SMO
{
    public class ExpertBio
    {

        public ExpertBio()
        { 
        
        }
        public ExpertBio(string bio)
        {
            ExpertComment = bio;
        }
        public ExpertBio(Model.ExpertBioData bio)
        {
            userId = bio.User.Id;
            ExpertComment = bio.Information;
        }
        public int Id { get; set; }
        public int userId { get; set; }
        public string ExpertComment { get; set; }

        
    }
}