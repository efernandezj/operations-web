using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities
{
    public class Bank
    {
        public int BankId { get; set; }

        public int SiteId { get; set; }
        public string BankName { get; set; }
        public bool IsActive { get; set; }

        /* Navigation Properties */
        public List<Employee> Employees { get; set; }
        public Site Site { get; set; }
    }
}
