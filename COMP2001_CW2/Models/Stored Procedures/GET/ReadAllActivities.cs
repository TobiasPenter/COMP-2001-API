using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace COMP2001_CW2.Models.Stored_Procedures.GET
{
    public class ReadAllActivities
    {

        [NotNull]
        public int ActivityID { get; set; }
        [NotNull]
        [MaxLength(20)]
        public string ActivityName { get; set; }

    }
}
