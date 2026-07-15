using DevExpress.Xpo;

namespace DxGrid.BindToDataUsingXPO.WebAPI.Data;

public class Customers(Session session) : XPBaseObject(session) {
    [Key, Persistent("CustomerID")]
    public int ID;

    private string customerName = string.Empty;
    public string CustomerName {
        get => customerName;
        set => SetPropertyValue(nameof(CustomerName), ref customerName, value);
    }
}
