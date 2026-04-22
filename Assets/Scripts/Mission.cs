using System;

public enum MissionType
{
    ServeCustomers,
    ReachSales
}

[System.Serializable]
public class Mission
{
    public string description;
    public MissionType type;
    public int targetValue;   // target customers or sales
    public int currentValue;  // progress

    private int originalTargetValue;
    private int originalCurrentValue;

    public event Action<Mission> OnMissionComplete;

    public bool IsComplete => currentValue >= targetValue;

    // Constructor to initialize mission and store original values
    public Mission(string description, MissionType type, int targetValue)
    {
        this.description = description;
        this.type = type;
        this.targetValue = targetValue;
        this.currentValue = 0;

        // Store originals
        originalTargetValue = targetValue;
        originalCurrentValue = 0;
    }

    public void AddProgress(int amount)
    {
        currentValue += amount;
        if (IsComplete)
        {
            OnMissionComplete?.Invoke(this);
        }
    }

    // Reset method
    public void Reset()
    {
        //targetValue = originalTargetValue;
        currentValue = originalCurrentValue;
    }
}

