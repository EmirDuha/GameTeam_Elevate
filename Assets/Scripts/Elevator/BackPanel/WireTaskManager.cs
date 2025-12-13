using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class WireTaskManager : MonoBehaviour
{
    [Header("References")]
    public BreakdownManager breakdownManager; 
    public Canvas mainCanvas;
    public GameObject minigamePanel;

    [Header("Elements")]
    public List<Wire> leftWires;       
    public List<Image> rightSockets; 
    public List<Color> cableColors;    

    private int successCount = 0;

    public void StartGame()
    {
        minigamePanel.SetActive(true);
        successCount = 0;
        ShuffleAndSetup();
    }

    private void ShuffleAndSetup()
    {
        List<Color> availableColors = new List<Color>(cableColors);
        List<int> rightSideIndices = new List<int>();

        for (int i = 0; i < leftWires.Count; i++) rightSideIndices.Add(i);

        ShuffleList(availableColors);
        ShuffleList(rightSideIndices);

        for (int i = 0; i < leftWires.Count; i++)
        {
            Color color = availableColors[i];       
            int targetIndex = rightSideIndices[i]; 

            rightSockets[targetIndex].color = color;

            leftWires[i].Setup(color, rightSockets[targetIndex].transform, this, mainCanvas);
        }
    }

    public void CheckCompletion()
    {
        successCount++;
        if (successCount >= leftWires.Count)
        {
            Debug.Log("TÜM KABLOLAR TAMAM!");
            Invoke("FinishTask", 0.5f); 
        }
    }

    private void FinishTask()
    {
        minigamePanel.SetActive(false);
        breakdownManager.ResetBreakdown(); 
    }

    private void ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int rnd = Random.Range(i, list.Count);
            list[i] = list[rnd];
            list[rnd] = temp;
        }
    }
}

