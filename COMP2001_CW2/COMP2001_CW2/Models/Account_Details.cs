using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace COMP2001_CW2.Models
{
    public class Account_Details
    {
        //Define the table
        [Key]
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
        [MaxLength (40)]
        public string Username {  get; set; }
        [NotNull]
        [MaxLength(40)]
        public string UserPassword { get; set; }
        public byte[] ProfilePicture { get; set; }
        [MaxLength(255)]
        public string AboutMe { get; set; }
        [NotNull]
        public bool ActivitySpeedPacePreference { get; set; }
        [NotNull]
        public DateTime? Birthday { get; set; }

        //Setup foreign keys for other tables
        public Activity_Link_Table Activity_Link_Table { get; set; }
        public Measurements measurements { get; set; }
    }
}
