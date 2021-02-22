using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskTech.Areas.AdminDigitalLife.ViewModels
{
    public class UserVM
    {
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public IList<string> Role { get; set; }
    }
}
