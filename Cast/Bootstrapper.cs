using System;
using Cast.Infrastructure;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Responses.Negotiation;
using Nancy.TinyIoc;
using Newtonsoft.Json;

namespace Cast
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

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            container.Register<JsonSerializer, CustomJsonSerializer>();
        }
    }
}