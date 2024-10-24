using System.Diagnostics;
using DragAndDropPOC.Models;
using Microsoft.AspNetCore.Mvc;

namespace DragAndDropPOC.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IWebHostEnvironment _webHostEnvironment;

		public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment)
		{
			_logger = logger;
			_webHostEnvironment = webHostEnvironment;
		}

		public IActionResult Index()
		{
			return View();
		}

		public async Task<IActionResult> Upload(IFormFile file)
		{
			var fileDirectory = "UploadedFiles";

			string filePath = Path.Combine(_webHostEnvironment.WebRootPath, fileDirectory);

			if (!Directory.Exists(filePath)) 
			
				Directory.CreateDirectory(filePath);
			
			filePath = Path.Combine(filePath, file.FileName);

			using (FileStream fileStream = System.IO.File.Create(filePath))
			{
				file.CopyTo(fileStream);
			}

			return RedirectToAction("Index");
		}

		public IActionResult Privacy()
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
