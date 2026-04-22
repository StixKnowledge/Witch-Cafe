using System.Collections.Generic;

public class Costumer
{
    public List<string> orders = new List<string>();

    public Costumer(List<string> initialOrders)
    {
        orders = new List<string>(initialOrders);
    }

    public bool ServeProduct(string product)
    {
        if (orders.Contains(product))
        {
            orders.Remove(product);
            return true; // Correct product served
        }
        return false; // Wrong product
    }

    public bool IsOrderComplete()
    {
        return orders.Count == 0;
    }
}