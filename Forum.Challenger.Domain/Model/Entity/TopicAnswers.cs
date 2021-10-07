using System;

namespace Forum.Challenger.Domain.Model.Entity
{
    public class TopicAnswers
    {
        public int CdPerson { get; set; }
        public DateTime DtUser { get; set; }
        public string NmUser { get; set; }
        public string DsEmail { get; set; }
        public string DsPassword { get; set; }
        public bool FlActive { get; set; }

    }
}
