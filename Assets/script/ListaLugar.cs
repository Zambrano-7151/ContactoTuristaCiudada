using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;
using System.Linq;
using SimpleJSON;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

public class Turismo
{
    public string Nombre;
    public Sprite Imagen;
    public string Descripcion;
    private string Id;
    //public string Tipo;

}

public class ListaLugar : MonoBehaviour
{
    public Sprite Huaytapallana;
    public Sprite LagunaPaca;
    public Sprite Ingenio;
    public GameObject PrefabTurismo;
    public Transform Contenedor;



    // Start is called before the first frame update
    void Start()
    {
        UnityWebRequest request = UnityWebRequest.Get("http://smartcityhyo.tk/api/Fotografia_Lugar/Listar_Fotografia.php");
        StartCoroutine(OnReponseLugarTuristico(request));

        //List<Turismo> LTurismo = new List<Turismo> {
        //    new Turismo{Nombre="Huaytapallana", Descripcion="Es un lugar ubicado en la ciudad de Huancayo"+"Y tiene una nevada muy hermoso", Tipo="Huancayo", Imagen=Huaytapallana},
        //    new Turismo{Nombre="LagunaPaca", Descripcion="Es una lajuna ubidado en el distrito depaca tiene una profundidad de 5000msm"+"Es un lugra ubidado en el provincia de Juaja", Tipo="Jauja", Imagen=LagunaPaca},
        //    new Turismo{Nombre="Ingenio", Descripcion="Es un lugar donde se cria truchas"+ "Ubicado en el distrito de concepcion", Tipo="Concepcion", Imagen=Ingenio}
        //    };
        //for (int i = 0; i <3; i++)
        //{
        //    foreach (var item in LTurismo)
        //    {
        //        GameObject _turismo = Instantiate(PrefabTurismo, Contenedor);
        //        _turismo.GetComponent<DetallesLugar>().Crear(item);
        //    }
        //}


    }

    private IEnumerator OnReponseLugarTuristico(UnityWebRequest req)
    {
        //var contentBytes = new UTF8Encoding().GetBytes(contentBody);
        //req.uploadHandler = new UploadHandlerRaw(contentBytes);
        req.downloadHandler = new DownloadHandlerBuffer();
        //Console.WriteLine(req);  
        //

        yield return req.SendWebRequest();

        //print(req.downloadHandler.text);
        Debug.Log(req.downloadHandler.text);
        if (req.isNetworkError || req.isHttpError)
        {
            Debug.Log(req.error);
        }
        else
        {
            //Debug.Log(req.downloadHandler.text);
            JSONNode data = JSON.Parse(req.downloadHandler.text);
            loadJson(data);


        }

    }
    public async void loadJson(JSONNode data)
    {

        List<Turismo> LTurismo = new List<Turismo>();
        foreach (JSONNode item in data["fotos"])
        //for (int i = 0; i < data["fotos"]; i++)

        {
            //JSONNode item = data["fotos"][i];
            //JSONNode lugar = JSON.Parse(item);
            Debug.Log(item);
            Turismo turismo = new Turismo();
            turismo.Nombre = item["FG_Descripcion"];
            turismo.Descripcion = item["Lugar"];
            //turismo.Tipo = item["LT_Hora_Inicio"];
            if (item["FG_URL"] == "")
            {
                turismo.Imagen = Ingenio;

            }
            else
            {
                //turismo.Imagen = Ingenio;
                Sprite imagen = await LoadURL(item["FG_URL"]);
                turismo.Imagen = imagen;

                Debug.Log("load image ========");
                //turismo.Imagen = resp;
                LTurismo.Add(turismo);

            }
            //Thread.Sleep(10000);

        }
        foreach (var item in LTurismo)
        {
            GameObject _turismo = Instantiate(PrefabTurismo, Contenedor);
            _turismo.GetComponent<DetalleLugar>().Crear(item);
            
        }
    }
    //public void OpenPanel(int id)
    //{
    //    MyPanel panel = _panels.FirstOrDefault((p) => p.id == id);

    //    ActivePanel(panel);
    //}

    //private void ActivePanel(MyPanel panel)
    //{
    //    foreach (var myPanel in _panels)
    //    {
    //        if (panel == myPanel)
    //        {
    //            myPanel.gameObject.SetActive(true);
    //        }
    //        else
    //        {
    //            myPanel.gameObject.SetActive(false);
    //        }
    //    }
    //}
    private async Task<Sprite> LoadURL(string url)
    {

        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        await request.SendWebRequest();
        //if (request.isNetworkError || request.isHttpError)
        //{
        //    Debug.Log(request.error);
        //    return null;
        //}
        //else
        //{
        //    if (request.isDone){
        Texture2D texture = DownloadHandlerTexture.GetContent(request);
        Debug.Log("Descargando Imagen");
        float ancho = texture.width;
        float alto = texture.height;
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        //yield break;
        // }
        //}

    }
}
public class UnityWebRequestAwaiter : INotifyCompletion
{
    private UnityWebRequestAsyncOperation asyncOp;
    private Action continuation;

    public UnityWebRequestAwaiter(UnityWebRequestAsyncOperation asyncOp)
    {
        this.asyncOp = asyncOp;
        asyncOp.completed += OnRequestCompleted;
    }

    public bool IsCompleted { get { return asyncOp.isDone; } }

    public void GetResult() { }

    public void OnCompleted(Action continuation)
    {
        this.continuation = continuation;
    }

    private void OnRequestCompleted(AsyncOperation obj)
    {
        continuation();
    }
}

public static class ExtensionMethods
{
    public static UnityWebRequestAwaiter GetAwaiter(this UnityWebRequestAsyncOperation asyncOp)
    {
        return new UnityWebRequestAwaiter(asyncOp);
    }
}


