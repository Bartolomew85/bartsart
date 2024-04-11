using HtmlAgilityPack;
using WikiArtParser.Core.Interfaces;
using WikiArtParser.Core.Models;

namespace WikiArtParser.Core
{
    public class WikiArtParser : IWikiArtParser
    {
        private readonly IArtRepository _artRepository;

        public WikiArtParser(IArtRepository artRepository)
        {
            _artRepository = artRepository;
        }

        public async Task Handle(WikiArtParserMessage message)
        {
            var artwork = await ParseUrl(message.Url);

            await _artRepository.SaveAsync(artwork);
        }

        private static async Task<ArtWork> ParseUrl(string url)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url);
                var contents = await response.Content.ReadAsStringAsync();

                var doc = new HtmlDocument();
                doc.LoadHtml(contents);

                var artwork = new ArtWork();
                artwork.Source = url;
                artwork.Title = doc.DocumentNode.SelectSingleNode("//h5[@itemprop = 'creator']/preceding-sibling::h3")?.InnerText.Trim() ?? string.Empty;
                artwork.Image = doc.DocumentNode.SelectSingleNode("//meta[@property = 'og:image']")?.GetAttributeValue("content", string.Empty) ?? string.Empty;
                artwork.Creator = doc.DocumentNode.SelectSingleNode("//h5[@itemprop = 'creator']")?.InnerText.Trim() ?? string.Empty;
                artwork.DateCreated = doc.DocumentNode.SelectSingleNode("//span[@itemprop = 'dateCreated']")?.InnerText ?? string.Empty;
                artwork.LocationCreated = doc.DocumentNode.SelectSingleNode("//span[@itemprop = 'locationCreated']")?.InnerText ?? string.Empty;

                return artwork;
            }
        }
    }
}
