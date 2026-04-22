using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;

    private GameData gameData;

    private List<IDataPersistence> dataPersistenceObjects;  //list of all objects in the scene that implement IDataPersistence/ meaning inherit from it

    private FileDataHandler dataHandler;

    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Data Persistence Manager in the scene. Destroying the newest one.");
            //Destroy(this.gameObject);
            //return;
        }
        instance = this;
        //DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        // i think you can save different files by changing the fileName in the inspector or by adding strings to initialization
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void NewGame()
    {
        gameData = new GameData();
    }

    public void LoadGame()
    {
        //Load any saved data from a file using the data handler
        gameData = dataHandler.Load();

        //if no data to load, initialize a new game
        if (gameData == null)
        {
            Debug.Log("No data was found. Initializing data to defaults.");
            NewGame();
        }

            //TO-DO: Push the loaded data to other scripts that need it
            foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
            {
                dataPersistenceObj.LoadData(gameData);
            }
        
        //Debug.Log($"Loaded death count = {gameData.deathCount}");
    }

    public void SaveGame()
    {
        //TO-DO: pass the data to other scripts so they update it 
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }
        //Debug.Log($"Saved death count = {gameData.deathCount}");

        //Save the game data to a file using data handler
        dataHandler.Save(gameData);

    }


    //dito pwede iedit kapag gusto mo mag save sa button etc.
    public void OnApplicationQuit()
    {
        SaveGame();
    }

    //private List<IDataPersistence> FindAllDataPersistenceObjects()
    //{
    //    IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
    //    return new List<IDataPersistence>(dataPersistenceObjects);
    //}

    //This method finds and returns all active objects in the scene that implement the IDataPersistence interface.
    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        //- FindObjectsSortMode.None means no sorting is applied to the results.
        var monoBehaviours = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);

        //- Filters the list to include only those MonoBehaviours that implement IDataPersistence.
        //- OfType<T>() is a LINQ method that safely casts and filters
        var dataPersistenceObjects = monoBehaviours.OfType<IDataPersistence>();

        //- Converts the filtered IEnumerable<IDataPersistence> into a concrete List<IDataPersistence> and returns it.
        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
