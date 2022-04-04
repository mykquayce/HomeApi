var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
	.JsonConfig<HomeApi.Models.DeviceLookup>(builder.Configuration.GetSection("Devices"))
	.JsonConfig<HomeApi.Models.InfraredLookup>(builder.Configuration.GetSection("Infrareds"));

builder.Services
	.JsonConfig<Helpers.GlobalCache.Config>(builder.Configuration.GetSection("GlobalCache"))
	.AddTransient<Helpers.GlobalCache.IService, Helpers.GlobalCache.Concrete.Service>();

builder.Services
	.Configure<Helpers.TPLink.Config>(builder.Configuration.GetSection("TPLink"))
	.AddTransient<Helpers.TPLink.ITPLinkClient, Helpers.TPLink.Concrete.TPLinkClient>();

builder.Services
	.AddTransient<HomeApi.Services.IInfraredService, HomeApi.Services.Concrete.InfraredService>()
	.AddTransient<HomeApi.Services.ISmartPlugService, HomeApi.Services.Concrete.SmartPlugService>();

builder.Services
	.AddIdentityClient(builder.Configuration.GetSection("identity"))
	.AddHttpClient<HomeApi.Clients.INetworkDiscoveryClient, HomeApi.Clients.Concrete.NetworkDiscoveryClient>(httpClient =>
	{
		httpClient.BaseAddress = new Uri("https://networkdiscovery");
	})
	.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler { AllowAutoRedirect = false, });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
