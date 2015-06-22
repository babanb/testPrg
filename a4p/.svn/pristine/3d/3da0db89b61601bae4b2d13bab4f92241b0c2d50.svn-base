namespace ADOPets.Web.ViewModels.Message
{
    public class SearchRecipientViewModel
    {
        public SearchRecipientViewModel()
        {

        }

        public SearchRecipientViewModel(Model.User user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            CityName = user.City;
            Country = user.CountryId.ToString();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email{ get; set; }
        public string CityName { get; set; }
        public string Country { get; set; }
    }
}