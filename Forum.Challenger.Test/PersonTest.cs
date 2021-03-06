using Forum.Challenger.Domain.Commands.Input;
using MediatR;
using System;
using Xunit;

namespace Forum.Challenger.Test
{
    public class PersonTest : IClassFixture<BaseTest>
    {
        IMediator _mediator;

        public PersonTest(BaseTest baseTest)
        {
            _mediator = baseTest.mediator;
        }

        [Fact(DisplayName = "Successful response PersonCreateTest")]
        public void PersonCreateTest()
        {
            var command = new PersonsCreateCommand
            (
                nmPerson: "Carlos Alberto",
                dsEmail: "carlosalberto@gmail.com",
                dsPassword: "123456",
                flActive: true
            );

            var response = _mediator.Send(command).Result;

            Assert.True(response.HasMessages);
        }
    }
}
