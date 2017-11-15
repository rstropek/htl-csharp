# ASP.NET Core

Web App and Web API Development with ASP.NET Core


<!-- .slide: class="left" -->
## Introduction

* Develop server-side logic for web apps
  * Serve static content
  * Dynamic content (e.g. HTML, CSS) creation on the server (*ASP.NET MVC*)
  * *HTTP Web APIs* called by other servers or *single page apps* (SPAs)
* Complete, *a la carte* framework
  * Covers all important aspects of web development
  * Can be reduced to specific needs
  * Can be extended with external libraries (NuGet)
* Open source, driven by Microsoft
* **Repeat fundamentals of [HTTP](https://en.wikipedia.org/wiki/Hypertext_Transfer_Protocol) if necessary**


<!-- .slide: class="left" -->
## Server-side rendering

<img src="images/aspnet-server-side.svg" width="125%" />


<!-- .slide: class="left" -->
## Server-side rendering

* Client is a browser
  * Explicitly started by user
  * Embedded browser (e.g. [CEFSharp](https://github.com/cefsharp/CefSharp), [WebView](https://developer.chrome.com/multidevice/webview/gettingstarted))
* Most of the business logic runs on the server
  * Minor parts of the logic runs on the client<br/>
    (e.g. form validation in JavaScript)
  * Server accesses databases and external services
* Server generates HTML, CSS, JavaScript


<!-- .slide: class="left" -->
## Web APIs + Single Page Apps (SPA)

<img src="images/aspnet-spa.svg" width="125%" />


<!-- .slide: class="left" -->
## Web APIs + Single Page Apps (SPA)

* Client can be a browser
  * Anything that can speak HTTP, JSON, etc.<br/>
    (e.g. mobile app, CLI, server, desktop app, IoT device)
* Static HTML/CSS/JS for SPA
* Logic
  * HTTP Web API requests for running server-side business logic
  * View logic (e.g. manipulating DOM) runs on client
  * [JSON](https://rstropek.github.io/htl-mobile-computing/#/3/7) for transmitting data


<!-- .slide: class="left" -->
## ASP.NET Core Fundamentals

* ASP.NET Core app is a *console app*
* Embedded web server (*Kestrel*)
  * Comes via NuGet
  * No external web server (e.g. *Tomcat*, *IIS*) necessary
  * *Reverse Proxy* recommended for production use
* Create empty ASP.NET Core app with `dotnet new web`


<!-- .slide: class="left" -->
## Project File: *.csproj*

```
<Project Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <TargetFramework>netcoreapp2.0</TargetFramework>
  </PropertyGroup>
  <ItemGroup>
    <Folder Include="wwwroot\" />
  </ItemGroup>
  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.All" Version="2.0.0" />
  </ItemGroup>
</Project>
```

* `wwwroot`: Folder for static content (e.g. HTML)
* `Microsoft.AspNetCore.All`: *Metapackage* for complete ASP.NET Core


<!-- .slide: class="left" -->
## Main Program: *Program.cs*

```
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace AspNetCoreBasics
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://*:8080")   // Listen on port 8080
                .UseStartup<Startup>()      // Web app code in class `Startup`
                .Build()
                .Run();
        }
    }
}
```


<!-- .slide: class="left" -->
## Web App Pipeline: *Startup.cs*

```
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace AspNetCoreBasics {
    public class Startup {
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            app.Use(async (context, next) => {
                if (context.Request.Cookies.ContainsKey("some-cookie")) {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("some-cookie not supported");
                } else { await next(); }
            });
            app.Use(async (context, next) => {
                await context.Response.WriteAsync("Hello ");
                await next();
            });
            app.Run(async (context) => { await context.Response.WriteAsync("World!"); });
        }
    }
}
```


<!-- .slide: class="left" -->
## Web App Pipeline: *Startup.cs*

<img src="images/request-delegate-pipeline.png" />

* Image Source [Microsoft docs](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware?tabs=aspnetcore2x)


<!-- .slide: class="left" -->
## Web App Pipeline: *Startup.cs*

* Create list of *Middlewares* (=*Pipeline*) using functions `Use`, `Map` and `Run`
  * `Use`: Perform some logic and optionally call `next` to invoke next middleware
  * `Map`: Build sub-middleware for specific URL prefix
  * `Run`: Last element in pipeline, no `next`
* **Everything is asynchronous**
* [Read more about *ASP.NET Core Startup*...](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/startup)


<!-- .slide: class="left" -->
## Middlewares: Static Files

```
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace myApp { 
    public class Startup {
        public void Configure(IApplicationBuilder app, ILoggerFactory log)
        {
            app.UseDefaultFiles(new DefaultFilesOptions {
                DefaultFileNames = new[] { "index.html" }
            });
            app.UseStaticFiles();

            // Just for demo purposes we add a second middleware after static files.
            app.Map("/helloworld", subApp => subApp.Run(
                async context => await context.Response.WriteAsync("Hello world!")));
        }
    }
}
```


<!-- .slide: class="left" -->
## Middlewares: Static Files

* Add ready-made middlewares using `Use...` functions
* For static files: `UseStaticFiles()`
* [Read more about static files middleware...](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/static-files)


<!-- .slide: class="left" -->
## Middlewares: Web API

```
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreBasics
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
}
```


<!-- .slide: class="left" -->
## Middlewares: Web API

```
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace app
{
    [Route("api/todo-items")]
    public partial class ToDoController : Controller
    {
        private static List<string> items = new List<String> { "Clean my room", "Feed the cat" };

        [HttpGet]
        public IActionResult GetAllItems()
        {
            return Ok(items);
        }
    }
}
```

* Get all todo items with<br/>
  `GET http://localhost:<port>/api/todo-items`


<!-- .slide: class="left" -->
## Middlewares: Web API

```
using Microsoft.AspNetCore.Mvc;

namespace app
{
    public partial class ToDoController
    {
        [HttpGet]
        [Route("{index}", Name = "GetSpecificItem")]
        public IActionResult GetItem(int index)
        {
            if (index >= 0 && index < items.Count)
            {
                return Ok(items[index]);
            }

            return BadRequest("Invalid index");
        }
    }
}
```

* Get todo item at index 1 with<br/>
  `GET http://localhost:<port>/api/todo-items/1`


<!-- .slide: class="left" -->
## Middlewares: Web API

```
using Microsoft.AspNetCore.Mvc;

namespace app
{
    public partial class ToDoController
    {
        [HttpPost]
        public IActionResult AddItem([FromBody] string newItem)
        {
            items.Add(newItem);
            return CreatedAtRoute("GetSpecificItem", new { index = items.IndexOf(newItem) }, newItem);
        }
    }
}
```

* Add todo item with HTTP `POST` request


<!-- .slide: class="left" -->
## Middlewares: Web API

```
using Microsoft.AspNetCore.Mvc;

namespace app
{
    public partial class ToDoController
    {
        [HttpPut]
        [Route("{index}")]
        public IActionResult UpdateItem(int index, [FromBody] string newItem)
        {
            if (index >= 0 && index < items.Count)
            {
                items[index] = newItem;
                return Ok();
            }

            return BadRequest("Invalid index");
        }
    }
}
```

* Update todo item with HTTP `PUT` request


<!-- .slide: class="left" -->
## Middlewares: Web API

```
using Microsoft.AspNetCore.Mvc;

namespace app
{
    public partial class ToDoController
    {
        [HttpDelete]
        [Route("{index}")]
        public IActionResult DeleteItem(int index)
        {
            if (index >= 0 && index < items.Count)
            {
                items.RemoveAt(index);
                return NoContent();
            }

            return BadRequest("Invalid index");
        }
    }
}
```

* Delete todo item with HTTP `DELETE` request


<!-- .slide: class="left" -->
## Don't Forget *CORS*!

<img src="images/CORS_principle.png" width="50%" />

* Image Source [MDN](https://developer.mozilla.org/en-US/docs/Web/HTTP/CORS)
* [CORS in ASP.NET Core...](https://docs.microsoft.com/en-us/aspnet/core/security/cors)


<!-- .slide: class="left" -->
## Testing Web APIs: Project File

```
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>netcoreapp2.0</TargetFramework>

    <IsPackable>false</IsPackable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.TestHost" Version="2.0.1" />
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="15.3.0-preview-20170628-02" />
    <PackageReference Include="MSTest.TestAdapter" Version="1.1.18" />
    <PackageReference Include="MSTest.TestFramework" Version="1.1.18" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\app\app.csproj" />
  </ItemGroup>

</Project>
```


<!-- .slide: class="left" -->
## Testing Web APIs: Test Web Server

```
using AspNetCoreBasics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;

namespace test
{
    [TestClass]
    public partial class TestToDoController
    {
        private HttpClient client;

        public TestToDoController()
        {
            var builder = new WebHostBuilder().UseStartup<Startup>();
            var testServer = new TestServer(builder);
            client = testServer.CreateClient();
        }
    }
}
```


<!-- .slide: class="left" -->
## Testing Web APIs

```
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
```


<!-- .slide: class="left" -->
## Testing Web APIs

```
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
```


<!-- .slide: class="left" -->
## Testing Web APIs

```
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace test {
    public partial class TestToDoController {
        [TestMethod]
        public async Task TestAdd()
        {
            var message = new HttpRequestMessage(HttpMethod.Post, "/api/todo-items")
            {
                Content = new StringContent("\"Do something\"", Encoding.UTF8, "application/json")
            };
            var response = await client.SendAsync(message);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.IsTrue(response.Headers.Location.PathAndQuery.Contains("/api/todo-items/"));
        }
    }
}
```


<!-- .slide: class="left" -->
## Testing Web APIs

```
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
```


<!-- .slide: class="left" -->
## Testing Web APIs

```
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
```
