using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace COMP2001_CW2.Models.Tables
{
    public class Measurements
    {
        //Define the table
        [Key]
        [NotNull]
        [MaxLength(100)]
        public string Email { get; set; }
        [NotNull]
        public bool Units { get; set; }
        [MaybeNull]
        public float UserWeight { get; set; }
        [MaybeNull]
        public float UserHeight { get; set; }


        //Setup foreign key
        [ForeignKey("Email")]
        public Account_Details accountDetailsLink { get; set; }
    }
}
