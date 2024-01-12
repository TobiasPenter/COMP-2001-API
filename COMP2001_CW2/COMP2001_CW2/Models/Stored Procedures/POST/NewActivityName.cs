using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace COMP2001_CW2.Models.Stored_Procedures.POST
{
    public class NewActivityName
    {
        [NotNull]
        [MaxLength(20)]
        public string ActivityName { get; set; }

    }
}
