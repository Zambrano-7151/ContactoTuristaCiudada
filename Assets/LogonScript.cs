using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System;

public class LogonScript : MonoBehaviour
{
    public GameObject email;
    public GameObject password;
    public GameObject login;

    public Button btnLogin;

    private string Email;
    private string Password;

    //now we create some model class to get response

  [Serializable]
    public class UserDetail
    {
      public string message;
        public bool status;
        public Data data;
    }
    [Serializable]
    public class Data
    {
        public string ID_Usuario;
    }

    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;

        btnLogin = login.GetComponent<Button>();
        btnLogin.onClick.AddListener(validateLogin);
    }

    // Update is called once per frame
    void Update()
    {
        //Email = email.GetComponent<InputField> ().text;
        //Password = password.GetComponent<InputField> ().text;

    }

    private void validateLogin()
    {
        Email = email.GetComponent<InputField>().text;
        Password = password.GetComponent<InputField> ().text;
        if (Email != "" && Password != "")
        {
            //  print("Login Success");
            // SceneManager.LoadScene(1);
            StartCoroutine(CallLogin(Email,Password));
        }
        else
        {

        }
    }

    public IEnumerator CallLogin(string Email, string Password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", Email);
        form.AddField("password", Email);
        //call api through UnityWebRequest
        UnityWebRequest www = UnityWebRequest.Post("http://smartcityhuancayo.herokuapp.com/Usuario/login.php",form);
        //here we will get back out response
        yield return www.Send ();

        if(www.error != null)
        {
            Debug.Log("Error " + www.error);
        }
        else
        {
            //login reponse
            Debug.Log("Response " + www.downloadHandler.text);
            UserDetail userDetail = JsonUtility.FromJson<UserDetail>(www.downloadHandler.text);

            if(userDetail.status == true)
            {
                print("Name: " + userDetail.data.ID_Usuario);
                SceneManager.LoadScene(1);
            }
            else {
                print("Login Success");
            }
        }
    }

}
