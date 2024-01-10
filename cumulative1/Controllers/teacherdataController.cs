using cumulative1.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace cumulative1.Controllers
{
    public class teacherdataController : ApiController
    {
     private schooldbcontext school = new schooldbcontext();
  
      
        [HttpGet]


        public IEnumerable<teacher> ListTeacher(string SearchKey = null)
        {
           
            MySqlConnection Conn = school.AccessDatabase();

           
            Conn.Open();

           
            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "Select * from teachers where lower(teacherfname) like lower(@key) or lower(teacherlname) like lower(@key) or lower(concat(teacherfname, ' ', teacherlname)) like lower(@key)";

            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();

            
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            List<teacher> teacher = new List<teacher> { };

            while (ResultSet.Read())
            {
               
                int teacherId = (int)ResultSet["teacherid"];
                string teacherFname = ResultSet["teacherfname"].ToString();
                string teacherLname = ResultSet["teacherlname"].ToString();
                string employeenumber = ResultSet["employeenumber"].ToString();
                DateTime hiredate = Convert.ToDateTime(ResultSet["hiredate"]);
                Decimal Salary = Convert.ToDecimal(ResultSet["salary"]);

                teacher Newteacher = new teacher();
                Newteacher.teacherId = teacherId;
                Newteacher.teacherFname = teacherFname;
                Newteacher.teacherLname = teacherLname;
                Newteacher.employeenumber = employeenumber;
                Newteacher.hiredate = hiredate;
                Newteacher.salary = Salary;


                teacher.Add(Newteacher);
            }

           
            Conn.Close();

            
            return teacher;
        }


       
        [HttpGet]
        public teacher Findteacher(int id)
        {
            teacher Newteacher = new teacher();

           
            MySqlConnection Conn = school.AccessDatabase();

            
            Conn.Open();

            
            MySqlCommand cmd = Conn.CreateCommand();

          
            cmd.CommandText = "Select * from teachers where teacherid = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                int teacherId = (int)ResultSet["teacherid"];
                string teacherFname = ResultSet["teacherfname"].ToString();
                string teacherLname = ResultSet["teacherlname"].ToString();
                string employeenumber = ResultSet["employeenumber"].ToString() ;
                DateTime hiredate = Convert.ToDateTime(ResultSet["hiredate"]);
                Decimal Salary = Convert.ToDecimal(ResultSet["salary"]);


                Newteacher.teacherId = teacherId;
                Newteacher.teacherFname = teacherFname;
                Newteacher.teacherLname = teacherLname;
                Newteacher.employeenumber = employeenumber;
                Newteacher.hiredate = hiredate;
                Newteacher.salary = Salary;
            }
            Conn.Close();

            return Newteacher;
        }


        
        [HttpPost]
      
        public void Deleteteacher(int id)
        {
           
            MySqlConnection Conn = school.AccessDatabase();

            
            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            
            cmd.CommandText = "Delete from teachers where teacherid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();


        }

        
        [HttpPost]
       
        public void Addteacher([FromBody] teacher Newteacher)
        {
          
            MySqlConnection Conn = school.AccessDatabase();

            Debug.WriteLine(Newteacher.teacherFname);

           
            Conn.Open();

       
            MySqlCommand cmd = Conn.CreateCommand();

          
            cmd.CommandText = "insert into teachers (teacherfname, teacherlname, employeenumber, hiredate, salary) values (@teacherFname,@teacherLname,@employeenumber, @hiredate, @salary)";
            cmd.Parameters.AddWithValue("@teacherFname", Newteacher.teacherFname);
            cmd.Parameters.AddWithValue("@teacherLname", Newteacher.teacherLname);
            cmd.Parameters.AddWithValue("@employeenumber", Newteacher.employeenumber);
            cmd.Parameters.AddWithValue("@hiredate", Newteacher.hiredate);
            cmd.Parameters.AddWithValue("@salary", Newteacher.salary);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();



        }


        [HttpPost]
       
        public void Updateteacher(int id, [FromBody] teacher teacherInfo)
        {
            
            MySqlConnection Conn = school.AccessDatabase();

           
            Conn.Open();

            
            MySqlCommand cmd = Conn.CreateCommand();

            
            cmd.CommandText = "update teachers set teacherfname=@teacherFname, teacherlname=@teacherLname, employeenumber=@employeenumber, hiredate=@hiredate, salary=@salary where teacherid=@teacherId";
            cmd.Parameters.AddWithValue("@teacherFname", teacherInfo.teacherFname);
            cmd.Parameters.AddWithValue("@teacherLname", teacherInfo.teacherLname);
            cmd.Parameters.AddWithValue("@employeenumber", teacherInfo.employeenumber);
            cmd.Parameters.AddWithValue("@hiredate", teacherInfo.hiredate);
            cmd.Parameters.AddWithValue("@salary", teacherInfo.salary);
            cmd.Parameters.AddWithValue("@teacherId", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();


        }

    }
}