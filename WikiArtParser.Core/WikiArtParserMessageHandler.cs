using WikiArtParser.Core.Interfaces;
using WikiArtParser.Core.Models;

namespace WikiArtParser.Core
{
    public class WikiArtParserMessageHandler : IWikiArtParserMessageHandler
    {
        private readonly IWikiArtParser _wikiArtParser;
        private readonly IArtRepository _artRepository;

        public WikiArtParserMessageHandler(IWikiArtParser wikiArtParser, IArtRepository artRepository)
        {
            _wikiArtParser = wikiArtParser;
            _artRepository = artRepository;
        }

        public async Task Handle(WikiArtParserMessage message)
        {
            var artwork = await _wikiArtParser.ParseUrl(message.Url);

            await _artRepository.SaveAsync(artwork);
        }
    }
}
