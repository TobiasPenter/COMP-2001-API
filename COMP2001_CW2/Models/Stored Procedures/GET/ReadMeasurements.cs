using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace COMP2001_CW2.Models.Stored_Procedures.GET
{
    public class ReadMeasurements
    {

        [NotNull]
        [MaxLength(100)]
        public string Email { get; set; }
        [NotNull]
        public bool Units { get; set; }
        [MaybeNull]
        public double UserWeight { get; set; }
        [MaybeNull]
        public double UserHeight { get; set; }

    }
}
