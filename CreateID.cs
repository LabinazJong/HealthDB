using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateID : MonoBehaviour {

    //필드
    public GameObject username;
    public GameObject password;
    public GameObject cm;
    public GameObject kg;
    public GameObject bf;
    public GameObject open_register;
    public GameObject main_penal;
    public GameObject M_Text;
    public GameObject F_Text;

    //이름 저장
    private string Username_text;
    private string Password_text;
    private string Cm_text;
    private string Kg_text;
    private string Bf_text; // 체지방률
    private string Gen_text;

    public GameObject Fail;

    public Sprite CreateFail_existID;
    public Sprite CreateFail_ID;
    public Sprite CreateFail_PW;
    public Sprite CreateFail_Cheak4_PW;
    public Sprite CreateFail_Age;

    public Sprite CreateClear; //회원가입 성공

    string CreateUserURL = "http://ec2-18-220-248-68.us-east-2.compute.amazonaws.com/EagleFlight2InsertUser.php";


    public void RegisterButton()
    {
        CreateUser(Username_text, Password_text, Cm_text, Kg_text, Bf_text, Gen_text);
        Debug.Log("생성완료");
        CreateBtn_closed();
    }

    void Update()
    {
        //탭할때 다음칸 넘기기
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (username.GetComponent<InputField>().isFocused)
            {
                password.GetComponent<InputField>().Select();
            }
            if (password.GetComponent<InputField>().isFocused)
            {
                cm.GetComponent<InputField>().Select();
            }
            if (cm.GetComponent<InputField>().isFocused)
            {
                kg.GetComponent<InputField>().Select();
            }
            if (kg.GetComponent<InputField>().isFocused)
            {
                bf.GetComponent<InputField>().Select();
            }
            if (bf.GetComponent<InputField>().isFocused)
            {
                username.GetComponent<InputField>().Select();
            }
        }
        //아이디, 비번, 나이, 킬로그램 빈칸이면 계정 생성안됨
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (Password_text != "" && Cm_text != "" && Username_text != "" && Kg_text != "" && Bf_text != "" && Gen_text != "")
            {
                RegisterButton();
            }
        }
        Username_text = username.GetComponent<InputField>().text;
        Password_text = password.GetComponent<InputField>().text;
        Cm_text = cm.GetComponent<InputField>().text;
        Kg_text = kg.GetComponent<InputField>().text;
        Bf_text = bf.GetComponent<InputField>().text;
    }

    public void CreateUser(string username, string password, string cm, string kg, string bf, string gen)
    {
        WWWForm form = new WWWForm();

        form.AddField("usernamePost", username);
        form.AddField("passwordPost", password);
        form.AddField("cmPost", cm);
        form.AddField("kgPost", kg);
        form.AddField("bfPost", bf);
        form.AddField("genPost", gen);

        WWW www = new WWW(CreateUserURL, form);
    }

    void ImageShowDown()
    {
        Fail.SetActive(false);
    }

    public void CreateBtn_open()
    {
        main_penal.SetActive(false);
        open_register.SetActive(true);
    }

    public void CreateBtn_closed()
    {
        open_register.SetActive(false);
        main_penal.SetActive(true);
    }

    public void Choice_Man()
    {
        F_Text.SetActive(false);
        M_Text.SetActive(true);
        Gen_text = "M";
    }

    public void Choice_FeMale()
    {
        M_Text.SetActive(false);
        F_Text.SetActive(true);
        Gen_text = "F";
    }
}