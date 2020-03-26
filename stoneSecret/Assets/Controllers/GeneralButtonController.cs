using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GeneralButtonController : MonoBehaviour
{

    public static GeneralButtonController instance { set; get; }
    private Text timerText;
    private float timer;
    private int seconds = 0;
    private bool winGame = false;
    private bool looseGame = false;
    public bool isPaused = false;
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
    private GameObject looseMenu, winMenu, menuPause, timerAndPause, timerFinishLoose, timerFinishWin, record,
        medal, record2, medal2, timerObj;
    private string buttonName;
    //Contador de luzes on 
    private int contLightsOn = 0;
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
    private int timerHard;
    public int contWrong = 0;
    public int contEnterNoWin = 0;
    public int contLightsOnAnimation;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        StartCoroutine(ClickbleOn());
        enterLight = GameObject.Find("Enter");
        pauseButton = GameObject.Find("Pause");
        allButtons = GameObject.Find("Button");
        fireFlies = GameObject.Find("FireFlies");
        isPaused = false;

        // Scene
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
        // Valor do record na tela
        if (SaveManager.instance != null)
        {
            SaveState auxSave = SaveManager.instance.state;
            if (isEasy)
            {
                if (auxSave.easyWin)
                {
                    medal.SetActive(true);
                    record.GetComponent<Text>().text = auxSave.easyRecord.ToString();


                }
            }
            if (isMedium)
            {
                if (auxSave.mediumWin)
                {
                    medal.SetActive(true);
                    record.GetComponent<Text>().text = auxSave.mediumRecord.ToString();
                }
            }
            if (isHard)
            {
                if (auxSave.hardWin)
                {
                    medal.SetActive(true);
                    record.GetComponent<Text>().text = auxSave.hardRecord.ToString();
                }
            }
        }
        // Método de gerar as listas randomicas
        RandomLists();
        if (SaveManager.instance != null)
        {
            timerHard = SaveManager.instance.state.hardRecord;
            if (!isHard)
            {
                timer = 0.0f;
            }
            else
            {
                timer = timerHard + 1;
            }
        }
        else
        {
            if (!isHard)
            {
                timer = 0.0f;
            }
            else
            {
                timer = 160 + 1;
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        // Timer
        if (!isHard)
        {
            if (SaveManager.instance != null)
            {
                if (!winGame & !isPaused & !looseGame & SaveManager.instance.state.playedTuto)
                {
                    timer += Time.deltaTime;
                    seconds = (int)timer;
                    timerText.text = seconds.ToString();
                }
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
                MainVarControl(buttonName);
                // Bs   ---------------------------------------------------------------------
                if (buttonName.Length == 2 && controlVarMain == false && isPaused == false)
                {


                    SetVarControl(buttonName, true);
                    contLightsOn++;
                    contLightsOnAnimation = contLightsOn;
                    clickedButtons.Add(buttonName);
                    b.GetComponent<Animator>().SetBool("buttonClicked", true);
                    if (contLightsOn == 1)
                    {
                        SetMainList(buttonName);
                    }
                }
                else if (buttonName.Length == 2 && controlVarMain == true && isPaused == false)
                {

                    SetVarControl(buttonName, false);
                    contLightsOn--;
                    contLightsOnAnimation = contLightsOn;
                    clickedButtons.Remove(buttonName);
                    b.GetComponent<Animator>().SetBool("buttonClicked", false);
                }

                // Luz do botão Enter
                if (contLightsOn == qtdElementToWin)
                {

                    enterLight.GetComponent<Animator>().SetBool("win", true);

                }
                else
                {
                    enterLight.GetComponent<Animator>().SetBool("win", false);
                }

                if (hit.collider.tag == "Enter" && contLightsOn < qtdElementToWin)
                {

                    StartCoroutine(LightOnEnterNow());
                    contEnterNoWin++;
                }

                //Pause
                if (hit.collider.tag == "Pause" && isPaused == false && !winGame)
                {

                    isPaused = true;
                    pauseButton.GetComponent<Animator>().SetBool("isPaused", true);

                    Vector3 aux1 = allButtons.GetComponent<Transform>().position;
                    Vector3 aux2 = menuPause.GetComponent<Transform>().position;
                    allButtons.GetComponent<Transform>().position = new Vector3(-999, aux1.y, aux1.z);
                    menuPause.GetComponent<Transform>().position = new Vector3(0, aux2.y, aux2.z);
                }
                else if (hit.collider.tag == "Pause" && isPaused == true && !winGame)
                {

                    isPaused = false;
                    pauseButton.GetComponent<Animator>().SetBool("isPaused", false);

                    Vector3 aux1 = allButtons.GetComponent<Transform>().position;
                    Vector3 aux2 = menuPause.GetComponent<Transform>().position;
                    allButtons.GetComponent<Transform>().position = new Vector3(0, aux1.y, aux1.z);
                    menuPause.GetComponent<Transform>().position = new Vector3(-999, aux2.y, aux2.z);
                }





                // Condição para ganhar
                if (isEasy == true)
                {

                    if (contLightsOn == 9 && hit.collider.tag == "Enter")
                    {

                        Debug.Log("win!!!");
                        winGame = true;




                    }
                }
                else if (isMedium == true)
                {
                    if (contLightsOn == 12 && hit.collider.tag == "Enter")
                    {
                        Debug.Log("win!!!");
                        winGame = true;


                    }
                }
                else if (isHard == true)
                {
                    if (contLightsOn == 12 && hit.collider.tag == "Enter")
                    {

                        if (timer > 0)
                        {
                            Debug.Log("win!!!");
                            winGame = true;

                        }
                        else
                        {
                            AllLightsOff();
                        }
                    }
                } // Condição ganhar

            }




        } // End click
        if (SaveManager.instance != null)
        {
            if (!SaveManager.instance.state.playedTuto)
            {
                timerObj.SetActive(false);
                if (contLightsOn > 0)
                {
                    if (contLightsOn < mainList.Count)
                    {
                        string nameNext = mainList[contLightsOn];
                        GameObject aux = GameObject.Find(nameNext);
                        aux.GetComponent<Animator>().SetBool("blink", true);
                    }
                }
                else
                {
                    foreach(string a in mainList)
                    {
                        GameObject aux = GameObject.Find(a);
                        aux.GetComponent<Animator>().SetBool("blink",false);
                    }
                }
            }
        }

        if (SaveManager.instance != null)
        {
            if (!SaveManager.instance.state.bixinhoActivation)
            {
                contEnterNoWin = 0;
                contWrong = 0;
                contLightsOnAnimation = 0;
            }
        }


        if (isEasy | isMedium)
        {
            if (seconds >= 9999)
            {
                looseGame = true;
            }
        }
        if (isHard)
        {
            if (seconds <= 0)
            {
                looseGame = true;
            }
        }

        // Win menu
        //
        if (!isHard)
        {
            if (winGame)
            {
                SaveState auxSave = SaveManager.instance.state;
                if (auxSave.playedTuto)
                {
                    Vector3 aux = allButtons.GetComponent<Transform>().position;
                    allButtons.GetComponent<Transform>().position = new Vector3(-999, aux.y, aux.z);
                    winMenu.SetActive(true);
                    timerAndPause.SetActive(false);
                    Text textTimerWin;
                    textTimerWin = timerFinishWin.GetComponent<Text>();
                    textTimerWin.text = timerText.text;
                    ParticleSystem.EmissionModule aux2 = fireFlies.GetComponent<ParticleSystem>().emission;
                    aux2.rateOverTime = 7;

                }
                //EasyRecord
                if (isEasy)
                {


                    if (auxSave.easyWin)
                    {
                        if (auxSave.playedTuto)
                        {
                            if (seconds < auxSave.easyRecord)
                            {
                                auxSave.easyRecord = seconds;
                                auxSave.breakRecordEasy = true;
                                SaveManager.instance.Save();
                            }
                            record2.GetComponent<Text>().text = auxSave.easyRecord.ToString();
                            medal2.SetActive(true);
                        }

                    }
                    else
                    {
                        if (auxSave.playedTuto)
                        {
                            auxSave.easyRecord = seconds;
                            auxSave.easyWin = true;

                            SaveManager.instance.Save();
                        }
                    }
                    if (!auxSave.playedTuto)
                    {
                        auxSave.playedTuto = true;
                        SaveManager.instance.Save();
                        SceneManager.LoadScene("GameEasy");
                    }

                }


                // MedumRecord
                if (isMedium)
                {
                    if (auxSave.mediumWin)
                    {
                        if (seconds < auxSave.mediumRecord)
                        {
                            auxSave.mediumRecord = seconds;
                            auxSave.breakRecordMedium = true;
                            SaveManager.instance.Save();
                        }
                        record2.GetComponent<Text>().text = auxSave.mediumRecord.ToString();
                        medal2.SetActive(true);
                    }
                    else
                    {
                        auxSave.mediumRecord = seconds;
                        auxSave.mediumWin = true;
                        SaveManager.instance.Save();
                    }
                }



            }

        }
        else
        {
            if (winGame)
            {
                Vector3 aux = allButtons.GetComponent<Transform>().position;
                allButtons.GetComponent<Transform>().position = new Vector3(-999, aux.y, aux.z);
                winMenu.SetActive(true);
                timerAndPause.SetActive(false);
                Text textTimerWin;
                textTimerWin = timerFinishWin.GetComponent<Text>();
                int resto = timerHard - seconds;
                textTimerWin.text = resto.ToString();
                ParticleSystem.EmissionModule aux2 = fireFlies.GetComponent<ParticleSystem>().emission;
                aux2.rateOverTime = 7;
                SaveState auxSave = SaveManager.instance.state;




                if (auxSave.hardWin)
                {
                    record2.GetComponent<Text>().text = auxSave.hardRecord.ToString();
                    medal2.SetActive(true);

                    if (resto < auxSave.hardRecord)
                    {
                        auxSave.hardRecord = resto;
                        auxSave.breakRecordHard = true;
                        SaveManager.instance.Save();
                    }

                }
                else
                {
                    auxSave.hardWin = true;
                    if (resto < auxSave.hardRecord)
                    {
                        auxSave.hardRecord = resto;
                        SaveManager.instance.Save();
                    }
                }
            }
        }

        if (contLightsOn >= 4)
        {
            contWrong = 0;
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
        if (contLightsOn == 2 && clickedButtons[1] != mainList[1])
        {
            AllLightsOff();

        }

        if (contLightsOn == 3 && clickedButtons[2] != mainList[2])
        {
            AllLightsOff();

        }

        if (contLightsOn == 4 && clickedButtons[3] != mainList[3])
        {
            AllLightsOff();
        }

        if (contLightsOn == 5 && clickedButtons[4] != mainList[4])
        {
            AllLightsOff();
        }

        if (contLightsOn == 6 && clickedButtons[5] != mainList[5])
        {
            AllLightsOff();
        }

        if (contLightsOn == 7 && clickedButtons[6] != mainList[6])
        {
            AllLightsOff();
        }

        if (contLightsOn == 8 && clickedButtons[7] != mainList[7])
        {
            AllLightsOff();
        }
        if (contLightsOn == 9 && clickedButtons[8] != mainList[8])
        {
            AllLightsOff();
        }
        if (isMedium == true | isHard == true)
        {
            if (contLightsOn == 10 && clickedButtons[9] != mainList[9])
            {
                AllLightsOff();
            }
            if (contLightsOn == 11 && clickedButtons[10] != mainList[10])
            {
                AllLightsOff();
            }

        }





    }// Update end

    public void RandomLists()
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

    public void AllLightsOff()
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
        contLightsOn = 0;
        contLightsOnAnimation = 0;
        contWrong++;
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


    public void MainVarControl(string b)
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
    public void SetMainList(string b)
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


    public void SetVarControl(string b, bool boolVar)
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


    private IEnumerator LightOnEnterNow()
    {

        enterLight.GetComponent<Animator>().SetBool("clicked", true);

        yield return new WaitForSeconds(0.5f);

        enterLight.GetComponent<Animator>().SetBool("clicked", false);

    }


    private IEnumerator ClickbleOn()
    {
        clickble = false;
        yield return new WaitForSeconds(0.5f);
        clickble = true;
    }
}
