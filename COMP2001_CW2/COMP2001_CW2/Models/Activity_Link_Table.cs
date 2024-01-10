﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace COMP2001_CW2.Models
{
    public class Activity_Link_Table
    {
        //Define the table
        [Key]
        [NotNull]
        [MaxLength(100)]
        public string Email {  get; set; }
        [NotNull]
        public int ActivityID { get; set; }

        //Setup the foreign key in this table
        [ForeignKey("Email")]
        public Account_Details Account_Details { get; set; }

        //Setup foreign key for other tables
        public Activity_Link_Table Activity_Name_Table { get; set; }
    }
}
