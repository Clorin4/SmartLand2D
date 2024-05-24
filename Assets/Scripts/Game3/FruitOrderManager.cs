using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Order
{
    public List<string> fruitNames;
    public List<Sprite> fruits;
    public List<int> quantities;
    public List<string> operations;
}

public class FruitOrderManager : MonoBehaviour
{
    [SerializeField]
    public List<Order> easyOrders;
    [SerializeField]
    public List<Order> normalOrders;
    [SerializeField]
    private List<Order> hardOrders;
    [SerializeField]
    private List<Order> harderOrders;
    [SerializeField]
    private List<Order> insaneOrders;
    [SerializeField]
    private List<Order> demonOrders;

    public List<Order> GetCurrentOrders(string difficulty)
    {
        switch (difficulty)
        {
            case "dif1":
                return easyOrders;
            case "dif2":
                return normalOrders;
            case "dif3":
                return hardOrders;
            case "dif4":
                return harderOrders;
            case "dif5":
                return insaneOrders;
            case "dif6":
                return demonOrders;
            default:
                Debug.LogError("Unknown difficulty level: " + difficulty);
                return null;
        }
    }

}
