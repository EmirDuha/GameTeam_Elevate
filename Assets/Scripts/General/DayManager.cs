using UnityEngine;

public class DayManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private StressSystem stressSystem;

    [Header("Day Settings")]
    [SerializeField] private int firstTargetFloor = 10;
    [SerializeField] private int targetFloorIncrement = 5;
    int currentTargetFloor;
    int currentDay = 1;
    public int firstFloor = 1;

    //Starts a new day by setting the target floor and resetting stress
    public void StartNewDay()
    {
        currentTargetFloor = firstTargetFloor + (currentDay - 1) * targetFloorIncrement;
        Debug.Log("Starting to day " + currentDay + " with target floor " + currentTargetFloor);

        stressSystem.ResetStress();
    }

    // Completes the current day and prepares for the next day
    public void CompleteDay()
    {
        Debug.Log("Day " + currentDay + " completed!");
        currentDay++;
        StartNewDay();
    }

    // Returns the current target floor for the day
    public int GetCurrentTargetFloor()
    {
        return currentTargetFloor;
    }

    private void Start()
    {
        // This runs automatically when you press Play
        StartNewDay(); 
    }
}