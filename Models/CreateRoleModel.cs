using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace University_Final_Project.Models
{
    public class CreateRoleModel
    {
        [Required]
        public string Role_Name { get; set; }

        public string Role_ID { get; set; }
    }
}
