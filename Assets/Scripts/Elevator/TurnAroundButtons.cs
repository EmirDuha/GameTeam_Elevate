using UnityEngine;

public class TurnAroundButtons : MonoBehaviour
{

    [SerializeField] private GameObject frontViewPanel;
    [SerializeField] private GameObject backViewPanel;

    public void ShowFrontView()
    {
        frontViewPanel.SetActive(true);
        backViewPanel.SetActive(false);
    }

    public void ShowBackView()
    {
        frontViewPanel.SetActive(false);
        backViewPanel.SetActive(true);
    }
}
