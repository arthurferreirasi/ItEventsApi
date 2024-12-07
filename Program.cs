using RpaAlura.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<WebDriverManager>();
builder.Services.AddScoped<ISymplaService, SymplaService>();
builder.Services.AddScoped<ISeleniumUtilService, SeleniumUtilService>();
builder.Services.Configure<SymplaSettings>(builder.Configuration.GetSection("SymplaSettings"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();

