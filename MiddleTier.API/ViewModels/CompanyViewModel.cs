using System;
using System.ComponentModel.DataAnnotations;

namespace MiddleTier.API.ViewModels
{
    public class CompanyViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required!")]
        [StringLength(256, ErrorMessage = "The field {0} need to have between {2} and {1} characterer.", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field {0} is required!")]
        [StringLength(256, ErrorMessage = "The field {0} need to have between {2} and {1} characterer.", MinimumLength = 2)]
        public string Exchange { get; set; }

        [Required(ErrorMessage = "The field {0} is required!")]
        public string Ticker { get; set; }

        [Required(ErrorMessage = "The field {0} is required!")]
        [StringLength(12, ErrorMessage = "The field {0} must have {1} alphanumeric characters.", MinimumLength = 12)]
        [RegularExpression("^[a-zA-Z]{2}[a-zA-Z0-9]{9}[0-9]{1}$", ErrorMessage = "The first two characters of an ISIN must be letters.")]
        public string ISIN { get; set; }

        public string WebSite { get; set; }
    }
}