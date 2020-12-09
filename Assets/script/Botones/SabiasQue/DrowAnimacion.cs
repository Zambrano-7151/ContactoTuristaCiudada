using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrowAnimacion : MonoBehaviour
{
    private Animator btnAnim;
    private btnRopa botonR;
    void Start()
    {
        btnAnim = GetComponent<Animator>();
        botonR = GameObject.Find("SabiasCode").GetComponent<btnRopa>();
    }

    // Update is called once per frame
    void Update()
    {
        if (botonR.showMenu)
        {
            btnAnim.SetBool("b_showMenuPanel", true);
        }
        if (!botonR.showMenu)
        {
            btnAnim.SetBool("b_showMenuPanel", false);
        }
    }
}
