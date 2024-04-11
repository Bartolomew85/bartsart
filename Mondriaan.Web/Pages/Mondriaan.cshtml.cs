using Microsoft.AspNetCore.Mvc.RazorPages;
using Mondriaan.Core;

namespace Mondriaan.Web.Pages
{
    public class MondriaanModel : PageModel
    {
        private readonly ILogger<MondriaanModel> _logger;

        public RectangleList Mondriaan { get; private set; }

        public MondriaanModel(ILogger<MondriaanModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            var rectangle = new Rectangle { Width = 300, Height = 400 };
            Mondriaan = rectangle.MakeMondrian();
        }
    }
}
