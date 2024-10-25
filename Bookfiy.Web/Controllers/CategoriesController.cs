using Bookfiy.Web.ViewModels;
using Bookify.CoreLayer;
using Bookify.CoreLayer.Entites;
using Bookify.Web.Filters;
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

		[AjaxOnly]
		public IActionResult Create() 
		{ 
			return PartialView("_Form");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryFormViewModel Vmodel)
        {
			if (!ModelState.IsValid)
				return BadRequest();
				//ViewData["Title"] = "Create";

                var Catogery = new Category { Name = Vmodel.Name };

				_unitOfWork.CategoryRepo.Add(Catogery);

			    await _unitOfWork.CompleteAsync();

			return PartialView("_CategoryRow",Catogery);
				
			
        }

		[HttpGet]
		[AjaxOnly]
		public async Task<IActionResult> Edit(int id) 
		{				
                var catogary =await _unitOfWork.CategoryRepo.GetByIdAsync(id);
			if (catogary == null)
				return NotFound();
			
				var VModel = new CategoryFormViewModel()
				{
					Id = catogary.Id,	
					Name = catogary.Name
				};
				return PartialView("_Form", VModel);
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
						return PartialView("_CategoryRow", Catogary);

                }
				return NotFound(); 
			
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ToggleStatus([FromRoute]int id) 
		{
			var cetagory = await _unitOfWork.CategoryRepo.GetByIdAsync(id);
			if (cetagory is null)
				return NotFound();

			cetagory.IsDeleted = !cetagory.IsDeleted;

			cetagory.LastUpdate = DateTime.Now;

	      	 await	_unitOfWork.CompleteAsync();

			return Ok(cetagory.LastUpdate.ToString());
		}
    }
}
