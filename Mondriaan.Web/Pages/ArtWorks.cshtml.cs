using Microsoft.AspNetCore.Mvc.RazorPages;
using WikiArtParser.Core.Interfaces;
using WikiArtParser.Core.Models;

namespace Mondriaan.Web.Pages
{
    public class ArtWorksModel : PageModel
    {
        private readonly ILogger<ArtWorksModel> _logger;
        private readonly IArtRepository _artRepository;
        private readonly IWikiArtParserMessageHandler _handler;

        public List<ArtWork> ArtWorks { get; private set; }

        public ArtWorksModel(ILogger<ArtWorksModel> logger,
            IArtRepository artRepository,
            IWikiArtParserMessageHandler handler)
        {
            _logger = logger;
            _artRepository = artRepository;
            _handler = handler;
        }

        public async Task OnGet()
        {
            var artWorks = await _artRepository.GetAllArtWorksAsync();
            ArtWorks = artWorks.OrderBy(x => x.DateCreated).ToList();
            _logger.LogInformation($"Found {artWorks.Count()} artworks in repository.");

            //await _handler.Handle(new WikiArtParserMessage { Url = "https://www.wikiart.org/en/leonardo-da-vinci/mona-lisa" });
        }
    }
}
