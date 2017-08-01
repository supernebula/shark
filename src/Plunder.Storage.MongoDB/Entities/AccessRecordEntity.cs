using Plunder.Compoment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Plunder.Storage.MongoDB.Entities
{
    [Serializable]
    public class AccessRecordEntity : AccessRecord
    {
        //[BsonRepresentation(BsonType.ObjectId)]
        public override string Id { set; get; }

        public ObjectId RecordId { set; get; }

        public BsonDateTime Created { get; set; }
        public BsonDateTime Updated { get; set; }
    }
    
}
