using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject menuMain, configuration, difficulty, buttonEasy, buttonMedium, buttonHard,
        medalEasy, medalMedium, medalHard, recordEasy, recordMedium, recordHard, thxBg1, thxBg2, thxBixinho, thxNext, coracao,
        bixinhoActivation, volumeMenuMain, lockedMedium, lockedHard, volumePause, menuPause, musicInfo, creditsMusic,
        animation1, nextAnim, logo;

    private Scene m_Scene;
    private string sceneName;
    private bool isEasy = false;
    private bool isMedium = false;
    private bool isHard = false;
    private bool thxScreen = false;
    private bool isNotLocked = true;
    private string theme0Credits;
    private string theme1Credits;
    private string theme2Credits;
    private string theme3Credits;
    private bool winPlayAd = false;
    private string theme0Link;
    private string theme1Link;
    private string theme2Link;
    private string theme3Link;
    private string mainLink;

    private string authorLink;
    private string ccLink;
    private bool clickble = false;
    void Start()
    {
        m_Scene = SceneManager.GetActiveScene();
        sceneName = m_Scene.name;
        if(sceneName == "MenuMain") {
        theme0Credits = "Song: \n Great Expectations \n by Kai Engel \n is licensed under CC BY.";
        theme1Credits = "Song: \n September \n by Kai Engel \n is licensed under CC BY.";
        theme2Credits = "Song: \n Cobweb Morning \n by Kai Engel \n is licensed under CC BY.";
        theme3Credits = "Song: \n Laburnum \n by Kai Engel \n is licensed under CC BY.";

        theme0Link = "https://freemusicarchive.org/music/Kai_Engel/Satin_1564/Kai_Engel_-_Satin_-_05_Great_Expectations_1199";
        theme1Link = "https://freemusicarchive.org/music/Kai_Engel/Chapter_Four__Fall/Kai_Engel_-_Chapter_Four_-_Fall_-_02_September";
        theme2Link = "https://freemusicarchive.org/music/Kai_Engel/Chapter_Four__Fall/Kai_Engel_-_Chapter_Four_-_Fall_-_04_Cobweb_Morning";
        theme3Link = "https://freemusicarchive.org/music/Kai_Engel/Chapter_Four__Fall/Kai_Engel_-_Chapter_Four_-_Fall_-_01_Laburnum";

        authorLink = "https://freemusicarchive.org/music/Kai_Engel";
        ccLink = "https://creativecommons.org/licenses/by/4.0/";
        }


        StartCoroutine(ClickbleOn());




        if (sceneName == "GameEasy")
        {
            isEasy = true;

        }
        else if (sceneName == "GameMedium")
        {

            isMedium = true;

        }
        else if (sceneName == "GameHard")
        {

            isHard = true;

        }


        if (isEasy | isMedium | isHard)
        {
            if (SaveManager.instance != null)
            {
                SaveState auxSave = SaveManager.instance.state;
                if (auxSave.volume != volumePause.GetComponent<Slider>().value)
                {
                    volumePause.GetComponent<Slider>().value = auxSave.volume;
                }
            }

        }

        if (sceneName == "MenuMain")
        {
            if (SaveManager.instance != null)
            {
                SaveState auxSave = SaveManager.instance.state;

                if (auxSave.playedTuto)
                {
                    lockedMedium.SetActive(false);
                    lockedHard.SetActive(false);
                    isNotLocked = false;
                }
                if (auxSave.easyWin == true)
                {
                    buttonEasy.GetComponent<Animator>().SetBool("win", true);
                    recordEasy.GetComponent<Text>().text = auxSave.easyRecord.ToString();
                    medalEasy.SetActive(true);
                    if (auxSave.breakRecordEasy)
                    {
                        buttonEasy.GetComponent<Animator>().SetBool("record", true);
                    }
                }
                if (auxSave.mediumWin == true)
                {
                    buttonMedium.GetComponent<Animator>().SetBool("win", true);
                    recordMedium.GetComponent<Text>().text = auxSave.mediumRecord.ToString();
                    medalMedium.SetActive(true);
                    if (auxSave.breakRecordMedium)
                    {
                        buttonMedium.GetComponent<Animator>().SetBool("record", true);
                    }
                }
                if (auxSave.hardWin == true)
                {
                    buttonHard.GetComponent<Animator>().SetBool("win", true);
                    recordHard.GetComponent<Text>().text = auxSave.hardRecord.ToString();
                    medalHard.SetActive(true);
                    if (auxSave.breakRecordHard)
                    {
                        buttonHard.GetComponent<Animator>().SetBool("record", true);
                    }
                }

                if (auxSave.easyWin && auxSave.mediumWin && auxSave.hardWin && !auxSave.thxMensage)
                {
                    thxScreen = true;
                    int itemIndex = Random.Range(1, 3);
                    if (itemIndex == 1)
                    {
                        thxBg1.SetActive(true);
                    }
                    if (itemIndex == 2)
                    {
                        thxBg2.SetActive(true);
                    }
                    thxBixinho.SetActive(true);
                    auxSave.thxMensage = true;
                    thxNext.SetActive(true);
                    SaveManager.instance.Save();
                }
                if (auxSave.thxMensage)
                {
                    coracao.SetActive(true);

                }



                if (auxSave.volume != volumeMenuMain.GetComponent<Slider>().value)
                {
                    volumeMenuMain.GetComponent<Slider>().value = auxSave.volume;
                }


            }
        }

    }
    private void Update()
    {
        if (clickble == true)
        {
            if (Input.GetMouseButtonDown(0))
            {

                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

                if (hit != null && hit.collider != null)
                {
                    if (hit.collider.tag == "Next")
                    {
                        SceneManager.LoadScene("MenuMain");
                    }

                    if (hit.collider.tag == "AuthorLink")
                    {
                        Application.OpenURL(authorLink);
                    }
                    if (hit.collider.tag == "CcLink")
                    {
                        Application.OpenURL(ccLink);
                    }
                    if (hit.collider.tag == "SongLink")
                    {

                        Application.OpenURL(mainLink);
                    }
                    if (hit.collider.tag == "ThxNext")
                    {
                        thxScreen = false;
                        thxBg2.SetActive(false);
                        thxBg1.SetActive(false);
                        thxBixinho.SetActive(false);
                        thxNext.SetActive(false);
                    }
                    if (hit.collider.tag == "Play" && !thxScreen)
                    {
                        menuMain.SetActive(false);
                        Vector3 aux = difficulty.GetComponent<Transform>().position;
                        difficulty.GetComponent<Transform>().position = new Vector3(0, aux.y, aux.z);

                    }
                    if (hit.collider.tag == "Back")
                    {
                        menuMain.SetActive(true);
                        Vector3 aux = difficulty.GetComponent<Transform>().position;
                        difficulty.GetComponent<Transform>().position = new Vector3(-999, aux.y, aux.z);
                        Vector3 aux2 = configuration.GetComponent<Transform>().position;
                        configuration.GetComponent<Transform>().position = new Vector3(-999, aux2.y, aux2.z);

                    }
                    if (hit.collider.tag == "BackInfoMusic")
                    {

                        Vector3 aux = musicInfo.GetComponent<Transform>().position;
                        musicInfo.GetComponent<Transform>().position = new Vector3(-9999, aux.y, aux.z);
                        Vector3 aux2 = musicInfo.GetComponent<Transform>().position;
                        configuration.GetComponent<Transform>().position = new Vector3(0, aux2.y, aux2.z);

                    }

                    if (hit.collider.tag == "MusicInfo")
                    {

                        Vector3 aux = musicInfo.GetComponent<Transform>().position;
                        musicInfo.GetComponent<Transform>().position = new Vector3(0, aux.y, aux.z);
                        Vector3 aux2 = configuration.GetComponent<Transform>().position;
                        configuration.GetComponent<Transform>().position = new Vector3(-999, aux2.y, aux2.z);

                    }
                    if (SaveManager.instance != null)
                    {
                        if (hit.collider.tag == "BixinhoActivation" && SaveManager.instance.state.bixinhoActivation)
                        {
                            bixinhoActivation.GetComponent<Animator>().SetBool("clicked", true);
                            SaveManager.instance.state.bixinhoActivation = false;
                            SaveManager.instance.Save();

                        }
                        else if (hit.collider.tag == "BixinhoActivation" && !SaveManager.instance.state.bixinhoActivation)
                        {
                            bixinhoActivation.GetComponent<Animator>().SetBool("clicked", false);
                            SaveManager.instance.state.bixinhoActivation = true;
                            SaveManager.instance.Save();

                        }
                    }
                    if (hit.collider.tag == "Configuration" && !thxScreen)
                    {
                        menuMain.SetActive(false);
                        Vector3 aux = difficulty.GetComponent<Transform>().position;
                        configuration.GetComponent<Transform>().position = new Vector3(0, aux.y, aux.z);

                    }
                    if (hit.collider.tag == "Menu")
                    {
                        SceneManager.LoadScene("MenuMain");
                    }

                    if (hit.collider.tag == "MenuAd")
                    {
                        if (AdController.instance != null)
                        {
                            AdController.instance.adCont++;

                            if (AdController.instance.adCont == 2)
                            {
                                AdController.instance.playAdBool = true;
                            }

                        }
             
                        SceneManager.LoadScene("MenuMain");

                    }

                    if (hit.collider.tag == "BEasy")
                    {
                        EasyButton();
                    }
                    if (hit.collider.tag == "BMedium" && !isNotLocked)
                    {
                        MediumButton();
                    }
                    if (hit.collider.tag == "BHard" && !isNotLocked)
                    {
                        HardButton();
                    }
                    if (isEasy)
                    {
                        if (hit.collider.tag == "Reset")
                        {
                            EasyButton();
                        }
                    }

                    if (isMedium)
                    {
                        if (hit.collider.tag == "Reset")
                        {
                            MediumButton();
                        }
                    }

                    if (isHard)
                    {
                        if (hit.collider.tag == "Reset")
                        {
                            HardButton();
                        }
                    }

                }
            }
        }

        if (sceneName == "MenuMain")
        {
            if (AdController.instance.winGame)
            {
                AdController.instance.playAdBool = true;
            }

            if (SaveManager.instance != null)
            {

                if (SaveManager.instance.state.bixinhoActivation)
                {
                    bixinhoActivation.GetComponent<Animator>().SetBool("clicked", false);
                }
                else
                {
                    bixinhoActivation.GetComponent<Animator>().SetBool("clicked", true);
                }

                if (configuration.GetComponent<Transform>().position.x == 0)
                {
                    if (SaveManager.instance.state.volume != volumeMenuMain.GetComponent<Slider>().value)
                    {
                        SaveManager.instance.state.volume = volumeMenuMain.GetComponent<Slider>().value;
                        SaveManager.instance.Save();
                    }

                }
                if (SaveManager.instance.state.thxMensage && musicInfo.GetComponent<Transform>().position.x == 0)
                {
                    coracao.SetActive(false);
                }
                else if (SaveManager.instance.state.thxMensage && musicInfo.GetComponent<Transform>().position.x != 0)
                {
                    coracao.SetActive(true);

                }
            }
            if(AudioController.instance != null) { 
            if (AudioController.instance.soundIndex == 0)
            {
                creditsMusic.GetComponent<Text>().text = theme0Credits;
                mainLink = theme0Link;
            }
            if (AudioController.instance.soundIndex == 1)
            {
                creditsMusic.GetComponent<Text>().text = theme1Credits;
                mainLink = theme1Link;
            }
            if (AudioController.instance.soundIndex == 2)
            {
                creditsMusic.GetComponent<Text>().text = theme2Credits;
                mainLink = theme2Link;
            }
            if (AudioController.instance.soundIndex == 3)
            {
                creditsMusic.GetComponent<Text>().text = theme3Credits;
                mainLink = theme3Link;
            }
            }
        }

        if (isEasy | isMedium | isHard)
        {
            if (menuPause.GetComponent<Transform>().position.x == 0)
            {
                if (SaveManager.instance != null)
                {
                    if (SaveManager.instance.state.volume != volumePause.GetComponent<Slider>().value)
                    {
                        SaveManager.instance.state.volume = volumePause.GetComponent<Slider>().value;
                        SaveManager.instance.Save();
                    }
                }
            }
        }

        if (isEasy | isMedium)
        {
            if (SaveManager.instance != null)
            {
                if (SaveManager.instance.state.bixinhoActivation)
                {
                    bixinhoActivation.GetComponent<Animator>().SetBool("clicked", false);
                }
                else
                {
                    bixinhoActivation.GetComponent<Animator>().SetBool("clicked", true);

                }
            }
        }
        {
        }


    }


    public void EasyButton()
    {
        SceneManager.LoadScene("GameEasy");
    }
    public void MediumButton()
    {
        SceneManager.LoadScene("GameMedium");
    }
    public void HardButton()
    {
        SceneManager.LoadScene("GameHard");
    }

    private IEnumerator ClickbleOn()
    {
        clickble = false;
        yield return new WaitForSeconds(0.5f);
        clickble = true;
    }

}
