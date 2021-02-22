using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskTech.Models
{
    public class Projects
    {
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string ProName { get; set; }
        [Required, StringLength(500)]
        public string ProTitle { get; set; }
        [Required, StringLength(50)]
        public string ProImage { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
    }
}
