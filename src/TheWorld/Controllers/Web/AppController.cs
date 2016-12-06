﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheWorld.ViewModels;
using TheWorld.Services;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TheWorld.Controllers
{
    public class AppController : Controller
    {
        private IMailService _mailService;

        public AppController(IMailService mailService)
        {
            _mailService = mailService;

        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            //throw new InvalidOperationException("thrown exception!");

            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {

            _mailService.SendMail("Rafael_Loustaunau@Roundrockisd.org", model.Email, "From theWorld", model.Message);

            return View();
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
