using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using SchoolRegistration.Models;
using System.Data;
using System.Xml.Linq;


namespace SchoolRegistration.Controllers
{
    public class HomeController : Controller
    {

        [HttpPost]
        public IActionResult Export()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[8]
            {
                new DataColumn("S_Id"),
                new DataColumn("S_Name"),
                new DataColumn("S_Address"),
                new DataColumn("S_Fees"),
                new DataColumn("S_Age"),
                new DataColumn("PhoneNumber"),
                new DataColumn("StandardName"),
                new DataColumn("SubjectName")
            });

            using (Student db = new Student())
            {
                var students = db.List();

                foreach (var student in students)
                {
                    dt.Rows.Add(student.S_Id, student.S_Name, student.S_Address, student.S_Fees, student.S_Age, student.PhoneNumber, student.StandardName, student.SubjectName);
                }
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    stream.Seek(0, SeekOrigin.Begin);

                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "StudentData.xlsx");
                }
            }
        }

        public IActionResult DownloadPDF()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Document document = new Document();
                PdfWriter writer = PdfWriter.GetInstance(document, ms);

                // Open the document for writing
                document.Open();

                // Create a table with 8 columns
                PdfPTable table = new PdfPTable(8);
                table.WidthPercentage = 100; // Table width as a percentage of the page width

                // Set column widths as a percentage of the table width
                float[] columnWidths = { 5f, 15f, 15f, 8f, 8f, 15f, 15f, 15f };
                table.SetWidths(columnWidths);

                // Create table header cells
                string[] headers = { "S_Id", "S_Name", "S_Address", "S_Fees", "S_Age", "PhoneNumber", "StandardName", "SubjectName" };
                foreach (string header in headers)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(header, new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD)));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);
                }

                // Replace this with your actual data retrieval logic
                List<Student> students = new List<Student>();
                using (Student db = new Student())
                {
					students = db.List();
				}
					
				// Populate the table with student data
				foreach (var student in students)
                {
                    table.AddCell(student.S_Id.ToString());
                    table.AddCell(student.S_Name);
                    table.AddCell(student.S_Address);
                    table.AddCell(student.S_Fees.ToString());
                    table.AddCell(student.S_Age.ToString());
                    table.AddCell(student.PhoneNumber.ToString());
                    table.AddCell(student.StandardName);
                    table.AddCell(student.SubjectName);
                }
                document.Add(table);
                document.Close();
                //return ms.ToArray();
                return File(ms.ToArray(), "application/pdf", "Students.pdf");
            }

            // Add the table to the document
            //document.Add(table);

            //// Close the document and writer
            //document.Close();
            //writer.Close();
        }
    }
}