﻿using System.Collections.Generic;
using Jaeger;
using Jaeger.Reporters;
using Jaeger.Samplers;
using OpenTracing.Util;

namespace Epsagon.Dotnet.Tracing.OpenTracingJaeger
{
    public class JaegerTracer
    {
        public static InMemoryReporter memoryReporter = new InMemoryReporter();
        public static Tracer tracer;

        public static Tracer CreateTracer()
        {
            var sampler = new ConstSampler(true);
            tracer = new Tracer.Builder("epsagon-tracer")
                .WithReporter(new CompositeReporter(memoryReporter))
                .WithSampler(sampler)
                .Build();

            if (!GlobalTracer.IsRegistered())
            {
                GlobalTracer.Register(tracer);
            }

            return tracer;
        }

        public static void Clear()
        {
            memoryReporter.Clear();
        }

        public static IEnumerable<Span> GetSpans() => memoryReporter.GetSpans();
    }
}
