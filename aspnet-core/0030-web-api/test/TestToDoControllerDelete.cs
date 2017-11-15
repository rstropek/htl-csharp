using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Threading.Tasks;

namespace test {
    public partial class TestToDoController {
        [TestMethod]
        public async Task TestDelete()
        {
            var response = await client.DeleteAsync("/api/todo-items/0");
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [TestMethod]
        public async Task TestInvalidDelete()
        {
            var response = await client.DeleteAsync("/api/todo-items/9999");
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
