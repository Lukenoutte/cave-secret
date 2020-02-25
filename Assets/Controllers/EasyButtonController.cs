using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EasyButtonController : MonoBehaviour
{

    /// Booleans que verificam qual luz está acesa 
    private Text TimerText;
    float timer = 0.0f;
    int seconds = 0;
    private bool winGame=false;
    private bool buttonOn1 = false;
    private bool buttonOn2 = false;
    private bool buttonOn3 = false;
    private bool buttonOn4 = false;
    private bool buttonOn5 = false;
    private bool buttonOn6 = false;
    private bool buttonOn7 = false;
    private bool buttonOn8 = false;
    private bool buttonOn9 = false;
    private bool pButtonOn1 = false;
    private bool controlVarMain = false;

    private List<string> generalList = new List<string>();
    public List<string> mainList; // Lista responsavel por controlar o jogo, escolhida dps que o usuário clicar no primeiro elemento.
    private GameObject b;
    private string buttonName = " ";
    //Contador de luzes on 
    private int countLightsOn = 0;
    // Listas que irão receber a ordem dos botões
    List<string> b1List = new List<string>();
    List<string> b2List = new List<string>();
    List<string> b3List = new List<string>();
    List<string> b4List = new List<string>();
    List<string> b5List = new List<string>();
    List<string> b6List = new List<string>();
    List<string> b7List = new List<string>();
    List<string> b8List = new List<string>();
    List<string> b9List = new List<string>();
    List<string> clickedButtons = new List<string>();
    //Lista que contem as lista a cima
    List<List<string>> listOfLists = new List<List<string>>();

    // Start is called before the first frame update
    void Start()
    {
        TimerText = GameObject.Find("Timer").GetComponent<Text>();

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

        // Add Listas a lista
        listOfLists.AddRange(new List<string>[] { b1List, b2List, b3List, b4List, b5List, b6List, b7List, b8List, b9List });



        // Método de gerar as listas randomicas
        randomLists();
    }

    // Update is called once per frame
    void Update()
    {

        // Timer
        if (winGame == false)
        {
            timer += Time.deltaTime;
            seconds = (int)timer;
            TimerText.text = seconds.ToString();
        }
        if (Input.GetMouseButtonDown(0))
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
                if (buttonName.Length == 2 && controlVarMain == false)
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
                else if (buttonName.Length == 2 && controlVarMain == true)
                {

                    setVarControl(buttonName, false);
                    countLightsOn--;
                    clickedButtons.Remove(buttonName);
                    b.GetComponent<Animator>().SetBool("buttonClicked", false);
                }


                // Condição para ganhar
                if (countLightsOn == 9 && hit.collider.tag == "Enter")
                {

                    Debug.Log("win!!!");
                    winGame = true; 

                }
            }
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



    }

    public void randomLists()
    {
        int countList = 8;
        foreach (List<string> subList in listOfLists)
        {
            // Lista antes de se tornar randomica
            generalList.AddRange(new string[] { "B1", "B2", "B3", "B4", "B5", "B6", "B7", "B8", "B9" });
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
        auxList.AddRange(new string[] { "B1", "B2", "B3", "B4", "B5", "B6", "B7", "B8", "B9" });
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
    }

}

