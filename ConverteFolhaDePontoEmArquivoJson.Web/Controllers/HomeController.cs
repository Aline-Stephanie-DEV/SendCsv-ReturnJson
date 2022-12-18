using ConverteFolhaDePontoEmArquivoJson.Web.Models;
using Domain;
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
                List<GastoDepartamento> gastos = new();
                foreach (var file in userfiles)
                {
                    string filename = file.FileName;
                    bool arquivoValido = Repository.Conversor.AvaliaNomeDoArquivo(filename);
                    if (!arquivoValido)
                    {
                        ViewBag.message = "Tipo de arquivo não suportado. " +
                            "Insira apenas arquivos nomeados da forma padrão: " +
                            "Departamento-Mes-AAAA.csv";
                    }
                    filename = Path.GetFileName(filename);
                    string uploadfilepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", filename);
                    using var stream = new FileStream(uploadfilepath, FileMode.Create);
                    await file.CopyToAsync(stream);
                    stream.Close();
                    gastos.Add(Repository.Conversor.GeraJson(uploadfilepath, filename));
                }
                Repository.Conversor.MontaJson(gastos);
                ViewBag.message = "Upload da pasta, com " + userfiles.Count.ToString() + " arquivo(s), concluído.";
                return DownloadArquivoJson();
            } 
            catch (Exception ex)
            {
                ViewBag.message = "Error" + ex.Message.ToString();
            }
        return View();
    }

    [HttpGet]
    public IActionResult DownloadArquivoJson()
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", "GastosPorDepartamento.json");

        if (System.IO.File.Exists(path))
        {
            return File(System.IO.File.OpenRead(path), "application/json", Path.GetFileName(path));
        }
        return NotFound();
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