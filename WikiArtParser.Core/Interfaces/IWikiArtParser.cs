using WikiArtParser.Core.Models;

namespace WikiArtParser.Core.Interfaces
{
    public interface IWikiArtParser
    {
        Task<ArtWork> ParseUrl(string url);
    }
}
