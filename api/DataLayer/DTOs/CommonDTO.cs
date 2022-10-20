using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DTOs
{
    public class DropDownItemDTO
    {
        public int Value { get; set; }
        public string Display { get; set; }
        public bool IsActive { get; set; }
        public List<DropDownItemDTO> Children { get; set; }
    }

    public class BanksDropDownItemDTO : DropDownItemDTO
    {
        public int SiteID { get; set; }
    }
}
