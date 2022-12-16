using ConverteFolhaDePontoEmArquivoJson.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace ConverteFolhaDePontoEmArquivoJson.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]  
    public async Task<IActionResult> Index(List<IFormFile> userfiles)
    {
        if (userfiles.Count > 0) 
        try
        {
            foreach (var file in userfiles)
            {
                string filename = file.FileName;
                bool arquivosValidos = Repository.Conversor.AvaliaNomeDoArquivo(filename);
                if (arquivosValidos)
                {
                    filename = Path.GetFileName(filename);
                    string uploadfilepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", filename);
                    using var stream = new FileStream(uploadfilepath, FileMode.Create);
                    await file.CopyToAsync(stream);
                    stream.Close();
                    Repository.Conversor.GeraJson(uploadfilepath, filename);
                    ViewBag.message = "Upload de " + userfiles.Count.ToString() + " arquivo(s) concluído.";
                }
                else
                    ViewBag.message = "Tipo de arquivo não suportado. Insira apenas arquivos nomeados da forma padrão: " +
                            "Departamento-Mes-AAAA.csv";
            }
        }
        catch (Exception ex)
        {
            ViewBag.message = "Error" + ex.Message.ToString();
        }
        return View();
    }

    public IActionResult About()
    {
        return View();
    }
    public IActionResult Home()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}