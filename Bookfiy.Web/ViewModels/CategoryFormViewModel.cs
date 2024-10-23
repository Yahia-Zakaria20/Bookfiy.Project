using System.ComponentModel.DataAnnotations;

namespace Bookfiy.Web.ViewModels
{
    public class CategoryFormViewModel
    {
        public int? Id { get; set; }

        [MaxLength(100,ErrorMessage = "Please enter no more than 100 characters")]
        public string Name { get; set; } = null!;
    }
}
