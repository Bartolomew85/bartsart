namespace WikiArtParser.Core;

using global::WikiArtParser.Core.Interfaces;
using global::WikiArtParser.Core.Models;
using System.Threading;

public class ArtRepository : IArtRepository
{
    public ArtRepository()
    {
    }

    public async Task<IEnumerable<ArtWork>> GetAllArtWorksAsync(CancellationToken cancellationToken = default)
    {
        return new List<ArtWork>()
        {
            new ArtWork() { Creator = "Edvard Munch", Title = "The Scream", Image = "https://uploads2.wikiart.org/images/edvard-munch/the-scream-1893(2).jpg!Large.jpg" },
            new ArtWork() { Creator = "Vincent van Gogh", Title = "Starry Night" , Image = "https://uploads3.wikiart.org/00475/images/vincent-van-gogh/the-starry-night-1889.jpg!Large.jpg"},
            new ArtWork() { Creator = "Claude Monet", Title = "Water-Lily Pond", Image = "https://uploads5.wikiart.org/images/claude-monet/the-japanese-bridge-the-water-lily-pond.jpg!Large.jpg" }
        };
    }

    public async Task<IEnumerable<ArtWork>> GetArtWorksAsync(string creator, CancellationToken cancellationToken = default)
    {
        var allArtWorks = await GetAllArtWorksAsync();
        return allArtWorks.Where(x => x.Creator == creator);
    }

    public async Task<ArtWork?> GetArtWorkAsync(string creator, string title, CancellationToken cancellationToken = default)
    {
        var allArtWorks = await GetAllArtWorksAsync();
        return allArtWorks.FirstOrDefault(x => x.Creator == creator && x.Title == title);
    }

    public async Task SaveAsync(ArtWork artwork, CancellationToken cancellationToken = default)
    {
    }

    public async Task DeleteArtWorkAsync(string creator, Guid title, CancellationToken cancellationToken = default)
    {
    }
}

