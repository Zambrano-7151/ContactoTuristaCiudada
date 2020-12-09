using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;

public class RegistrarConexion : MonoBehaviour
{
    // Start is called before the first frame update
   // public InputField txtRUsuario;
    public InputField txtRContrasena;
    public InputField txtRCContrasena;
    public InputField txtREmail;
    public InputField txtRNombre;
    public InputField txtRApellido;
    public InputField txtRDireccion;
    public InputField txtRFechaNacimiento;
    public InputField txtRNacionalidad;
    public InputField txtRTelefono;
    public InputField txtRTipo;

    public string nombreUsuario;
    public string email;
    public string contrasena;    
    public string url = "http://smartcityhyo.tk/api/Usuario/Insert_Usuario.php";
    //public string url = "http://smartcityhuancayo.herokuapp.com/Usuario/Insert_Usuario.php";

    [Header("PANELS")]
    public GameObject welcomePanel;

    private Dictionary<string, string> _jsonResults = new Dictionary<string, string>();

    //public string Apellido;
    //public string Direccion;
    //public string FechaNacimiento;
    //public string Nacionalidad;
    //public string Telefono;
    //public string Tipo;
    public Text error;
    // var rect_movible: Rect;
    public void registrarUsuario()
    {
        //StartCoroutine(registrar());
        StartCoroutine(IniciarSesionRoutine());

    }
    private string GetJsonBody()
    {
        if (txtRCContrasena.text == txtRContrasena.text)
        {
            return $"{{\"US_Nombres\":\"{txtRNombre.text}\",\"US_Apellidos\":\"{txtRApellido.text}\",\"US_Direccion\":\"{txtRDireccion.text}\",\"US_Fecha_Nacimiento\":\"{txtRFechaNacimiento.text}\",\"US_Nacionalidad\":\"{txtRNacionalidad.text}\",\"US_Telefono\":\"{txtRTelefono.text}\",\"US_Email\":\"{txtREmail.text}\",\"US_Contrasena\":\"{txtRCContrasena.text}\",\"US_Tipo\":\"{txtRTipo.text}\"}}";
        }
        else
            return error.text = "Las dos contraseñas no coinciden"; 
        //return $"{{\"US_Nombres\":\"{txtRNombre.text}\",\"US_Apellidos\":\"{txtRApellido.text}\",\"US_Direccion\":\"{txtRDireccion.text}\",\"US_Fecha_Nacimiento\":\"{txtRFechaNacimiento.text}\",\"US_Nacionalidad\":\"{txtRNacionalidad.text}\",\"US_Telefono\":\"{txtRTelefono.text}\",\"US_Email\":\"{txtRCContrasena.text}\",\"US_Tipo\":\"{txtRTipo.text}\"}}";
    }

    private byte[] GetJsonBinary()
    {
        return System.Text.Encoding.UTF8.GetBytes(GetJsonBody());
    }
    private void JsonToDictionary(string json)
    {
        var jsonText = json.Substring(1, json.Length - 2);
        var parts = jsonText.Split(',');

        // Clear
        _jsonResults.Clear();

        for (int i = 0; i < parts.Length; i++)
        {
            var keyPair = parts[i].Split(':');
            var key = keyPair[0].Replace('"'.ToString(), "");
            var value = keyPair[1].Replace('"'.ToString(), "");
            _jsonResults.Add(key, value);
        }

    }

    IEnumerator IniciarSesionRoutine()
    {
        var www = new WWW(url, GetJsonBinary());

        yield return www;

        // Resultado
        // {"status":true,"message":"Login Satisfactorio!","ID_Usuario":"127","US_Email":"juan@gmail.com"}
        Debug.Log(www.text);

        JsonToDictionary(www.text);

       // Debug.Log(_jsonResults["status"]);
        Debug.Log(_jsonResults["message"]);

        if (_jsonResults["message"]== "Usuario creado correctamente.")
        {
            welcomePanel.SetActive(true);
            error.text = "Usuario creado correctamente";

        }
        else
        {
            Debug.LogError(_jsonResults["message"]);
            error.text = "Error en la creacion del nuevo usuario";
        }
    }

