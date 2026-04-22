using System;
using System.Collections.Generic;
using UnityEngine;

public class CostumerManager : MonoBehaviour
{
    public PlayerInteraction playerInteraction;
    public ProductManager productManager;
    public HealthManager healthManager;
    public MissionManager missionManager;
    public GameManager gameManager;

    public Transform spawnPoint;
    public GameObject costumerPrefab;   //turn this into a pool later

    public Queue<List<string>> orderQueue = new Queue<List<string>>();
    public CostumerAI currentCostumer;

    public float shopBalance = 0f;

    private void Start()
    {
        CheckCurrentDay();
        // Example queue

        playerInteraction.OnServeProduct += HandleServeProduct;
        playerInteraction.OnExitInteraction += HandleClearWhenExitedUI;
        gameManager.OnGameStarted += CheckIfGameStarted;
        gameManager.OnNextLevel += SpawnNextLevelCostumers;
        MissionManager.Instance.OnDayMissionComplete += OnCostumerClear;
    }

    private void OnCostumerClear()
    {
        orderQueue.Clear();
    }

    private void SpawnNextLevelCostumers()
    {
        Debug.Log("Spawning next level costumers.");
        SpawnNextCostumer();
    }

    private void CheckIfGameStarted()
    {
        SpawnNextCostumer();
    }

    public void CheckCurrentDay()
    {
        orderQueue.Clear();

        switch (MissionManager.Instance.currentDay)
        {
            case 0:
                orderQueue.Enqueue(new List<string> { "Water" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Chamomile" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Coffee" });
                orderQueue.Enqueue(new List<string> { "Matcha" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                break;
            case 1:
                orderQueue.Enqueue(new List<string> { "Matcha" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "Water" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Chamomile" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Coffee" });
                orderQueue.Enqueue(new List<string> { "Matcha" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                break;
            case 2:
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "Water" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Chamomile" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Coffee" });
                orderQueue.Enqueue(new List<string> { "Matcha" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "Water" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Chamomile" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Coffee" });
                orderQueue.Enqueue(new List<string> { "Matcha" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                break;
            case 3:
                orderQueue.Enqueue(new List<string> { "Water" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "Water" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Chamomile" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Coffee" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Chamomile" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Coffee" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" }); ;
                orderQueue.Enqueue(new List<string> { "Chamomile" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Coffee" });
                orderQueue.Enqueue(new List<string> { "Matcha" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Chamomile" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Coffee" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "Water" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Chamomile" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Coffee" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Chamomile" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Coffee" });
                
                
                break;
            case 4:
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Chamomile" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Coffee" });
                orderQueue.Enqueue(new List<string> { "Matcha" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "Water" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "Water" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Chamomile" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Coffee" });
                orderQueue.Enqueue(new List<string> { "Matcha" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "Water" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Chamomile" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Coffee" });
                orderQueue.Enqueue(new List<string> { "Matcha" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                break;
            case 5:
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "Water" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Chamomile" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Coffee" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "Water" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Chamomile" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Coffee" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Chamomile" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Coffee" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "Water" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Chamomile" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Coffee" });
                orderQueue.Enqueue(new List<string> { "Matcha" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                break;
            case 6:
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Coffee" });
                orderQueue.Enqueue(new List<string> { "Matcha" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "Water" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Chamomile" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Coffee" });
                orderQueue.Enqueue(new List<string> { "Matcha" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Chamomile" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Coffee" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "Water" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Chamomile" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Coffee" });
                orderQueue.Enqueue(new List<string> { "Matcha" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                break;
            case 7:
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "Water" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "Water" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Water" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Chamomile" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Coffee" });
                orderQueue.Enqueue(new List<string> { "Matcha" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "Chamomile" });
                orderQueue.Enqueue(new List<string> { "Chamomile" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Coffee" });
                orderQueue.Enqueue(new List<string> { "Matcha" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "Chamomile" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Coffee" });
                orderQueue.Enqueue(new List<string> { "Matcha" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Chamomile" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Coffee" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "Water" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Chamomile" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Coffee" });
                break;
            case 8:
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Coffee" });
                orderQueue.Enqueue(new List<string> { "Matcha" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "Chamomile" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Coffee" });
                orderQueue.Enqueue(new List<string> { "Matcha" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Chamomile" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Coffee" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "Water" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Chamomile" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Coffee" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Chamomile" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Coffee" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "Water" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Chamomile" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Coffee" });
                break;
            case 9:
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "Chamomile" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Coffee" });
                orderQueue.Enqueue(new List<string> { "Matcha" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Chamomile" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Coffee" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "Water" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Chamomile" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Coffee" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Chamomile" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Coffee" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "Water" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Chamomile" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Coffee" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Chamomile" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Coffee" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Pan de Regla" });
                orderQueue.Enqueue(new List<string> { "Water" });
                orderQueue.Enqueue(new List<string> { "Pandesal" });
                orderQueue.Enqueue(new List<string> { "Chamomile" });
                orderQueue.Enqueue(new List<string> { "StrawberryCake" });
                orderQueue.Enqueue(new List<string> { "Coffee" });
                break;
            default:
                Debug.Log("No orders defined for this day.");
                // Default settings
                break;
        }
    }

    private void HandleClearWhenExitedUI()
    {
        productManager.ClearProducts();
    }

    private void HandleServeProduct()
    {
        List<string> products = productManager.GetProductsToServe();

        if (products == null || products.Count == 0 || currentCostumer == null)
            return;

        if (currentCostumer.gameObject == null) // destroyed object
            return;



        int servedCount = currentCostumer.ServeProducts(products);

        if (servedCount > 0)
        {
            Debug.Log("Served " + servedCount + " products.");
            productManager.ClearProducts();
            float orderTotal = 0;

            foreach(var product in products)
            {
                orderTotal += productManager.GetProductPrice(product);
            }
            shopBalance += orderTotal;
            Debug.Log("Customer paid: " + orderTotal + " | Shop Balance: " + shopBalance);
            // Update mission progress
            MissionManager.Instance.RegisterCustomerServed(orderTotal);

            if (currentCostumer.IsOrderComplete())
            {
                Debug.Log("Customer finished order and leaves.");
                Destroy(currentCostumer.gameObject);
                //currentCostumer = null; // clear reference
                SpawnNextCostumer();
            }
            if (MissionManager.Instance.missionComplete)
            {
                Destroy(currentCostumer.gameObject);
                MissionManager.Instance.missionComplete = false;
            }

        }
        else
        {
            // Minus Life
            healthManager.UpdateLife();

            productManager.ClearProducts();
            Debug.Log("No correct products served.");
        }

        
    }
    public void SpawnNextCostumer()
    {
        
        if (orderQueue.Count > 0)
        {
            
            GameObject newCostumerObj = Instantiate(costumerPrefab, spawnPoint.position, Quaternion.identity);
            currentCostumer = newCostumerObj.GetComponent<CostumerAI>();
            currentCostumer.InitializeOrders(orderQueue.Dequeue(), productManager);

            //costumer ++
            currentCostumer.OnCostumerLeft += SpawnNextCostumer;


            Debug.Log("New customer spawned with orders: " + string.Join(", ", currentCostumer.GetOrders()));
        }
        else
        {
            Debug.Log("No more customers!");
        }
    }
}
