using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace COMP2001_CW2.Models.Stored_Procedures.DELETE
{
    public class DeleteAccount
    {

        [NotNull]
        [MaxLength(100)]
        public string email { get; set; }

    }
}
