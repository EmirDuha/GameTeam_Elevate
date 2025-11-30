using UnityEngine;
using UnityEngine.UI;

public class FloorButton : MonoBehaviour
{
    [SerializeField] private int floorNumber;
    [SerializeField] private ElevatorController elevatorController;

    public void Setup(int floor, ElevatorController controller)
    {
        floorNumber = floor;
        elevatorController = controller;
    }
    public void OnClick()
    {
        elevatorController.GoToFloor(floorNumber);
    }
}
