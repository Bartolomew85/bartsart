using WikiArtParser.Core.Models;

namespace WikiArtParser.Core.Interfaces
{
    public interface IWikiArtParserMessageHandler
    {
        Task Handle(WikiArtParserMessage message);
    }
}
