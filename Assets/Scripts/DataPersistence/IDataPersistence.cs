using UnityEngine;

public interface IDataPersistence
{

    //example from DeathCountText.cs --> IDataPersistence --> GameData ..............
    //this method is called by the DataPersistenceManager to load the death count from the GameData object
    //public void LoadData(GameData data)
    //{
    //    this.deathCount = data.deathCount;
    //}
    void LoadData(GameData data);
    void SaveData(ref GameData data);   //- The ref keyword means you're passing the actual reference itself, not a copy.
}
