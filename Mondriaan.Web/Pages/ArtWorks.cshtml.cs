using Microsoft.AspNetCore.Mvc.RazorPages;
using WikiArtParser.Core.Interfaces;
using WikiArtParser.Core.Models;

namespace Mondriaan.Web.Pages
{
    public class ArtWorksModel : PageModel
    {
        private readonly ILogger<ArtWorksModel> _logger;
        private readonly IArtRepository _artRepository;

        public List<ArtWork> ArtWorks { get; private set; }

        public ArtWorksModel(ILogger<ArtWorksModel> logger,
            IArtRepository artRepository)
        {
            _logger = logger;
            _artRepository = artRepository;
        }

        public async Task OnGet()
        {
            var artWorks = await _artRepository.GetAllArtWorksAsync();
            ArtWorks = artWorks.OrderBy(x => x.DateCreated).ToList();

        }
    }
}
