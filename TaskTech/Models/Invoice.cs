using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskTech.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        [Required, MaxLength(20), DisplayName("Invoice Name")]
        public string InvoiceName { get; set; }
        [Required, MaxLength(300), DisplayName("Invoice Title")]
        public string InvoiceTitle { get; set; }
        [Required, DisplayName("Invoice Date")]
        public DateTime InvoiceDate { get; set; }
        [Required, MaxLength(50), DisplayName("Client Name")]
        public string ClientName { get; set; }
        [Required, DisplayName("Net Amount")]
        [Range(1, int.MaxValue, ErrorMessage = "Amount must be greater than 0!!!")]
        public int NetAmount { get; set; }
        [Required, DisplayName("Tax Amount")]
        [Range(1, int.MaxValue, ErrorMessage = "Amount must be greater than 0!!!")]
        public int TazAmount { get; set; }
        [Required, DisplayName("Total Amount")]
        [Range(1, int.MaxValue, ErrorMessage = "Amount must be greater than 0!!!")]
        public int TotalAmount { get; set; }
        [Required, MaxLength(100), DisplayName("Project Name")]
        public string ProjectName { get; set; }
        [Required, MaxLength(700),]
        public string Note { get; set; }
        [Required, DisplayName("Payment Status")]
        public Boolean PaymentStatusPending { get; set; }
        public Projects Projects { get; set; }
    }
}
