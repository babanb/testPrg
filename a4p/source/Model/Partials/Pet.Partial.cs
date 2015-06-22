using Model.Interfaces;

namespace Model
{
    public partial class Pet : ISoftDelete
    {
        public int PetId
        {
            get { return Id; }
        }


    }


}
