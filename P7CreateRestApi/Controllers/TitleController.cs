using Microsoft.AspNetCore.Mvc;

namespace P7CreateRestApi.Controllers
{
    public class TitleController : Controller
    {
        // GET: TitleController
        public ActionResult Index()
        {
            return View();
        }

        // GET: TitleController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TitleController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TitleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TitleController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TitleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TitleController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TitleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
