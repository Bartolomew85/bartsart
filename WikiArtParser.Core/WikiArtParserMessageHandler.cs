using Microsoft.Extensions.Logging;
using System.Text.Json;
using WikiArtParser.Core.Interfaces;
using WikiArtParser.Core.Models;

namespace WikiArtParser.Core
{
    public class WikiArtParserMessageHandler : IWikiArtParserMessageHandler
    {
        private readonly IWikiArtParser _wikiArtParser;
        private readonly IArtRepository _artRepository;
        private readonly ILogger<WikiArtParserMessageHandler> _logger;

        public WikiArtParserMessageHandler(IWikiArtParser wikiArtParser, IArtRepository artRepository, ILogger<WikiArtParserMessageHandler> logger)
        {
            _wikiArtParser = wikiArtParser;
            _artRepository = artRepository;
            _logger = logger;
        }

        public async Task Handle(WikiArtParserMessage message)
        {
            var artwork = await _wikiArtParser.ParseUrl(message.Url);
            _logger.LogInformation(JsonSerializer.Serialize(artwork));

            await _artRepository.SaveAsync(artwork);
        }
    }
}
