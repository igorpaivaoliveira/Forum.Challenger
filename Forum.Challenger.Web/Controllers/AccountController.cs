using Flunt.Notifications;
using Forum.Challenger.Domain.Commands.Input;
using Forum.Challenger.Domain.Extensions;
using Forum.Challenger.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Forum.Challenger.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Index()
        {
            return View("Create");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(PersonsModel person)
        {
            string message;
            try
            {
                var request = new PersonsCreateCommand(dtPerson: DateTime.Now, nmPerson: person.NmPerson.Trim(), dsEmail: person.DsEmail.Trim(), dsPassword: person.DsPassword.Trim(), flActive: true);

                var response = await _mediator.Send(request);

                if (!response.HasMessages)
                {
                    person = new PersonsModel().SetPropertyAutomap(response.Value);

                    HttpContext.Session.SetInt32("cdPerson", person.CdPerson);
                    HttpContext.Session.SetString("nmPerson", person.NmPerson);
                    message = "Account was created successfully!";
                    TempData["message"] = message;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    Notification notification = response.Messages.FirstOrDefault();
                    message = notification.Message;
                }
            }
            catch(Exception ex)
            {
                message = ex.Message;
            }

            ViewBag.error = message;
            return View("Create");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("Index");
        }
    }
}
