
using COJ.Web.Domain.Values;

namespace COJ.Web.Domain.Models
{
    public class SignUpModel
    {
        public string Username { get; set; }
        public string Nick { get; set; }
        public string Password { get; set; }
        public int LanguageId { get; set; }
        public int CountryId { get; set; }
        public int InstitutionId { get; set; }
        public int LocaleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public Sex Sex { get; set; }
    }
}
