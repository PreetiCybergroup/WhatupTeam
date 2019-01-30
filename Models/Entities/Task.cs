using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WhatupTeam.Models.Entities
{
    public class Task
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaskID { get; set; }
        [Required]
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Tag { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public DateTime ResolvedOn { get; set; }
        public int EmployeeID { get; set; }

        [ForeignKey("EmployeeID")]
        public virtual Employees Employee { get; set; }
        public int ProjectTeamID { get; set; }

        [ForeignKey("ProjectTeamID")]
        public virtual ProjectTeam ProjectTeam {get; set;}
}
}