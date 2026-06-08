using DevExpress.Xpo;

namespace DxGrid.BindToDataUsingXPO.WebAPI.Data;

public class Orders(Session session) : XPBaseObject(session) {
    [Key, Persistent("OrderID")]
    public int ID;

    private DateTime orderDate;
    public DateTime OrderDate {
        get => orderDate;
        set => SetPropertyValue(nameof(OrderDate), ref orderDate, value);
    }

    private DateTime expectedDeliveryDate;
    public DateTime ExpectedDeliveryDate {
        get => expectedDeliveryDate;
        set => SetPropertyValue(nameof(ExpectedDeliveryDate), ref expectedDeliveryDate, value);
    }

    private bool isUndersupplyBackordered;
    public bool IsUndersupplyBackordered {
        get => isUndersupplyBackordered;
        set => SetPropertyValue(nameof(IsUndersupplyBackordered), ref isUndersupplyBackordered, value);
    }

    private string? customerPurchaseOrderNumber;
    public string? CustomerPurchaseOrderNumber {
        get => customerPurchaseOrderNumber;
        set => SetPropertyValue(nameof(CustomerPurchaseOrderNumber), ref customerPurchaseOrderNumber, value);
    }

    private DateTime? pickingCompletedWhen;
    public DateTime? PickingCompletedWhen {
        get => pickingCompletedWhen;
        set => SetPropertyValue(nameof(PickingCompletedWhen), ref pickingCompletedWhen, value);
    }

    private int? customerID;
    public int? CustomerID {
        get => customerID;
        set => SetPropertyValue(nameof(CustomerID), ref customerID, value);
    }

    private int? salesPersonID;
    public int? SalesPersonID {
        get => salesPersonID;
        set => SetPropertyValue(nameof(SalesPersonID), ref salesPersonID, value);
    }

    private int? contactPersonID;
    public int? ContactPersonID {
        get => contactPersonID;
        set => SetPropertyValue(nameof(ContactPersonID), ref contactPersonID, value);
    }
}