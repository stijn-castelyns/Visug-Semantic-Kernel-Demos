using Microsoft.SemanticKernel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NativeFunctionDemo.Filters;
internal class MyPromptFilter : IPromptFilter
{
    public void OnPromptRendered(PromptRenderedContext context)
    {
        Debug.WriteLine($"Rendered prompt: {context.RenderedPrompt}");
    }

    public void OnPromptRendering(PromptRenderingContext context)
    {
        Debug.WriteLine($"Rendering prompt for {context.Function.Name}");
    }
}
