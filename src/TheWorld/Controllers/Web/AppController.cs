using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheWorld.ViewModels;
using TheWorld.Services;
using Microsoft.Extensions.Configuration;
using TheWorld.Models;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TheWorld.Controllers
{
    public class AppController : Controller
    {
        private IMailService _mailService;
        private IConfigurationRoot _config;
        private IWorldRepository _repository;
        private ILogger<AppController> _logger;

        public AppController(IMailService mailService, 
            IConfigurationRoot config, 
            IWorldRepository repository,
            ILogger<AppController> logger)
        {
            _mailService = mailService;
            _config = config;
            _repository = repository;
            _logger = logger;

        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            try
            {
                var data = _repository.GetAllTrips();

                return View(data);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed to get trips in index page: { ex.Message}");
                return Redirect("/error");
            }
            
        }

        public IActionResult Contact()
        {
            //throw new InvalidOperationException("thrown exception!");

            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
   
            if (model.Email.ToUpper().Contains("AOL.COM"))
            {
                ModelState.AddModelError("Email", "We dont support AOL addresses");
          
            }

            if (ModelState.IsValid && ModelState.ErrorCount < 1)
            {
                _mailService.SendMail(_config["MailSettings:ToAddress"], model.Email, "From theWorld", model.Message);

                ModelState.Clear();

                ViewBag.UserMessage = "Message Sent";
            }
            
                        
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
