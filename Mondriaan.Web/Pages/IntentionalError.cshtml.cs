using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Mondriaan.Web.Pages
{
    public class IntentionalErrorModel : PageModel
    {
        private readonly ILogger<ArtWorksModel> _logger;

        public IntentionalErrorModel(ILogger<ArtWorksModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGet()
        {
            throw new NotImplementedException();
        }
    }
}
