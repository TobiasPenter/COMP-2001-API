using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace COMP2001_CW2.Models.Stored_Procedures.GET
{
    public class ReadAccountActivities
    {
        [NotNull]
        public string Email { get; set; }
        [NotNull]
        public string ActivityName { get; set; }

    }
}
