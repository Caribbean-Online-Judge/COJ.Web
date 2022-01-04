
using System.ComponentModel.DataAnnotations;
using COJ.Web.Domain.Values;

namespace COJ.Web.Domain.Models
{
    public class SignUpRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        /// <summary>
        /// Id of Programming language
        /// </summary>
        [Required]
        public int LanguageId { get; set; }
        /// <summary>
        /// Id of Country
        /// </summary>
        [Required]
        public int CountryId { get; set; }
        /// <summary>
        /// Id of Institution
        /// </summary>
        [Required]
        public int InstitutionId { get; set; }
        /// <summary>
        /// Id of Locale (Culture)
        /// </summary>
        [Required]
        public int LocaleId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        [Required]
        public Sex Sex { get; set; }
    }
}
