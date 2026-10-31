using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TheSingularityWorkshop.LearningExplorer;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<ExplorerApp>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

await builder.Build().RunAsync();
