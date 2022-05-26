using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace University_Final_Project.Models
{
    public class EditRoleViewModel
    {
        public string id { get; set; }
        public string Role_Name { get; set; }

        public List<string> users { get; set; }
    }
}
