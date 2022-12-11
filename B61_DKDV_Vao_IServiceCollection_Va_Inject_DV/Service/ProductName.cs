using System.Collections.Generic;

public class ProductName
{
    private List<string> name {get; set;}
    public ProductName()
    {
        name = new List<string> {
            "IPhone 7",
            "Samsung G7",
            "Nokia 123"
        };
    }
    public List<string> GetName()
    {
        return name;
    }
}