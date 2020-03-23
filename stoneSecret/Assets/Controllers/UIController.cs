using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public GameObject menuMain, configuration, difficulty, buttonEasy, buttonMedium, buttonHard,
        medalEasy, medalMedium, medalHard, recordEasy, recordMedium, recordHard, thxBg1, thxBg2, thxBixinho, thxNext, coracao,
        bixinhoActivation, menuPause;
    private Scene m_Scene;
    private string sceneName;
    private bool isEasy = false;
    private bool isMedium = false;
    private bool isHard = false;
    private bool thxScreen = false;


    private bool clickble = false;
    void Start()
    {

        m_Scene = SceneManager.GetActiveScene();
        sceneName = m_Scene.name;
        if (sceneName == "Animation1")
        {
            if (SaveManager.instance.state.played == true)
            {
                SceneManager.LoadScene("MenuMain");
            }

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
        if (sceneName == "MenuMain")
        {
            if (SaveManager.instance != null)
            {
                SaveState auxSave = SaveManager.instance.state;
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
                        configuration.SetActive(true);

                    }
                    if (hit.collider.tag == "Menu")
                    {
                        SceneManager.LoadScene("MenuMain");
                    }

                    if (hit.collider.tag == "BEasy")
                    {
                        EasyButton();
                    }
                    if (hit.collider.tag == "BMedium")
                    {
                        MediumButton();
                    }
                    if (hit.collider.tag == "BHard")
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
            if (configuration.activeSelf) { 
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

        if (isEasy | isMedium)
        {
            if (menuPause.activeSelf)
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
