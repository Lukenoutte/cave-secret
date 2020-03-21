using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationController : MonoBehaviour
{
    private GameObject bixinho, bixinho1Menu, bixinho2Menu, mainMenu;

    public GameObject bixinho3Dif, bixinho4Menu, difficulty, bixinho5Dif, configuration, bixinho6Config, bixinho7Config,
        bixinho8Menu, bixinho9Config, bixinho10Dif, bixinho11Dif, bixinhoBregaFunk, bixinho12Config;

    public GameObject bixinho4Easy;
    private string animName;
    private bool animOnMenu;
    private bool animOnDif;
    private bool animOnConfig;
    private int idAnimMenu;
    private int idAnimDif;
    private int idAnimConfig;
    private int contClick = 0;
    private bool tutorialAnim;
    private Scene m_Scene;
    private string sceneName;
    private bool animOnEasy;
    private bool execulted;

    // Start is called before the first frame update
    void Start()
    {
        m_Scene = SceneManager.GetActiveScene();
        sceneName = m_Scene.name;
        execulted = false;
        tutorialAnim = true;
        animOnMenu = false;
        animOnDif = false;
        animOnConfig = false;
        animOnEasy = false;
        bixinho1Menu = GameObject.Find("/Canvas/MainMenu/Play/Bixinho1Menu");
        bixinho2Menu = GameObject.Find("/Canvas/MainMenu/Bixinho2Menu");
        mainMenu = GameObject.Find("/Canvas/MainMenu");



    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if (hit != null && hit.collider != null)
            {
                animName = hit.collider.gameObject.name.ToString();
                bixinho = GameObject.Find(animName);
                if (hit.collider.tag == "Bixinho")
                {
                    StartCoroutine(EraseAnim(bixinho));
                    contClick++;
                }
            }
        }

        // Scene Menu
        if (sceneName == "MenuMain")
        {
            // Main menu animations
            if (mainMenu.activeSelf == false)
            {
                animOnMenu = false;
                bixinho1Menu.SetActive(false);
                bixinho2Menu.SetActive(false);
                bixinho4Menu.SetActive(false);
                bixinho8Menu.SetActive(false);
                bixinhoBregaFunk.SetActive(false);
            }
            else if (animOnMenu == false)
            {
                bool AnimActive = false;
                int oldIdMenu = idAnimMenu;
                idAnimMenu = Random.Range(1, 6);
                while (oldIdMenu == idAnimMenu)
                {
                    idAnimMenu = Random.Range(1, 6);
                }

                if (bixinho2Menu.activeSelf | bixinho4Menu.activeSelf | bixinho8Menu.activeSelf | bixinho1Menu.activeSelf | bixinhoBregaFunk.activeSelf)
                {
                    AnimActive = true;
                }

                if (idAnimMenu == 1 && animOnMenu == false && !AnimActive)
                {
                    animOnMenu = true;
                    bixinho1Menu.SetActive(true);



                }
                if (idAnimMenu == 2 && animOnMenu == false && !AnimActive)
                {
                    animOnMenu = true;
                    bixinho2Menu.SetActive(true);

                }
                if (idAnimMenu == 3 && animOnMenu == false && !AnimActive)
                {
                    animOnMenu = true;
                    bixinho4Menu.SetActive(true);

                }
                if (idAnimMenu == 4 && animOnMenu == false && !AnimActive)
                {
                    animOnMenu = true;
                    bixinho8Menu.SetActive(true);

                }

                if (idAnimMenu == 5 && animOnMenu == false && !AnimActive && contClick > 15)
                {
                    animOnMenu = true;
                    bixinhoBregaFunk.SetActive(true);

                }
            } // End

            // Dif animations
            if (difficulty.GetComponent<Transform>().position.x < 0)
            {
                animOnDif = false;
                bixinho3Dif.SetActive(false);
                bixinho5Dif.SetActive(false);
                bixinho10Dif.SetActive(false);
                bixinho11Dif.SetActive(false);
            }
            else if (difficulty.GetComponent<Transform>().position.x == 0)
            {
                if (SaveManager.instance.state.played == false && animOnDif == false && tutorialAnim == true)
                {
                    animOnDif = true;
                    tutorialAnim = false;
                    bixinho3Dif.SetActive(true);
                }
                else
                {

                    int oldIdDif = idAnimDif;
                    bool AnimActive = false;
                    idAnimDif = Random.Range(1, 5);
                    while (oldIdDif == idAnimDif)
                    {
                        idAnimDif = Random.Range(1, 5);
                    }

                    if (bixinho5Dif.activeSelf | bixinho3Dif.activeSelf | bixinho10Dif.activeSelf | bixinho11Dif.activeSelf)
                    {
                        AnimActive = true;
                    }

                    if (idAnimDif == 1 && animOnDif == false && !AnimActive)
                    {
                        animOnDif = true;
                        bixinho3Dif.SetActive(true);

                    }
                    if (idAnimDif == 2 && animOnDif == false && !AnimActive)
                    {
                        animOnDif = true;
                        bixinho5Dif.SetActive(true);

                    }

                    if (idAnimDif == 3 && animOnDif == false && !AnimActive)
                    {
                        animOnDif = true;
                        bixinho10Dif.SetActive(true);

                    }

                    if (idAnimDif == 4 && animOnDif == false && !AnimActive)
                    {
                        animOnDif = true;
                        bixinho11Dif.SetActive(true);

                    }
                }
            } // End


            // Config animations
            if (configuration.activeSelf == false)
            {
                animOnConfig = false;
                bixinho6Config.SetActive(false);
                bixinho7Config.SetActive(false);
                bixinho9Config.SetActive(false);
                bixinho12Config.SetActive(false);

            }
            else if (animOnConfig == false)
            {

                int oldIdConfig = idAnimConfig;
                bool AnimActive = false;
                idAnimConfig = Random.Range(1, 5);
                while (oldIdConfig == idAnimConfig)
                {
                    idAnimConfig = Random.Range(1, 5);
                }

                if (bixinho7Config.activeSelf | bixinho6Config.activeSelf | bixinho9Config.activeSelf | bixinho12Config.activeSelf)
                {
                    AnimActive = true;
                }

                if (idAnimConfig == 1 && animOnConfig == false && !AnimActive)
                {
                    animOnConfig = true;
                    bixinho6Config.SetActive(true);



                }
                if (idAnimConfig == 2 && animOnConfig == false && !AnimActive)
                {
                    animOnConfig = true;
                    bixinho7Config.SetActive(true);
                }
                if (idAnimConfig == 3 && animOnConfig == false && !AnimActive)
                {
                    animOnConfig = true;
                    bixinho9Config.SetActive(true);
                }
                if (idAnimConfig == 4 && animOnConfig == false && !AnimActive)
                {
                    animOnConfig = true;
                    bixinho12Config.SetActive(true);

                }
            } // End 
        }

        // Scene Easy
        if (sceneName == "GameEasy")
        {

            if (GeneralButtonController.instance.contWrong == 2 && !animOnEasy && !execulted)
            {
                bixinho4Easy.SetActive(true);
                animOnEasy = true;
                execulted = true;
                StartCoroutine(AutoDestroyAnim(bixinho4Easy));
            }
        }

    } // End update

    private IEnumerator EraseAnim(GameObject anim)
    {
        GameObject aux = anim;
        aux.GetComponent<Animator>().SetBool("clicked", true);


        yield return new WaitForSeconds(3);
        aux.SetActive(false);
        if (sceneName == "MenuMain")
        {
            if (mainMenu.activeSelf)
            {
                animOnMenu = false;
            }
            if (difficulty.activeSelf)
            {
                animOnDif = false;
            }
            if (configuration.activeSelf)
            {
                animOnConfig = false;
            }
        }
        if (sceneName == "GameEasy")
        {
            animOnEasy = false;
        }

    }

    private IEnumerator AutoDestroyAnim(GameObject anim)
    {
        GameObject aux = anim;
        yield return new WaitForSeconds(6);
        aux.GetComponent<Animator>().SetBool("clicked", true);
        yield return new WaitForSeconds(1);
        aux.SetActive(false);
        if (sceneName == "GameEasy")
        {
            animOnEasy = false;
        }
        
    }


}
