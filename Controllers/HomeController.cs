using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DbConnection;

namespace ajaxNotes.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            string query = "SELECT * FROM notes";
            var notes = DbConnector.Query(query);
            ViewBag.notes = notes;
            return View();
        }

        [HttpPost]
        [Route("postNote")]
        public IActionResult Post(string title, string description)
        {
            string dbtitle = title;
            string dbdescription = description;
            string query = $"INSERT INTO notes (Title, Description, created_at, updated_at) VALUES ('{dbtitle}', '{dbdescription}', NOW(), NOW());";
            DbConnector.Execute(query);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("edit/{id}")]
        public IActionResult Edit(int id)
        {
            string query = $"SELECT * FROM notes WHERE id={id}";
            var note = DbConnector.Query(query);
            ViewBag.Edit = note;
            return View("edit");
        }

        [HttpPost]
        [Route("update/{id}")]
        public IActionResult Update(string title, string description, int id)
        {
            string query = $"UPDATE notes SET Title = '{title}', Description = '{description}', updated_at = NOW() WHERE id={id}";
            DbConnector.Execute(query);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            string query = $"DELETE FROM notes WHERE id={id}";
            DbConnector.Execute(query);
            return RedirectToAction("Index");
        }
    }
}
