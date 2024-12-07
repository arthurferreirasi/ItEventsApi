var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ISymplaService, SymplaService>();
builder.Services.AddScoped<ISeleniumUtilService, SeleniumUtilService>();
builder.Services.Configure<SymplaSettings>(builder.Configuration.GetSection("SymplaSettings"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();

