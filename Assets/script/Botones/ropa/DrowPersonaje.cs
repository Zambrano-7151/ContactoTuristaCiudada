﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrowPersonaje : MonoBehaviour
{
    private Animator btnAnim;
    private btnRopa botonR;
    void Start()
    {
        btnAnim = GetComponent<Animator>();
        botonR = GameObject.Find("RopaCode").GetComponent<btnRopa>();
    }

    // Update is called once per frame
    void Update()
    {
        if (botonR.showMenu)
        {
            btnAnim.SetBool("b_showMenuPR", true);
        }
        if (!botonR.showMenu)
        {
            btnAnim.SetBool("b_showMenuPR", false);
        }
    }
}
