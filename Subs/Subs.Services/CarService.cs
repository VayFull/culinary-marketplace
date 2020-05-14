using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using Subs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Subs.Services
{
    public class CarService
    {
        IGridFSBucket gridFS;
        IMongoCollection<Car> Cars;

        public CarService()
        {
            string connectionString = "mongodb://localhost:27017/mobilestore";
            var connection = new MongoUrlBuilder(connectionString);
            // получаем клиента для взаимодействия с базой данных
            MongoClient client = new MongoClient(connectionString);
            // получаем доступ к самой базе данных
            IMongoDatabase database = client.GetDatabase(connection.DatabaseName);
            // получаем доступ к файловому хранилищу
            gridFS = new GridFSBucket(database);
            // обращаемся к коллекции Products
            Cars = database.GetCollection<Car>("Cars");
        }

        public async Task Create(Car car)
        {
            await Cars.InsertOneAsync(car);
        }

        public async Task<Car> GetCar(string id)
        {
            return await Cars.Find(new BsonDocument("_id", new ObjectId(id))).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Car>> GetCars()
        {
            var builder = new FilterDefinitionBuilder<Car>();
            var filter = builder.Empty;
            return await Cars.Find(filter).ToListAsync();
        }
    }
}
