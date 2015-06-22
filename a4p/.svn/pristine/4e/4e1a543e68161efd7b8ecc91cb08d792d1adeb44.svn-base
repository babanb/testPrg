using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADOPets.Web.ViewModels.Econsultation
{
    public class VideoConferenceModel
    {
        public VideoConferenceModel()
        {

        }

        public VideoConferenceModel(Model.EConsultation Econsult)
        {
            EcID = Econsult.ID;
            PetId = Econsult.PetId.Value;
            PetOnwerId = Econsult.UserId.Value;
            VetId = Econsult.VetId.Value;
        }

        public int? EcID { get; set; }
        public string UserName { get; set; }
        public int? PetId { get; set; }
        public decimal? RoomID { get; set; }
        public int? PetOnwerId { get; set; }
        public int? VetId { get; set; }
        public string DocName { get; set; }
        public List<Model.EconsultationMsg> ChatMsg { get; set; }
        public List<EconsultDocumentViewModel> Files { get; set; }
    }
}