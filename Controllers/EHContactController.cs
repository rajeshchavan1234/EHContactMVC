using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using EHContactMVC.Models;
using System.Threading.Tasks;


namespace EHContactMVC.Controllers
{
    public class EHContactController : Controller
    {
        //GET: EHContact
        public ActionResult Index()
        {
            var list = new SelectList(new[]
            {
                new { Status = "1", Name = "Active" },
                new { Status = "0", Name = "Inactive" },
            }, "Status", "Name", 1);
            ViewData["list"] = list;

            IEnumerable<mvcEHContactModel> contacts = null;
            var responseTask = GlobalVariables.WebApiClient.GetAsync("EHContacts");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<mvcEHContactModel>>();
                readTask.Wait();
                contacts = readTask.Result;
            }
            else
            {
                contacts = Enumerable.Empty<mvcEHContactModel>();
                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            }
            return View(contacts);
        }

        //POST GET: EHContact
        public ActionResult create()
        {

            return View();
        }

        //POST: EHContact
        [HttpPost]
        public ActionResult create(mvcEHContactModel EHContact)
        {
            var postTask = GlobalVariables.WebApiClient.PostAsJsonAsync<mvcEHContactModel>("EHContacts", EHContact);
            postTask.Wait();
            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            return View(EHContact);
        }

        public ActionResult Edit(int id)
        {
            mvcEHContactModel contact = null;
            var responseTask = GlobalVariables.WebApiClient.GetAsync("EHContacts?id=" + id.ToString());
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<mvcEHContactModel>();
                readTask.Wait();
                contact = readTask.Result;
            }
            return View(contact);
        }

        [HttpPost]
        public ActionResult Edit(mvcEHContactModel contact)
        {
            var putTask = GlobalVariables.WebApiClient.PutAsJsonAsync<mvcEHContactModel>("EHContacts", contact);
            putTask.Wait();
            var result = putTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        public ActionResult Delete(int id)
        {
            var deleteTask = GlobalVariables.WebApiClient.DeleteAsync("EHContacts/" + id.ToString());
            deleteTask.Wait();
            var result = deleteTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}