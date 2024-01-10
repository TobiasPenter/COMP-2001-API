using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace COMP2001_CW2.Models
{
    public class Activity_Name_Table
    {
        //Define the table
        [Key]
        [NotNull]
        public int ActivityID {  get; set; }
        [NotNull]
        [MaxLength(20)]
        public String ActivityName { get; set; }

        //Setup foreign key for this table
        [ForeignKey("ActivityID")]
        public Activity_Name_Table ActivityNameTable { get; set; }

    }
}
