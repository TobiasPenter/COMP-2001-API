using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace COMP2001_CW2.Models
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
        public float UserWeight { get; set; }
        public float UserHeight { get; set; }


        //Setup foreign key
        [ForeignKey("Email")]
        public Account_Details Account_Details { get; set; }
    }
}
