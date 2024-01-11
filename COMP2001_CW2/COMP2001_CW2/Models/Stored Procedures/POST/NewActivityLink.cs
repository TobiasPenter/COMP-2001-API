using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace COMP2001_CW2.Models.Stored_Procedures.POST
{
    public class NewActivityLink
    {

        [NotNull]
        [MaxLength(100)]
        public string Email { get; set; }
        public int ActivityID { get; set; }

    }
}
