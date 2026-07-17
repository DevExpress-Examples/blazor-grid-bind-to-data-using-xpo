<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/1259051272/26.1.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T1332233)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->
# Blazor Grid - Bind the Component to Data Using XPO

This example uses the [DevExpress eXpress Persistent Objects (XPO) ORM](https://docs.devexpress.com/XPO/1998/express-persistent-objects) to bind the DevExpress Blazor Grid (`DxGrid`)  to a database: a Web API backend retrieves XPO persistent objects and passes data to a Blazor application.

## Implementation Details

### Configure the Server to Access Data Using XPO


1. In the [Program.cs](CS/DxGrid.BindToDataUsingXPO.WebAPI/Program.cs) file, register the XPO data layer and unit of work.
1. Define persistent object types used in the app and a connection string:

    ```cs
    builder.Services.AddXpoDefaultDataLayer(ServiceLifetime.Singleton, dl => dl
        .UseConnectionString(builder.Configuration.GetConnectionString("WideWorldImportersExample"))
        .UseThreadSafeDataLayer(true)
        .UseConnectionPool(false)
        .UseAutoCreationOption(DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema)
        .UseEntityTypes(typeof(People), typeof(Orders), typeof(Customers))
    );
    builder.Services.AddXpoDefaultUnitOfWork();
    ```

1. In the [BindToDataUsingXPO.WebAPI\Data](CS/DxGrid.BindToDataUsingXPO.WebAPI/Data) folder, inherit persistent objects (such as [Orders](CS/DxGrid.BindToDataUsingXPO.WebAPI/Data/Orders.cs)) from the `XPBaseObject` to expose properties that you wish to map to database columns.

    ```cs
    public class Orders(Session session) : XPBaseObject(session) {
        [Key, Persistent("OrderID")]
        public int ID;

        private DateTime orderDate;
        public DateTime OrderDate {
            get => orderDate;
            set => SetPropertyValue(nameof(OrderDate), ref orderDate, value);
        }
        // ...
    }
    ```

### Retrieve Data Using a Web API Controller

Create an API controller that queries XPO's `UnitOfWork`, shapes the result, and uses the `DevExtreme.AspNet.Data` library [DataSourceLoader](https://devexpress.github.io/DevExtreme.AspNet.Data/net/api/DevExtreme.AspNet.Data.DataSourceLoader.html) to apply paging, sorting, and filtering based on the grid [load options](CS/DxGrid.BindToDataUsingXPO.WebAPI/Controllers):

```cs
[HttpGet]
public ActionResult Get(DataSourceLoadOptions loadOptions) {
    try {
        var query = uow.Query<Orders>();
        var projection = query.Select(p => new {
            Id = p.ID,
            p.OrderDate,
            p.ExpectedDeliveryDate,
            // ...
        });

        loadOptions = loadOptions ?? new DataSourceLoadOptions();
        loadOptions.PrimaryKey = new[] { "Id" };
        loadOptions.PaginateViaPrimaryKey = true;
        var loadResult = DataSourceLoader.Load(projection, loadOptions);

        return Ok(loadResult);
    }
    catch(Exception ex) {
        return BadRequest(ex);
    }
}
```

### Bind a Blazor Grid to Data

1. In your Blazor client application ([DxGrid.BindToDataUsingXPO](CS/DxGrid.BindToDataUsingXPO/DxGrid.BindToDataUsingXPO.csproj)), add a `DxGrid` component with multiple [DxGridDataColumn](https://docs.devexpress.com/Blazor/DevExpress.Blazor.DxGridDataColumn) objects:

    ```razor
    <DxGrid>
        <Columns>
            <DxGridDataColumn FieldName="@nameof(OrderDto.Id)" Width="80" />
            <DxGridDataColumn FieldName="@nameof(OrderDto.OrderDate)" />
            <DxGridDataColumn FieldName="@nameof(OrderDto.ExpectedDeliveryDate)" />
            <DxGridDataColumn FieldName="@nameof(OrderDto.PickingCompletedWhen)" />
            <DxGridDataColumn FieldName="@nameof(OrderDto.CustomerPurchaseOrderNumber)" />
            <DxGridDataColumn FieldName="@nameof(OrderDto.CustomerID)" Width="120" />
        </Columns>
    </DxGrid>
    ```

1. Create a [GridDevExtremeDataSource\<T\>](https://docs.devexpress.com/Blazor/DevExpress.Blazor.GridDevExtremeDataSource-1) instance and pass the `HttpClient` parameter and controller endpoint URI. Assign the instance to the Grid `Data` property:

    ```razor
    <DxGrid Data="GridDataSource">
        <Columns>
        @* ... *@
        </Columns>
    </DxGrid>

    @code {
        GridDevExtremeDataSource<OrderDto>? GridDataSource { get; set; } = null;

        protected override void OnInitialized() {
            var httpClient = ClientFactory.CreateClient("XpoApi");
            var uri = new Uri(httpClient.BaseAddress!, "Orders");
            GridDataSource = new(httpClient, uri);
        }
    }
    ```

The grid sends requests to a Web API that delegates data shaping to the XPO query provider. The Grid loads data necessary for screen display (instead of the entire dataset).


## Files to Review

* [DxGrid.BindToDataUsingXPO.WebAPI - Program.cs](CS/DxGrid.BindToDataUsingXPO.WebAPI/Program.cs)
* [DxGrid.BindToDataUsingXPO.WebAPI - Orders.cs](CS/DxGrid.BindToDataUsingXPO.WebAPI/Data/Orders.cs)
* [DxGrid.BindToDataUsingXPO - Program.cs](CS/DxGrid.BindToDataUsingXPO/Program.cs)
* [DxGrid.BindToDataUsingXPO - Index.razor](CS/DxGrid.BindToDataUsingXPO/Components/Pages/Index.razor)

## Documentation

* [XPO](https://docs.devexpress.com/XPO/2004/express-persistent-objects)
* [GridDevExtremeDataSource\<T\>](https://docs.devexpress.com/Blazor/DevExpress.Blazor.GridDevExtremeDataSource-1)
* [DevExtreme.AspNet.Data.DataSourceLoader](https://devexpress.github.io/DevExtreme.AspNet.Data/net/api/DevExtreme.AspNet.Data.DataSourceLoader.html)

<!-- feedback -->
## Does This Example Address Your Development Requirements/Objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=blazor-grid-bind-to-data-using-xpo&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=blazor-grid-bind-to-data-using-xpo&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
