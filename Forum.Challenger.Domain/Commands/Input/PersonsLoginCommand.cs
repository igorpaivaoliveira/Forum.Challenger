using Forum.Challenger.Domain.Commands.Output;
using System;

namespace Forum.Challenger.Domain.Commands.Input
{
    public class PersonsLoginCommand : Request<Response>
    {
        public PersonsLoginCommand(string nmUser = null, string dsEmail = null, string dsPassword = null)
        {
            DsEmail = dsEmail;
            DsPassword = dsPassword;
        }

        public string DsEmail { get; set; }
        public string DsPassword { get; set; }

        public override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
