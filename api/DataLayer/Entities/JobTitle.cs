using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class JobTitle
    {
        public int JobId { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }

        /* Navigation Properties */
        public List<Employee> Employees { get; set; }
    }
}
