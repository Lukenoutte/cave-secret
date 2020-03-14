using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationController : MonoBehaviour
{
    private GameObject bixinho, bixinho1Menu, bixinho2Menu, mainMenu;

    public GameObject bixinho3Dif, bixinho4Menu, difficulty, bixinho5Dif, configuration, bixinho6Config, bixinho7Config, bixinho8Menu;
    private string animName;
    private bool animOnMenu;
    private bool animOnDif;
    private bool animOnConfig;
    public int idAnimMenu;
    public int idAnimDif;
    public int idAnimConfig;

    // Start is called before the first frame update
    void Start()
    {

        animOnMenu = false;
        animOnDif = false;
        animOnConfig = false;
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
                    StartCoroutine(eraseAnim(bixinho));
                }
            }
        }
        // Main menu animations
        if (mainMenu.activeSelf == false)
        {
            animOnMenu = false;
            bixinho1Menu.SetActive(false);
            bixinho2Menu.SetActive(false);
            bixinho4Menu.SetActive(false);
            bixinho8Menu.SetActive(false);
        }
        else if (animOnMenu == false)
        {
            bool AnimActive = false;
            int oldIdMenu = idAnimMenu;
            idAnimMenu = Random.Range(1, 5);
            while (oldIdMenu == idAnimMenu)
            {
                idAnimMenu = Random.Range(1, 5);
            }

            if (bixinho2Menu.activeSelf | bixinho4Menu.activeSelf | bixinho8Menu.activeSelf | bixinho1Menu.activeSelf)
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
        } // End

        // Dif animations
        if (difficulty.activeSelf == false)
        {
            animOnDif = false;
            bixinho3Dif.SetActive(false);
            bixinho5Dif.SetActive(false);

        }
        else if (animOnDif == false)
        {
            int oldIdDif = idAnimDif;
            bool AnimActive = false;
            idAnimDif = Random.Range(1, 3);
            while (oldIdDif == idAnimDif)
            {
                idAnimDif = Random.Range(1, 3);
            }

            if (bixinho5Dif.activeSelf | bixinho3Dif.activeSelf)
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
        } // End


        // Config animations
        if (configuration.activeSelf == false)
        {
            animOnConfig = false;
            bixinho6Config.SetActive(false);
            bixinho7Config.SetActive(false);


        }
        else if (animOnConfig == false)
        {

            int oldIdConfig = idAnimConfig;
            bool AnimActive = false;
            idAnimConfig = Random.Range(1, 3);
            while (oldIdConfig == idAnimConfig)
            {
                idAnimConfig = Random.Range(1, 3);
            }

            if (bixinho7Config.activeSelf | bixinho6Config.activeSelf)
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

        } // End 
    }

    private IEnumerator eraseAnim(GameObject anim)
    {
        GameObject aux = anim;
        aux.GetComponent<Animator>().SetBool("clicked", true);


        yield return new WaitForSeconds(3);
        aux.SetActive(false);
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


}
