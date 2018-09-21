using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{

    public GameObject username;
    public GameObject password;

    public string Username;
    private string Password;

    public GameObject Fail;
    public Sprite LoginFail_ID;
    public Sprite LoginFail_PW;
    public Sprite LoginFail_Cheak;
    public Sprite LoginFail_Notexist;

    public CalorieManager CM;
    public GameObject LoginPenal;
    public GameObject MainPenal;
    public GameObject SelectPenal;

    string LoginURL = "http://ec2-18-220-248-68.us-east-2.compute.amazonaws.com/EagleFlight2Login.php";

    void Start()
    {
        CM = GameObject.Find("CalorieManager").GetComponent<CalorieManager>();  
    }

    public void LoginButton()
    {
        StartCoroutine(LoginToDB(Username, Password));
    }

    void ImageShowDown()
    {
        Fail.SetActive(false);
    }

    public void LoginPenal_Open()
    {
        MainPenal.SetActive(false);
        LoginPenal.SetActive(true);
    }
    public void LoginPenal_Closed()
    {
        MainPenal.SetActive(true);
        LoginPenal.SetActive(false);
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (username.GetComponent<InputField>().isFocused)
            {
                password.GetComponent<InputField>().Select();
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (Password != "")
            {
                LoginButton();
            }
        }

        Username = username.GetComponent<InputField>().text;
        Password = password.GetComponent<InputField>().text;

    }
    IEnumerator LoginToDB(string username, string password)
    {
        WWWForm form = new WWWForm();

        form.AddField("usernamePost", username);
        form.AddField("passwordPost", password);

        WWW www = new WWW(LoginURL, form);

        yield return www;

        if (www.text.Contains("login success"))
        {
            CalorieManager.instance.userID = username;

            string[] splitedData = www.text.Split('-');
            string cm = splitedData[1];
            string kg = splitedData[2];
            string gen = splitedData[3];

            CalorieManager.instance.height = cm;
            CalorieManager.instance.weight = kg;
            CalorieManager.instance.gender = gen;
            SceneManager.LoadScene("Mode");
        }
    }
}