using Amazon.DynamoDBv2.DataModel;

namespace WikiArtParser.Core.Models
{
    [DynamoDBTable("ArtWorks")]
    public class ArtWork
    {
        [DynamoDBHashKey()]
        public string Creator { get; set; } = string.Empty;
        [DynamoDBRangeKey()]
        public string Title { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string DateCreated { get; set; } = string.Empty;
        public string LocationCreated { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
    }
}
