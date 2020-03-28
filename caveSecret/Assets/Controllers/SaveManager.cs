using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance { set; get; }
    public SaveState state;

    private void Awake()
    {
        if(instance == null) { 
        DontDestroyOnLoad(gameObject);
        instance = this;
        Load();
        }
    }

    public void Save()
    {
        PlayerPrefs.SetString("save",Helper.Serialize<SaveState>(state));

    }

    public void Load()
    {
        if(PlayerPrefs.HasKey("save")){
             state = Helper.Desirialize<SaveState>(PlayerPrefs.GetString("save"));
        }
        else
        {
            state = new SaveState();
            Save();
            Debug.Log("Criando novo save");
        }
    }
}
