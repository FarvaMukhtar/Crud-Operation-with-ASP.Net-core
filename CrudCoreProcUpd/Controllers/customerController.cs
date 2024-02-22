using Microsoft.AspNetCore.Mvc;
using CrudCoreProcUpd.Models;
using CrudCoreProcUpd.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace CrudCoreProcUpd.Controllers
{
    public class customerController : Controller
    {
        customerDAL cstmDAL = new customerDAL();

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(npmodel cust)
        {
            if (ModelState.IsValid)
            {
                //List<SelectListItem> nameDropdown = cstmDAL.GetdropdownNames();

                //// Assign the dropdown list to a property in your model
                //cust.UsernameList = nameDropdown;

                bool isValidLogin = cstmDAL.ValidateLogin(cust.name, cust.password);
                if (isValidLogin)
                {
                    // Redirect to the list screen

                    HttpContext.Session.SetString("Username", cust.name);

                    return RedirectToAction("List");
                }
                else
                {
                    ViewData["Message"] = "Invalid username or password";
                }

            }

            return View(cust);
        }

        public IActionResult List()
        {
            if (HttpContext.Session.GetString("Username") != null)
            {
                var data = cstmDAL.GetCustomers();
                return View(data);
            }
            else
            {
                //redirect to the login page
                return RedirectToAction("Login");
            }
        }
        
        public IActionResult Create()
        {
            var customer = new customermodel();
            customer.UsernameList = cstmDAL.GetdropdownNames(); // Ensure you populate the dropdown list
            return View(customer);

        }
        [HttpPost]
        public IActionResult Create(customermodel customer)
        {
            List<SelectListItem> nameDropdown = cstmDAL.GetdropdownNames();

            // Assign the dropdown list to a property in your model
            customer.UsernameList = nameDropdown;

            if (cstmDAL.InsertCustomer(customer))
            {
                HttpContext.Session.SetString("CustomerCreated", "true");
                TempData["InsertMsg"] = "<script>Data Created Successfully.......</script>";
                return RedirectToAction("List");
            }
            else
            {
                TempData["ErrorErrorMsg"] = "Data not saved.......";
            }
            return View();
        }

        public IActionResult Details(int id)
        {
            var data = cstmDAL.GetCustomers().Find(item => item.id == id);
            return View(data);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var data = cstmDAL.GetCustomers().Find(item => item.id == id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(customermodel customer)
        {

            if (cstmDAL.UpdateCustomer(customer))
            {
                TempData["UpdateMsg"] = "<script>alert('Data Updated Successfully .......')</script>";
                return RedirectToAction("List");
            }
            else
            {
                TempData["UpdateErrorMsg"] = "<script>alert('Data not updated.......')</script>";
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            int r = cstmDAL.DeleteCustomer(id);
            if (r > 0)
            {
                TempData["DeleteMsg"] = "<script>alert('Data deleted Successfully.......')</script>";
                return RedirectToAction("List");
            }
            else
            {
                TempData["DeleteErrorMsg"] = "<script>alert('Here Some Error to delete the data.......')</script>";
            }
            return View();
        }


        public ActionResult Logout()
        {
            // Clear the session when the user logs out
            HttpContext.Session.Clear();

            HttpContext.Session.Remove("key");
            // Redirect to the login page
            return RedirectToAction("Login");
        }

    }
}
