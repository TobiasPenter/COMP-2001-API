using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace COMP2001_CW2.Models.Stored_Procedures.PUT
{
    public class UpdateProfilePicture
    {

        [NotNull]
        [MaxLength(100)]
        public string Email { get; set; }
        [MaybeNull]
        public byte[] ProfilePicture { get; set; }

    }
}
