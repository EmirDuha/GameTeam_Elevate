using UnityEngine;
using UnityEngine.UI;

public class FloorButton : MonoBehaviour
{
    [SerializeField] private int floorNumber;
    [SerializeField] private ElevatorController elevatorController;
    [SerializeField] private DayManager dayManager;
    
    public void Setup(int floor, ElevatorController controller)
    {
        floorNumber = floor;
        elevatorController = controller;
    }
    public void OnClick()
    {
        if (floorNumber <= dayManager.GetCurrentTargetFloor())
        elevatorController.GoToFloor(floorNumber);
        else
        {
            Debug.Log("Cannot go to floor " + floorNumber + " yet.");
        }
    }
}
