using DnsClient;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using MarketView.Commons;
using log4net.Config;
using Microsoft.Extensions.Configuration;

namespace MarketView.Data
{

    public interface IDataHandler
    {

    }

    public interface IMongoHandler
    {
        IMongoClient Client { get; }
        IMongoDatabase Db { get; }
       
        MongoConfigurations Configurations { get; }

        void Initialize(string database = "");
    }

    public class MongoHandler : IMongoHandler, IDataHandler
    {

        protected IMongoClient _client;
        protected IMongoDatabase _db;
        protected MongoConfigurations _configurations;

        public IMongoClient Client
        {
            get { return _client; }
        }

        public IMongoDatabase Db
        {
            get { return _db; }
        }

        public MongoConfigurations Configurations
        {
            get 
            {
                return _configurations; 
            }
        }

        public MongoHandler(MongoConfigurations configurations)
        {
            _configurations = configurations;
            Initialize();
        }

        
        public MongoHandler(string database)
        {
            Initialize(database);
        }

        public MongoHandler()
        {
            
        }


        public void Initialize(string database = "")
        {

            var dbName = string.IsNullOrWhiteSpace(database) ? _configurations.DatabaseName : database;

            //// If credentials are available, create client differently
            if (!string.IsNullOrWhiteSpace(_configurations.UserName)
                && !string.IsNullOrWhiteSpace(_configurations.Password))
            {
                var credential = MongoCredential.CreateCredential(dbName, _configurations.UserName, _configurations.Password);
                var mongoServerAddress = new MongoServerAddress(_configurations.Host, _configurations.Port);

                var mongoClientSettings = new MongoClientSettings() { Credentials = new[] { credential }, Server = mongoServerAddress };
                _client = new MongoClient(mongoClientSettings);

            }
            else
            {
                //// If no credentials, use connection string and connect directly

                _client = new MongoClient(_configurations.ConnectionString);
                
                //_client = new MongoClient("mongodb://localhost:27017");
                
                
            }

            try
            {
                //_db = _client.GetDatabase("MutualFundDataBase");
                _db = _client.GetDatabase(dbName);

            }
            catch (Exception ex)
            {
                Commons.Logging.LogException(this.GetType().Name, ex.Message, ex);
                throw new ApplicationException("Database could not be connected to.");
            }
        }

    }


}
