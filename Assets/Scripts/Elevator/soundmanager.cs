using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class soundmanager : MonoBehaviour
//umarım çalışır ilk unity projem bu.
{
  [SerializeField] private UnityEngine.UI.Image SoundOnIcon;
  [SerializeField] private UnityEngine.UI.Image SoundOffIcon;  

private bool ismuted;

private void Start()
    {
        if(!PlayerPrefs.HasKey("ismuted"))
        {
            PlayerPrefs.SetInt("ismuted",0); 
            Load();
        }

        else
        {
            Load();
        }

     UpdateButtonIcon();
     AudioListener.pause=ismuted;
    }       
   

  public void OnButtonPress()
  {
    if (! ismuted)
    {
        ismuted = true;
        AudioListener.pause=true;
    }
else
        {
            
            ismuted = false;
            AudioListener.pause=false;
        }
    
    Save();
    UpdateButtonIcon();
     }
private void UpdateButtonIcon()
    {
        if (ismuted)
        {
            SoundOnIcon.enabled = true;
            SoundOffIcon.enabled = false;
        }
        else
        {
            SoundOnIcon.enabled = false;
            SoundOffIcon.enabled = true;
        }
    }
     private void Load()
    {
        ismuted=PlayerPrefs.GetInt("ismuted")==1;

    }

    private void Save()
    {
        PlayerPrefs.SetInt("ismuted", ismuted ? 1 : 0);

    }
    }
