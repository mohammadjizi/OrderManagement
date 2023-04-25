namespace Order;

public class Order
{
    public string Id { get; set; }

    public string Name { get; set; }

    public Order (string id, string name)
    {
        this.Id=id;
        this.Name=name;
    }

}
