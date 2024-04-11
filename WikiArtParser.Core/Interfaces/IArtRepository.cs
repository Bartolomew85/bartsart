using WikiArtParser.Core.Models;

namespace WikiArtParser.Core.Interfaces
{
    public interface IArtRepository
    {
        Task<IEnumerable<ArtWork>> GetAllArtWorksAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<ArtWork>> GetArtWorksAsync(string creator, CancellationToken cancellationToken = default);
        Task<ArtWork?> GetArtWorkAsync(string creator, string title, CancellationToken cancellationToken = default);
        Task SaveAsync(ArtWork artwork, CancellationToken cancellationToken = default);
        Task DeleteArtWorkAsync(string creator, Guid title, CancellationToken cancellationToken = default);
    }
}
