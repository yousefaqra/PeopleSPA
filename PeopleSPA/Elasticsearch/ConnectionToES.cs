using Elasticsearch.Net;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeopleSPA.Elasticsearch
{
    public class ConnectionToES
    {

        public static ElasticClient ESClinet()
        {
            ConnectionSettings connectionSettings;
            ElasticClient elasticClient;
            StaticConnectionPool connectionPool;

            var nodes = new Uri[]
            {
                new Uri("http://localhost:9200/"),
            };

            connectionPool = new StaticConnectionPool(nodes);
            connectionSettings = new ConnectionSettings(connectionPool);
            elasticClient = new ElasticClient(connectionSettings);

            return elasticClient;

        }
     



    }
}
