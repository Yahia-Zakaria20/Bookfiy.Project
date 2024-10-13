using Bookify.CoreLayer;
using Microsoft.AspNetCore.Mvc;

namespace Bookfiy.Web.Controllers
{
	public class CategoriesController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		public CategoriesController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<IActionResult> Index()
		{
			var Categories =await _unitOfWork.CategoryRepo.GetAllAsync();

			return View(Categories);
		}
	}
}
