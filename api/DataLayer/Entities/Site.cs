using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class Site
    {
        public int SiteId { get; set; }
        public string SiteName { get; set; }
        public bool IsActive { get; set; }

        /* Navigation Properties */
        public List<Employee> Employees { get; set; }
        public List<Bank> Banks { get; set; }
    }
}
