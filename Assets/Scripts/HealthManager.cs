using System;
using UnityEngine;

public class HealthManager : MonoBehaviour
{

    public event Action OnHealthZero;

    public GameObject[] lives;   // Array of life icons or objects
    private int currentLifeIndex; // Tracks which life to remove next

    void Start()
    {
        // Start with all lives active
        currentLifeIndex = lives.Length - 1;
    }

    public void UpdateLife()
    {
        if (currentLifeIndex >= 0)
        {
            // Disable the current life object
            lives[currentLifeIndex].SetActive(false);

            // Move to the next life
            currentLifeIndex--;
        }

        // If all lives are gone  Game Over
        if (currentLifeIndex < 0)
        {
            OnHealthZero?.Invoke();
        }
    }

    public void ResetLives()
    {
        // Reactivate all life objects
        foreach (var life in lives)
        {
            life.SetActive(true);
        }
        // Reset the life index
        currentLifeIndex = lives.Length - 1;
    }
}