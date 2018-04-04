# WPF Cash Register Sample

## Introduction

In this exercise, you create a simple cash register. The final result should be a user interface that looks like this:

![Cash Register](cash-register-main-window.png)

## Specification

* Create a backend Web API using ASP.NET Core and Entity Framework Core
* Create a frontend with WPF that uses the Web API backend.
* The database should store products. Every product consists of:
  * ID (numeric, unique key)
  * Product name (mandatory)
  * Unit price (numeric, mandatory)
* The database should store receipt lines. Every receipt line consists of:
  * ID (numeric, unique key)
  * Reference to the bought product
  * Amount of pieces bought
  * Total price (numeric, amount * product's unit price, calculated by backend)
* The database should store receipts. Every receipt consists of:
  * A list of receipt lines (at least one)
  * Receipt timestamp (auto-assigned by backend)
  * Total price (numeric, sum of total prices of all receipt lines, calculated by backend)

## Hands-on Lab

### Setting Up the Project

We want to build a WPF frontend with an ASP.NET Core Web API in the backend. The backend should use Entity Framework Core for accessing the database. Frontend and backend should *share the data model* that is necessary in both application tiers. Your first job is to setup the project in Visual Studio.

* Create a new *ASP.NET Core Web Application* in Visual Studio. Use the project template for Web APIs offered by Visual Studio.
  * Proposed solution name: *CashRegister*
  * Proposed project name: *CashRegister.WebApi*

* Create a new *WPF App (.NET Framework)* project to your solution
  * Proposed project name: *CashRegister.UI*

* Add a new *Class Library (.NET Standard)* project to your solution
  * Proposed project name: *CashRegister.Shared*

* Create the dependencies between your projects
  * Create a dependency from *CashRegister.WebApi* to *CashRegister.Shared*
  * Create a dependency from *CashRegister.UI* to *CashRegister.Shared*
  * Create a dependency from *CashRegister.Tests* to *CashRegister.Shared*

* Add the necessary NuGet packages for Entity Framework Core to the *CashRegister.WebApi* project according to your [EF cheat sheet](https://github.com/rstropek/htl-csharp/blob/master/entity-framework/ef-aspnet-cheat-sheet.md)

* Add the following NuGet package to your WPF project:
  * *Newtonsoft.Json*: Powerful library for handling JSON
  * *Polly*: Powerful library for retry policies (e.g. if network connection is shaky)
  * *Prism.Core*: Useful helper classes for implementing [MVVM (*Model View ViewModel*)](http://prismlibrary.github.io/docs/wpf/Implementing-MVVM.html)

### Create the Model

For our cash register, we need products, receipts and receipt lines. Here are proposed C# classes representing the model:

```cs
public class Product
{
    public int ID { get; set; }
    public string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
}

public class ReceiptLine
{
    public int ID { get; set; }
    public Product Product { get; set; }
    public int Amount { get; set; }
    public decimal TotalPrice { get; set; }
}

public class Receipt
{
    public int ID { get; set; }
    public DateTime ReceiptTimestamp { get; set; }
    public List<ReceiptLine> ReceiptLines { get; set; }
    public decimal TotalPrice { get; set; }
}
```

### Create and Configure the Data Context

* Add an Entity Framework data context class `CashRegisterDataContext` to the *CashRegister.WebApi* project according to your [EF cheat sheet](https://github.com/rstropek/htl-csharp/blob/master/entity-framework/ef-aspnet-cheat-sheet.md)

* Use the appropriate data context method (can be found in [EF cheat sheet](https://github.com/rstropek/htl-csharp/blob/master/entity-framework/ef-aspnet-cheat-sheet.md)) to ensure that *all* properties of the model are required (i.e. not nullable). Making the relations required is nice-to-have. Here is the proposed code for making the properties required:

    ```cs
    modelBuilder.Entity<Product>().Property(p => p.ProductName).IsRequired();
    modelBuilder.Entity<Product>().Property(p => p.UnitPrice).IsRequired();

    modelBuilder.Entity<ReceiptLine>().Property(rl => rl.Amount).IsRequired();
    modelBuilder.Entity<ReceiptLine>().Property(rl => rl.TotalPrice).IsRequired();

    modelBuilder.Entity<Receipt>().Property(r => r.ReceiptTimestamp).IsRequired();
    modelBuilder.Entity<Receipt>().Property(r => r.TotalPrice).IsRequired();
    ```

* Add a connection string to your SQL Server *LocalDB* to *appsettings.json* according to your [EF cheat sheet](https://github.com/rstropek/htl-csharp/blob/master/entity-framework/ef-aspnet-cheat-sheet.md). The connection string looks something like `Server=(localdb)\\dev;Database=CashRegister;Trusted_Connection=True`.

* Add the necessary code to register EF in `Startup.ConfigureServices` according to your [EF cheat sheet](https://github.com/rstropek/htl-csharp/blob/master/entity-framework/ef-aspnet-cheat-sheet.md).

* Create the migration code using Entity Framework command line tools (see [EF cheat sheet](https://github.com/rstropek/htl-csharp/blob/master/entity-framework/ef-aspnet-cheat-sheet.md) for details)

* Create the database schema using Entity Framework command line tools (see [EF cheat sheet](https://github.com/rstropek/htl-csharp/blob/master/entity-framework/ef-aspnet-cheat-sheet.md) for details)

* Add some demo data to the `Products` database table. Here are some sample SQL statements that you can use:

    ```sql
    insert into Products (ProductName, UnitPrice) values ('Bananen 1kg', 1.99)
    insert into Products (ProductName, UnitPrice) values ('Äpfel 1kg', 2.99)
    insert into Products (ProductName, UnitPrice) values ('Trauben Weiß 500g', 2.49)
    insert into Products (ProductName, UnitPrice) values ('Himbeeren 125g', 1.89)
    insert into Products (ProductName, UnitPrice) values ('Karotten 500g', 1.19)
    insert into Products (ProductName, UnitPrice) values ('Eissalat 1 Stück', 0.99)
    insert into Products (ProductName, UnitPrice) values ('Zucchini 1 Stück', 0.99)
    insert into Products (ProductName, UnitPrice) values ('Knoblauch 150g', 2.49)
    insert into Products (ProductName, UnitPrice) values ('Joghurt 200g', 0.49)
    insert into Products (ProductName, UnitPrice) values ('Butter', 2.59)
    ```

### Create Web APIs

* **Try to implement all Web API methods asynchronously using `Task`, `async`, and `await`**

* Tip: Visual Studio generated a demo controller `ValuesController` in the folder *Controllers*. Copy it and change it as needed.

* Create a Web API controller `ProductsController` with a method `Get` that returns all products
  * HTTP method: `GET`
  * Route: `/api/products`
  * Optional *query parameter* for filtering products based on the product name (e.g. `/api/products?nameFilter=Bana`)

    ```cs
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private CashRegisterDataContext DataContext;

        public ProductsController(CashRegisterDataContext dataContext)
        {
            DataContext = dataContext;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> Get([FromQuery]string nameFilter = null)
        {
            IQueryable<Product> products = DataContext.Products;

            // Apply filter if one is given
            if (!string.IsNullOrEmpty(nameFilter))
            {
                products = products.Where(p => p.ProductName.Contains(nameFilter));
            }

            return await products.ToListAsync();
        }
    }
    ```

* Create a Web API to create a receipt
  * HTTP method: `POST`
  * Route: `/api/receipts`
  * Success status code: *Created* (201)
  * JSON body and C# model for *data transformation objects* (DTOs):

    *Example for a valid JSON body:*

    ```json
    [
        { "productID": 1, "amount": 2 },
        { "productID": 2, "amount": 1 }
    ]
    ```

    *C# classes for DTOs (find an appropriate place where to put them):*

    ```cs
    public class ReceiptLineDto
    {
        public int ProductID { get; set; }
        public int Amount { get; set; }
    }
    ```

    *Code for generating a `Receipt` from a `ReceiptDto`:*

    ```cs
    // Read product data from DB for incoming product IDs
    var products = new Dictionary<int, Product>();

    // Here you have to add code that reads all products referenced by product IDs
    // in receiptDto.Lines and store them in the `products` dictionary.

    // Build receipt from DTO
    var newReceipt = new Receipt
    {
        ReceiptTimestamp = DateTime.UtcNow,
        ReceiptLines = receiptLineDto.Select(rl => new ReceiptLine
        {
            ID = 0,
            Product = products[rl.ProductID],
            Amount = rl.Amount,
            TotalPrice = rl.Amount * products[rl.ProductID].UnitPrice
        }).ToList()
    };
    newReceipt.TotalPrice = newReceipt.ReceiptLines.Sum(rl => rl.TotalPrice);
    ```

* Implement the following checks in the previously mentioned Web API to create receipts. If a check fails, return a *bad request* status code.
  * There has to be at least one receipt line
  * Incoming product IDs must refer to *existing* product IDs in the database

Now the backend is done. Make sure to test it with an interactive client like *Postman*. We can start with the frontend.

### WPF UI - ViewModel Structure

* Add a view model class `MainWindowViewModel` to the WPF project. Note the use of the [base class `BindableBase` from the `Prism.Core` NuGet package](http://prismlibrary.github.io/docs/wpf/Implementing-MVVM.html#implementing-inotifypropertychanged). It gives you a lot of nice helper functions to implement `System.ComponentModel.INotifyPropertyChanged` ([see docs](https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.inotifypropertychanged)). This interface is the basis for change notification in WPF data binding.

    ```cs
    public class MainWindowViewModel : BindableBase
    {
    }
    ```

* Our view must be linked to our view model using the `DataContext`:

    ```cs
    public partial class MainWindow : Window
    {
        private MainWindowViewModel ViewModel;

        public MainWindow()
        {
            InitializeComponent();

            // Set the data context for data binding
            DataContext = ViewModel = new MainWindowViewModel();
        }
    }
    ```

* Add a property to `MainWindowViewModel` containing all products. We will have to read the products from the backend Web API. Note how we implement a property that notifies WPF when it changes. This kind of properties is easy to implement if you have the Prism library. Also note the use of `ObservableCollection` ([see docs](https://docs.microsoft.com/en-us/dotnet/api/system.collections.objectmodel.observablecollection-1)). This collection works well with WPF data binding (UI refreshes automatically if its content changes).

    ```cs
    private ObservableCollection<Product> products;
    public ObservableCollection<Product> Products
    {
        get { return products; }
        set { SetProperty(ref products, value); }
    }
    ```

* Next, we need a view model class representing an item in the shopping basket with product ID, product name, amount, and total price. There is no such class yet. As this class will be used with data binding, we have to make it a `BindableBase` (see description above) again.

    ```cs
    public class ReceiptLineViewModel : BindableBase
    {
        private int productID;
        public int ProductID
        {
            get { return productID; }
            set { SetProperty(ref productID, value); }
        }

        private string productName;
        public string ProductName
        {
            get { return productName; }
            set { SetProperty(ref productName, value); }
        }

        private int amount;
        public int Amount
        {
            get { return amount; }
            set { SetProperty(ref amount, value); }
        }

        private decimal totalPrice;
        public decimal TotalPrice
        {
            get { return totalPrice; }
            set { SetProperty(ref totalPrice, value); }
        }
    }
    ```

* Based on our new `ReceiptLineViewModel` class, we can add a property to `MainWindowViewModel` containing the shopping basket. It will be used for data binding, so we use `ObservableCollection` again.

    ```cs
    private ObservableCollection<ReceiptLineViewModel> basket = new ObservableCollection<ReceiptLineViewModel>();
    public ObservableCollection<ReceiptLineViewModel> Basket
    {
        get { return basket; }
        set { SetProperty(ref basket, value); }
    }
    ```

* Our UI has to display a total sum over all items in the shopping basket. Therefore, we need a property for that, too. This property is calculated. Therefore, it just needs a getter, no setter.

    ```cs
    public decimal TotalSum => Basket.Sum(rl => rl.TotalPrice);
    ```

* Last but not least we need two *Commands* (*add item to shopping basket*, and *checkout*) that the buttons can trigger when pressed. The *Prism* library gives us a [helper class for commands named `DelegateCommand`](http://prismlibrary.github.io/docs/wpf/Implementing-MVVM.html#implementing-command-objects). A delegate command is representing a function that should be called when the button is pressed *and* an *execution state*. If the *execution state* is `false`, the command cannot be called and WPF will *automatically disable the bound button*. If it is `true`, the button is enabled.

    ```cs
    // The command takes an integer input parameter because it receives the
    // ID of the product which should be added.
    public DelegateCommand<int?> AddToBasketCommand { get; }

    public DelegateCommand CheckoutCommand { get; }
    ```

Now the structure of our view model is done. We will come back to our `MainWindowViewModel` class later when we implement the methods that talk to our backend. For now, we want to focus on the view.

### WPF UI - View

Here is the code for the view. Note that the code contains a lot of comments. Make sure to read them closely. They contain a lot of valuable information. **Do not just copy and paste the code into your project without working it through**.

```xml
<!-- Note the `d:DataContext` attribute. It will give you IntelliSense by looking
     at your view model during design time. -->
<Window x:Class="CashRegister.UI.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:CashRegister.UI"
        mc:Ignorable="d"
        d:DataContext="{d:DesignInstance Type=local:MainWindowViewModel, IsDesignTimeCreatable=False}"
        Title="Cash Register Exercise" Height="700" Width="1000">
    <Window.Resources>
        <!-- WPF Styles are used to format the UI -->
        <Style x:Key="TotalSum" TargetType="TextBlock">
            <Setter Property="FontFamily" Value="Lucida Console" />
            <Setter Property="FontSize" Value="50" />
            <Setter Property="HorizontalAlignment" Value="Right" />
        </Style>

        <!-- Note the `BasedOn` attribute -->
        <Style x:Key="TotalSumText" TargetType="TextBlock" BasedOn="{StaticResource ResourceKey=TotalSum}">
            <Setter Property="FontSize" Value="20" />
            <Setter Property="Margin" Value="0,5,0,0" />
        </Style>

        <Style x:Key="Basket" TargetType="ItemsControl">
            <Setter Property="FontFamily" Value="Lucida Console" />
            <Setter Property="FontSize" Value="10" />
            <Setter Property="Margin" Value="0,0,0,5" />
        </Style>

        <Style x:Key="CheckoutButton" TargetType="Button">
            <Setter Property="FontFamily" Value="Lucida Console" />
            <Setter Property="FontSize" Value="50" />
        </Style>

        <Style x:Key="ProductButton" TargetType="Button">
            <Setter Property="Width" Value="150" />
            <Setter Property="Height" Value="100" />
            <Setter Property="Margin" Value="5,5,0,0" />
        </Style>
    </Window.Resources>
    <Grid>
        <Grid.ColumnDefinitions>
            <!-- Note the use of column weights -->
            <ColumnDefinition Width="2*" />
            <ColumnDefinition Width="*" />
        </Grid.ColumnDefinitions>

        <!-- Note the use of a `ScrollViewer`. This gives us a scrollbar on
             small screens. -->
        <ScrollViewer VerticalScrollBarVisibility="Auto">
            <!-- Note the use of an `ItemsControl` here. It behaves just like a
                 listbox without the ability to select an item. In fact, it is
                 the base class of the listbox. -->
            <ItemsControl Margin="10,10,5,10" ItemsSource="{Binding Path=Products}">
                <ItemsControl.ItemTemplate>
                    <!-- Note how we turn every item in the itemscontrol into a button by
                            using a data template. -->
                    <DataTemplate>
                        <!-- Note the use of a command binding here. Read more
                             at http://prismlibrary.github.io/docs/wpf/Implementing-MVVM.html#commands
                             The C# code in the view model will also make commands clearer -->
                        <!-- Note how we reference styles using `StaticResource` -->
                        <!-- Note the use of `RelativeSource` here. It is used to access properties of 
                             a parent control (in our case `ItemsControl`) -->
                        <Button Style="{StaticResource ResourceKey=ProductButton}" Content="{Binding Path=ProductName}"
                                Command="{Binding RelativeSource={RelativeSource AncestorType=ItemsControl}, Path=DataContext.AddToBasketCommand}"
                                CommandParameter="{Binding Path=ID}" />
                    </DataTemplate>
                </ItemsControl.ItemTemplate>
                <!-- Note how we switch the panel of the itemscontrol to a `WrapPanel`.
                     Therefore, buttons flow from left to right with line breaks. -->
                <ItemsControl.ItemsPanel>
                    <ItemsPanelTemplate>
                        <WrapPanel />
                    </ItemsPanelTemplate>
                </ItemsControl.ItemsPanel>
            </ItemsControl>
        </ScrollViewer>

        <Grid Grid.Column="1" Margin="5,10,10,10">
            <Grid.RowDefinitions>
                <RowDefinition Height="*" />
                <!-- Note sizing based on content with `Auto` -->
                <RowDefinition Height="Auto" />
                <RowDefinition Height="Auto" />
                <RowDefinition Height="100" />
            </Grid.RowDefinitions>

            <!-- Note the use of a data grid -->
            <DataGrid ItemsSource="{Binding Path=Basket}" AutoGenerateColumns="False">
                <DataGrid.Columns>
                    <DataGridTextColumn Header="Produkt" Binding="{Binding Path=ProductName}" MinWidth="200" />
                    <DataGridTextColumn Header="Menge" Binding="{Binding Path=Amount}" />
                    <DataGridTextColumn Header="Preis" Binding="{Binding Path=TotalPrice}" />
                </DataGrid.Columns>
            </DataGrid>
            <TextBlock Grid.Row="1" Text="Gesamtsumme:" Style="{StaticResource ResourceKey=TotalSumText}" />
            <TextBlock Grid.Row="2" Text="{Binding Path=TotalSum}" Style="{StaticResource ResourceKey=TotalSum}" />
            <Button Grid.Row="4" Style="{StaticResource ResourceKey=CheckoutButton}" Command="{Binding CheckoutCommand}">Checkout</Button>
        </Grid>
    </Grid>
</Window>
```

### WPF UI - View Model Logic

Now we have to implement the logic in our view model.

* Add an `HttpClient` instance that we can use to access our backend Web API:

    ```cs
    // Add a HttpClient instance that we can use to access our backend Web API
    private HttpClient HttpClient = new HttpClient
    {
        BaseAddress = new Uri("http://localhost:55495"),
        Timeout = TimeSpan.FromSeconds(5)
    };
    ```

* Add a constructor that configures our command. Note the comments in the code:

    ```cs
    public MainWindowViewModel()
    {
        // Connect the command with the handling function
        AddToBasketCommand = new DelegateCommand<int?>(OnAddToBasket);

        // Connect the command with the handling function AND define a function
        // that returns true only if the command can be executed (i.e. the button
        // can be pressed).
        CheckoutCommand = new DelegateCommand(async () => await OnCheckout(), () => Basket.Count > 0);

        // Whenever something in the shopping basket changes, we have to notify WPF
        // that the total sum has changed and the execution state of our checkout command
        // might have changed.
        Basket.CollectionChanged += (_, __) =>
        {
            CheckoutCommand.RaiseCanExecuteChanged();
            RaisePropertyChanged(nameof(TotalSum));
        };
    }
    ```

* With the *Polly* library, we can define a retry policy that we use whenever we access a Web API that might fail.

    ```cs
    private Policy RetryPolicy = Policy.Handle<HttpRequestException>().RetryAsync(5);
    ```

* Next, we create an initialization function that loads products from our backend.

    ```cs
    public async Task InitAsync()
    {
        // Here, we use the NuGet package Polly to get failure handling and
        // retry. This is optional. If you want to implement it without retry,
        // that's fine, too. However, in practice you want retry policies in
        // case of shaky networks.
        var productsString = await RetryPolicy.ExecuteAndCaptureAsync(
            async () => await HttpClient.GetStringAsync("/api/products"));
        Products = JsonConvert.DeserializeObject<ObservableCollection<Product>>(productsString.Result);
    }
    ```

* We have to call the initialization function once the view has been loaded. Therefore, we have to add one line to our `MainWindow` constructor:

    ```cs
    public MainWindow()
    {
        ...

        // When the view has been loaded, give the view model
        // a chance to initialize.
        Loaded += async (_, __) => await ViewModel.InitAsync();
    }
    ```

* Next, we add the logic that should be executed when a product has to be put into our shopping basket:

    ```cs
    private void OnAddToBasket(int? productID)
    {
        // Lookup the product based on the ID
        var product = Products.First(p => p.ID == productID);

        // Check whether the product is already in the basket
        var basketItem = Basket.FirstOrDefault(p => p.ProductID == productID);
        if (basketItem != null)
        {
            // Product already in the basket -> add amount and total price
            basketItem.Amount++;
            basketItem.TotalPrice += product.UnitPrice;
        }
        else
        {
            // New product -> add item to basket
            Basket.Add(new ReceiptLineViewModel
            {
                ProductID = product.ID,
                Amount = 1,
                ProductName = product.ProductName,
                TotalPrice = product.UnitPrice
            });
        }
    }
    ```

* Next, we add the logic that should be executed when the user is done with shopping and wants to check out:

    ```cs
    private async Task OnCheckout()
    {
        // Turn all items in the basket into DTO objects
        var dto = Basket.Select(b => new ReceiptLineDto
        {
            ProductID = b.ProductID,
            Amount = b.Amount
        }).ToList();

        // Create JSON content that can be sent using HTTP POST
        using (var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json"))
        {
            // Send the receipt to the backend
            var response = await RetryPolicy.ExecuteAndCaptureAsync(async () => await HttpClient.PostAsync("/api/receipts2", content));

            // Throw exception if something went wrong
            response.Result.EnsureSuccessStatusCode();
        }

        // Clear basket so shopping can start from scratch
        Basket.Clear();
    }
    ```

## Advanced Exercises

If you want to practice, try the following exercises on your own:

* Add meaningful unit tests
* Create a similar UI in *Java* that consumes the same Web API backend. Consider using [OkHttp](https://github.com/square/okhttp/wiki/Recipes) for it.
* Create a similar UI in *JavaScript* that consumes the same Web API backend. Consider using [jQuery](http://api.jquery.com/category/ajax/) or [Angular](https://angular.io/guide/http) for it.

## Sample Solution

You can find the final project solution [in GitHub](https://github.com/rstropek/htl-csharp/tree/master/wpf/9020-register/CashRegister). However, try to work through this hands-on-lab yourself before taking a look at the sample solution.
