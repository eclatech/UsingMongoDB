using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UsingMongoDB.Models
{
    public class Employee
    {
        [BsonId]
        public ObjectId EmployeeId { get; set; }
        public string EmployeeName { get; set; }
    }
}
