using Forum.Challenger.Domain.Commands.Output;
using System;

namespace Forum.Challenger.Domain.Commands.Input
{
    public class TopicsCreateCommand : Request<Response>
    {
        public TopicsCreateCommand(int cdTopic = 0, DateTime? dtTopic = null, DateTime? dtTopicLastEdition = null, string dsTitle = null, string dsText = null, int cdPerson = 0, bool flActive = false)
        {
            CdTopic = cdTopic;
            DtTopic = dtTopic;
            DtTopicLastEdition = dtTopicLastEdition;
            DsTitle = dsTitle;
            DsText = dsText;
            CdPerson = cdPerson;
            FlActive = flActive;
        }

        public int CdTopic { get; set; }
        public DateTime? DtTopic { get; set; }
        public DateTime? DtTopicLastEdition { get; set; }
        public string DsTitle { get; set; }
        public string DsText { get; set; }
        public int CdPerson { get; set; }
        public bool FlActive { get; set; }

        public override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
