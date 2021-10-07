using Forum.Challenger.Domain.Commands.Output;
using System;

namespace Forum.Challenger.Domain.Commands.Input
{
    public class PersonsCreateCommand : Request<Response>
    {
        public PersonsCreateCommand(int cdPerson = 0, DateTime? dtPerson = null, string nmPerson = null, string dsEmail = null, string dsPassword = null, bool flActive = false)
        {
            CdPerson = cdPerson;
            DtPerson = dtPerson;
            NmPerson = nmPerson;
            DsEmail = dsEmail;
            DsPassword = dsPassword;
            FlActive = flActive;
        }

        public int CdPerson { get; set; }
        public DateTime? DtPerson { get; set; }
        public string NmPerson { get; set; }
        public string DsEmail { get; set; }
        public string DsPassword { get; set; }
        public bool FlActive { get; set; }

        public override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
