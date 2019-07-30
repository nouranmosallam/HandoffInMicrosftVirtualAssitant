using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using Microsoft.Bot.Schema;
using Underscore.Bot.MessageRouting;
using Underscore.Bot.MessageRouting.Logging;


namespace VirtualAssistant.Middleware.Handoff.Logging
{
    public class AggregationChannelLogger
    {
        private MessageRouter _messageRouter;

        public AggregationChannelLogger(MessageRouter messageRouter)
        {
            _messageRouter = messageRouter;
        }

        public async void Log(string message, [CallerMemberName] string methodName = "")
        {
            if (!string.IsNullOrWhiteSpace(methodName))
            {
                message = $"{DateTime.Now}> {methodName}: {message}";
            }

            bool wasSent = false;

            foreach (ConversationReference aggregationChannel in
                _messageRouter.RoutingDataManager.GetAggregationChannels())
            {
                ResourceResponse resourceResponse =
                    await _messageRouter.SendMessageAsync(aggregationChannel, message);

                if (resourceResponse != null)
                {
                    wasSent = true;
                }
            }

            if (!wasSent)
            {
                System.Diagnostics.Debug.WriteLine(message);
            }
        }
    }
}
