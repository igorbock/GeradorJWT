var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazorBootstrap();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7097") });
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.InjectClipboard();

await builder.Build().RunAsync();
