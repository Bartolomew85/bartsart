namespace WikiArtParser.Core;

using Amazon.DynamoDBv2.DataModel;
using global::WikiArtParser.Core.Interfaces;
using global::WikiArtParser.Core.Models;
using System.Threading;

public class ArtRepository : IArtRepository
{
    private readonly IDynamoDBContext _dynamoDbContext;

    public ArtRepository(IDynamoDBContext dynamoDbContext)
    {
        _dynamoDbContext = dynamoDbContext;
    }

    public async Task<IEnumerable<ArtWork>> GetAllArtWorksAsync(CancellationToken cancellationToken = default)
    {
        return await _dynamoDbContext
            .ScanAsync<ArtWork>(new List<ScanCondition>())
            .GetRemainingAsync(cancellationToken);
    }

    public async Task<IEnumerable<ArtWork>> GetArtWorksAsync(string creator, CancellationToken cancellationToken = default)
    {
        return await _dynamoDbContext
            .QueryAsync<ArtWork>(creator)
            .GetRemainingAsync(cancellationToken);
    }

    public async Task<ArtWork?> GetArtWorkAsync(string creator, string title, CancellationToken cancellationToken = default)
    {
        return await _dynamoDbContext.LoadAsync<ArtWork?>(creator, title, cancellationToken);
    }

    public async Task SaveAsync(ArtWork artwork, CancellationToken cancellationToken = default)
    {
        await _dynamoDbContext.SaveAsync(artwork, cancellationToken);
    }

    public async Task DeleteArtWorkAsync(string creator, Guid title, CancellationToken cancellationToken = default)
    {
        await _dynamoDbContext.DeleteAsync<ArtWork>(creator, title, cancellationToken);
    }
}

