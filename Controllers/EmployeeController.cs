using System.Configuration;
using System.Web.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using UsingMongoDB.Models;

namespace UsingMongoDB.Controllers
{
    public class EmployeeController : Controller
    {
        public MongoCollection<Employee> GetEmployeeCollection()
        {
            //var connectionstring = ConfigurationManager.AppSettings.Get("mongodb://appharbor_e87c0db3-70da-44e5-942e-944cb39b13d1:r2gr4bs5ui8r64jsoias42d5iq@ds045628.mongolab.com:45628");
            //var url = new MongoUrl(connectionstring);
            //var client = new MongoClient(url);
            //var server = client.GetServer();
            //var db = server.GetDatabase(url.DatabaseName);

            var con = new MongoConnectionStringBuilder(ConfigurationManager.ConnectionStrings["MongoDBConnection"].ConnectionString);

            var server = MongoServer.Create(con);
            var db = server.GetDatabase(con.DatabaseName);
            
            return db.GetCollection<Employee>("employee");
        }

        public ActionResult Index()
        {
            var collection = GetEmployeeCollection();
            
            return View(collection.AsQueryable());
        }

        public ActionResult Create()
        {
            return View(new Employee());
        }

        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            var collection = GetEmployeeCollection();
            collection.Insert(employee);
            
            return RedirectToAction("Index");
        }

        public ActionResult Update(string employeeId)
        {
            var collection = GetEmployeeCollection();
            var bsonId = new ObjectId(employeeId);

            var employee = collection.FindOneById(bsonId);

            return View(employee);
        }
        
        [HttpPost]
        public ActionResult Update(Employee employee)
        {
            var collection = GetEmployeeCollection();

            collection.Save(employee);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(string employeeId)
        {
            var collection = GetEmployeeCollection();
            var bsonId = new ObjectId(employeeId);
            collection.Remove(Query.EQ("_id", bsonId));

            return RedirectToAction("Index");
        }
    }
}
