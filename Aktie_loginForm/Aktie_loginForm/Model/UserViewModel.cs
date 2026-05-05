namespace Aktie_loginForm.Model
{
    public class UserViewModel
    {
        public int Id { get; }
        public string Navn { get; }
        public string Email { get; }

        public UserViewModel(int id, string navn, string email)
        {
            Id = id;
            Navn = navn;
            Email = email;
        }
    }
}