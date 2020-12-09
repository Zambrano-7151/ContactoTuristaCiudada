using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrowButton : MonoBehaviour
{
    private Animator btnAnim;
    private Boton boton;
    void Start()
    {
        btnAnim = GetComponent<Animator>();
        boton = GameObject.Find("Code").GetComponent<Boton>();
    }

    // Update is called once per frame
    void Update()
    {
        if (boton.showMenu)
        {
            btnAnim.SetBool("b_showMenu", true);
        }
        if (!boton.showMenu)
        {
            btnAnim.SetBool("b_showMenu", false);
        }
    }
}
