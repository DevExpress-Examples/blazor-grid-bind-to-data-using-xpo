namespace DxGrid.BindToDataUsingXPO.Models;

public class OrderDto {
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime ExpectedDeliveryDate { get; set; }
    public bool IsUndersupplyBackordered { get; set; }
    public string? CustomerPurchaseOrderNumber { get; set; }
    public DateTime? PickingCompletedWhen { get; set; }
    public int? CustomerID { get; set; }
    public int? SalesPersonID { get; set; }
    public int? ContactPersonID { get; set; }
}
