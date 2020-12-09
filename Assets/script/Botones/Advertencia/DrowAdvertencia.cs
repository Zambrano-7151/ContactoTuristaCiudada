using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrowAdvertencia : MonoBehaviour
{
    private Animator btnAnim;
    private btnAdvertencia boton;
    void Start()
    {
        btnAnim = GetComponent<Animator>();
        boton = GameObject.Find("AdvertenciaCode").GetComponent<btnAdvertencia>();
    }

    // Update is called once per frame
    void Update()
    {
        if (boton.showMenuA)
        {
            btnAnim.SetBool("b_showMenuA", true);
        }
        if (!boton.showMenuA)
        {
            btnAnim.SetBool("b_showMenuA", false);
        }
    }
}
