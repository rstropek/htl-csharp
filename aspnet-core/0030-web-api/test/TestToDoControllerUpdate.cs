using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace test {
    public partial class TestToDoController {
        [TestMethod]
        public async Task TestUpdate() {
            var message = new HttpRequestMessage(HttpMethod.Put, "/api/todo-items/0") {
                Content = new StringContent("\"New task\"", Encoding.UTF8, "application/json")
            };
            var response = await client.SendAsync(message);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            response = await client.GetAsync("/api/todo-items/0");
            Assert.AreEqual("New task", await response.Content.ReadAsStringAsync());
        }
    }
}
