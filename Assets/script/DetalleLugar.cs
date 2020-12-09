using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetalleLugar : MonoBehaviour
{
    public Text Nombre;
    public Image Imagen;
    public Text Descripcion;
    private Text id;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void Crear(Turismo turismo)
    {
        Nombre.text = turismo.Nombre;
        Imagen.sprite = turismo.Imagen;
        Descripcion.text = turismo.Descripcion;
    }
   
}
