using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnSabiasQue : MonoBehaviour
{
    public bool showMenuS;
    public void ButtonShowMenu()
    {
        if (!showMenuS)
        {
            showMenuS = true;
        }
        else if (showMenuS)
        {
            showMenuS = false;
        }

    }
}
