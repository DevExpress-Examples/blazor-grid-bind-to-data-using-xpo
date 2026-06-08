using DevExpress.Xpo;

namespace DxGrid.BindToDataUsingXPO.WebAPI.Data;

public class People(Session session) : XPBaseObject(session) {
    [Key, Persistent("PersonID")]
    public int ID;

    private string fullName;
    public string FullName {
        get => fullName;
        set => SetPropertyValue(nameof(FullName), ref fullName, value);
    }
}
