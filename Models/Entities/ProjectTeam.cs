using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WhatupTeam.Models.Entities
{
    public class ProjectTeam
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectTeamID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]

        public DateTime CreateOn { get; set; }
        
        public int EmployeeID { get; set; }

        [ForeignKey("EmployeeID")]
        public virtual Employees Employee { get; set; }
        [Required]

        
        public int CompanyID { get; set; }

        [ForeignKey("CompanyID")]
        public virtual Company Company { get; set; }
    }
}