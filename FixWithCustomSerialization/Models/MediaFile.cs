using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace FixWithCustomSerialization.Models
{
    public class MediaFile
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Name { get; set; } = "";

        public string ImageDataB64 { get; set; } = "";

        public string PublicUrl { get; set; } = "";
    }
}
