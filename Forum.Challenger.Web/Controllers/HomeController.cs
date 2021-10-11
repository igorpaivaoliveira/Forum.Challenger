using AutoMapper;
using Forum.Challenger.Domain.Commands.Input;
using Forum.Challenger.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Forum.Challenger.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public HomeController(ILogger<HomeController> logger, IMediator mediator, IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _mediator.Send(new VwTopicDetailsListCommand());

            var topics = new List<VwTopicDetailsModel>();
            _mapper.Map(response.Value, topics);

            if (TempData.Count > 0)
            {
                string message = TempData["message"].ToString();
                ViewBag.success = message;
            }

            return View(topics);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
