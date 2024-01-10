using cumulative1.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cumulative1.Controllers
{
    public class teacherController : Controller
    {
        // GET: teacher
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List(string SearchKey = null)
        {
            teacherdataController controller = new teacherdataController();
            IEnumerable<teacher> teachers = controller.ListTeacher(SearchKey);
            return View(teachers);
        }

      
        public ActionResult Show(int id)
        {
            teacherdataController controller = new teacherdataController();
            teacher Selectedteacher = controller.Findteacher(id);


            return View(Selectedteacher);
        }

       
        public ActionResult Delete(int id)
        {
            teacherdataController controller = new teacherdataController();
            controller.Deleteteacher(id);
            return RedirectToAction("List");
        }


        public ActionResult DeleteConfirm(int id)
        {
            teacherdataController controller = new teacherdataController();
            teacher Newteacher = controller.Findteacher(id);

            return View(Newteacher);
        }

      
        //GET ://teacher/New
        public ActionResult New()
        {
            return View();
        }

      

       
        [HttpPost]
        public ActionResult Create(string teacherFname, string teacherLname, string employeename, DateTime hiredate, decimal salary)
        {
           

            Debug.WriteLine("Create Method accessed!");
            Debug.WriteLine(teacherFname);
            Debug.WriteLine(teacherLname);
            Debug.WriteLine(employeename);
            Debug.WriteLine(hiredate);
            Debug.WriteLine(salary);

            teacher Newteacher = new teacher();
            Newteacher.teacherFname = teacherFname;
            Newteacher.teacherLname = teacherLname;
            Newteacher.employeenumber = employeename;
            Newteacher.hiredate = hiredate;
            Newteacher.salary = salary;




            teacherdataController controller = new teacherdataController();
            controller.Addteacher(Newteacher);

            return RedirectToAction("List");
        }


      
        public ActionResult Update(int id)
        {
            teacherdataController controller = new teacherdataController();
            teacher Selectedteacher = controller.Findteacher(id);

            return View(Selectedteacher);
        }

       

      
        [HttpPost]
        public ActionResult Update(int id, string teacherFname, string teacherLname, string employeenumber, DateTime hiredate, decimal salary)
        {
            teacher teacherInfo = new teacher();
            teacherInfo.teacherFname = teacherFname;
            teacherInfo.teacherLname = teacherLname;
            teacherInfo.employeenumber = employeenumber;
            teacherInfo.hiredate = hiredate;
            teacherInfo.salary = salary;

            teacherdataController controller = new teacherdataController();
            controller.Updateteacher(id, teacherInfo);

            return RedirectToAction("Show/" + id);
        }

    }
}