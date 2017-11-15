using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Threading.Tasks;

namespace test {
    public partial class TestToDoController {
        [TestMethod]
        public async Task TestGetWithIndex() {
            var response = await client.GetAsync("/api/todo-items/0");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsFalse(string.IsNullOrWhiteSpace(await response.Content.ReadAsStringAsync()));
        }

        [TestMethod]
        public async Task TestInvalidGetWithIndex() {
            var response = await client.GetAsync("/api/todo-items/9999");
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
