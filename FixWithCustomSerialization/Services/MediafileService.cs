using FixWithCustomSerialization.Contracts;
using FixWithCustomSerialization.Models;
using MongoDB.Driver;

namespace FixWithCustomSerialization.Services
{
    public class MediafileService
    {
        private readonly IMongoCollection<MediaFile> _MediaFiles;
        public MediafileService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _MediaFiles = database.GetCollection<MediaFile>(settings.CollectionName);
        }
        public async Task<List<MediaFile>> GetAllAsync()
        {
            return await _MediaFiles.Find(s => true).ToListAsync();
        }
        public async Task<MediaFile> GetByIdAsync(string Name)
        {
            return await _MediaFiles.Find<MediaFile>(s => s.Name == Name).FirstOrDefaultAsync();
        }
        public async Task<MediaFile> CreateAsync(MediaFile MediaFile)
        {
            await _MediaFiles.InsertOneAsync(MediaFile);
            return MediaFile;
        }
        public async Task UpdateAsync(string Name, MediaFile MediaFile)
        {
            await _MediaFiles.ReplaceOneAsync(s => s.Name == Name, MediaFile);
        }
        public async Task DeleteAsync(string Name)
        {
            await _MediaFiles.DeleteOneAsync(s => s.Name == Name);
        }
    }
}
