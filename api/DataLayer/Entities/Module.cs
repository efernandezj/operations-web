using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class Module
    {
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public string SubModuleName { get; set; }
        public string ComponentSubModuleName { get; set; }
        public bool IsActive { get; set; }


        /* Navigation Properties */
        public List<Policy> Policies { get; set; }
    }
}
