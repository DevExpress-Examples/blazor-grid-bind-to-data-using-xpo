using DevExpress.Xpo;
using DevExtreme.AspNet.Data;
using DxGrid.BindToDataUsingXPO.WebAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace DxGrid.BindToDataUsingXPO.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomersController : ControllerBase {
    private readonly ILogger<PersonController> _logger;
    private readonly UnitOfWork uow;

    public CustomersController(ILogger<PersonController> logger, UnitOfWork uow) {
        _logger = logger;
        this.uow = uow;
    }

    [HttpGet]
    public ActionResult Get(DataSourceLoadOptions loadOptions) {
        try {
            var query = uow.Query<Customers>();
            var projection = query.Select(p => new {
                Id = p.ID,
                p.CustomerName,
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
