using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class Policy
    {
        public int PolicyId { get; set; }   // pk
        public int RoleId { get; set; }     // fk
        public int ModuleId { get; set; }   // fk
        public bool View { get; set; }
        public bool Create { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }


        /* Navigation Properties */
        public Role Role { get; set; }
        public Module Module { get; set; }
    }
}
