using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrowPanel : MonoBehaviour
{
    private Animator btnAnim;
    private btnSabiasQue botonR;
    void Start()
    {
        btnAnim = GetComponent<Animator>();
        botonR = GameObject.Find("SabiasCode").GetComponent<btnSabiasQue>();
    }

    // Update is called once per frame
    void Update()
    {
        if (botonR.showMenuS)
        {
            btnAnim.SetBool("b_showMenuPanel", true);
        }
        if (!botonR.showMenuS)
        {
            btnAnim.SetBool("b_showMenuPanel", false);
        }
    }
}
