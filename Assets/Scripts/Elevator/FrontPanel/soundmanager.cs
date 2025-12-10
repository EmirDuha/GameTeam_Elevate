using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class soundmanager : MonoBehaviour
//umarım çalışır ilk unity projem bu.
{
    [SerializeField] private StressSystem stressSystem;

    private bool ismuted;

    private void Start()
    {
        PlayerPrefs.SetInt("ismuted", 1);
        Load();
        
        AudioListener.pause = ismuted;
    }


    public void OnButtonPress()
    {
        if (!ismuted)
        {
            ismuted = true;
            AudioListener.pause = true;
            stressSystem.isMusicOn = false;
        }
        else
        {

            ismuted = false;
            AudioListener.pause = false;
            stressSystem.isMusicOn = true;
        }

        Save();
    }

    private void Load()
    {
        ismuted = PlayerPrefs.GetInt("ismuted") == 1;

    }

    private void Save()
    {
        PlayerPrefs.SetInt("ismuted", ismuted ? 1 : 0);

    }
}
