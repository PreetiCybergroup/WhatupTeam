using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WhatupTeam.Models.Entities
{
    public class EmployeeType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeTypeID { get; set; }

        [Required]
        public string Role { get; set; }
        public DateTime CreatedOn { get; set; }

    }
}