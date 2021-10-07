using Forum.Challenger.Domain.Commands.Output;
using System;

namespace Forum.Challenger.Domain.Commands.Input
{
    public class TopicsUpdateCommand : Request<Response>
    {
        public TopicsUpdateCommand(int cdTopic = 0, string dsTitle = null, string dsText = null, bool flActive = false)
        {
            CdTopic = cdTopic;
            DsTitle = dsTitle;
            DsText = dsText;
            FlActive = flActive;
        }

        public int CdTopic { get; set; }
        public string DsTitle { get; set; }
        public string DsText { get; set; }
        public bool FlActive { get; set; }

        public override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
