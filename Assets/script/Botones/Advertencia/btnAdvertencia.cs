using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnAdvertencia : MonoBehaviour
{
    public bool showMenuA;
    public void ButtonShowMenu()
    {
        if (!showMenuA)
        {
            showMenuA = true;
        }
        else if (showMenuA)
        {
            showMenuA = false;
        }

    }
}
