using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;
using System.Collections;
using System.Linq;
using SimpleJSON;
using TMPro;
using System.Text;

public class TurismoConexion : MonoBehaviour
{
    private const string WEB_URL = "http://smartcityhyo.tk/api/LugarTuristico/Consultar_Lugar_Turistico_ID.php?ID_Lugar_Turistico=12";
    public Text titulo;
    public Text descripcion;
    public Image Foto;
    public Image Foto1;
    //public Text provincia;
    public Text distrito;
    public Text HoraInicio;
    public Text HoraFin;

    [Header("DEBUG")] [SerializeField] 
    private TextMeshProUGUI _logText;
    public String Lat;
    public String Long;
    public float minimumDistance = 100;


    private bool _firstTime = false;
    private bool _alreadyVisited = false;

    private int focusTime;
    private int pauseTime;

    //public InputField titulo;
    void OnApplicationPause(bool focusing)
    {
        focusTime++;
         _logText.text = $"Pause {focusing} {focusTime}";

        if (focusing == true) return;


        if (_firstTime == true)
        {
            if (_alreadyVisited == true) return;

            _logText.text = $"Gps loading...";

            var gpsService = new GpsService((data) =>
            {

                Lat = "-11.896950";
                Long = "-75.049550";

                var distance = GpsService.Distance(
                    (double)data.latitude, (double)data.longitude,
                    double.Parse(Lat), double.Parse(Long));

                if (distance <= minimumDistance)
                {
                    //sendAddVista();
                    _logText.text = $"Distance: {distance} (Llegó!)";
                    _alreadyVisited = true;
                }
                else
                {
                    _logText.text = $"Distance: {distance} (No llegó)";

                }
            }, null);

            StartCoroutine(gpsService.StartServiceRoutine());
        }
        else
        {
            _firstTime = true;
            _logText.text = $"First time {_firstTime}";
        }
    }
    public void lugaresTuristicos()
    {
        UnityWebRequest request = UnityWebRequest.Get(WEB_URL);

        StartCoroutine(OnReponse(request));
    }

    private IEnumerator LoadURL(string url, Action<Sprite> onResponse)
    {
        Debug.Log("load image ========");
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();
        Texture2D texture = DownloadHandlerTexture.GetContent(request);
        Debug.Log(texture);
        onResponse(Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f)));
        yield break;


    }

    private IEnumerator OnReponse(UnityWebRequest req)
    {
        //var contentBytes = new UTF8Encoding().GetBytes(contentBody);
        //req.uploadHandler = new UploadHandlerRaw(contentBytes);
        req.downloadHandler = new DownloadHandlerBuffer();
        //Console.WriteLine(req);  
        //

        yield return req.SendWebRequest();

        //print(req.downloadHandler.text);
        titulo.text = req.downloadHandler.text;
        descripcion.text = req.downloadHandler.text;

        //provincia.text = req.downloadHandler.text;
        distrito.text = req.downloadHandler.text;
        HoraInicio.text = req.downloadHandler.text;
        HoraFin.text = req.downloadHandler.text;
        Debug.Log(req.downloadHandler.text);
        if (req.isNetworkError || req.isHttpError)
        {
            Debug.Log(req.error);
        }
        else
        {
            //Debug.Log(req.downloadHandler.text);
            JSONNode data = JSON.Parse(req.downloadHandler.text);
            titulo.text = data["LT_Nombre"];
            descripcion.text = data["LT_Descripcion"];
            //provincia.text = data["LT_Nombre"];
            distrito.text = data["Distrito"];
            HoraInicio.text = data["LT_Hora_Inicio"];
            HoraFin.text = data["LT_Hora_Fin"];
            //StartCoroutine(LoadURL(data["LT_Image"]));
            StartCoroutine(LoadURL("https://cde.peru.com//ima/0/0/9/4/7/947266/924x530/nevado-huaytapallana.jpg", (resp) =>
            {
                Foto.sprite = resp;
            }));
            StartCoroutine(LoadURL("https://www.canalipe.tv/sites/default/files/styles/895x490/public/web/noticias/img_main/04-17/huaytapallana_00.jpg?itok=aSTwgRfn", (resp) =>
            {
                Foto1.sprite = resp;
            }));
            //Foto1.sprite = StartCoroutine(LoadURL("https://www.canalipe.tv/sites/default/files/styles/895x490/public/web/noticias/img_main/04-17/huaytapallana_00.jpg?itok=aSTwgRfn"));
            Lat = data["\tLT_Latitud"];
            Long = data["LT_Longitud"];

        }
    }

    [ContextMenu("AbrirMapa")]
    public void AbrirMapa()
    {
        sendAddVista();
        // Destino estatico, mas adelante, cambiar por un valor obtenido de la Base de datos
        Lat = "-11.896950";
        Long = "-75.049550";

        var gpsService = new GpsService((data) =>
        {
            //Application.OpenURL("https://www.google.com/maps/search/?api=1&query=" + Lat + "," + Long);
            var url = $"https://www.google.com/maps/dir/?api=1&origin={data.latitude},{data.longitude}&destination=" +
                      Lat + "," + Long + "&travelmode=driving";
            Debug.Log(url);
            Application.OpenURL(url);
        }, _logText);

        StartCoroutine(gpsService.StartServiceRoutine());
    }
    private IEnumerator OnReponseVisita(UnityWebRequest req)
    {

        req.downloadHandler = new DownloadHandlerBuffer();


        yield return req.Send();


        Debug.Log(req.downloadHandler.text);
        if (req.isNetworkError || req.isHttpError)
        {
            Debug.Log(req.error);
        }
        else
        {

            JSONNode data = JSON.Parse(req.downloadHandler.text);
            Debug.Log("Completado!");
        }
    }

    private void sendAddVista()
    {
        var dataJson = "{\"ID_Lugar_Turistico\": 12}";
        var request = new UnityWebRequest("http://smartcityhyo.tk/api/LugarTuristico/Insertar_Visitas_Lugar.php", "POST");
        byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(dataJson);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        StartCoroutine(OnReponseVisita(request));
        Debug.Log("Completado!");
    }

}
