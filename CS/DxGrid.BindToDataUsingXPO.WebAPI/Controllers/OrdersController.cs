using DevExpress.Xpo;
using DevExtreme.AspNet.Data;
using DxGrid.BindToDataUsingXPO.WebAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace DxGrid.BindToDataUsingXPO.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase {
    private readonly ILogger<PersonController> _logger;
    private readonly UnitOfWork uow;

    public OrdersController(ILogger<PersonController> logger, UnitOfWork uow) {
        _logger = logger;
        this.uow = uow;
    }

    [HttpGet]
    public ActionResult Get(DataSourceLoadOptions loadOptions) {
        try {
            var query = uow.Query<Orders>();
            var projection = query.Select(p => new {
                Id = p.ID,
                p.OrderDate,
                p.ExpectedDeliveryDate,
                p.IsUndersupplyBackordered,
                p.CustomerPurchaseOrderNumber,
                p.PickingCompletedWhen,
                p.CustomerID,
                p.SalesPersonID,
                p.ContactPersonID,
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
}
