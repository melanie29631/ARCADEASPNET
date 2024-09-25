using ARCADEASPNET.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IO;

namespace ARCADEASPNET.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string filePath;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            filePath = Path.Combine(env.ContentRootPath, "App_Data", "punteos.txt");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Snake()
        {
            return View();
        }

        public IActionResult TicTacToe()
        {
            return View();
        }

        public IActionResult prueba() 
        {
            return View();
        }

       
        [HttpPost]
        public IActionResult GuardarPunteo(int puntaje)
        {
            try
            {
                if (!System.IO.File.Exists(filePath))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                    System.IO.File.Create(filePath).Close();
                }

                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine(puntaje);
                }

                return Json(new { success = true, message = "Punteo guardado correctamente." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar el puntaje.");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult ObtenerPunteos()
        {
            try
            {
                if (System.IO.File.Exists(filePath))
                {
                    string[] puntajes = System.IO.File.ReadAllLines(filePath);
                    return Json(puntajes);
                }
                else
                {
                    return Json(new { success = false, message = "El archivo de puntajes no existe." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los puntajes.");
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
