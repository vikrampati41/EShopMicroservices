﻿using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse> 
        where TRequest : notnull, IRequest<TResponse> where TResponse : notnull
        //Where is filter, TRrequest should not be null and also inherited from IRequest<TResponse> object
        //where TResponse should not be null
        ////This Logging beh applicable to all operation ie all request bcz
        //this line -> where TRequest : notnull, IRequest<TRequest> where TResponse : notnull
        
        //IPipelineBehavior is works like IAsyncActionFilter
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation("[START] Handle Request={Request} - Response={Response} - RequestData={RequestData}",
                typeof(TRequest).Name, typeof(TResponse).Name, request);

            var timer = new Stopwatch();
            timer.Start();

            var response = await next();
            timer.Stop();
            var timeTaken = timer.Elapsed;

            if (timeTaken.Seconds > 3)
            {
                logger.LogWarning("[PERFORMANCE] The request {Request} took {TimeTaken} seconds."
                    ,typeof(TRequest).Name, timeTaken.Seconds);
            }

            logger.LogInformation("[END] Handle {Request} with {Response}", typeof(TRequest).Name, typeof(TResponse).Name);
            return response;
        }
    }
}
