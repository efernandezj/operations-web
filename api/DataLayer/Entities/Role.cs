using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class Role
    {
        public int RoleId { get; set; } // pk
        public string RoleName { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsActive { get; set; }


        /* Navigation Properties */
        public User Creator { get; set; }
        public User Modifier { get; set; }
        public List<Policy> Policies { get; set; }

    }
}
