using Forum.Challenger.Domain.Commands.Output;
using System;

namespace Forum.Challenger.Domain.Commands.Input
{
    public class TopicsFirstCommand : Request<Response>
    {
        public TopicsFirstCommand(int cdTopic = 0)
        {
            CdTopic = cdTopic;
        }

        public int CdTopic { get; set; }
        
        public override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
