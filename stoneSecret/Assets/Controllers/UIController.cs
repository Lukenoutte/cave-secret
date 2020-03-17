using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    
    public GameObject menuMain, configuration, difficulty, buttonEasy, buttonMedium, buttonHard;
    private Scene m_Scene;
    private string sceneName;
    private bool isEasy = false;
    private bool isMedium = false;
    private bool isHard = false;


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
                    if (hit.collider.tag == "Play")
                    {
                        menuMain.SetActive(false);
                        difficulty.SetActive(true);

                    }
                    if (hit.collider.tag == "Configuration")
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
            if (difficulty.activeSelf == true)
            {
                if (SaveManager.instance.state.easyWin == true)
                {
                    buttonEasy.GetComponent<Animator>().SetBool("win", true);
                }
                if (SaveManager.instance.state.mediumWin == true)
                {
                    buttonMedium.GetComponent<Animator>().SetBool("win", true);
                }
                if (SaveManager.instance.state.hardWin == true)
                {
                    buttonHard.GetComponent<Animator>().SetBool("win", true);
                }
            }
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
