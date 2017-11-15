using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;

namespace test
{
    public partial class TestToDoController
    {
        [TestMethod]
        public async Task TestGetAll()
        {
            var response = await client.GetAsync("/api/todo-items");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var result = JsonConvert.DeserializeObject<string[]>(await response.Content.ReadAsStringAsync());
            Assert.IsTrue(result.Length > 0);
            CollectionAssert.AllItemsAreNotNull(result);
        }
    }
}
