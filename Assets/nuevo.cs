using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class nuevo : MonoBehaviour
{
    // public string url = "http://smartcityhuancayo.herokuapp.com/Usuario/login.php";
    public string url = "http://smartcityhyo.tk/api/Usuario/login.php";
    
    [Header("CONTROLS")]
    public InputField txtUsuario;
    public InputField txtContrasena;

    [Header("PANELS")]
    public GameObject welcomePanel;

    private Dictionary<string, string> _jsonResults = new Dictionary<string, string>();

    void Start()
    {
        // Default values
        txtUsuario.text = "asd@gmail.com";
        txtContrasena.text = "11111";
    }

    /// <summary>
    /// Login button OnClick event
    /// </summary>
    public void IniciarSesion()
    {
        StartCoroutine(IniciarSesionRoutine());
    }

    private string GetJsonBody()
    {
        return $"{{\"US_Email\":\"{txtUsuario.text}\",\"US_Contrasena\":\"{txtContrasena.text}\"}}";
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

        Debug.Log(_jsonResults["status"]);
        Debug.Log(_jsonResults["message"]);

        if (bool.Parse(_jsonResults["status"]))
        {
            welcomePanel.SetActive(true);

           // Debug.Log(_jsonResults["ID_Usuario"]);
          //  Debug.Log(_jsonResults["US_Email"]);
        }
        else
        {
            Debug.LogError(_jsonResults["message"]);
        }
    }

    IEnumerator IniciarSesionRoutine2()
    {
        UnityWebRequest www = new UnityWebRequest(url, UnityWebRequest.kHttpVerbGET);
        UploadHandlerRaw handler = new UploadHandlerRaw(GetJsonBinary());
        handler.contentType = "application/x-www-form-urlencoded"; // might work with 'multipart/form-data'
        www.uploadHandler = handler;

        yield return www.SendWebRequest();

        if (www.isHttpError || www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Result
            Debug.Log(www.downloadHandler);
        }
    }
}
//{
//    public InputField txtUsuario;
//    public InputField txtContrasena;
//    public GameObject welcomePanel;

//    string highscore_url = "http://smartcityhuancayo.herokuapp.com/Usuario/login.php?US_Email=" + txtUsuario.text + "&US_Contrasena="
//          + txtContrasena.text + "";

//    public void iniciarSesion()
//    {
//        StartCoroutine(login2());
//        StartCoroutine(datos());
//    }
//    IEnumerator login2()
//    {
//        var url = "http://smartcityhuancayo.herokuapp.com/Usuario/login.php?US_Email=" + txtUsuario.text + "&US_Contrasena="
//             + txtContrasena.text + "";


//        WWWForm form = new WWWForm();
//        form.AddField("US_Email", txtUsuario.text);
//        form.AddField("US_Contrasena", txtContrasena.text);
//        var inicioSesion = UnityWebRequest.Get(url);
//        yield return inicioSesion.SendWebRequest();
//        if (inicioSesion.isNetworkError || inicioSesion.isHttpError)
//        {
//            print("Error inicioSesion: " + inicioSesion.error);
//        }
//        else
//        {
//            show the highscores
//            Debug.Log(inicioSesion.downloadHandler.text);
//            welcomePanel.SetActive(true);
//        }
//    }
