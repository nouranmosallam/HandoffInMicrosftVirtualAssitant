
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Bot.Builder.Core.Extensions
{
    public class CatchExceptionMiddleware<T> : IMiddleware where T : Exception
    {
        private readonly Func<ITurnContext, T, Task> _handler;

        public CatchExceptionMiddleware(Func<ITurnContext, T, Task> callOnException)
        {
            _handler = callOnException;
        }

        public async Task OnTurnAsync(ITurnContext context, NextDelegate next, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                // Continue to route the activity through the pipeline
                // any errors further down the pipeline will be caught by
                // this try / catch
                await next(cancellationToken).ConfigureAwait(false);
            }
            catch (T ex)
            {
                // If an error is thrown and the exception is of type T then invoke the handler
                await _handler.Invoke(context, ex).ConfigureAwait(false);
            }
        }

    }
}
