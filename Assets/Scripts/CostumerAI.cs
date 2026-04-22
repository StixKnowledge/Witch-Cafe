using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using System;
using Random = System.Random;
using UnityEngine.UI;

public class CostumerAI : MonoBehaviour
{
    [SerializeField] HealthManager healthManager;
    //[SerializeField] PlayerInteraction playerInteraction;
    public Transform targetPoint;
    public GameObject costumerUI;

    [Header("Sprites")]
    public SpriteRenderer currentCostumerWalkingSprite;
    public Sprite[] costumerWalkingSprites;
    [Space(3)]
    public Image currentCostumerBubbleImage;
    public Sprite[] costumerBubbleSprites;

    public TextMeshProUGUI orderText;
    public TextMeshProUGUI waitTime;

    private NavMeshAgent agent;
    private bool isCostumerAtTarget = false;
    private List<string> orders = new List<string>();

    // Wait time settings
    public float maxWaitTime = 0f; // seconds
    public float waitTimer = 0f;
    private bool isWaiting = false;

    Random rand = new Random();
    public event Action OnCostumerLeft;

    public bool clickStartOrder = false;

    string[] croissantOrders = { "Bonjooour… je voudrais le petit pain… là… oui, celui qui fait crooou-crooou.",
                                "Euh… s’il vous plaît… le beurré… le très beurré… mmm, oui.",
                                "Un de ces trucs… comment dire… flaky magnifique, s’il vous plaît."};
    string[] pandesalOrders = { "HEHE—give me the fluffy round bread! The happy bread!",
                                "I want the soft one! The one you can squish! BREAD TIME!",
                                "Gimme the little golden bun! The fun one! YAAAY!"};
    string[] strawberrycakeOrders = { "I need something sweet. Like, dangerously sweet.",
                                    "The dessert with the berries! I need happiness in cake form.",
                                    "Got anything cute, fluffy, and sugary? I’ll take that."};
    string[] pandereglaOrders = { "Ugh… give me the bread that looks like comfort. Please.",
                                    "I want the soft one… the reddish one. I need it today.",
                                    "Cramps are killing me. Just give me that warm, comforting bread."};
    string[] waterOrders = { "I just need water. Plain water. Before I crumble into dust.",
                                    "Kh— cough cough— water, please! I swallowed something wrong!",
                                    "Can I have the clear drink that keeps people alive? Yeah. That one."};
    string[] coffeeOrders = { "I need your strongest wake-up-in-a-cup. Immediately.",
                                    "The one with caffeine. LOTS of caffeine. Before I pass out.",
                                    "Give me the magic bean drink… I need to function."};
    string[] chamomileOrders = { "Please… I need the calming one. The one that tastes like a nap.",
                                    "Something soothing, please. My brain is melting.",
                                    "Do you have anything gentle? My nerves and my stomach both hate me."};
    string[] matchaOrders = { "Give me that elegant green drink—something that makes me look mysterious when I sip it.",
                                    "I need the fancy one—yes, the one people post on social media.",
                                    "I want the drink that screams ‘I’m classy and I know it."};

    public void InitializeOrders(List<string> newOrders, ProductManager productManager)
    {
        //We can use tag to track who is the costumer later if needed

        orders = new List<string>(newOrders);
        //if (orderText != null)
        //    //orderText.text = string.Join(", ", orders);

        if (orderText != null)
        {
            foreach(var order in orders)
            {
                int randomNumber = rand.Next(0, 3); // generates 0, 1, or 2

                if (order == "Croissant")
                {
                    orderText.text = croissantOrders[randomNumber];
                }
                else if (order == "Pandesal")
                {
                    orderText.text = pandesalOrders[randomNumber];
                }
                else if (order == "Pan de Regla")
                {
                    orderText.text = pandereglaOrders[randomNumber];
                }
                else if (order == "StrawberryCake")
                {
                    orderText.text = strawberrycakeOrders[randomNumber];
                }
                else if( order == "Water")
                {
                    orderText.text = waterOrders[randomNumber];
                }
                else if( order == "Coffee")
                {
                    orderText.text = coffeeOrders[randomNumber];
                }
                else if( order == "Chamomile")
                {
                    orderText.text = chamomileOrders[randomNumber];
                }
                else if( order == "Matcha")
                {
                    orderText.text = matchaOrders[randomNumber];
                }
                else
                {
                    Debug.Log("Unknown order: " + order);
                }
            }
               
        }

        //  Calculate total wait time based on products
        maxWaitTime = 0f;
        foreach (var product in orders)
        {
            maxWaitTime += productManager.GetProductWaitTime(product);
        }

        waitTimer = maxWaitTime;
        isWaiting = false; // will start when they reach the counter
    }

