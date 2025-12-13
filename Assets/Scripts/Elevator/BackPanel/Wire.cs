using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Wire : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Image wireImage;            
    public LineRenderer lineRenderer;   

    [Header("Settings")]
    public float detectionRadius = 1.0f;
    [HideInInspector] public Transform successTarget;
    [HideInInspector] public bool isConnected = false; 

    private Canvas myCanvas;
    private WireTaskManager taskManager;
    private Color myColor;

    
    public void Setup(Color color, Transform target, WireTaskManager manager, Canvas canvas)
    {
        myColor = color;
        wireImage.color = color;
        
    
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        lineRenderer.positionCount = 0; 

        successTarget = target;
        taskManager = manager;
        myCanvas = canvas;
        isConnected = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Tıklama Başarılı! Çizgi çizilmeye çalışılıyor...");
        if (isConnected) return; 

        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, transform.position); 
        lineRenderer.SetPosition(1, GetMousePos());      
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isConnected) return;
        lineRenderer.SetPosition(1, GetMousePos()); 
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isConnected) return;

        
        float distance = Vector3.Distance(GetMousePos(), successTarget.position);

        Debug.Log("Hedefe Mesafe: " + distance);
        if (distance < detectionRadius) 
        {
            isConnected = true;
            lineRenderer.SetPosition(1, successTarget.position); 
            taskManager.CheckCompletion(); 
        }
        else 
        {
            lineRenderer.positionCount = 0;
        }
    }

    
    private Vector3 GetMousePos()
    {
        Vector2 movePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            myCanvas.transform as RectTransform,
            Input.mousePosition,
            myCanvas.worldCamera,
            out movePos);
        return myCanvas.transform.TransformPoint(movePos);
    }
}

