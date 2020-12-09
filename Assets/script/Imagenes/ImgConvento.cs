using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using SimpleJSON;
using System;

public class ImgConvento : MonoBehaviour
{
    private const string WEB_URL = "http://smartcityhyo.tk/api/LugarTuristico/Consultar_Lugar_Turistico_ID.php?ID_Lugar_Turistico=23";
    private const string API_URL = "http://smartcityhuancayo.herokuapp.com/";
    public Text titulo;
    public Image Foto;



    //public InputField titulo;

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

        if (req.isNetworkError || req.isHttpError)
        {
            Debug.Log(req.error);
        }
        else
        {
            //Debug.Log(req.downloadHandler.text);
            JSONNode data = JSON.Parse(req.downloadHandler.text);
            titulo.text = data["LT_Nombre"];

            //StartCoroutine(LoadURL(data["LT_Image"]));
            StartCoroutine(LoadURL("https://cde.peru.com//ima/0/1/7/1/8/1718597/924x530/santa-rosa-ocopa.jpg", (resp) => {
                Foto.sprite = resp;
            }));
            //StartCoroutine(LoadURL("https://www.canalipe.tv/sites/default/files/styles/895x490/public/web/noticias/img_main/04-17/huaytapallana_00.jpg?itok=aSTwgRfn", (resp) => {
            //    Foto1.sprite = resp;
            //}));
            //Foto1.sprite = StartCoroutine(LoadURL("https://www.canalipe.tv/sites/default/files/styles/895x490/public/web/noticias/img_main/04-17/huaytapallana_00.jpg?itok=aSTwgRfn"));


        }
    }
}
