using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrowAventura : MonoBehaviour
{
    private Animator btnAnim;
    private btnAventura boton;
    void Start()
    {
        btnAnim = GetComponent<Animator>();
        boton = GameObject.Find("AventuraCode").GetComponent<btnAventura>();
    }

    // Update is called once per frame
    void Update()
    {
        if (boton.showMenu)
        {
            btnAnim.SetBool("b_showMenuAV", true);
        }
        if (!boton.showMenu)
        {
            btnAnim.SetBool("b_showMenuAV", false);
        }
    }
}
