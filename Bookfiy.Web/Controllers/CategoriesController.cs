using Bookfiy.Web.ViewModels;
using Bookify.CoreLayer;
using Bookify.CoreLayer.Entites;
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

		
		public IActionResult Create() 
		{ 
			return View("Form");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryFormViewModel Vmodel)
        {
			if (ModelState.IsValid) 
			{
				//ViewData["Title"] = "Create";

                var Catogery = new Category { Name = Vmodel.Name };

				_unitOfWork.CategoryRepo.Add(Catogery);

			    await _unitOfWork.CompleteAsync();

				return RedirectToAction(nameof(Index));
				
			}
            return View("Form", Vmodel);
        }

		[HttpGet]
		public async Task<IActionResult> Edit(int Id) 
		{				
                var catogary =await _unitOfWork.CategoryRepo.GetByIdAsync(Id);
			if (catogary != null)
			{
				var VModel = new CategoryFormViewModel()
				{
					Id = catogary.Id,	
					Name = catogary.Name
				};
				return View("Form", VModel);
			}

                return NotFound();
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit([FromRoute]int Id,CategoryFormViewModel viewModel) // id from route  vmodel from Requestbody Form 
		{
			if (Id != viewModel.Id)
			   return BadRequest();	

				var Catogary = await _unitOfWork.CategoryRepo.GetByIdAsync(Id);
				if (Catogary != null)
				{
					Catogary.Name = viewModel.Name;	

			     	Catogary.LastUpdate = DateTime.Now;

				   _unitOfWork.CategoryRepo.Update(Catogary);

				  var count  =   await _unitOfWork.CompleteAsync();

					if (count > 0)
						return RedirectToAction(nameof(Index));

				}
				return NotFound(); 
			
		}
    }
}
