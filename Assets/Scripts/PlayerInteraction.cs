using UnityEngine;
using TMPro;
using System;
using System.Collections;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] CostumerAI costumerAI;
    public GameObject brebObject;
    public GameObject drinksObject;
    public GameObject bookObject;
    public GameObject closeObjectNotify;
    public GameObject isInstructionUIOpen;
    public GameObject clickStartNotify;

    public TextMeshProUGUI productNamesText;
    
    public event Action OnServeProduct;
    public event Action OnExitInteraction;
    public event Action OnBrebOpen;
    public event Action OnDrinksOpen;

    public ProductManager productManager;

    bool isInstructionOpen = false;

    //public bool onStartOrderClicked = false;


    private void Update()
    {
        isInstructionOpen = isInstructionUIOpen.activeSelf;

        if(!isInstructionOpen)
        {
            Debug.Log("CostumerAI assigned");
            costumerAI = GameObject.Find("Costumer(Clone)").GetComponent<CostumerAI>();
            isInstructionOpen = false;
        }
    }
    IEnumerator OnCloseObjectNotif()
    {
        closeObjectNotify.SetActive(true);
        yield return new WaitForSeconds(2f);
        closeObjectNotify.SetActive(false);
    }
    IEnumerator OnClickStartNotif()
    {
        clickStartNotify.SetActive(true);
        yield return new WaitForSeconds(2f);
        clickStartNotify.SetActive(false);
    }
    public void OnBrebClicked()
    {
        if(brebObject.activeSelf)
        {
            OnExitInteraction?.Invoke();
            brebObject.SetActive(false);
            productNamesText.text = "";
        }
        else if (drinksObject.activeSelf)
        {
            StartCoroutine(OnCloseObjectNotif());
            Debug.Log("Close drinks first");
        }
        else if (!costumerAI.clickStartOrder)
        {
            StartCoroutine(OnClickStartNotif());
            Debug.Log("PRESS START BUTTON FIRST");
        }
        else
        {
            Debug.Log("Breb clicked");
            brebObject.SetActive(true);
            OnBrebOpen?.Invoke();
        }
    }
    public void OnDrinksClicked()
    {
        if (drinksObject.activeSelf)
        {
            OnExitInteraction?.Invoke();
            drinksObject.SetActive(false);
            productNamesText.text = "";
        }
        else if(brebObject.activeSelf)
        {
            StartCoroutine(OnCloseObjectNotif());
            Debug.Log("Close breb first");
        }
        else if(!costumerAI.clickStartOrder)
        {
            StartCoroutine(OnClickStartNotif());
            Debug.Log("PRESS START BUTTON FIRST");
        }
        else
        {
            Debug.Log("Breb clicked");
            drinksObject.SetActive(true);
            OnDrinksOpen?.Invoke();
        }
    }

    public void OnBookClicKed()
    {
        if(bookObject.activeSelf)
        {
            OnExitInteraction?.Invoke();
            bookObject.SetActive(false);
            productNamesText.text = "";
        }
        else
        {
            Debug.Log("Book clicked");
            bookObject.SetActive(true);
        }
    }


    public void OnExitClicked(GameObject objectToClose)
    {
        OnExitInteraction?.Invoke();
        Debug.Log("Exit clicked");
        objectToClose.SetActive(false);
        productNamesText.text = "";
    }

    public void OnServeClicked()
    {
        OnServeProduct?.Invoke();
    }

    public void OnClearServing()
    {
        productManager.ClearProducts();
    }
}
