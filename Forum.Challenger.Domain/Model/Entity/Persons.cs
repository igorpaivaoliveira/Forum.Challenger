using System;

namespace Forum.Challenger.Domain.Model.Entity
{
    public class Persons
    {
        public int CdPerson { get; set; }
        public DateTime DtPerson { get; set; }
        public string NmPerson { get; set; }
        public string DsEmail { get; set; }
        public string DsPassword { get; set; }
        public bool FlActive { get; set; }

    }
}
