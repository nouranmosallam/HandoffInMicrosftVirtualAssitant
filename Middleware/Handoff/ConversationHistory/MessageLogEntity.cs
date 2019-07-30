using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace VirtualAssistant.Middleware.Handoff.ConversationHistory
{
    public class MessageLogEntity : TableEntity
    {
        public string Body
        {
            get;
            set;
        }
    }
}
