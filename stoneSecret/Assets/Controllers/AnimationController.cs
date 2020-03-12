using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationController : MonoBehaviour
{
    private GameObject bixinho, bixinho1Menu, bixinho2Menu, mainMenu;
    [SerializeField]
    private GameObject  bixinho3Dif, bixinho4Menu, difficulty;
    private string animName;
    private bool animOnMenu;
    private bool animOnDif;
    public int idAnim;


    // Start is called before the first frame update
    void Start()
    {

        animOnMenu = false;
        animOnDif = false;
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
        }
        else if (animOnMenu == false)
        {
            int oldId = idAnim;
            idAnim = Random.Range(1, 4);
            while (oldId == idAnim)
            {
                idAnim = Random.Range(1, 4);
            }


            if (idAnim == 1 && animOnMenu == false && !bixinho2Menu.activeSelf && !bixinho4Menu.activeSelf)
            {
                animOnMenu = true;
                bixinho1Menu.SetActive(true);



            }
            if (idAnim == 2 && animOnMenu == false && !bixinho1Menu.activeSelf && !bixinho4Menu.activeSelf)
            {
                animOnMenu = true;
                bixinho2Menu.SetActive(true);

            }
            if (idAnim == 3 && animOnMenu == false && !bixinho1Menu.activeSelf && !bixinho2Menu.activeSelf)
            {
                animOnMenu = true;
                bixinho4Menu.SetActive(true);

            }
        } // End

        // Dif animations
        if (difficulty.activeSelf == false)
        {
            animOnDif = false;
            bixinho3Dif.SetActive(false);

        }
        else if (animOnDif == false)
        {
            bixinho3Dif.SetActive(true);
            animOnDif = true;
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

    }


}
