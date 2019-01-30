using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WhatupTeam.Models.Entities;

namespace WhatupTeam.Models.DatabaseAccess
{
    public class WhatupTeamDatabaseInitializer: DropCreateDatabaseIfModelChanges<WhatupTeamDatabaseContext>
    {
       public WhatupTeamDatabaseInitializer()
        {
          WhatupTeamDatabaseContext context = new WhatupTeamDatabaseContext();
          Seed(context);
        }

        protected override void Seed(WhatupTeamDatabaseContext context)
        {
            Company _company = new Company { Name = "Cyber Group", Location = "Noida", Country = "India", ZipCode = "201310" };
            context.Company.Add(_company);
            context.SaveChanges();
        }

        


    }

}