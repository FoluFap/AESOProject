using MarginalPrice.MPService;
using MarginalPrice.ServiceIntegration;
using System.Net;
using System.Net.Mail;

var builder = WebApplication.CreateBuilder(args);
//var builder2 = new ConfigurationBuilder().AddJsonFile("appsettings.json");
//var config2 = builder2.Build();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IaesoServiceInt, aesoServiceInt>();
builder.Services.AddTransient<IMailService, MailService>();
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

var app = builder.Build();



//var smtpClient = new SmtpClient(config2["Smtp:Host"])
//{
//    Port = int.Parse(config2["Smtp:Port"]),
//    Credentials = new NetworkCredential(config2["Smtp:Username"], config2["Smtp:Password"]),
//    EnableSsl = true,
//};

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
