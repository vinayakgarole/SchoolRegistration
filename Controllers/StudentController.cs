

using Microsoft.AspNetCore.Mvc;
using PdfSharp.Drawing.Layout;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using SchoolRegistration.Models;


namespace SchoolRegistration.Controllers
{
    public class StudentController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                List<Student> data = new List<Student>();
                Student db = new Student();
                data = db.List();
                return View(data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            try
            {
                Student db = new Student();
                ViewBag.StandardName = db.GetStandardName();
                ViewBag.SubjectName = db.GetSubjectName();
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Create(Student model, IFormFile file)
        {
            using (Student db = new Student())
            {
                try
                {
                    ViewBag.StandardName = db.GetStandardName();
                    ViewBag.SubjectName = db.GetSubjectName();
                    if (file != null && file.Length > 0)
                    {
                        // Define the path to save the file in the wwwroot folder
                        string uploadsFolder = Path.Combine("D:\\Wasm demo\\New WASM\\SchoolRegistration\\wwwroot\\", "uploads");
                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        // Generate a unique file name to prevent conflicts
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }

                        // Update the model with the file path
                        model.ProfilePicture = uniqueFileName; // Assuming you have a property called "ImagePath" in your Student model
                    }
                    db.Insert(model);

                    return RedirectToAction("Index", "Student");
                }
                catch (Exception ex)
                {
                    return View(model);
                }
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            using (Student db = new Student())
            {
                ViewBag.StandardName = db.GetStandardName();
                ViewBag.SubjectName = db.GetSubjectName();
                Student obj = db.SelectStudent(id);
                return View(obj);
            }
        }

        [HttpPost]
        public ActionResult Edit(Student model, IFormFile file)
        {
            using (Student db = new Student())
            {
                try
                {
                    ViewBag.StandardName = db.GetStandardName();
                    ViewBag.SubjectName = db.GetSubjectName();

                    if (file != null && file.Length > 0)
                    {
                        string uploadsFolder = Path.Combine("D:\\Wasm demo\\New WASM\\SchoolRegistration\\wwwroot\\", "uploads");
                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }

                        model.ProfilePicture = uniqueFileName; 
                    }

                    db.Update(model);

                    return RedirectToAction("Index", "Student");
                }
                catch (Exception ex)
                {
                    return View(model);
                }
            }
        }



        //public  void DownloadPDF()
        //{
        //    // Create a new document
        //    Document document = new Document();
        //    Section section = document.AddSection();

        //    // Define a table with 8 columns
        //    Table table = section.AddTable();
        //    table.Style = "Table";
        //    table.Borders.Color = Colors.Black;
        //    table.Rows.LeftIndent = 0;

        //    // Define the column widths
        //    table.AddColumn("1cm");
        //    table.AddColumn("3cm");
        //    table.AddColumn("3cm");
        //    table.AddColumn("2cm");
        //    table.AddColumn("2cm");
        //    table.AddColumn("3cm");
        //    table.AddColumn("3cm");
        //    table.AddColumn("3cm");

        //    // Define header row
        //    Row row = table.AddRow();
        //    row.HeadingFormat = true;
        //    row.Format.Alignment = ParagraphAlignment.Center;
        //    row.Format.Font.Bold = true;

        //    // Add header cells
        //    Cell cell = row.Cells[0];
        //    cell.AddParagraph("S_Id");
        //    cell = row.Cells[1];
        //    cell.AddParagraph("S_Name");
        //    cell = row.Cells[2];
        //    cell.AddParagraph("S_Address");
        //    cell = row.Cells[3];
        //    cell.AddParagraph("S_Fees");
        //    cell = row.Cells[4];
        //    cell.AddParagraph("S_Age");
        //    cell = row.Cells[5];
        //    cell.AddParagraph("PhoneNumber");
        //    cell = row.Cells[6];
        //    cell.AddParagraph("StandardName");
        //    cell = row.Cells[7];
        //    cell.AddParagraph("SubjectName");
        //    Student db = new Student();
        //    var students = db.List();

        //    // Populate the table with student data
        //    foreach (var student in students)
        //    {
        //        row = table.AddRow();
        //        row.Format.Alignment = ParagraphAlignment.Left;

        //        row.Cells[0].AddParagraph(student.S_Id.ToString());
        //        row.Cells[1].AddParagraph(student.S_Name);
        //        row.Cells[2].AddParagraph(student.S_Address);
        //        row.Cells[3].AddParagraph(student.S_Fees.ToString());
        //        row.Cells[4].AddParagraph(student.S_Age.ToString());
        //        row.Cells[5].AddParagraph(student.PhoneNumber.ToString());
        //        row.Cells[6].AddParagraph(student.StandardName);
        //        row.Cells[7].AddParagraph(student.SubjectName);
        //    }

        //    // Save the document to a file or stream
        //    PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(false);
        //    pdfRenderer.Document = document;
        //    pdfRenderer.RenderDocument();
        //    pdfRenderer.PdfDocument.Save("StudentInformation.pdf");
        //}



        public ActionResult Delete(int id)
        {
            using (Student db = new Student())
            {
                db.delete(id);
                TempData["DeleteSuccess"] = "Record has been deleted successfully";
                return RedirectToAction("Index");
            }
        }
    }
}
