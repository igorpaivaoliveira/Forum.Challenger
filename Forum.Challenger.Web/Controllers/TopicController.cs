using AutoMapper;
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
    public class TopicController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public TopicController(IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<ActionResult> Index()
        {
            return View();
        }

        public async Task<ActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateTopic(TopicsModel topicModel)
        {
            string message;
            try
            {
                var request = new TopicsCreateCommand(dtTopic: DateTime.Now, dsTitle: topicModel.DsTitle, dsText: topicModel.DsText, cdPerson: topicModel.CdPerson, flActive: true);

                var response = await _mediator.Send(request);

                if (!response.HasMessages)
                {
                    message = "Topic was created successfully!";
                    TempData["message"] = message;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    var notification = response.Messages.FirstOrDefault();
                    message = notification.Message;
                }
            }
            catch
            {
                message = "Error when registering topic!";
            }

            ViewBag.error = message;
            return View("Create");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int cdTopico)
        {
            var response = await _mediator.Send(new TopicsFirstCommand(cdTopic: cdTopico));

            var topics = new TopicsModel();
            _mapper.Map(response.Value, topics);

            return View(topics);
        }

        [HttpPost]
        public async Task<ActionResult> EditTopic(TopicsModel topicModel)
        {
            string message;
            try
            {
                var request = new TopicsUpdateCommand(cdTopic: topicModel.CdTopic, dsTitle: topicModel.DsTitle, dsText: topicModel.DsText, flActive: topicModel.FlActive);
                var response = await _mediator.Send(request);

                if (!response.HasMessages)
                {
                    message = "Topic has been updated successfully!";
                    TempData["message"] = message;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    var notification = response.Messages.FirstOrDefault();
                    message = notification.Message;
                }

            }
            catch
            {
                message = "Error when updating topic!";
            }

            ViewBag.error = message;
            return View("Edit");            
        }
    }
}
