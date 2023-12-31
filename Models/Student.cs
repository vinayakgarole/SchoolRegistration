using Microsoft.AspNetCore.Mvc.Rendering;
using PdfSharp.Pdf;
using SchoolRegistration.Data;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace SchoolRegistration.Models
{
    public class Student : IDisposable
    {
        public int S_Id { get; set; }
        public string? S_Name { get; set; }
        public string? S_Address { get; set; }
        public decimal S_Fees { get; set; }
        public int S_Age { get; set; }
        public long PhoneNumber { get; set; }
        public int StandardId { get; set; }
        public string? StandardName { get; set; }
        public int SubjectId { get; set; }
        public string? SubjectName { get; set; }
        public string? ProfilePicture { get; set; }

        #region "constructors"

        ConnectionClass obj_con = null;

        //Default Constructor
        public Student()
        {
            obj_con = new ConnectionClass();
        }

        public List<Student> getAll()
        {
            try
            {
                obj_con.clearParameter();
                DataTable dt = ConvertDatareadertoDataTable(obj_con.ExecuteReader("Sp_SelectAllStudent", CommandType.StoredProcedure));
                obj_con.CommitTransaction();
                obj_con.closeConnection();
                return ConvertToList(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Sp_SelectAllStudent: " + ex.Message, ex);
            }
        }

        private DataTable ConvertDatareadertoDataTable(IDataReader dataReader)
        {
            throw new NotImplementedException();
        }

        public List<Student> ConvertToList(DataTable dt)
        {
            List<Student> list = new List<Student>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Student obj_Student = new Student();
                if (Convert.ToString(dt.Rows[i]["S_Id"]) != "")
                {
                    obj_Student.S_Id = Convert.ToInt32(dt.Rows[i]["S_Id"]);
                }
                if (Convert.ToString(dt.Rows[i]["S_Name"]) != "")
                {
                    obj_Student.S_Name = Convert.ToString(dt.Rows[i]["S_Name"]);
                }
                if (Convert.ToString(dt.Rows[i]["S_Address"]) != "")
                {
                    obj_Student.S_Address = Convert.ToString(dt.Rows[i]["S_Address"]);
                }
                if (Convert.ToString(dt.Rows[i]["S_Fees"]) != "")
                {
                    obj_Student.S_Fees = Convert.ToInt64(dt.Rows[i]["S_Fees"]);
                }
                if (Convert.ToString(dt.Rows[i]["S_Age"]) != "")
                {
                    obj_Student.S_Age = Convert.ToInt32(dt.Rows[i]["S_Age"]);
                }
                if (Convert.ToString(dt.Rows[i]["PhoneNumber"]) != "")
                {
                    obj_Student.PhoneNumber = Convert.ToInt64(dt.Rows[i]["PhoneNumber"]);
                }
                if (Convert.ToString(dt.Rows[i]["StandardName"]) != "")
                {
                    obj_Student.StandardName = Convert.ToString(dt.Rows[i]["StandardName"]);
                }
                if (Convert.ToString(dt.Rows[i]["SubjectName"]) != "")
                {
                    obj_Student.SubjectName = Convert.ToString(dt.Rows[i]["SubjectName"]);
                }
                if (Convert.ToString(dt.Rows[i]["ProfilePicture"]) != "")
                {
                    obj_Student.ProfilePicture = Convert.ToString(dt.Rows[i]["ProfilePicture"]);
                }
                list.Add(obj_Student);
            }
            return list;
        }


        public List<Student> List()
        {
            List<Student> list = new List<Student>();
            try
            {
                obj_con.clearParameter();

                DataTable dt = obj_con.ExecuteReaderTable("Sp_SelectAllStudent", System.Data.CommandType.StoredProcedure);
                return ConvertToList(dt);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Insert DataData
        public void Insert(Student model)
        {
            try
            {
                obj_con.addParameter("@S_Name", model.S_Name);
                obj_con.addParameter("@S_Address", model.S_Address);
                obj_con.addParameter("@S_Fees", model.S_Fees);
                obj_con.addParameter("@S_Age", model.S_Age);
                obj_con.addParameter("@PhoneNumber", model.PhoneNumber);
                obj_con.addParameter("@StandardId ", model.StandardId);
                obj_con.addParameter("@SubjectId ", model.SubjectId);
                obj_con.addParameter("@ProfilePicture ", model.ProfilePicture);
                obj_con.ExecuteNoneQuery("Sp_InsertStudent", System.Data.CommandType.StoredProcedure);
                obj_con.CommitTransaction();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int Update(Student obj)
        {
            try
            {
                obj_con.clearParameter();
                obj_con.addParameter("@S_Id", obj.S_Id);
                obj_con.addParameter("@S_Name", obj.S_Name);
                obj_con.addParameter("@S_Address", obj.S_Address);
                obj_con.addParameter("@S_Fees", obj.S_Fees);
                obj_con.addParameter("@S_Age", obj.S_Age);
                obj_con.addParameter("@StandardId", obj.StandardId);
                obj_con.addParameter("@SubjectId", obj.SubjectId);
                obj_con.addParameter("@PhoneNumber", obj.PhoneNumber);
                obj_con.addParameter("@ProfilePicture", obj.ProfilePicture);
                obj_con.BeginTransaction();
                obj_con.ExecuteNoneQuery("Sp_UpdateStudent", CommandType.StoredProcedure);
                obj_con.CommitTransaction();
                return obj.S_Id = Convert.ToInt32(obj_con.getValue("@S_Id"));
            }
            catch (Exception ex)
            {
                obj_con.RollbackTransaction();
                throw new Exception("Sp_UpdateStudent:" + ex.Message);
            }
        }


        public Student selectById(Int32? S_Id)
        {
            try
            {
                obj_con.clearParameter();
                obj_con.addParameter("@S_Id", S_Id);
                DataTable dt = obj_con.ExecuteReaderTable("Sp_SelectStudent", CommandType.StoredProcedure);
                obj_con.CommitTransaction();
                obj_con.closeConnection();
                return ConvertToOjbect(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Sp_SelectStudent:" + ex.Message);
            }
        }

        public Student ConvertToOjbect(DataTable dt)
        {
            Student obj_Student = new Student();

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                if (Convert.ToString(dt.Rows[i]["S_Id"]) != "")
                {
                    obj_Student.S_Id = Convert.ToInt32(dt.Rows[i]["S_Id"]);
                }
                if (Convert.ToString(dt.Rows[i]["S_Name"]) != "")
                {
                    obj_Student.S_Name = Convert.ToString(dt.Rows[i]["S_Name"]);
                }
                if (Convert.ToString(dt.Rows[i]["S_Address"]) != "")
                {
                    obj_Student.S_Address = Convert.ToString(dt.Rows[i]["S_Address"]);
                }
                if (Convert.ToString(dt.Rows[i]["S_Fees"]) != "")
                {
                    obj_Student.S_Fees = Convert.ToInt64(dt.Rows[i]["S_Fees"]);
                }
                if (Convert.ToString(dt.Rows[i]["S_Age"]) != "")
                {
                    obj_Student.S_Age = Convert.ToInt32(dt.Rows[i]["S_Age"]);
                }
                if (Convert.ToString(dt.Rows[i]["PhoneNumber"]) != "")
                {
                    obj_Student.PhoneNumber = Convert.ToInt64(dt.Rows[i]["PhoneNumber"]);
                }
                if (Convert.ToString(dt.Rows[i]["StandardId"]) != "")
                {
                    obj_Student.StandardId = Convert.ToInt32(dt.Rows[i]["StandardId"]);
                }
                if (Convert.ToString(dt.Rows[i]["SubjectId"]) != "")
                {
                    obj_Student.SubjectId = Convert.ToInt32(dt.Rows[i]["SubjectId"]);
                }
                if (Convert.ToString(dt.Rows[i]["ProfilePicture"]) != "")
                {
                    obj_Student.ProfilePicture = Convert.ToString(dt.Rows[i]["ProfilePicture"]);
                }
            }
            return obj_Student;
        }

        //delete data from database 
        public void delete(Int32? S_Id)
        {
            try
            {
                obj_con.clearParameter();
                obj_con.BeginTransaction();
                obj_con.addParameter("@S_Id", S_Id);
                obj_con.ExecuteNoneQuery("Sp_DeleteStudent", CommandType.StoredProcedure);
                obj_con.CommitTransaction();
            }
            catch (Exception ex)
            {
                obj_con.RollbackTransaction();
                throw new Exception("Sp_DeleteStudent:" + ex.Message);
            }
        }

        public void Dispose()
        {

            obj_con.closeConnection();

            GC.SuppressFinalize(this);
        }

        public Student SelectStudent(Int32? S_Id)
        {
            try
            {
                obj_con.clearParameter();
                obj_con.addParameter("@S_Id", S_Id);
                DataTable dt = obj_con.ExecuteReaderTable("Sp_SelectStudent", CommandType.StoredProcedure);
                obj_con.CommitTransaction();
                obj_con.closeConnection();
                return ConvertToOjbect(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Sp_SelectStudent:" + ex.Message);
            }
        }

        public List<SelectListItem> GetStandardName()
        {
            try
            {
                obj_con.clearParameter();
                DataTable dt = obj_con.ExecuteReaderTable("Sp_GetStandardNameForStandardDropdown", CommandType.StoredProcedure);
                obj_con.CommitTransaction();
                obj_con.closeConnection();
                List<SelectListItem> lst = new List<SelectListItem>();


                SelectListItem sli1 = new SelectListItem();
                sli1.Value = "";
                sli1.Text = "Select Standard Name";
                lst.Add(sli1);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SelectListItem sli = new SelectListItem();


                    sli.Value = Convert.ToString(dt.Rows[i][0]);
                    sli.Text = Convert.ToString(dt.Rows[i][1]);

                    lst.Add(sli);
                }
                return lst;
            }
            catch (Exception ex)
            {
                throw new Exception("Sp_GetStandardNameForStandardDropdown:" + ex.Message);
            }
        }

        public List<SelectListItem> GetSubjectName()
        {
            try
            {
                obj_con.clearParameter();
                DataTable dt = obj_con.ExecuteReaderTable("Sp_GetSubjectNameForSubjectDropdown", CommandType.StoredProcedure);
                obj_con.CommitTransaction();
                obj_con.closeConnection();
                List<SelectListItem> lst = new List<SelectListItem>();


                SelectListItem sli1 = new SelectListItem();
                sli1.Value = "";
                sli1.Text = "Select Subject Name";
                lst.Add(sli1);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SelectListItem sli = new SelectListItem();


                    sli.Value = Convert.ToString(dt.Rows[i][0]);
                    sli.Text = Convert.ToString(dt.Rows[i][1]);

                    lst.Add(sli);
                }
                return lst;
            }
            catch (Exception ex)
            {
                throw new Exception("Sp_GetSubjectNameForSubjectDropdown:" + ex.Message);
            }
        }
    }
}
#endregion