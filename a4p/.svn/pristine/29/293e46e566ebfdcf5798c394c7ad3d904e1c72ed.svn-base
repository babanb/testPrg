using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Model;

namespace ADOPets.Web.ViewModels.SMO
{
    public class AddExpertCommitteeModel
    {
        public AddExpertCommitteeModel()
        {
        }

        public int Id { get; set; }

        public string Message { get; set; }      

        public int SMOId { get; set; }

        public int VeterianId { get; set; }

        public System.DateTime Date { get; set; }


        public Model.SMOExpertCommittee Map()
        {
            var smo = new Model.SMOExpertCommittee
            {
                Message=Message,
                SMORequestId=SMOId,
                VeterinaryExpertId=VeterianId,
                Date=Date
            };
            return smo;

        }
    }
}