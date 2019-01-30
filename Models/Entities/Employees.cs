using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WhatupTeam.Models.Entities
{
    public class Employees
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

        public bool IsActive { get; set; }

        [Required]
        public int EmployeeTypeID { get; set; }

        [ForeignKey("EmployeeTypeID")]
        public EmployeeType employeeType { get; set; }
        [Required]

        public DateTime createdOn { get; set; }
        
        public int CompanyID { get; set; }

        [ForeignKey("CompanyID")]
        public virtual Company company { get; set; }




    }
}