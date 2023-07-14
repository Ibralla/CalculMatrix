using System.Text.Json.Serialization;
using MatrixModel;
using Microsoft.JSInterop;

namespace viewMatrix
{
    // This class provides an example of how JavaScript functionality can be wrapped
    // in a .NET class for easy consumption. The associated JavaScript module is
    // loaded on demand when first needed.
    //
    // This class can be registered as scoped DI service and then injected into Blazor
    // components for use.

    public class ExampleJsInterop : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        public ExampleJsInterop(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/viewMatrix/exampleJsInterop.js").AsTask());
        }

        public async ValueTask<string> GetInputvalue()
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<string>("getInputvalue");
        }

		public async ValueTask<string> getInputID()
		{
			var module = await moduleTask.Value;
			return await module.InvokeAsync<string>("getInputID");
		}

        public async Task UpdateResult(Matrix data)
        {
            var module = await moduleTask.Value;
            await module.InvokeAsync<string>("updateResult", data.ToString());
        }
        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}