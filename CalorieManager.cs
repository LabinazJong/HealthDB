using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CalorieManager : MonoBehaviour
{
    public static CalorieManager instance = null;

    public string userID;
    public string height;
    public string weight;
    public string gender;

    string SendURL = "http://ec2-18-220-248-68.us-east-2.compute.amazonaws.com/EagleFlight2Update.php";

    DateTime today;

    Dictionary<string, float> dateAndKcal = new Dictionary<string, float>();

    public float playTime = 0;
    public float distance = 0;
    public float speed = 0;
    public float power = 0;
    public float fat = 0;
    public bool datapass;

    const float kcalPerSec = 0.07583f; // 몸무게 : 60kg, 자전거무게 : 10kg, 평균속도 12.9km/h 기준 (에버노트 자전거 칼로리표 참조)

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        datapass = false;
        today = DateTime.Now;
    }

    void Update()
    {
        if (today.Hour != DateTime.Now.Hour)
        {
            CalculateCalorie();

            today = DateTime.Now;
        }

        if (SceneManager.GetActiveScene().name == "Stage1" || SceneManager.GetActiveScene().name == "Stage2" || SceneManager.GetActiveScene().name == "Stage3")
        {
            playTime += Time.deltaTime;
        }
    }

    public void SendDataPlz()
    {
        Debug.Log("전송시작");
        CalculateCalorie();
        StartCoroutine("SendData");
    }


    public IEnumerator SendData()
    {
        if (playTime < 10)
            yield return false;

        foreach (KeyValuePair<string, float> kvPair in dateAndKcal)
        {
            WWWForm form = new WWWForm();

            form.AddField("idPost", userID); // 해당 유저의 아이디
            form.AddField("genPost", gender); // 성별
            form.AddField("todayPost", kvPair.Key);
            form.AddField("caloriePost", kvPair.Value.ToString());//칼로리
            form.AddField("playtimePost", playTime.ToString());//플레이타임
            form.AddField("disPost", distance.ToString()); // 거리
            form.AddField("speedPost", "0"); // 스피드
            form.AddField("powerPost", "0"); // 파워

            WWW www = new WWW(SendURL, form);

            yield return www; 
            Debug.Log(www.text);
            if(www.text=="Everything ok.")
            {
                datapass = true;
            }
        }

        dateAndKcal.Clear();

        today = DateTime.Now;

        playTime = 0;

    }

    public void CalculateCalorie()
    {
        float kcal = playTime * kcalPerSec;

        kcal += kcal * UnityEngine.Random.Range(-0.03f, 0.03f); // 칼로리 오차범위 +-3%

        distance = 12.9f / 3600 * playTime;

        distance += distance * UnityEngine.Random.Range(-0.03f, 0.03f); // 거리 오차범위 +-3%

        dateAndKcal.Add(today.ToString("yyyy-MM-dd-HH-mm-ss"), kcal);
    }
}