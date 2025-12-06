using UnityEngine;

public class StressSystem : MonoBehaviour
{
    [Header("Stress Settings")]
    [SerializeField] private float stressLevel = 0f;
    [SerializeField] public float stressIncreaseRate = 10f;
    [SerializeField] private float stressDecreaseRate = 4f;
    [SerializeField] private float maxStressLevel = 100f;
    [SerializeField] private float minStressLevel = 0f;
    public bool isUnderStress = false;
    public bool inMaxStressState = false;

    [Header("Object References")]
    [SerializeField] private UnityEngine.UI.Slider StressBar;

    private void Start(){
        stressLevel = minStressLevel;
    }

    private void FixedUpdate()
    {
        if (isUnderStress)
        {
            IncreaseStress(stressIncreaseRate);
        }
        else
        {
            if (stressLevel > minStressLevel)
            DecreaseStress(stressDecreaseRate);
        }

        if (!inMaxStressState)
        StressBar.value = stressLevel / maxStressLevel;
    }

    public float GetStressLevel()
    {
        return stressLevel;
    }

    private void IncreaseStress(float amount)
    {
        stressLevel += amount * Time.deltaTime;
        stressLevel = Mathf.Clamp(stressLevel, minStressLevel, maxStressLevel);
        CheckMaxStressState();
    }

    private void DecreaseStress(float amount)
    {
        stressLevel -= amount * Time.deltaTime;
        stressLevel = Mathf.Clamp(stressLevel, minStressLevel, maxStressLevel);
    }

    private void CheckMaxStressState()
    {
        if (!inMaxStressState && stressLevel >= maxStressLevel)
        {
            inMaxStressState = true;
            Debug.Log("Stres Jumpscare");
            isUnderStress = false;
        }
        else
        {
            inMaxStressState = false;
        }
    }
}
