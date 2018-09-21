using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DBManager : MonoBehaviour
{
    Dictionary<string, float> healthDataDic = new Dictionary<string, float>();
    Dictionary<string, float> playtimeDic = new Dictionary<string, float>();
    Dictionary<string, float> distanceDic = new Dictionary<string, float>();

    List<float> kcalList = new List<float>();
    List<float> secondList = new List<float>();
    List<float> distanceList = new List<float>();

    DateTime standardDate;

    public string user;

    bool isInit;

    public int leftArrowCount = 0;
    int todayCount = 0;

    [Header("BarChart")]
    public GameObject DayBar;
    public GameObject WeekBar;
    public GameObject MonthBar;
    public GameObject YearBar;

    [Header("Gender Image")]
    public Image gender_Image;
    public Sprite gender_M;
    public Sprite gender_F;

    [Header("Circle Graph Text")]
    public Text circleTextKcal;
    public Text circleTextTime;
    public Text circleTextDistance;
    public Text plus_circleTextKcal;
    public Text plus_circleTextTime;
    public Text plus_circleTextDistance;

    [Header("Circle Graph Green Value")]
    public Image circleGraphKcal;
    public Image circleGraphTime;
    public Image circleGraphDistance;

    [Header("Top Texts")]
    public Text[] TopTexts;

    [Header("Bottom Texts")]
    public Text[] bottomTexts;

    [Header("Button image change")]
    public Image day;
    public Image week;
    public Image month;
    public Image year;
    public Image kcal;
    public Image time;
    public Image distance;
    public Sprite day_h;
    public Sprite week_h;
    public Sprite month_h;
    public Sprite year_h;
    public Sprite day_n;
    public Sprite week_n;
    public Sprite month_n;
    public Sprite year_n;
    public Sprite kcal_h;
    public Sprite time_h;
    public Sprite distance_h;
    public Sprite kcal_n;
    public Sprite time_n;
    public Sprite distance_n;


    Font NanumSquareR;
    Font NanumSquareB;

    int startIndex = 0;

    string[] dayOfWeek = new string[] { "SUN", "MON", "TUE", "WED", "THU", "FRI", "SAT" };

    string[] graphFuntionNames = new string[] { "GraphDay", "GraphWeek", "GraphMonth", "GraphYear" };
    int sideTabIndex = 0;
    int topTabIndex = 0;



    void Start()
    {
        isInit = true;
        user = CalorieManager.instance.userID;

        NanumSquareR = Resources.Load<Font>("UI/NewUI/NanumSquareR");
        NanumSquareB = Resources.Load<Font>("UI/NewUI/NanumSquareB");

        standardDate = DateTime.Now;
        todayCount = (int)standardDate.DayOfWeek;

        startIndex = (todayCount + 4) % 7;

        for (int i = 0; i < bottomTexts.Length; i++)
        {
            if (i == 3)
            {
                bottomTexts[i].font = NanumSquareB;
                bottomTexts[i].fontSize = 20;
                bottomTexts[i].text = "TODAY";
            }
            else
            {
                bottomTexts[i].font = NanumSquareR;
                bottomTexts[i].fontSize = 18;
                bottomTexts[i].text = dayOfWeek[(startIndex + i) % 7];
            }
        }


        TopTexts[0].text = CalorieManager.instance.userID;
        TopTexts[1].text = CalorieManager.instance.height +" cm";
        TopTexts[2].text = CalorieManager.instance.weight +" kg";

        if (CalorieManager.instance.gender == "M")
        {
            gender_Image.sprite = gender_M;
        }
        else if (CalorieManager.instance.gender == "F")
        {
            gender_Image.sprite = gender_F;
            gender_Image.SetNativeSize();
        }

        StartCoroutine("GraphDay");
    }

    void ImageChanger(Image image, Sprite sprite)
    {
        image.sprite = sprite;
    }

    public void SetSideTabIndex(int index)
    {
        leftArrowCount = 0;
        sideTabIndex = index;

        if (sideTabIndex == 0)
        {
            SetBottomTextDay();
            ImageChanger(day, day_h);
            ImageChanger(week, week_n);
            ImageChanger(month, month_n);
            ImageChanger(year, year_n);
        }
        else if (sideTabIndex == 1)
        {
            SetBottomTextWeek();
            ImageChanger(day, day_n);
            ImageChanger(week, week_h);
            ImageChanger(month, month_n);
            ImageChanger(year, year_n);
        }
        else if (sideTabIndex == 2)
        {
            SetBottomTextMonth();
            ImageChanger(day, day_n);
            ImageChanger(week, week_n);
            ImageChanger(month, month_h);
            ImageChanger(year, year_n);
        }
        else if (sideTabIndex == 3)
        {
            SetBottomTextYear();
            ImageChanger(day, day_n);
            ImageChanger(week, week_n);
            ImageChanger(month, month_n);
            ImageChanger(year, year_h);
        }

        StartCoroutine(graphFuntionNames[sideTabIndex]);

        //해당 인덱스에 관한 그래프가 아니면 active false 해줌
        if (index == 0)
        {
            DayBar.SetActive(true);
            WeekBar.SetActive(false);
            MonthBar.SetActive(false);
            YearBar.SetActive(false);
            DayBar.GetComponent<BarAnimation>().Animate();
        }
        else if (index == 1)
        {
            DayBar.SetActive(false);
            WeekBar.SetActive(true);
            MonthBar.SetActive(false);
            YearBar.SetActive(false);
            WeekBar.GetComponent<BarAnimation>().Animate();
        }
        else if (index == 2)
        {
            DayBar.SetActive(false);
            WeekBar.SetActive(false);
            MonthBar.SetActive(true);
            YearBar.SetActive(false);
            MonthBar.GetComponent<BarAnimation>().Animate();
        }
        else if (index == 3)
        {
            DayBar.SetActive(false);
            WeekBar.SetActive(false);
            MonthBar.SetActive(false);
            YearBar.SetActive(true);
            YearBar.GetComponent<BarAnimation>().Animate();
        }
    }
    public void SetTopTabIndex(int index) // 상단 메뉴 버튼
    {
        leftArrowCount = 0;
        topTabIndex = index;
        StartCoroutine(graphFuntionNames[sideTabIndex]);

        if (topTabIndex == 0)
        {
            ImageChanger(kcal, kcal_h);
            ImageChanger(time, time_n);
            ImageChanger(distance, distance_n);
            
        }
        else if (topTabIndex == 1)
        {
            ImageChanger(kcal, kcal_n);
            ImageChanger(time, time_h);
            ImageChanger(distance, distance_n);

        }
        else if (topTabIndex == 2)
        {
            ImageChanger(kcal, kcal_n);
            ImageChanger(time, time_n);
            ImageChanger(distance, distance_h);
        }
    }


    public void OnLeftArrowClicked()
    {
        leftArrowCount++;

        if (sideTabIndex == 0)
            SetBottomTextDay();
        else if (sideTabIndex == 1)
            SetBottomTextWeek();
        else if (sideTabIndex == 2)
            SetBottomTextMonth();
        else if (sideTabIndex == 3)
            SetBottomTextYear();

        StartCoroutine(graphFuntionNames[sideTabIndex]);
    }

    public void OnRightArrowClicked()
    {
        leftArrowCount--;

        if (sideTabIndex == 0)
            SetBottomTextDay();
        else if (sideTabIndex == 1)
            SetBottomTextWeek();
        else if (sideTabIndex == 2)
            SetBottomTextMonth();
        else if (sideTabIndex == 3)
            SetBottomTextYear();

        StartCoroutine(graphFuntionNames[sideTabIndex]);
    }

    IEnumerator QueryHelthData(string date, string userid)
    {
        healthDataDic.Clear();
        playtimeDic.Clear();
        distanceDic.Clear();

        WWWForm form = new WWWForm();

        form.AddField("numPost", date);
        form.AddField("userPost", userid);

        WWW userData = new WWW("http://ec2-18-220-248-68.us-east-2.compute.amazonaws.com/EagleFlight2Search.php", form);

        yield return userData;

        string userDataString = userData.text;

        string[] userdata = userDataString.Split(';');

        // 검색된 데이터 배열 다 뽑기
        for (int i = 0; i < userdata.Length; i++)
        {
            if (userdata[i] != "")
            {
                string[] splitedData = userdata[i].Split('-');
                string dateString = splitedData[0] + "-" + splitedData[1] + "-" + splitedData[2] + "-" + splitedData[3];
                float kcal = float.Parse(splitedData[6]);
                float min = float.Parse(splitedData[7]);
                float distance = float.Parse(splitedData[8]);
                float speed = float.Parse(splitedData[9]);
                float power = float.Parse(splitedData[10]);

                if (healthDataDic.ContainsKey(dateString))
                    healthDataDic[dateString] += kcal;
                else
                    healthDataDic.Add(dateString, kcal);

                if (playtimeDic.ContainsKey(dateString))
                    playtimeDic[dateString] += min;
                else
                    playtimeDic.Add(dateString, min);

                if (distanceDic.ContainsKey(dateString))
                    distanceDic[dateString] += distance;
                else
                    distanceDic.Add(dateString, distance);
            }
        }
    }

    public float CaloriesOfTheDate(string date)
    {
        float kcal;

        if (healthDataDic.TryGetValue(date, out kcal))
            return kcal;

        return 0;
    }

    public float PlayTimeOfTheDate(string date)
    {
        float min;

        if (playtimeDic.TryGetValue(date, out min))
            return min;

        return 0;
    }

    public float DistanceOfTheDate(string date)
    {
        float distance;

        if (distanceDic.TryGetValue(date, out distance))
            return distance;

        return 0;
    }

    void ListInit()
    {
        kcalList.Clear();
        secondList.Clear();
        distanceList.Clear();
    }

    public void TopSelectMenuValue(GameObject Bar) //상단 메뉴에 따라 데이터 받는게 바뀌는 (ex. 칼로리 소모량, 운동시간, 운동거리 등등)
    {
        if (topTabIndex == 0)
        {
            if (leftArrowCount == -3)
            {
                for (int i = 0; i < 7; i++)
                {
                    if (i < 1)
                    {
                        Bar.GetComponent<BarChartFeed>().data[i] = kcalList[i + 6];
                    }
                    if (i >= 1)
                    {
                        Bar.GetComponent<BarChartFeed>().data[i] = 0;
                    }
                }
            }

            else if (leftArrowCount == -2)
            {
                for (int i = 0; i < 7; i++)
                {
                    if (i < 2)
                    {
                        Bar.GetComponent<BarChartFeed>().data[i] = kcalList[i + 5];
                    }
                    if (i >= 2)
                    {
                        Bar.GetComponent<BarChartFeed>().data[i] = 0;
                    }
                }
            }

            else if (leftArrowCount == -1)
            {
                for (int i = 0; i < 7; i++)
                {
                    if (i < 3)
                    {
                        Bar.GetComponent<BarChartFeed>().data[i] = kcalList[i + 4];
                    }
                    if (i >= 3)
                    {
                        Bar.GetComponent<BarChartFeed>().data[i] = 0;
                    }
                }
            }

            else if (leftArrowCount == 0)
            {
                for (int i = 0; i < 7; i++)
                {
                    if (i < 4)
                    {
                        Bar.GetComponent<BarChartFeed>().data[i] = kcalList[i + 3];
                    }
                    if (i >= 4)
                    {
                        Bar.GetComponent<BarChartFeed>().data[i] = 0;
                    }
                }
            }
            else if (leftArrowCount == 1)
            {

                for (int i = 0; i < 7; i++)
                {
                    if (i < 5)
                    {
                        Bar.GetComponent<BarChartFeed>().data[i] = kcalList[i + 2];
                    }
                    if (i >= 5)
                    {
                        Bar.GetComponent<BarChartFeed>().data[i] = 0;
                    }
                }
            }
            else if (leftArrowCount == 2)
            {

                for (int i = 0; i < 7; i++)
                {
                    if (i < 6)
                    {
                        Bar.GetComponent<BarChartFeed>().data[i] = kcalList[i + 1];
                    }
                    if (i >= 6)
                    {
                        Bar.GetComponent<BarChartFeed>().data[i] = 0;
                    }
                }
            }
            else if (leftArrowCount == 3)
            {
                for (int i = 0; i < 7; i++)
                {
                    Bar.GetComponent<BarChartFeed>().data[i] = kcalList[i];
                }
            }
            else if (leftArrowCount > 3)
            {
                for (int i = 0; i < 7; i++)
                {
                    Bar.GetComponent<BarChartFeed>().data[i] = kcalList[i];
                }
            }

        } //칼로리 소모량 탭
        else if (topTabIndex == 1)
        {
            if (leftArrowCount == -3)
            {
                for (int i = 0; i < 7; i++)
                {
                    if (i < 1)
                    {
                        Bar.GetComponent<BarChartFeed>().data[i] = secondList[i + 6];
                    }
                    if (i >= 1)
                    {
                        Bar.GetComponent<BarChartFeed>().data[i] = 0;
                    }
                }
            }

            else if (leftArrowCount == -2)
            {
                for (int i = 0; i < 7; i++)
                {
                    if (i < 2)
                    {
                        Bar.GetComponent<BarChartFeed>().data[i] = secondList[i + 5];
                    }
                    if (i >= 2)
                    {
                        Bar.GetComponent<BarChartFeed>().data[i] = 0;
                    }
                }
            }

            else if (leftArrowCount == -1)
            {
                for (int i = 0; i < 7; i++)
                {
                    if (i < 3)
                    {
                        Bar.GetComponent<BarChartFeed>().data[i] = secondList[i + 4];
                    }
                    if (i >= 3)
                    {
                        Bar.GetComponent<BarChartFeed>().data[i] = 0;
                    }
                }
            }

            else if (leftArrowCount == 0)
            {
                for (int i = 0; i < 7; i++)
                {
                    if (i < 4)
                    {
                        Bar.GetComponent<BarChartFeed>().data[i] = secondList[i + 3];
                    }
                    if (i >= 4)
                    {
                        Bar.GetComponent<BarChartFeed>().data[i] = 0;
                    }
                }
            }
            else if (leftArrowCount == 1)
            {

                for (int i = 0; i < 7; i++)
                {
                    if (i < 5)
                    {
                        Bar.GetComponent<BarChartFeed>().data[i] = secondList[i + 2];
                    }
                    if (i >= 5)
                    {
                        Bar.GetComponent<BarChartFeed>().data[i] = 0;
                    }
                }
            }
            else if (leftArrowCount == 2)
            {

                for (int i = 0; i < 7; i++)
                {
                    if (i < 6)
                    {
                        Bar.GetComponent<BarChartFeed>().data[i] = secondList[i + 1];
                    }
                    if (i >= 6)
                    {
                        Bar.GetComponent<BarChartFeed>().data[i] = 0;
                    }
                }
            }
            else if (leftArrowCount == 3)
            {
                for (int i = 0; i < 7; i++)
                {
                    Bar.GetComponent<BarChartFeed>().data[i] = secondList[i];
                }
            }
            else if (leftArrowCount > 3)
            {
                for (int i = 0; i < 7; i++)
                {
                    Bar.GetComponent<BarChartFeed>().data[i] = secondList[i];
                }
            }
        } //운동시간 탭
        else if (topTabIndex == 2)
        {
            if (leftArrowCount == -3)
            {
                for (int i = 0; i < 7; i++)
                {
                    if (i < 1)
                    {
                        Bar.GetComponent<BarChartFeed>().data[i] = distanceList[i + 6];
                    }
                    if (i >= 1)
                    {
                        Bar.GetComponent<BarChartFeed>().data[i] = 0;
                    }
                }
            }

            else if (leftArrowCount == -2)
            {
                for (int i = 0; i < 7; i++)
                {
                    if (i < 2)
                    {
                        Bar.GetComponent<BarChartFeed>().data[i] = distanceList[i + 5];
                    }
                    if (i >= 2)
                    {
                        Bar.GetComponent<BarChartFeed>().data[i] = 0;
                    }
                }
            }

            else if (leftArrowCount == -1)
            {
                for (int i = 0; i < 7; i++)
                {
                    if (i < 3)
                    {
                        Bar.GetComponent<BarChartFeed>().data[i] = distanceList[i + 4];
                    }
                    if (i >= 3)
                    {
                        Bar.GetComponent<BarChartFeed>().data[i] = 0;
                    }
                }
            }

            else if (leftArrowCount == 0)
            {
                for (int i = 0; i < 7; i++)
                {
                    if (i < 4)
                    {
                        Bar.GetComponent<BarChartFeed>().data[i] = distanceList[i + 3];
                    }
                    if (i >= 4)
                    {
                        Bar.GetComponent<BarChartFeed>().data[i] = 0;
                    }
                }
            }
            else if (leftArrowCount == 1)
            {

                for (int i = 0; i < 7; i++)
                {
                    if (i < 5)
                    {
                        Bar.GetComponent<BarChartFeed>().data[i] = distanceList[i + 2];
                    }
                    if (i >= 5)
                    {
                        Bar.GetComponent<BarChartFeed>().data[i] = 0;
                    }
                }
            }
            else if (leftArrowCount == 2)
            {

                for (int i = 0; i < 7; i++)
                {
                    if (i < 6)
                    {
                        Bar.GetComponent<BarChartFeed>().data[i] = distanceList[i + 1];
                    }
                    if (i >= 6)
                    {
                        Bar.GetComponent<BarChartFeed>().data[i] = 0;
                    }
                }
            }
            else if (leftArrowCount == 3)
            {
                for (int i = 0; i < 7; i++)
                {
                    Bar.GetComponent<BarChartFeed>().data[i] = distanceList[i];
                }
            }
            else if (leftArrowCount > 3)
            {
                for (int i = 0; i < 7; i++)
                {
                    Bar.GetComponent<BarChartFeed>().data[i] = distanceList[i];
                }
            }
        } // 운동거리 탭
    }

    IEnumerator GraphDay()
    {
        ListInit();
        int minusCount = 0;

        if (leftArrowCount > 3)
            minusCount = leftArrowCount - 3;

        // 7일전 부터 시작이라 오늘부터 6일을 빼주고 왼쪽 화살표를 4번 이상 누를때마다 시작일을 하루씩 더 빼줌
        DateTime date = standardDate.AddDays(-6 - minusCount);

        DateTime startDate = date;

        yield return StartCoroutine(QueryHelthData(date.ToString("yyyy"), user));

        for (int i = 0; i < 7; i++)
        {
            int barcount = i + 6;
            int bbar = i + 3;
            if (date.Year != startDate.Year) // 검색하다가 해가 바뀌면 바뀐년도로 DB를 다시 요청
            {
                yield return StartCoroutine(QueryHelthData(date.ToString("yyyy"), user));
                startDate = date;
            }

            float dayKcal = 0;
            float dayMinute = 0;
            float dayDistance = 0;

            for (int j = 0; j < 24; j++)
            {
                float hourKcal = CaloriesOfTheDate(date.ToString("yyyy-MM-dd") + "-" + j.ToString("D2"));
                dayKcal += hourKcal;

                float min = PlayTimeOfTheDate(date.ToString("yyyy-MM-dd") + "-" + j.ToString("D2"));
                dayMinute += min;

                float dis = DistanceOfTheDate(date.ToString("yyyy-MM-dd") + "-" + j.ToString("D2"));
                dayDistance += dis;
            }

            if (i == 0)
                Debug.LogWarning(date + " : " + dayKcal); // 확인용 코드
            else if (i == 6)
                Debug.LogError(date + " : " + dayKcal); // 확인용 코드 
            else
                print(date + " : " + dayKcal); // 확인용 코드


            kcalList.Add(dayKcal);
            secondList.Add(dayMinute);
            distanceList.Add(dayDistance);

            date = date.AddDays(1);
        }

        TopSelectMenuValue(DayBar); //데이터 넣어주기



        if (isInit)
            SetCircleGraph();

        // 이곳에 일별 그래프 함수 호출
        DayBar.GetComponent<BarChartFeed>().wait = true;

    }

    IEnumerator GraphWeek()
    {
        ListInit();

        int ordinal = (int)standardDate.DayOfWeek;

        int minusCount = 0;

        if (leftArrowCount > 3)
            minusCount = leftArrowCount - 3;

        // 7일전 부터 시작이라 오늘이 한주의 몇뻔째 요일인지 계산하여 빼주고(이번주의 첫날인 일요일이됨) 6주를 더 빼줌(그래프에 7칸씩 표시되기 때문) 그리고 왼쪽 화살표를 4번 이상 누를때마다 시작일을 한주씩 더 빼줌
        DateTime date = standardDate.AddDays(-ordinal - (7 * 6) - (minusCount * 7));

        DateTime startDate = date;

        yield return StartCoroutine(QueryHelthData(date.ToString("yyyy"), user));

        for (int i = 0; i < 7; i++)
        {
            float weekKcal = 0;
            float weekMinute = 0;
            float weekDistance = 0;

            for (int j = 0; j < 7; j++)
            {
                if (date.Year != startDate.Year) // 검색하다가 해가 바뀌면 바뀐년도로 DB를 다시 요청
                {
                    yield return StartCoroutine(QueryHelthData(date.ToString("yyyy"), user));
                    startDate = date;
                }

                for (int k = 0; k < 24; k++)
                {
                    float hourKcal = CaloriesOfTheDate(date.ToString("yyyy-MM-dd") + "-" + k.ToString("D2"));
                    weekKcal += hourKcal;

                    float min = PlayTimeOfTheDate(date.ToString("yyyy-MM-dd") + "-" + k.ToString("D2"));
                    weekMinute += min;

                    float dis = DistanceOfTheDate(date.ToString("yyyy-MM-dd") + "-" + k.ToString("D2"));
                    weekDistance += dis;
                }

                date = date.AddDays(1);
            }

            if (i == 0)
                Debug.LogWarning(date.AddDays(-7) + " : " + weekKcal); // 확인용 코드 (하루가 더해진 날짜기 때문에 그 전주의 합계가 표시됨)
            else if (i == 6)
                Debug.LogError(date.AddDays(-7) + " : " + weekKcal); // 확인용 코드 (하루가 더해진 날짜기 때문에 그 전주의 합계가 표시됨)
            else
                print(date.AddDays(-7) + " : " + weekKcal); // 확인용 코드 (하루가 더해진 날짜기 때문에 그 전주의 합계가 표시됨)
            //print(date + " : " + weekMinute); // 확인용 코드 (하루가 더해진 날짜기 때문에 그 전주의 합계가 표시됨)
            //print(date + " : " + weekDistance); // 확인용 코드(하루가 더해진 날짜기 때문에 그 전주의 합계가 표시됨)

            kcalList.Add(weekKcal);
            secondList.Add(weekMinute);
            distanceList.Add(weekDistance);
        }

        // 이곳에 주별 그래프 함수 호출
        TopSelectMenuValue(WeekBar); //데이터 넣어주기
        WeekBar.GetComponent<BarChartFeed>().wait = true;
    }

    IEnumerator GraphMonth()
    {
        ListInit();

        int minusCount = 0;

        if (leftArrowCount > 3)
            minusCount = leftArrowCount - 3;

        // 이번달의 첫날(1일) 로 만듬
        DateTime date = standardDate.AddDays(-standardDate.Day + 1);

        // 왼쪽 화살표를 4번 이상 누를때마다 시작일을 한달씩 더 빼줌
        date = date.AddMonths(-6 - minusCount);

        DateTime startDate = date;

        yield return StartCoroutine(QueryHelthData(date.ToString("yyyy"), user));

        for (int i = 0; i < 7; i++)
        {
            float monthKcal = 0;
            float monthMinute = 0;
            float monthDistance = 0;

            int endDayOfMonth = DateTime.DaysInMonth(date.Year, date.Month);

            for (int j = 0; j < endDayOfMonth; j++)
            {
                if (date.Year != startDate.Year) // 검색하다가 해가 바뀌면 바뀐년도로 DB를 다시 요청
                {
                    yield return StartCoroutine(QueryHelthData(date.ToString("yyyy"), user));
                    startDate = date;
                }

                for (int k = 0; k < 24; k++)
                {
                    float hourKcal = CaloriesOfTheDate(date.ToString("yyyy-MM-dd") + "-" + k.ToString("D2"));
                    monthKcal += hourKcal;

                    float min = PlayTimeOfTheDate(date.ToString("yyyy-MM-dd") + "-" + k.ToString("D2"));
                    monthMinute += min;

                    float dis = DistanceOfTheDate(date.ToString("yyyy-MM-dd") + "-" + k.ToString("D2"));
                    monthDistance += dis;
                }

                date = date.AddDays(1);
            }

            if (i == 0)
                Debug.LogWarning(date.AddMonths(-1) + " : " + monthKcal); // 확인용 코드 (하루가 더해진 날짜기 때문에 그 전달의 합계가 표시됨)
            else if (i == 6)
                Debug.LogError(date.AddMonths(-1) + " : " + monthKcal); // 확인용 코드 (하루가 더해진 날짜기 때문에 그 전달의 합계가 표시됨)
            else
                print(date.AddMonths(-1) + " : " + monthKcal); // 확인용 코드 (하루가 더해진 날짜기 때문에 그 전달의 합계가 표시됨)
  

            kcalList.Add(monthKcal);
            secondList.Add(monthMinute);
            distanceList.Add(monthDistance);
        }

        // 이곳에 월별 그래프 함수 호출
        TopSelectMenuValue(MonthBar); //데이터 넣어주기
        MonthBar.GetComponent<BarChartFeed>().wait = true;
    }

    IEnumerator GraphYear()
    {
        ListInit();

        int minusCount = 0;

        if (leftArrowCount > 3)
            minusCount = leftArrowCount - 3;

        // 이번년의 첫날(1월 1일) 로 만듬
        DateTime date = standardDate.AddDays(-standardDate.DayOfYear + 1);

        // 왼쪽 화살표를 4번 이상 누를때마다 시작일을 1년씩 더 빼줌
        date = date.AddYears(-6 - minusCount);

        DateTime startDate = date;

        yield return StartCoroutine(QueryHelthData(date.ToString("yyyy"), user));

        for (int i = 0; i < 7; i++)
        {
            float yearKcal = 0;
            float yearMinute = 0;
            float yearDistance = 0;

            int endDayOfYear = DateTime.IsLeapYear(date.Year) ? 366 : 365;

            for (int j = 0; j < endDayOfYear; j++)
            {
                if (date.Year != startDate.Year) // 검색하다가 해가 바뀌면 바뀐년도로 DB를 다시 요청
                {
                    yield return StartCoroutine(QueryHelthData(date.ToString("yyyy"), user));
                    startDate = date;
                }

                for (int k = 0; k < 24; k++)
                {
                    float hourKcal = CaloriesOfTheDate(date.ToString("yyyy-MM-dd") + "-" + k.ToString("D2"));
                    yearKcal += hourKcal;

                    float min = PlayTimeOfTheDate(date.ToString("yyyy-MM-dd") + "-" + k.ToString("D2"));
                    yearMinute += min;

                    float dis = DistanceOfTheDate(date.ToString("yyyy-MM-dd") + "-" + k.ToString("D2"));
                    yearDistance += dis;
                }

                date = date.AddDays(1);
            }

            if (i == 0)
                Debug.LogWarning(date.AddYears(-1) + " : " + yearKcal); // 확인용 코드 (하루가 더해진 날짜기 때문에 그 전년의 합계가 표시됨)
            else if (i == 6)
                Debug.LogError(date.AddYears(-1) + " : " + yearKcal); // 확인용 코드 (하루가 더해진 날짜기 때문에 그 전년의 합계가 표시됨)
            else
                print(date.AddYears(-1) + " : " + yearKcal); // 확인용 코드 (하루가 더해진 날짜기 때문에 그 전년의 합계가 표시됨)
  
            kcalList.Add(yearKcal);
            YearBar.GetComponent<BarChartFeed>().data[i] = yearKcal;
            secondList.Add(yearMinute);
            distanceList.Add(yearDistance);
        }

        // 이곳에 년별 그래프 함수 호출
        TopSelectMenuValue(YearBar); //데이터 넣어주기
        YearBar.GetComponent<BarChartFeed>().wait = true;
    }

    void SetCircleGraph()
    {
        isInit = false;

        circleTextKcal.text = kcalList[6].ToString() + " Kcal";

        int hours = (int)secondList[6] / 3600;
        int minute = (int)secondList[6] % 3600 / 60;

        circleTextTime.text = hours + "h " + minute + "m";

        float roundDistance = (float)Math.Round(distanceList[6], 2);

        circleTextDistance.text = roundDistance.ToString()+"km";

        circleGraphKcal.fillAmount = kcalList[6] / 285;
        circleGraphTime.fillAmount = secondList[6] / 3735;
        circleGraphDistance.fillAmount = distanceList[6] / 13.38375f;

        if (kcalList[6]>285)
        {
            plus_circleTextKcal.text = "+ "+(kcalList[6] - 285).ToString();
        }
        else
        {
            plus_circleTextKcal.text = "";
        }
        if (secondList[6] > 3735)
        {
            secondList[6] -= 3735f;
            plus_circleTextTime.text = "+ " + hours + "h " + minute + "m";
        }
        else
        {
            plus_circleTextTime.text = "";
        }
        if(distanceList[6]>13.38375)
        {
            plus_circleTextDistance.text = "+ "+(distanceList[6] - 13.38375f).ToString();
        }
        else
        {
            plus_circleTextDistance.text = "";
        }
    }

    void SetBottomTextDay()
    {
        int startIndex = this.startIndex;

        for (int i = 0; i < bottomTexts.Length; i++)
        {
            if (i == (3 + leftArrowCount))
            {
                bottomTexts[i].font = NanumSquareB;
                bottomTexts[i].fontSize = 20;
                bottomTexts[i].text = "TODAY";
            }
            else
            {
                bottomTexts[i].font = NanumSquareR;
                bottomTexts[i].fontSize = 18;
                bottomTexts[i].text = dayOfWeek[Mathf.Abs((startIndex - leftArrowCount + i + 7) % 7)];
            }
        }
    }

    void SetBottomTextWeek()
    {
        DateTime movedWeek = standardDate.AddDays(-leftArrowCount * 7);

        for (int i = 0; i < bottomTexts.Length; i++)
        {
            if (i == (3 + leftArrowCount))
            {
                bottomTexts[i].font = NanumSquareB;
                bottomTexts[i].fontSize = 20;
            }
            else
            {
                bottomTexts[i].font = NanumSquareR;
                bottomTexts[i].fontSize = 18;
            }

            bottomTexts[i].text = GetWeekOfMonth(movedWeek.AddDays((i - 3) * 7));
        }
    }

    void SetBottomTextMonth()
    {
        DateTime startDate = standardDate.AddMonths(-3 - leftArrowCount);

        for (int i = 0; i < bottomTexts.Length; i++)
        {
            if (i == 3 + leftArrowCount)
            {
                bottomTexts[i].font = NanumSquareB;
                bottomTexts[i].fontSize = 20;
            }
            else
            {
                bottomTexts[i].font = NanumSquareR;
                bottomTexts[i].fontSize = 18;
            }

            if (startDate.Year != startDate.AddMonths(-1).Year)
                bottomTexts[i].text = startDate.Year.ToString() + "년 " + startDate.Month.ToString() + "월";
            else
                bottomTexts[i].text = startDate.Month.ToString() + "월";

            startDate = startDate.AddMonths(1);
        }
    }

    void SetBottomTextYear()
    {
        DateTime startDate = standardDate.AddYears(-3 - leftArrowCount);

        for (int i = 0; i < bottomTexts.Length; i++)
        {
            if (i == 3 + leftArrowCount)
            {
                bottomTexts[i].font = NanumSquareB;
                bottomTexts[i].fontSize = 20;
            }
            else
            {
                bottomTexts[i].font = NanumSquareR;
                bottomTexts[i].fontSize = 18;
            }

            bottomTexts[i].text = startDate.Year.ToString();

            startDate = startDate.AddYears(1);
        }
    }

    string GetWeekOfMonth(DateTime date)
    {
        int plusDayCount = 6 - (int)date.DayOfWeek;

        //주차를 구할 일자(전달의 마지막주와 이번달의 첫재주 문제때문에 이번주의 제일 마지막 일로 계산함)
        DateTime calculationDate = date.AddDays(plusDayCount);

        DateTime firstDayOfMonth = new DateTime(calculationDate.Year, calculationDate.Month, 1); //기준일(그 달의 1일)

        Calendar calenderCalc = CultureInfo.CurrentCulture.Calendar;

        //DayOfWeek.Sunday 인수는 기준 요일
        int weekNumber = calenderCalc.GetWeekOfYear(calculationDate, CalendarWeekRule.FirstDay, DayOfWeek.Sunday) - calenderCalc.GetWeekOfYear(firstDayOfMonth, CalendarWeekRule.FirstDay, DayOfWeek.Sunday) + 1;

        string weekText = calculationDate.Month + "월 " + weekNumber + "주";

        return weekText;
    }
}