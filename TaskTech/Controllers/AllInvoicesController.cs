using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TaskTech.Data;
using TaskTech.Models;

namespace TaskTech.Controllers
{
    public class AllInvoicesController : Controller
    {
        private readonly ApplicationDbContext _db;
        public AllInvoicesController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Invoice> InvoLists = _db.Invoicess;
            return View(InvoLists);
        }

        [HttpGet]
        public async Task<IActionResult> Index(string InvoicSearch)
        {
            ViewData["GetInvoiceDetails"] = InvoicSearch;
            var invoquery = from x in _db.Invoicess select x;

            if (InvoicSearch != null)
            {
                invoquery = invoquery.Where(x => x.ProjectName.Contains(InvoicSearch) || x.ClientName.Contains(InvoicSearch));
            }
            return View(await invoquery.AsNoTracking().ToListAsync());
        }

        public IActionResult ExportExcel()
        {
            using (var workbook = new XLWorkbook())
            {
                var SheetName = workbook.Worksheets.Add("Reports");
                SheetName.Range("A1:S1").Merge();
                SheetName.Cell(1, 1).Value = "Invoice Reports";
                SheetName.Cell(1, 1).Style.Font.Bold = true;
                SheetName.Cell(1, 1).Style.Font.FontColor = XLColor.Aqua;
                SheetName.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                SheetName.Cell(1, 1).Style.Font.FontSize = 50;

                SheetName.Cell(4, 1).Value = "Invoice Id";
                SheetName.Cell(4, 2).Value = "Invoice Date";
                SheetName.Cell(4, 3).Value = "Client Name";
                SheetName.Cell(4, 4).Value = "Net Amount";
                SheetName.Cell(4, 5).Value = "Tax Amount";
                SheetName.Cell(4, 6).Value = "Total Amount";
                SheetName.Cell(4, 7).Value = "Project Name";
                SheetName.Cell(4, 8).Value = "Note";
                SheetName.Cell(4, 9).Value = "Payment Status";
                SheetName.Cell(4, 10).Value = "Projects Id";
                SheetName.Cell(4, 11).Value = "Invoice Name";
                SheetName.Cell(4, 12).Value = "Invoice Title";

                SheetName.Range("A4:S4").Style.Fill.BackgroundColor = XLColor.AliceBlue;

                System.Data.DataTable dt = new System.Data.DataTable();
                SqlConnection con = new SqlConnection("Server=localhost;Database=TechTask;Trusted_Connection=True;MultipleActiveResultSets=True");
                SqlDataAdapter ad = new SqlDataAdapter("select * from Invoices", con);
                ad.Fill(dt);
                int i = 5;
                foreach (System.Data.DataRow row in dt.Rows)
                {
                    SheetName.Cell(i, 1).Value = row[0].ToString();
                    SheetName.Cell(i, 2).Value = row[1].ToString();
                    SheetName.Cell(i, 3).Value = row[2].ToString();
                    SheetName.Cell(i, 4).Value = row[3].ToString();
                    SheetName.Cell(i, 5).Value = row[4].ToString();
                    SheetName.Cell(i, 6).Value = row[5].ToString();
                    SheetName.Cell(i, 7).Value = row[6].ToString();
                    SheetName.Cell(i, 8).Value = row[7].ToString();
                    SheetName.Cell(i, 9).Value = row[8].ToString();
                    SheetName.Cell(i, 10).Value = row[9].ToString();
                    SheetName.Cell(i, 11).Value = row[10].ToString();
                    SheetName.Cell(i, 12).Value = row[11].ToString();
                    i = i + 1;
                }
                i = i - 1;

                SheetName.Cells("A4:S" + i).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                SheetName.Cells("A4:S" + i).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                SheetName.Cells("A4:S" + i).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                SheetName.Cells("A4:S" + i).Style.Border.RightBorder = XLBorderStyleValues.Thin;

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(
                            content,
                            "application/vnd.openxmlformats-officedocument-spreadsheetml.sheet",
                            "InvoiceReport.xlsx"
                        );
                }
            }

        }
    }
}
