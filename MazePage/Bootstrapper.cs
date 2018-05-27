using System;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Responses.Negotiation;

namespace MazePage
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override Func<ITypeCatalog, NancyInternalConfiguration> InternalConfiguration =>
            NancyInternalConfiguration.WithOverrides(builder =>
            {
                builder.StatusCodeHandlers.Clear();
                builder.ResponseProcessors.Clear();
                builder.ResponseProcessors.Add(typeof(JsonProcessor));
            });
    }
}