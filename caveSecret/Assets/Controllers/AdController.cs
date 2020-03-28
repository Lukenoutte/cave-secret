using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class AdController : MonoBehaviour
{
    private string storeIdAndroid = "3527818";
    private string storeIdIOS = "3527819";
    private string video_ad = "video";
    public static AdController instance;
    public bool playAdBool = false;
    public bool winGame = false;
    public int adCont = 0;
    private Scene m_Scene;
    private string sceneName;
    private bool auxWin = false;
    // Start is called before the first frame update
    void Start()
    {
        m_Scene = SceneManager.GetActiveScene();
        sceneName = m_Scene.name;

        Advertisement.Initialize(storeIdAndroid, false);

    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (playAdBool)
        {

            playAdBool = false;
            PlayAd();
            winGame = false;
            adCont = 0;
            Debug.Log("Ad");


        }




    }

    public void PlayAd()
    {
        if (Advertisement.IsReady(video_ad))
        {

            Advertisement.Show(video_ad);
        }
    }
}
