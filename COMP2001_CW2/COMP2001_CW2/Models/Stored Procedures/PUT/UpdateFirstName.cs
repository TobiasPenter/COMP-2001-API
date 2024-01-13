using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace COMP2001_CW2.Models.Stored_Procedures.PUT
{
    public class UpdateFirstName
    {

        [NotNull]
        [MaxLength(100)]
        public string Email { get; set; }
        [NotNull]
        [MaxLength(20)]
        public string FirstName { get; set; }

    }
}
