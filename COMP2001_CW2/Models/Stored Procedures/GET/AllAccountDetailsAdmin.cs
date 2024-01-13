using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace COMP2001_CW2.Models.Stored_Procedures
{
    public class AllAccountDetailsAdmin
    {

        [NotNull]
        [MaxLength(100)]
        public string Email { get; set; }
        [NotNull]
        [MaxLength(20)]
        public string FirstName { get; set; }
        [NotNull]
        [MaxLength(40)]
        public string LastName { get; set; }
        [NotNull]
        [MaxLength(40)]
        public string Username { get; set; }
        [NotNull]
        [MaxLength(40)]
        public string UserPassword { get; set; }
        [MaybeNull]
        public byte[] ProfilePicture { get; set; }
        [MaybeNull]
        [MaxLength(255)]
        public string AboutMe { get; set; }
        [NotNull]
        public bool ActivitySpeedPacePreference { get; set; }
        [NotNull]
        public DateOnly Birthday { get; set; }

    }
}
