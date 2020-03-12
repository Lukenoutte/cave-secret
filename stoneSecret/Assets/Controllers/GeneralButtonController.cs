using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GeneralButtonController : MonoBehaviour
{


    private Text timerText;
    private float timer;
    private int seconds = 0;
    private bool winGame = false;
    private bool looseGame = false;
    private bool isPaused = false;
    /// Booleans que verificam qual luz está acesa 
    private bool buttonOn1 = false;
    private bool buttonOn2 = false;
    private bool buttonOn3 = false;
    private bool buttonOn4 = false;
    private bool buttonOn5 = false;
    private bool buttonOn6 = false;
    private bool buttonOn7 = false;
    private bool buttonOn8 = false;
    private bool buttonOn9 = false;
    private bool buttonOnC1 = false;
    private bool buttonOnC2 = false;
    private bool buttonOnC3 = false;
    private GameObject pauseButton;
    private GameObject allButtons;
    private GameObject fireFlies;
    private bool controlVarMain = false;
    private GameObject enterLight;

    private List<string> generalList = new List<string>();
    [SerializeField]
    private List<string> mainList; // Lista responsavel por controlar o jogo, escolhida dps que o usuário clicar no primeiro elemento.
    private GameObject b;
    private GameObject bg2;
    [SerializeField]
    private GameObject looseMenu, winMenu, menuPause, timerAndPause, timerFinishLoose, TimerFinishWin;
    private string buttonName;
    //Contador de luzes on 
    private int countLightsOn = 0;
    // Listas que irão receber a ordem dos botões
    private List<string> b1List = new List<string>();
    private List<string> b2List = new List<string>();
    private List<string> b3List = new List<string>();
    private List<string> b4List = new List<string>();
    private List<string> b5List = new List<string>();
    private List<string> b6List = new List<string>();
    private List<string> b7List = new List<string>();
    private List<string> b8List = new List<string>();
    private List<string> b9List = new List<string>();
    private List<string> c1List = new List<string>();
    private List<string> c2List = new List<string>();
    private List<string> c3List = new List<string>();
    private List<string> clickedButtons = new List<string>();
    //Lista que contem as lista a cima
    private List<List<string>> listOfLists = new List<List<string>>();
    private int qtdElementToWin;
    private Scene m_Scene;
    private string sceneName;
    private bool isEasy = false;
    private bool isMedium = false;
    private bool isHard = false;
    private bool clickble = false;



    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(clickbleOn());
        enterLight = GameObject.Find("Enter");
        pauseButton = GameObject.Find("Pause");
        allButtons = GameObject.Find("Button");
        fireFlies = GameObject.Find("FireFlies");

        isPaused = false;
        m_Scene = SceneManager.GetActiveScene();
        sceneName = m_Scene.name;
        bg2 = GameObject.Find("Bg2");
        timerText = GameObject.Find("Timer").GetComponent<Text>();
        // Toda lista terá como seu primeiro item o botão a quem ela está ligada
        b1List.Add("B1");
        b2List.Add("B2");
        b3List.Add("B3");
        b4List.Add("B4");
        b5List.Add("B5");
        b6List.Add("B6");
        b7List.Add("B7");
        b8List.Add("B8");
        b9List.Add("B9");
        c1List.Add("C1");
        c2List.Add("C2");
        c3List.Add("C3");

        // Add Listas a lista
        if (sceneName == "GameEasy")
        {
            listOfLists.AddRange(new List<string>[] { b1List, b2List, b3List, b4List, b5List, b6List, b7List, b8List, b9List });
            isEasy = true;
            qtdElementToWin = 9;
        }
        else if (sceneName == "GameMedium")
        {
            listOfLists.AddRange(new List<string>[] { b1List, b2List, b3List, b4List, b5List, b6List, b7List, b8List, b9List, c1List, c2List, c3List });
            isMedium = true;
            qtdElementToWin = 12;
        }
        else if (sceneName == "GameHard")
        {
            listOfLists.AddRange(new List<string>[] { b1List, b2List, b3List, b4List, b5List, b6List, b7List, b8List, b9List, c1List, c2List, c3List });
            isHard = true;
            qtdElementToWin = 12;
        }

        // Método de gerar as listas randomicas
        randomLists();

        if (!isHard)
        {
            timer = 0.0f;
        }
        else
        {
            timer = 161;
        }



    }

    // Update is called once per frame
    void Update()
    {
        // Timer
        if (!isHard)
        {
            if (!winGame & !isPaused & !looseGame)
            {
                timer += Time.deltaTime;
                seconds = (int)timer;
                timerText.text = seconds.ToString();
            }
        }
        else if (!winGame && !isPaused && !looseGame)
        {
            timer -= Time.deltaTime;
            seconds = (int)timer;
            timerText.text = seconds.ToString();
        }

        if (Input.GetMouseButtonDown(0) && clickble)
        {

            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if (hit != null && hit.collider != null)
            {
                // Pega elemento que foi clicado
                buttonName = hit.collider.gameObject.tag.ToString();
                b = GameObject.Find(buttonName);
                mainVarControl(buttonName);
                // Bs   ---------------------------------------------------------------------
                if (buttonName.Length == 2 && controlVarMain == false && isPaused == false)
                {


                    setVarControl(buttonName, true);
                    countLightsOn++;
                    clickedButtons.Add(buttonName);
                    b.GetComponent<Animator>().SetBool("buttonClicked", true);
                    if (countLightsOn == 1)
                    {
                        setMainList(buttonName);
                    }
                }
                else if (buttonName.Length == 2 && controlVarMain == true && isPaused == false)
                {

                    setVarControl(buttonName, false);
                    countLightsOn--;
                    clickedButtons.Remove(buttonName);
                    b.GetComponent<Animator>().SetBool("buttonClicked", false);
                }

                // Luz do botão Enter
                if (countLightsOn == qtdElementToWin)
                {

                    enterLight.GetComponent<Animator>().SetBool("win", true);

                }
                else
                {
                    enterLight.GetComponent<Animator>().SetBool("win", false);
                }

                if (hit.collider.tag == "Enter" && countLightsOn < qtdElementToWin)
                {

                    StartCoroutine(lightOnEnterNow());

                }

                //Pause
                if (hit.collider.tag == "Pause" && isPaused == false && !winGame)
                {

                    isPaused = true;
                    pauseButton.GetComponent<Animator>().SetBool("isPaused", true);

                    Vector3 aux = allButtons.GetComponent<Transform>().position;
                    allButtons.GetComponent<Transform>().position = new Vector3(-999, aux.y, aux.z);
                    menuPause.SetActive(true);
                }
                else if (hit.collider.tag == "Pause" && isPaused == true && !winGame)
                {

                    isPaused = false;
                    pauseButton.GetComponent<Animator>().SetBool("isPaused", false);

                    Vector3 aux = allButtons.GetComponent<Transform>().position;
                    allButtons.GetComponent<Transform>().position = new Vector3(0, aux.y, aux.z);
                    menuPause.SetActive(false);
                }





                // Condição para ganhar
                if (isEasy == true)
                {

                    if (countLightsOn == 9 && hit.collider.tag == "Enter")
                    {

                        Debug.Log("win!!!");
                        winGame = true;

                    }
                }
                else if (isMedium == true)
                {
                    if (countLightsOn == 12 && hit.collider.tag == "Enter")
                    {
                        Debug.Log("win!!!");
                        winGame = true;
                    }
                }
                else if (isHard == true)
                {
                    if (countLightsOn == 12 && hit.collider.tag == "Enter")
                    {

                        if (timer > 0)
                        {
                            Debug.Log("win!!!");
                            winGame = true;
                        }
                        else
                        {
                            allLightsOff();
                        }
                    }
                } // Condição ganhar

            }




        } // End click


        if (isEasy | isMedium)
        {
            if(seconds >= 9999)
            {
                looseGame = true;
            }
        }
        if (isHard)
        {
            if(seconds <= 0)
            {
                looseGame = true;
            }
        }

        // Win menu
        //
        if (winGame)
        {
            Vector3 aux = allButtons.GetComponent<Transform>().position;
            allButtons.GetComponent<Transform>().position = new Vector3(-999, aux.y, aux.z);
            winMenu.SetActive(true);
            timerAndPause.SetActive(false);
            Text textTimerWin;
            textTimerWin = TimerFinishWin.GetComponent<Text>();
            textTimerWin.text = timerText.text;
            ParticleSystem.EmissionModule aux2 = fireFlies.GetComponent<ParticleSystem>().emission;
            aux2.rateOverTime = 7;
        }

        // Loose menu
        //
        if (looseGame)
        {
            Vector3 aux = allButtons.GetComponent<Transform>().position;
            allButtons.GetComponent<Transform>().position = new Vector3(-999, aux.y, aux.z);
            looseMenu.SetActive(true);
            timerAndPause.SetActive(false);
            Text textTimerWin;
            textTimerWin = timerFinishLoose.GetComponent<Text>();
            textTimerWin.text = timerText.text;
            ParticleSystem.EmissionModule aux2 = fireFlies.GetComponent<ParticleSystem>().emission;
            aux2.rateOverTime = 0;
        }

        // Compara se o botão clicado corresponde com o da lista.
        if (countLightsOn == 2 && clickedButtons[1] != mainList[1])
        {
            allLightsOff();

        }

        if (countLightsOn == 3 && clickedButtons[2] != mainList[2])
        {
            allLightsOff();

        }

        if (countLightsOn == 4 && clickedButtons[3] != mainList[3])
        {
            allLightsOff();
        }

        if (countLightsOn == 5 && clickedButtons[4] != mainList[4])
        {
            allLightsOff();
        }

        if (countLightsOn == 6 && clickedButtons[5] != mainList[5])
        {
            allLightsOff();
        }

        if (countLightsOn == 7 && clickedButtons[6] != mainList[6])
        {
            allLightsOff();
        }

        if (countLightsOn == 8 && clickedButtons[7] != mainList[7])
        {
            allLightsOff();
        }
        if (countLightsOn == 9 && clickedButtons[8] != mainList[8])
        {
            allLightsOff();
        }
        if (isMedium == true | isHard == true)
        {
            if (countLightsOn == 10 && clickedButtons[9] != mainList[9])
            {
                allLightsOff();
            }
            if (countLightsOn == 11 && clickedButtons[10] != mainList[10])
            {
                allLightsOff();
            }

        }





    }// Update end

    public void randomLists()
    {
        int countList = 0;
        if (isEasy == true)
        {
            countList = 8;
        }
        else if (isMedium == true | isHard == true)
        {
            countList = 11;
        }
        foreach (List<string> subList in listOfLists)
        {
            // Lista antes de se tornar randomica
            if (isEasy == true)
            {
                generalList.AddRange(new string[] { "B1", "B2", "B3", "B4", "B5", "B6", "B7", "B8", "B9" });
            }
            else if (isMedium == true | isHard == true)
            {
                generalList.AddRange(new string[] { "B1", "B2", "B3", "B4", "B5", "B6", "B7", "B8", "B9", "C1", "C2", "C3" });
            }

            List<string> auxList = subList;
            for (int i = 0; i < countList + 1; i++)
            {
                int itemIndex = Random.Range(0, generalList.Count);
                // Se o item a ser adicionado já existir (ser o primeiro da lista adicionado anteriormente) ele pula o elemento.
                if (!auxList.Contains(generalList[itemIndex]))
                {
                    auxList.Add(generalList[itemIndex]);

                }
                generalList.RemoveAt(itemIndex);
            }
        }

    }

    public void allLightsOff()
    {
        List<string> auxList = new List<string>();


        if (isEasy == true)
        {
            auxList.AddRange(new string[] { "B1", "B2", "B3", "B4", "B5", "B6", "B7", "B8", "B9" });
        }
        else if (isMedium == true | isHard == true)
        {
            auxList.AddRange(new string[] { "B1", "B2", "B3", "B4", "B5", "B6", "B7", "B8", "B9", "C1", "C2", "C3" });
        }

        GameObject bToOff;
        foreach (string a in auxList)
        {
            bToOff = GameObject.Find(a);
            bToOff.GetComponent<Animator>().SetBool("buttonClicked", false);
        }
        countLightsOn = 0;
        buttonOn1 = false;
        buttonOn2 = false;
        buttonOn3 = false;
        buttonOn4 = false;
        buttonOn5 = false;
        buttonOn6 = false;
        buttonOn7 = false;
        buttonOn8 = false;
        buttonOn9 = false;
        buttonOnC1 = false;
        buttonOnC2 = false;
        buttonOnC3 = false;
        bg2.GetComponent<BgController>().ShakeMe();
        clickedButtons.Clear();
    }


    public void mainVarControl(string b)
    {
        if (b == "B1")
        {
            controlVarMain = buttonOn1;
        }
        if (b == "B2")
        {
            controlVarMain = buttonOn2;
        }
        if (b == "B3")
        {
            controlVarMain = buttonOn3;
        }
        if (b == "B4")
        {
            controlVarMain = buttonOn4;
        }
        if (b == "B5")
        {
            controlVarMain = buttonOn5;
        }
        if (b == "B6")
        {
            controlVarMain = buttonOn6;
        }
        if (b == "B7")
        {
            controlVarMain = buttonOn7;
        }
        if (b == "B8")
        {
            controlVarMain = buttonOn8;
        }
        if (b == "B9")
        {
            controlVarMain = buttonOn9;
        }
        if (b == "C1")
        {
            controlVarMain = buttonOnC1;
        }
        if (b == "C2")
        {
            controlVarMain = buttonOnC2;
        }
        if (b == "C3")
        {
            controlVarMain = buttonOnC3;
        }
    }
    public void setMainList(string b)
    {
        if (b == "B1")
        {
            mainList = b1List;
        }
        if (b == "B2")
        {
            mainList = b2List;
        }
        if (b == "B3")
        {
            mainList = b3List;
        }
        if (b == "B4")
        {
            mainList = b4List;
        }
        if (b == "B5")
        {
            mainList = b5List;
        }
        if (b == "B6")
        {
            mainList = b6List;
        }
        if (b == "B7")
        {
            mainList = b7List;
        }
        if (b == "B8")
        {
            mainList = b8List;
        }
        if (b == "B9")
        {
            mainList = b9List;
        }
        if (b == "C1")
        {
            mainList = c1List;
        }
        if (b == "C2")
        {
            mainList = c2List;
        }
        if (b == "C3")
        {
            mainList = c3List;
        }
    }


    public void setVarControl(string b, bool boolVar)
    {
        if (b == "B1")
        {
            buttonOn1 = boolVar;
        }
        if (b == "B2")
        {
            buttonOn2 = boolVar;
        }
        if (b == "B3")
        {
            buttonOn3 = boolVar;
        }
        if (b == "B4")
        {
            buttonOn4 = boolVar;
        }
        if (b == "B5")
        {
            buttonOn5 = boolVar;
        }
        if (b == "B6")
        {
            buttonOn6 = boolVar;
        }
        if (b == "B7")
        {
            buttonOn7 = boolVar;
        }
        if (b == "B8")
        {
            buttonOn8 = boolVar;
        }
        if (b == "B9")
        {
            buttonOn9 = boolVar;
        }
        if (b == "C1")
        {
            buttonOnC1 = boolVar;
        }
        if (b == "C2")
        {
            buttonOnC2 = boolVar;
        }
        if (b == "C3")
        {
            buttonOnC3 = boolVar;
        }
    }


    private IEnumerator lightOnEnterNow()
    {

        enterLight.GetComponent<Animator>().SetBool("clicked", true);

        yield return new WaitForSeconds(0.5f);

        enterLight.GetComponent<Animator>().SetBool("clicked", false);

    }


    private IEnumerator clickbleOn()
    {
        clickble = false;
        yield return new WaitForSeconds(0.5f);
        clickble = true;
    }
}
