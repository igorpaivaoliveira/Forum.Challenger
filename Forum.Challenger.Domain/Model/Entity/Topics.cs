using System;

namespace Forum.Challenger.Domain.Model.Entity
{
    public class Topics
    {
        public int CdTopic { get; set; }
        public DateTime DtTopic { get; set; }
        public DateTime? DtTopicLastEdition { get; set; }
        public string DsTitle { get; set; }
        public string DsText { get; set; }
        public int CdPerson { get; set; }
        public bool FlActive { get; set; }

    }
}