    private void Start()
    {
        healthManager = GameObject.Find("HealthManager").GetComponent<HealthManager>();
        //playerInteraction = GameObject.Find("Player").GetComponent<PlayerInteraction>();
        RandomizeCharacter();

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.SetDestination(targetPoint.position);
    }
    void RandomizeCharacter()
    {
        if (costumerWalkingSprites == null || costumerWalkingSprites.Length == 0)
        {
            Debug.LogError("No walking sprites assigned!");
            return;
        }
        if (costumerBubbleSprites == null || costumerBubbleSprites.Length == 0)
        {
            Debug.LogError("No bubble sprites assigned!");
            return;
        }

        int randomNumber = UnityEngine.Random.Range(0, costumerWalkingSprites.Length);
        int randomCharacter = randomNumber;
        if (currentCostumerWalkingSprite != null)
            currentCostumerWalkingSprite.sprite = costumerWalkingSprites[randomCharacter];

        if (currentCostumerBubbleImage != null && randomCharacter < costumerBubbleSprites.Length)
            currentCostumerBubbleImage.sprite = costumerBubbleSprites[randomCharacter];
    }
    public void OnStartOrderClicked()
    {
        clickStartOrder = true;
    }

    private void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < .1f && !isCostumerAtTarget)
        {
            Debug.Log("Costumer reached the target point.");
            isCostumerAtTarget = true;

            costumerUI.SetActive(true);
            // Start waiting
            isWaiting = true;
        }

        if (isWaiting && clickStartOrder)
        {
            waitTimer -= Time.deltaTime;

            UpdateTimer(waitTimer);
            //Debug.Log("Customer waiting time left: " + waitTimer.ToString("F2") + " seconds.");
            if (waitTimer <= 0f)
            {
                //Minus Life
                healthManager.UpdateLife();
                Debug.Log("Customer got tired of waiting and left!");
                LeaveShop();
                clickStartOrder = false;
                isWaiting = false;
            }
            
        }

    }

    public int ServeProducts(List<string> products)
    {
        if (!isWaiting) return 0; // can't serve if they already left
        if(!clickStartOrder) return 0; // can't serve if they haven't started order yet

        if (products.Count > orders.Count)
        {
            Debug.Log("Wrong number of products served.");
            return 0;
        }
        else
        {
            foreach (var product in products)
            {
                if (!orders.Contains(product))
                {
                    //Minus life
                    //HealthManager.Instance.UpdateLife();
                    Debug.Log("Correct Number but wrong product served: " + product);
                    return 0;
                }
            }
            int servedCount = 0;

            foreach (var product in products)
            {
                if (orders.Contains(product))
                {
                    orders.Remove(product);
                    servedCount++;
                }
                else
                {
                    Debug.Log("Wrong product served: " + product);
                }
            }

            if (orderText != null)
                orderText.text = string.Join(", ", orders);

            if (IsOrderComplete())
            {
                Debug.Log("Customer order complete!");
                LeaveShop();
            }

            return servedCount;
        }
            
    }

    public bool IsOrderComplete()
    {
        return orders.Count == 0;
    }

    private void LeaveShop()
    {
        costumerUI.SetActive(false);
        Destroy(gameObject);

        //susubscrbie dito ang healthSystem
        OnCostumerLeft?.Invoke();

    }

    public List<string> GetOrders()
    {
        return new List<string>(orders);
    }

    public float CalculateOrderTotal(ProductManager productManager)
    {
        float total = 0f;
        foreach (var product in orders)
        {
            total += productManager.GetProductPrice(product);
        }
        return total;
    }

    public void UpdateTimer(float timeLeft)
    {
        waitTime.text = "Patience Time: " + timeLeft.ToString("F1") + "s";
    }
}
