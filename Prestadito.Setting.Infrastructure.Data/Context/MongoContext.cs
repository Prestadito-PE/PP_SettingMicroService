﻿using MongoDB.Driver;
using Prestadito.Setting.Infrastructure.Data.Interface;

namespace Prestadito.Setting.Infrastructure.Data.Context
{
    public class MongoContext
    {
        private readonly MongoClient client;
        private readonly IMongoDatabase database;

        public MongoContext(ISecurityDBSettings settings)
        {
            client = new MongoClient(settings.ConnectionURI);
            database = client.GetDatabase(settings.DatabaseName);
        }

        public IMongoClient Client
        {
            get
            {
                return client;
            }
        }

        public IMongoDatabase Database
        {
            get
            {
                return database;
            }
        }
    }
}
