using ConverteFolhaDePontoEmArquivoJson.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ConverteFolhaDePontoEmArquivoJson.Web.Controllers
{
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
        public IActionResult Index(IFormFile userfile)
        {
            try
            {
                string filename = userfile.FileName;
                if (filename.Contains(".csv"))
                {
                    filename = Path.GetFileName(filename);
                    string uploadfilepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", filename);
                    using var stream = new FileStream(uploadfilepath, FileMode.Create);
                    userfile.CopyToAsync(stream);
                    stream.Close();
                    ViewBag.message = "Upload concluído";
                    //Repository.Conversor.GeraJson(uploadfilepath);
                }
                else ViewBag.message = "Tipo de arquivo não suportado. Insira um arquivo .csv";
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
}