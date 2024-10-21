using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ass2.Model.Models.DTO.FootballPlayer
{
    public class CreateFootballPlayerDTO
    {
        [Required]
        [RegularExpression(@"^[A-Z][a-zA-Z\s@#0-9]*$", ErrorMessage = "Full name must start with a capital letter and can contain letters, spaces, @, #, and digits.")]
        public string FullName { get; set; } = null!;

        [Required]
        [StringLength(100, MinimumLength = 9, ErrorMessage = "Achievements must be between 9 and 100 characters.")]
        public string Achievements { get; set; } = null!;

        [Required(ErrorMessage = "Birthday is required.")]
        [DataType(DataType.Date)]
        [BirthdayValidation("01-01-2007", ErrorMessage = "Birthday must be before 01-01-2007.")]
        public DateTime? Birthday { get; set; }

        [Required]
        public string PlayerExperiences { get; set; } = null!;

        [Required]
        [StringLength(100, MinimumLength = 9, ErrorMessage = "Nomination must be between 9 and 100 characters.")]
        public string Nomination { get; set; } = null!;

        public string? FootballClubId { get; set; }
        public class BirthdayValidationAttribute : ValidationAttribute
        {
            private readonly DateTime _minDate;

            public BirthdayValidationAttribute(string minDate)
            {
                _minDate = DateTime.Parse(minDate);
            }

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value is DateTime birthday)
                {
                    if (birthday >= _minDate)
                    {
                        return new ValidationResult(ErrorMessage ?? $"Birthday must be before {_minDate.ToShortDateString()}.");
                    }
                }

                return ValidationResult.Success;
            }
        }
    }
}
