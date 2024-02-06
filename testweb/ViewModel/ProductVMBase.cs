using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace test.Models.ViewModel
{
    public class ProductVMBase
    {
        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}