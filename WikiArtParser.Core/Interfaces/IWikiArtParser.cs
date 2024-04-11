using WikiArtParser.Core.Models;

namespace WikiArtParser.Core.Interfaces
{
    public interface IWikiArtParser
    {
        Task Handle(WikiArtParserMessage message);
    }
}
