using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductManager : MonoBehaviour
{
    public PlayerInteraction playerInteraction;
    private List<string> productsToServe = new List<string>();
    //public TextMeshProUGUI productNamesText;

    public GameObject productToServeParent;
    public GameObject[] productToServeImages;
    public Transform[] productImageFoodPositions;
    public Transform[] productImageDrinkPositions;

    int productClickedCount = 0;

    #region PRODUCT PRICES
    // Price list
    private Dictionary<string, float> productPrices = new Dictionary<string, float>()
    {
        { "Croissant", 20F },
        { "Pan de Regla", 10f },
        { "Pandesal", 5f },
        { "StrawberryCake", 25f },
        { "Water", 5f },
        { "Chamomile", 10f },
        { "Matcha", 25f },
        { "Coffee", 15f }
    };
    // Get price of a product
    public float GetProductPrice(string product)
    {
        if (productPrices.ContainsKey(product))
            return productPrices[product];
        return 0f;
    }
    #endregion

    #region PRODUCT WAIT TIMES
    private Dictionary<string, float> productWaitTimes = new Dictionary<string, float>()
    {
        { "Croissant", 10f },
        { "Pan de Regla",10f },
        { "Pandesal", 10f },
        { "StrawberryCake", 10f },
        { "Water", 10f },
        { "Chamomile", 10f },
        { "Matcha", 10f },
        { "Coffee", 10f }
    };

    public float GetProductWaitTime(string product)
    {
        if (productWaitTimes.ContainsKey(product))
            return productWaitTimes[product];
        return 5f; // default fallback
    }
    #endregion

    // Define this once (e.g., at class level)
    private Dictionary<string, int> ProductMap = new Dictionary<string, int>
    {
        { "Croissant", 0 },
        { "Pan de Regla", 1 },
        { "Pandesal", 2 },
        { "StrawberryCake", 3 },
        { "Water", 4 },
        { "Chamomile", 5 },
        { "Matcha", 6 },
        { "Coffee", 7 }
    };

    //private Dictionary<string, int> drinksProductMap = new Dictionary<string, int>
    //{
    //    
    //};

    //void CheckIsBrebOpen()
    //{
    //    ProductMap.Clear();
    //    ProductMap.Add("Croissant", 0);
    //    ProductMap.Add("Pan de Regla", 1);
    //    ProductMap.Add("Pandesal", 2);
    //    ProductMap.Add("StrawberryCake", 3);

    //}

    //void CheckIfDrinksOpen()
    //{
    //    ProductMap.Clear();
    //    ProductMap.Add("Water", 0);
    //    ProductMap.Add("Chamomile", 1);
    //    ProductMap.Add("Matcha", 2);
    //    ProductMap.Add("Coffee", 3);
    //}
    public void OnProductClicked(TextMeshProUGUI productName)
    {
        //Transform[] productImagePositions;
        //if(playerInteraction.brebObject.activeSelf)
        //{
        //    productImagePositions = productImagePosition;
        //}
        //else if(playerInteraction.drinksObject.activeSelf)
        //{
        //    productImagePositions = productImageDrinkPositions;
        //}
        //else
        //{
        //    Debug.LogWarning("No product category is open.");
        //    return;
        //}
        string clickedProduct = productName.text;

        // Limit check
        if (productClickedCount >= productImageFoodPositions.Length)
        {
            Debug.Log("Maximum of " + productImageFoodPositions.Length + " products can be added to the order.");
            return;
        }

        // Validate product
        if (!ProductMap.ContainsKey(clickedProduct))
        {
            Debug.LogWarning("Unknown product: " + clickedProduct);
            return;
        }


        // Add to list
        productsToServe.Add(clickedProduct);

        // Get prefab index
        int productIndex = ProductMap[clickedProduct];
        GameObject product;

        // Choose the correct parent array based on productIndex
        Transform[] targetPositions = productIndex > 3
            ? productImageDrinkPositions
            : productImageFoodPositions;

        // Instantiate at the current slot
        product = Instantiate(productToServeImages[productIndex], targetPositions[productClickedCount]);

        Debug.Log(clickedProduct + " added to the order.");

        // Increment AFTER using the slot
        productClickedCount++;

        // Parent to slot and reset local position
        product.transform.SetParent(targetPositions[productClickedCount - 1], false);
        product.transform.localPosition = Vector3.zero;

    }

    


    public List<string> GetProductsToServe()
    {
        return new List<string>(productsToServe);
    }

    public void ClearProducts()
    {
        productClickedCount = 0;
        productsToServe.Clear();

        //pwede ko iclear both

        for(int i = 0; i < productImageFoodPositions.Length; i++)
        {
            if (productImageFoodPositions[i].childCount > 0)
            {
                foreach (Transform child in productImageFoodPositions[i])
                {
                    Destroy(child.gameObject);
                }
            }
        }
        for(int i = 0; i < productImageDrinkPositions.Length; i++)
        {
            if (productImageDrinkPositions[i].childCount > 0)
            {
                foreach (Transform child in productImageDrinkPositions[i])
                {
                    Destroy(child.gameObject);
                }
            }
        }
    }

}

