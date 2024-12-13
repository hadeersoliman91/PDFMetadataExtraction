using PDFMetadata.Application.Business;
using PDFMetadata.Application.Services;
using PDFMetadata.Core.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddLogging(configure => configure.AddFilter("Logs/app.log", LogLevel.Error));
builder.Services.AddSingleton<IPDFExtractionService, PDFExtractionService>();
builder.Services.AddSingleton<IReportGenerator, ReportGenerator>(); 
builder.Services.AddSingleton<IPDFMetadataExtraction, PDFMetadataExtraction>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=PDFMetadata}/{action=Index}/{id?}");

app.Run();
