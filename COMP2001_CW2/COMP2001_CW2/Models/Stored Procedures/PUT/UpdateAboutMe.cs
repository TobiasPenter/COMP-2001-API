using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace COMP2001_CW2.Models.Stored_Procedures.PUT
{
    public class UpdateAboutMe
    {

        [NotNull]
        [MaxLength(100)]
        public string Email { get; set; }
        [NotNull]
        [MaxLength(255)]
        public string AboutMe { get; set; }

    }
}
