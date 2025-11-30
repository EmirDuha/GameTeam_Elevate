using UnityEngine;

public class backgroundmusic : MonoBehaviour
{
    private static backgroundmusic backgroundMusic;
   void Awake()
    {
        if (backgroundMusic == null)
        {
            backgroundMusic = this;
            DontDestroyOnLoad(backgroundMusic);
        }
        else
        {
            Destroy(gameObject);
        }
    } 
}  
