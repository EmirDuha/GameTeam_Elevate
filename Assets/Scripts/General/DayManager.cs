using UnityEngine;

public class DayManager : MonoBehaviour
{

    [SerializeField] private int firstTargetFloor = 10;
    [SerializeField] private int targetFloorIncrement = 5;
    int currentTargetFloor;
    int currentDay = 1;
    public int firstFloor = 1;

    public void StartNewDay()
    {
        currentTargetFloor = firstTargetFloor + (currentDay - 1) * targetFloorIncrement;
        Debug.Log("Starting to day " + currentDay + "with target floor " + currentTargetFloor);
    }

    public void CompleteDay()
    {
        Debug.Log("Day " + currentDay + " completed!");
        currentDay++;
        StartNewDay();
    }

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