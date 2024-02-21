using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TrustGuard_API.Configurations;
using TrustGuard_API.Models;

namespace TrustGuard_API.Services;

public class MessagesService
{
    private readonly IMongoCollection<Messages> _messagesCollection;

    public MessagesService(IOptions<MongoSettings> mongoSettings)
    {
        // 1.creating a client
        var mongoClient = new MongoClient(mongoSettings.Value.ConenctionString);
        // 2.connecting with the database
        var mongoDb = mongoClient.GetDatabase(mongoSettings.Value.DatabaseName);
        // 3.pull the connection
        _messagesCollection = mongoDb.GetCollection<Messages>(mongoSettings.Value.CollectionName);
    }

    public async Task<List<Messages>> GetAsync() => await _messagesCollection.Find(_ => true).ToListAsync();
    
    public async Task<Messages> GetAsync(string id) =>  
        await _messagesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Messages message) => await _messagesCollection.InsertOneAsync(message);
}