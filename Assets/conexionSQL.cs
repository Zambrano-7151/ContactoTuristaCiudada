using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class conexionSQL : MonoBehaviour
{
    public InputField txtUsuario;
    public InputField txtContrasena;
    public InputField txtRUsuario;
    public InputField txtRContrasena;
    public InputField txtREmail;

    public string email;
    public string contrasena;

    public bool sesionIniciada;

    public GameObject welcomePanel;
    public Text error;

    //public Text titulo;
    //public Text descripcion;
    //public Text provincia;
    //public Text distrito;
    //public Text horaInicio;
    //public Text horaFin;




    // Start is called before the first frame update
    /// <summary>
    ///  400 - No pudo establecer conexion
    ///  401 - No encontro datos
    ///  402 - el usuario ya existe
    ///  200 - datos encontrados
    ///  201 - Usuario Registrado   
    /// 
    /// </summary>
    public void iniciarSesion()
    {
        StartCoroutine(login());
        //StartCoroutine(datosLugar());
    }



    IEnumerator login()
    {
        //WWW coneccion = new WWW("http://localhost:8080/contacto/login.php?uss="+txtUsuario.text+"&pss="+txtContrasena.text+"");
        WWW coneccion = new WWW("http://smartcityhuancayo.herokuapp.com/Usuario/login.php?US_Email=" + txtUsuario.text + "&US_Contrasena="
            + txtRContrasena.text + "");

        yield return (coneccion);
        if (coneccion.text == "true")
        {
            print("El usuario si existe");
            welcomePanel.SetActive(true);
            //User.text = txtUsuario.text;
        }
        else //if (coneccion.text == "401")
        {
            error.text = "Usuario o contraseña incorrectos";
        }
        //else
        //{
        //   error.text = "Error en la conexion con la base";

        //}
    }

    //IEnumerator datosLugar()
    //{
    //    WWW coneccion = new WWW("http://localhost:8080/contacto/Prueba.php?uss=1");
    //    yield return (coneccion);
    //    string[] nDatos = coneccion.text.Split('|');
    //    if (nDatos.Length != 2)
    //    {
    //        titulo.text = "Error en la conexion";
    //    }
    //    else
    //    {
    //        titulo = nDatos[0];
    //        descripcion = nDatos[1];
            
    //    }

    //}
}
//IEnumerator registrar()
//{
//    WWW coneccion = new WWW("http://localhost:8080/contacto/registro.php?uss=" + txtRUsuario.text+ "&pss=" + txtRContrasena.text +"&ema="+txtREmail.text+"");
//    yield return (coneccion);
//    if (coneccion.text == "402")
//    {
//        error.text = "Usuario ya existe";
//    }
//    else
//    {
//       // string[] nDatos = coneccion.text.Split('|');
//        if (coneccion.text == "201")
//        {
//            nombreUsuario = txtRUsuario.text;
//            email = txtREmail.text;
//            contrasena = txtRContrasena.text;
//            sesionIniciada = true;
//        }
//        else
//        {
//            error.text = "Error en la conexion ";
//        }
//    }
//}