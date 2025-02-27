using BarayeAzadi.Application.Common.Utility;
using BarayeAzadi.Application.Services.Implementation;
using BarayeAzadi.Application.Services.Interface;
using BarayeAzadi.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarayeAzadi.Web.Controllers
{
    public class StatementController : Controller
    {
        private readonly IStatementService _statementService;
        public StatementController(IStatementService statementService)
        {
            _statementService = statementService;
        }
        public IActionResult IndexFarsi()
        {
            return View(_statementService.GetAllStatement().Where(u => u.Language == "Farsi" && u.Type != "DemoVideo")
                .OrderByDescending(u => u.Created_Date));
        }
        public IActionResult IndexEnglish()
        {
            return View(_statementService.GetAllStatement().Where(u => u.Language == "English" && u.Type != "DemoVideo")
                .OrderByDescending(u => u.Created_Date));
        }
        public IActionResult DemoVideoEnglish()
        {
            return View(_statementService.GetAllStatement().Where(u => u.Language == "English" && u.Type=="DemoVideo")
                .OrderByDescending(u => u.StatementId));
                
        }
        public IActionResult DemoVideoFarsi()
        {
            return View(_statementService.GetAllStatement().Where(u => u.Language == "Farsi" && u.Type == "DemoVideo")
                .OrderByDescending(u => u.StatementId));

        }
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult IndexAdmin()
        {
            return View(_statementService.GetAllStatement());
        }
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = SD.Role_Admin)]
        [HttpPost]
        public IActionResult Create(Statement statement)
        {
            //if (!string.IsNullOrEmpty(statement.Text) && !string.IsNullOrEmpty(statement.MediaUrl))
            //{
            //    ModelState.AddModelError("Name", "Text and Media could not be empty at the same time!");
            //}
            if (ModelState.IsValid)
            {
                _statementService.CreateStatement(statement);

                TempData["success"] = "Your statement has been sent successfully!";
                return RedirectToAction(nameof(Index), "Home");
            }
            TempData["error"] = "Statement could not be created!Try again later!";

            return RedirectToAction(nameof(Create));
        }
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult Update(int statementId)
        {
            Statement? obj = _statementService.GetStatementById(statementId);
            if (obj is null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(obj);
        }
        [Authorize(Roles = SD.Role_Admin)]
        [HttpPost]
        public IActionResult Update(Statement obj)
        {

            if (ModelState.IsValid && obj.StatementId > 0)
            {
                _statementService.UpdateStatement(obj);
                TempData["success"] = "Statement has been updated successfully!";
                return RedirectToAction(nameof(IndexAdmin));
            }
            TempData["error"] = "Statement could not be updated!";
            return View();
        }
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult Delete(int statementId)
        {
            Statement? obj = _statementService.GetStatementById(statementId);
            if (obj is null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(obj);
        }
        [Authorize(Roles = SD.Role_Admin)]
        [HttpPost]
        public IActionResult Delete(Statement obj)
        {
            bool deleted = _statementService.DeleteStatement(obj.StatementId);
            if (deleted)
            {
                    TempData["success"] = "Statement has been deleted successfully!";
                    return RedirectToAction(nameof(IndexAdmin));

            }
            TempData["error"] = "Statement could not be deleted!";
            return View();
        }
        public IActionResult StatementText(int statementId)
        {
            Statement? obj = _statementService.GetStatementById(statementId);
            if (obj is null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(obj);
        }
    }
}
