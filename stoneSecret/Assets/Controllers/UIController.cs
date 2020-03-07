using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    private Scene m_Scene;
    private string sceneName;
    private bool isEasy = false;
    private bool isMedium = false;
    private bool isHard = false;
    private GameObject menuButton;
    private GeneralButtonController allButtons;

    void Start()
    {
        menuButton = GameObject.Find("Menu");
        
        m_Scene = SceneManager.GetActiveScene();
        sceneName = m_Scene.name;


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
        if (Input.GetMouseButtonDown(0))
        {

            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if (hit != null && hit.collider != null)
            {
                if (hit.collider.tag == "Menu")
                {
                    SceneManager.LoadScene("MenuMain");
                }
                if (isEasy)
                {
                    if (hit.collider.tag == "Reset")
                    {
                        easyButton();
                    }
                }

                if (isMedium)
                {
                    if (hit.collider.tag == "Reset")
                    {
                        mediumButton();
                    }
                }

                if (isHard)
                {
                    if (hit.collider.tag == "Reset")
                    {
                        hardButton();
                    }
                }

            }
        }
    }


    public void easyButton()
    {
        SceneManager.LoadScene("GameEasy");
    }
    public void mediumButton()
    {
        SceneManager.LoadScene("GameMedium");
    }
    public void hardButton()
    {
        SceneManager.LoadScene("GameHard");
    }


}
