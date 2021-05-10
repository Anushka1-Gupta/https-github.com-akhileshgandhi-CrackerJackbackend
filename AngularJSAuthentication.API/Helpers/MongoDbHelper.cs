using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using LinqKit;
using NLog;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.API.Helpers
{
    public class MongoDbHelper<T>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static string connStr => ConfigurationManager.AppSettings["mongoConnStr"];
        private static MongoUrl url = new MongoUrl(connStr);
        public MongoClient dbClient = new MongoClient(new MongoClientSettings()
        {
            Server = url.Server,
            MaxConnectionPoolSize = 3000,
            WaitQueueSize = 2000
        });


        public async Task<bool> InsertLog(T data)
        {
            try
            {
                IMongoDatabase db = dbClient.GetDatabase(ConfigurationManager.AppSettings["mongoDbName"]);
                var collection = db.GetCollection<BsonDocument>(typeof(T).Name + "_" + DateTime.Now.ToString(@"MMddyyyy"));
                var bsonDoc = data.ToBsonDocument();
                await collection.InsertOneAsync(bsonDoc);
            }
            catch (Exception ex)
            {
                logger.Error(new StringBuilder("MongoDB Insert Error: ").Append(ex.ToString()).ToString());
            }

            return true;
        }



        public async Task<bool> InsertAsync(T data)
        {
            try
            {
                IMongoDatabase db = dbClient.GetDatabase(ConfigurationManager.AppSettings["mongoDbName"]);
                var collection = db.GetCollection<BsonDocument>(typeof(T).Name);
                var bsonDoc = data.ToBsonDocument();
                await collection.InsertOneAsync(bsonDoc);
            }
            catch (Exception ex)
            {
                logger.Error(new StringBuilder("MongoDB Insert Error: ").Append(ex.ToString()).ToString());
            }

            return true;
        }

        public bool Insert(T data, string EntityName = "")
        {

            IMongoDatabase db = dbClient.GetDatabase(ConfigurationManager.AppSettings["mongoDbName"]);
            var collection = string.IsNullOrEmpty(EntityName) ? db.GetCollection<BsonDocument>(typeof(T).Name) : db.GetCollection<BsonDocument>(EntityName);
            var bsonDoc = data.ToBsonDocument();
            collection.InsertOne(bsonDoc);


            return true;
        }

        public bool InsertMany(List<T> data, string EntityName = "")
        {
            try
            {
                IMongoDatabase db = dbClient.GetDatabase(ConfigurationManager.AppSettings["mongoDbName"]);
                var collection = string.IsNullOrEmpty(EntityName) ? db.GetCollection<T>(typeof(T).Name) : db.GetCollection<T>(EntityName);
                collection.InsertMany(data);
            }
            catch (Exception ex)
            {
                logger.Error(new StringBuilder("MongoDB Insert Error: ").Append(ex.ToString()).ToString());
            }

            return true;
        }

        public async Task<bool> InsertManyAsync(List<T> data, string EntityName = "")
        {
            try
            {
                IMongoDatabase db = dbClient.GetDatabase(ConfigurationManager.AppSettings["mongoDbName"]);
                var collection = string.IsNullOrEmpty(EntityName) ? db.GetCollection<T>(typeof(T).Name) : db.GetCollection<T>(EntityName);
                await collection.InsertManyAsync(data);
            }
            catch (Exception ex)
            {
                logger.Error(new StringBuilder("MongoDB Insert Error: ").Append(ex.ToString()).ToString());
            }

            return true;
        }

        public async Task<bool> ReplaceAsync(ObjectId id, T data, string collectionName = "")
        {
            try
            {
                IMongoDatabase db = dbClient.GetDatabase(ConfigurationManager.AppSettings["mongoDbName"]);
                if (string.IsNullOrEmpty(collectionName))
                {
                    collectionName = typeof(T).Name;
                }

                var collection = db.GetCollection<BsonDocument>(collectionName);//.FindOneAndReplaceAsync(filter,data);

                var bsonDoc = data.ToBsonDocument();
                var filter = Builders<BsonDocument>.Filter.Eq("_id", id);

                var result = await collection.FindOneAndReplaceAsync(filter, bsonDoc);

                return true;

            }
            catch (Exception ex)
            {
                logger.Error(new StringBuilder("MongoDB Insert Error: ").Append(ex.ToString()).ToString());
            }

            return true;
        }

        public bool Replace(ObjectId id, T data, string collectionName = "")
        {
            try
            {
                if (string.IsNullOrEmpty(collectionName))
                {
                    collectionName = typeof(T).Name;
                }

                IMongoDatabase db = dbClient.GetDatabase(ConfigurationManager.AppSettings["mongoDbName"]);

                var collection = db.GetCollection<BsonDocument>(collectionName);//.FindOneAndReplaceAsync(filter,data);

                var bsonDoc = data.ToBsonDocument();
                var filter = Builders<BsonDocument>.Filter.Eq("_id", id);

                var result = collection.FindOneAndReplace(filter, bsonDoc);

                return true;

            }
            catch (Exception ex)
            {
                logger.Error(new StringBuilder("MongoDB Insert Error: ").Append(ex.ToString()).ToString());
            }

            return true;
        }

        public bool ReplaceWithoutFind(ObjectId id, T data, string collectionName = "")
        {
            try
            {
                if (string.IsNullOrEmpty(collectionName))
                {
                    collectionName = typeof(T).Name;
                }

                IMongoDatabase db = dbClient.GetDatabase(ConfigurationManager.AppSettings["mongoDbName"]);

                var collection = db.GetCollection<BsonDocument>(collectionName);//.FindOneAndReplaceAsync(filter,data);

                var bsonDoc = data.ToBsonDocument();
                var filter = Builders<BsonDocument>.Filter.Eq("_id", id);

                var result = collection.ReplaceOne(filter, bsonDoc);

                return true;

            }
            catch (Exception ex)
            {
                logger.Error(new StringBuilder("MongoDB Insert Error: ").Append(ex.ToString()).ToString());
            }

            return true;
        }
        public async Task<bool> ReplaceWithoutFindAsync(ObjectId id, T data, string collectionName = "")
        {
            try
            {
                if (string.IsNullOrEmpty(collectionName))
                {
                    collectionName = typeof(T).Name;
                }

                IMongoDatabase db = dbClient.GetDatabase(ConfigurationManager.AppSettings["mongoDbName"]);

                var collection = db.GetCollection<BsonDocument>(collectionName);//.FindOneAndReplaceAsync(filter,data);

                var bsonDoc = data.ToBsonDocument();
                var filter = Builders<BsonDocument>.Filter.Eq("_id", id);

                var result = await collection.ReplaceOneAsync(filter, bsonDoc);

                return true;

            }
            catch (Exception ex)
            {
                logger.Error(new StringBuilder("MongoDB Insert Error: ").Append(ex.ToString()).ToString());
            }

            return true;
        }
        public List<T> GetAll()
        {
            IMongoDatabase db = dbClient.GetDatabase("SK");
            var collection = db.GetCollection<T>(typeof(T).Name);
            return collection.Find(x => true).ToList();
        }

        public List<T> Select(Expression<Func<T, bool>> condition, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int? skip = null, int? take = null, bool isLog = false, string dateformat = "", string collectionName = "")
        {
            IMongoDatabase db = dbClient.GetDatabase(ConfigurationManager.AppSettings["mongoDbName"]);

            if (string.IsNullOrEmpty(collectionName))
            {
                collectionName = typeof(T).Name;

                if (isLog && !string.IsNullOrEmpty(dateformat))
                    collectionName = collectionName + "_" + dateformat;
            }

            var collection = db.GetCollection<T>(collectionName);

            var query = collection.AsQueryable().AsExpandable().Where(condition);

            if (skip.HasValue && take.HasValue)
            {
                if (orderBy != null)
                {
                    query = orderBy(query).Skip(skip.Value).Take(take.Value);
                    return query.ToList();
                }

                return query.Skip(skip.Value).Take(take.Value).ToList();

            }
            else
            {
                if (orderBy != null)
                    query = orderBy(query);

                return query.ToList();


            }
        }


        public int Count(Expression<Func<T, bool>> condition, bool isLog = false, string dateformat = "", string collectionName = "")
        {
            IMongoDatabase db = dbClient.GetDatabase(ConfigurationManager.AppSettings["mongoDbName"]);

            if (string.IsNullOrEmpty(collectionName))
            {
                collectionName = typeof(T).Name;

                if (isLog && !string.IsNullOrEmpty(dateformat))
                    collectionName = collectionName + "_" + dateformat;
            }

            var collection = db.GetCollection<T>(collectionName);

            if (typeof(T) == typeof(T))
            {
                return collection.AsQueryable().AsExpandable().Where(condition).Count();
            }
            return 0;
        }
    }
}