    //IEnumerator registrar2()
    //{
    //    //WWW coneccion = new WWW("http://localhost:8080/contacto/registro.php?uss=" + txtRUsuario.text + "&pss=" + txtRCContrasena.text + "&ema=" + txtREmail.text);
    //    WWW coneccion = new WWW("https://smartcityhuancayo.herokuapp.com/Insert_Usuario.php?US_Nombres="+txtRNombre.text+"&US_Apellidos="+txtRApellido.text+"&US_Direccion="
    //        +txtRDireccion.text+"&US_Fecha_Nacimiento="+txtRFechaNacimiento.text+"&US_Nacionalidad="+txtRNacionalidad.text+"&US_Telefono="+txtRTelefono.text+"&US_Email="
    //        +txtREmail.text+"&US_Contrasena="+txtRContrasena.text+"&US_Tipo="+txtRTipo.text);
    //    yield return (coneccion);
    //    if (coneccion.text == "402")
    //    {

    //       // error.text = "Usuario ya existe";
    //    }
    //    else
    //    {

    //        if (coneccion.text == "201")
    //        {
    //            if (txtRNombre.text == "" || txtREmail.text == "" || txtRContrasena.text == "")
    //            {
    //                // error.text = "Por favor llenar todos los campos";
    //                print("Por favor llenar todos los campos");
    //            }
    //            else

    //            {
    //                if (txtRContrasena.text.Length == 11)
    //                {
    //                    if (txtRContrasena.text == txtRCContrasena.text)
    //                    {
    //                        nombreUsuario = txtRNombre.text;
    //                        //Apellido = txtRApellido.text;
    //                        //Direccion = txtRDireccion.text;
    //                        //FechaNacimiento = txtRFechaNacimiento.text;
    //                        //Nacionalidad = txtRNacionalidad.text;
    //                        //Telefono = txtRTelefono.text;
    //                        //Tipo = txtRTipo.text;
    //                        email = txtREmail.text;
    //                        contrasena = txtRCContrasena.text;
    //                        //error.text = "se registro correctamente";
    //                        print("se registro correctamente");
    //                    }
    //                    else
    //                        //  error.text = "las contraseñas no son iguales";}
    //                        print("las contraseñas no son iguales");

    //                }
    //                else
    //                    //   error.text = "su contraseña es muy corta";
    //                    print("Su contraseña es muy corta");

    //            }
    //        }
    //        else
    //        {
    //            print(coneccion.text);
    //        }


    //    }
    //}

    //IEnumerator registrar()
    //{
    //    WWWForm form = new WWWForm();
    //    form.AddField("US_Nombres", txtRNombre.text);

    //    form.AddField("US_Apellidos", txtRApellido.text);

    //    form.AddField("US_Direccion", txtRDireccion.text);

    //    form.AddField("US_Fecha_Nacimiento", txtRFechaNacimiento.text);

    //    form.AddField("US_Nacionalidad", txtRNacionalidad.text);

    //    form.AddField("US_Telefono", txtRTelefono.text);

    //    form.AddField("US_Email", txtREmail.text);

    //    form.AddField("US_Contrasena", txtRCContrasena.text);

    //    form.AddField("US_Tipo", txtRTipo.text);

    //    var registro = UnityWebRequest.Post(highscore_url, form);

    //    // Wait until the download is done
    //    yield return registro.SendWebRequest();
    //    if (registro.isNetworkError || registro.isHttpError)
    //    {
    //        print("Error downloading: " + registro.error);
    //    }
    //    else
    //    {
    //        // show the highscores
    //        Debug.Log(registro.downloadHandler.text);
    //    }
    //}
}
