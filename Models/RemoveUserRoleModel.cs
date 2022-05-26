using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace University_Final_Project.Models
{
    public class RemoveUserRoleModel
    {
        public string user_id { get; set; }
        public IList<string> roles { get; set; }
    }
}
