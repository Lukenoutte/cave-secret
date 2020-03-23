using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationController : MonoBehaviour
{
    private GameObject bixinho, bixinho1Menu, bixinho2Menu, mainMenu;

    public GameObject bixinho3Dif, bixinho4Menu, difficulty, bixinho5Dif, configuration, bixinho6Config, bixinho7Config,
        bixinho8Menu, bixinho9Config, bixinho10Dif, bixinho11Dif, bixinhoBregaFunk, bixinho12Config;

    public GameObject bixinho4InGame, bixinho2InGame, bixinho5InGame, bixinho6InGame, bixinho8InGame,
        bixinho1InGame, bixinho7InGame, bixinho3InGame, bixinho9InGame, bixinho10InGame, bixinho11InGame, bixinho12InGame,
        bixinho13InGame, bixinho14InGame, bixinho1Pause;
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
    public bool animOnInGame;
    private bool execulted;
    private int wrongFixoTuto = 6;
    private int rightFixo = 3;
    private int contEnterFixo = 1;
    private int idAnimInGameTuto;
    private int idAnimInGameRight;
    private int idAnimInGameEnter;
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
        animOnInGame = false;
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
            if (mainMenu != null)
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
                    if (SaveManager.instance != null)
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
        } // Menu

        // In Game
        if (sceneName == "GameEasy" | sceneName == "GameMedium")
        {
            GeneralButtonController aux = GeneralButtonController.instance;
            if (aux.contWrong == 2 && !animOnInGame && !execulted && !SaveManager.instance.state.played)
            {
                bixinho4InGame.SetActive(true);
                animOnInGame = true;
                execulted = true;
                StartCoroutine(AutoDestroyAnim(bixinho4InGame));
            }
            if (aux.isPaused)
            {
                if (animOnInGame)
                {
                    desableAnimationInGame();
                }

                bixinho1Pause.SetActive(true);
            }
            else
            {

                bixinho1Pause.SetActive(false);

            }
            if (SaveManager.instance != null)
            {
                if (!SaveManager.instance.state.bixinhoActivation)
                {
                    desableAnimationInGame();
                    animOnInGame = true;
                }


            }

            if (aux.contWrong == wrongFixoTuto && aux.countLightsOn < 2 && !animOnInGame)
            {

                int oldIdEasy = idAnimInGameTuto;
                idAnimInGameTuto = Random.Range(1, 9);
                while (oldIdEasy == idAnimInGameTuto)
                {
                    idAnimInGameTuto = Random.Range(1, 9);
                }

                if (idAnimInGameTuto == 1 && !animOnInGame)
                {
                    animOnInGame = true;
                    bixinho2InGame.SetActive(true);
                    StartCoroutine(AutoDestroyAnim(bixinho2InGame));
                }

                if (idAnimInGameTuto == 2 && !animOnInGame)
                {
                    animOnInGame = true;
                    bixinho5InGame.SetActive(true);
                    StartCoroutine(AutoDestroyAnim(bixinho5InGame));
                }
                if (idAnimInGameTuto == 3 && !animOnInGame)
                {
                    animOnInGame = true;
                    bixinho6InGame.SetActive(true);
                    StartCoroutine(AutoDestroyAnim(bixinho6InGame));
                }
                if (idAnimInGameTuto == 4 && !animOnInGame)
                {
                    animOnInGame = true;
                    bixinho8InGame.SetActive(true);
                    StartCoroutine(AutoDestroyAnim(bixinho8InGame));
                }
                if (idAnimInGameTuto == 5 && !animOnInGame)
                {
                    animOnInGame = true;
                    bixinho9InGame.SetActive(true);
                    StartCoroutine(AutoDestroyAnim(bixinho9InGame));
                }
                if (idAnimInGameTuto == 6 && !animOnInGame)
                {
                    animOnInGame = true;
                    bixinho10InGame.SetActive(true);
                    StartCoroutine(AutoDestroyAnim(bixinho10InGame));
                }
                if (idAnimInGameTuto == 7 && !animOnInGame)
                {
                    animOnInGame = true;
                    bixinho11InGame.SetActive(true);
                    StartCoroutine(AutoDestroyAnim(bixinho11InGame));
                }
                if (idAnimInGameTuto == 8 && !animOnInGame)
                {
                    animOnInGame = true;
                    bixinho13InGame.SetActive(true);
                    StartCoroutine(AutoDestroyAnim(bixinho13InGame));
                }

                wrongFixoTuto += 6;
            }

            if (aux.countLightsOn == rightFixo && !animOnInGame)
            {
                int oldIdEasy = idAnimInGameRight;
                idAnimInGameRight = Random.Range(1, 5);
                while (oldIdEasy == idAnimInGameRight)
                {
                    idAnimInGameRight = Random.Range(1, 5);
                }


                if (idAnimInGameRight == 1 && !animOnInGame)
                {
                    animOnInGame = true;
                    bixinho1InGame.SetActive(true);
                    StartCoroutine(AutoDestroyAnim(bixinho1InGame));
                    rightFixo += 3;
                }

                if (idAnimInGameRight == 2 && !animOnInGame)
                {
                    animOnInGame = true;
                    bixinho7InGame.SetActive(true);
                    StartCoroutine(AutoDestroyAnim(bixinho7InGame));
                    rightFixo += 3;
                }
                if (idAnimInGameRight == 3 && !animOnInGame)
                {
                    animOnInGame = true;
                    bixinho12InGame.SetActive(true);
                    StartCoroutine(AutoDestroyAnim(bixinho12InGame));
                    rightFixo += 3;
                }
                if (idAnimInGameRight == 4 && !animOnInGame)
                {
                    animOnInGame = true;
                    bixinho14InGame.SetActive(true);
                    StartCoroutine(AutoDestroyAnim(bixinho14InGame));
                    rightFixo += 3;
                }
            }
            if (aux.contEnterNoWin == contEnterFixo && !animOnInGame)
            {

                int oldIdEasy = idAnimInGameEnter;
                idAnimInGameEnter = Random.Range(1, 3);
                while (oldIdEasy == idAnimInGameEnter)
                {
                    idAnimInGameEnter = Random.Range(1, 3);
                }
                if (idAnimInGameEnter == 1 && !animOnInGame)
                {
                    animOnInGame = true;
                    bixinho3InGame.SetActive(true);
                    StartCoroutine(AutoDestroyAnim(bixinho3InGame));
                    contEnterFixo += 3;
                }

                if (idAnimInGameEnter == 2 && !animOnInGame)
                {
                    animOnInGame = true;
                    bixinho4InGame.SetActive(true);
                    StartCoroutine(AutoDestroyAnim(bixinho4InGame));
                    contEnterFixo += 3;
                }


            }

        }

    } // End update

    private IEnumerator EraseAnim(GameObject anim)
    {
        GameObject aux = anim;
        aux.GetComponent<Animator>().SetBool("clicked", true);


        yield return new WaitForSeconds(1);
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
        if (sceneName == "GameEasy" | sceneName == "GameMedium")
        {
            animOnInGame = false;
        }

    }

    private IEnumerator AutoDestroyAnim(GameObject anim)
    {
        if (animOnInGame)
        {
            GameObject aux = anim;
            yield return new WaitForSeconds(5);
            aux.GetComponent<Animator>().SetBool("clicked", true);
            yield return new WaitForSeconds(1);
            aux.SetActive(false);
            if (sceneName == "GameEasy" | sceneName == "GameMedium")
            {
                animOnInGame = false;
            }
        }
    }

    private void desableAnimationInGame()
    {

        animOnInGame = false;
        bixinho1InGame.SetActive(false);
        bixinho2InGame.SetActive(false);
        bixinho3InGame.SetActive(false);
        bixinho4InGame.SetActive(false);
        bixinho5InGame.SetActive(false);
        bixinho6InGame.SetActive(false);
        bixinho7InGame.SetActive(false);
        bixinho8InGame.SetActive(false);
        bixinho9InGame.SetActive(false);
        bixinho10InGame.SetActive(false);
        bixinho11InGame.SetActive(false);
        bixinho12InGame.SetActive(false);
        bixinho13InGame.SetActive(false);
        bixinho14InGame.SetActive(false);
        bixinho1Pause.SetActive(false);
    }

}
