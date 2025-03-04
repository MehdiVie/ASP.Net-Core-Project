﻿using BarayeAzadi.Application.Common.Utility;
using BarayeAzadi.Application.Services.Implementation;
using BarayeAzadi.Application.Services.Interface;
using BarayeAzadi.Domain.Entities;
using BarayeAzadi.Infrastructure.Migrations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarayeAzadi.Web.Controllers
{
    public class ContactusController : Controller
    {
        private readonly IContactusService _contactusService;

        public ContactusController(IContactusService contactusService)
        {
            _contactusService = contactusService;
        }
        
        [Authorize(Roles =SD.Role_Admin)]
        public IActionResult Index()
        {
            return View(_contactusService.GetAllContactuss());   
        }
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult Details(int contactusId)
        {
            Contactus? obj = _contactusService.GetContactusById(contactusId);
            if (obj is null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(obj);
        }
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult Delete(int contactId)
        {
            Contactus? obj =  _contactusService.GetContactusById(contactId);
            
            
            if (obj is null)
            {
                return RedirectToAction("Error", "Home");
            }
            bool deleted = _contactusService.DeleteContactus(obj);
            if (deleted)
            {
                TempData["success"] = "Statement has been deleted successfully!";
                return RedirectToAction(nameof(Index));

            }
            TempData["error"] = "Statement could not be deleted!";
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult Create(Contactus contactus)
        {
            if (contactus.Name.ToLower().ToString() == contactus.Message.ToLower().ToString())
            {
                ModelState.AddModelError("Name", "Message and Name could not be the same!");
            }
            if (ModelState.IsValid)
            {
                _contactusService.CreateContactus(contactus);

                TempData["success"] = "Your message has been sent successfully!";
                return RedirectToAction(nameof(Index), "Home");
            }
            TempData["error"] = "Message could not be created!Try again later!";
            
            return RedirectToAction(nameof(Create));
        }

    }
}
