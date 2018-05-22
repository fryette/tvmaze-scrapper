using System;
using System.Collections.Generic;
using System.IO;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.IO;
using Nancy.ModelBinding;
using Nancy.Responses.Negotiation;
using Nancy.TinyIoc;
using Newtonsoft.Json;

namespace Cast
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override Func<ITypeCatalog, NancyInternalConfiguration> InternalConfiguration =>
            NancyInternalConfiguration.WithOverrides(builder => builder.StatusCodeHandlers.Clear());

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            container.Register<JsonSerializer, CustomJsonSerializer>();
        }
    }
}