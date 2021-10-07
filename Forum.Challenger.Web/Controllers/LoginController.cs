using Forum.Challenger.Domain.Commands.Input;
using Forum.Challenger.Domain.Extensions;
using Forum.Challenger.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Challenger.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IMediator _mediator;

        public LoginController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public ActionResult Index()
        {
            return View("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(PersonsLoginModel personLogin)
        {
            if (personLogin?.DsEmail != null && personLogin?.DsPassword != null)
            {
                var request = new PersonsLoginCommand(dsEmail: personLogin.DsEmail, dsPassword: personLogin.DsPassword);

                var response = await _mediator.Send(request);

                if (!response.HasMessages)
                {
                    var person = new PersonsModel().SetPropertyAutomap(response.Value);

                    HttpContext.Session.SetInt32("cdPerson", person.CdPerson);
                    HttpContext.Session.SetString("nmPerson", person.NmPerson);
                    return RedirectToAction("Index", "Home");
                }

                ViewBag.error = "Invalid user or pass";
                return View("Login");
            }
            else
            {
                ViewBag.error = "Invalid Account";
                return View("Login");
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("cdPerson");
            HttpContext.Session.Remove("nmPerson");
            return RedirectToAction("Index", "Home");
        }

    }
}
