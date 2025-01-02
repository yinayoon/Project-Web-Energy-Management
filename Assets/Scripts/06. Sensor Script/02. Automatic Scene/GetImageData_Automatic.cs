using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Text;
using System;

public class GetImageData_Automatic : MonoBehaviour
{
    [SerializeField]
    string url;

    [SerializeField]
    Image panel;
    [SerializeField]
    RawImage testImage;
    [SerializeField]
    Image image;
    [SerializeField]
    Slider slider;
    [SerializeField]
    Dropdown dropdown;

    // 시간
    string startY;
    string startM;
    string startD;
    string startHh;
    string startMm;
    string startSs;

    string endY;
    string endM;
    string endD;
    string endHh;
    string endMm;
    string endSs;

    [Header ("- Start Input Field")]
    [SerializeField]
    Text sText_Y;
    [SerializeField]
    Text sText_M;
    [SerializeField]
    Text sText_D;
    [SerializeField]
    Text sText_Hh;
    [SerializeField]
    Text sText_Mm;
    [SerializeField]
    Text sText_Ss;

    [Header("- End Input Field")]
    [SerializeField]
    Text eText_Y;
    [SerializeField]
    Text eText_M;
    [SerializeField]
    Text eText_D;
    [SerializeField]
    Text eText_Hh;
    [SerializeField]
    Text eText_Mm;
    [SerializeField]
    Text eText_Ss;

    string str;

    int beforeDropdownNum;
    bool DropdownNumSign;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetTexture());
        DropdownNumSign = false;
    }

    // Update is called once per frame
    void Update()
    {
        #region 날짜 시간 관련 문자열 데이터
        DateTime StartDate = DateTime.Now.AddMinutes(-10);
        DateTime endDate = DateTime.Now;

        startY = StartDate.Year.ToString();
        startM = string.Format("{0:D2}", StartDate.Month);
        startD = string.Format("{0:D2}", StartDate.Day);
        startHh = string.Format("{0:D2}", StartDate.Hour);
        startMm = string.Format("{0:D2}", StartDate.Minute);
        startSs = string.Format("{0:D2}", StartDate.Second);

        endY = endDate.Year.ToString();
        endM = string.Format("{0:D2}", endDate.Month);
        endD = string.Format("{0:D2}", endDate.Day);
        endHh = string.Format("{0:D2}", endDate.Hour);
        endMm = string.Format("{0:D2}", endDate.Minute);
        endSs = string.Format("{0:D2}", endDate.Second);
        #endregion

        if (beforeDropdownNum != dropdown.value)
            DropdownNumSign = true;
        else
            DropdownNumSign = false;

        if (panel.transform.gameObject.activeSelf == false)
        {
            dropdown.value = 0;
            slider.value = 5;
        }
        
        SetData();
        url = str.ToString();

        beforeDropdownNum = dropdown.value;
    }

    public void SetData()
    {
        if (GetDataNameFroms(dropdown.value) == "*")
        {
            str = "http://io.energyiotlab.com:54242/q?" +
            $"start={sText_Y.text}/{sText_M.text}/{sText_D.text}-{sText_Hh.text}:{sText_Mm.text}:{sText_Ss.text}&" + // 시작시간
            $"end={eText_Y.text}/{eText_M.text}/{eText_D.text}-{eText_Hh.text}:{eText_Mm.text}:{eText_Ss.text}" + // 끝시간
            "&m=sum:dku-jinri.env%7Bmac=" +
            $"floe{TriggerSensor.SensorName}," + // 센서 이름
            "sensor=not_literal_or(co2)%7D&o=&m=sum:dku-jinri.env%7Bmac=" +
            $"floe{TriggerSensor.SensorName}," + // 센서 이름
            "sensor=co2%7D&o=axis%20x1y2&yrange=%5B0:%5D&y2range=%5B0:%5D&key=out%20center%20top%20horiz%20box&wxh=1900x743&style=linespoint&png";
        }
        else
        {
            str = "http://io.energyiotlab.com:54242/q?" +
                $"start={sText_Y.text}/{sText_M.text}/{sText_D.text}-{sText_Hh.text}:{sText_Mm.text}:{sText_Ss.text}&" +
                $"end={eText_Y.text}/{eText_M.text}/{eText_D.text}-{eText_Hh.text}:{eText_Mm.text}:{eText_Ss.text}" +
                "&m=sum:dku-jinri.env%7Bmac=" +
                $"{GetDataNameFroms(dropdown.value)}" +
                ",sensor=" +
                $"floe{TriggerSensor.SensorName}" +
                "%7D&o=&m=sum:dku-jinri.env%7Bmac=" +
                $"floe{TriggerSensor.SensorName}" +
                ",sensor=" +
                $"{GetDataNameFroms(dropdown.value)}" +
                "%7D&o=axis%20x1y2&yrange=%5B0:%5D&y2range=%5B0:%5D&key=out%20center%20top%20horiz%20box&wxh=1900x743&style=linespoint&png";
        }
    }

    string _str;
    public string GetDataNameFroms(int value)
    {        
        switch (value)
        {
            case 0:
                _str = "*";
                break;
            case 1:
                _str = "co2";
                break;
            case 2:
                _str = "temperature";
                break;
            case 3:
                _str = "dewpoint";
                break;
            case 4:
                _str = "humidity";
                break;
            case 5:
                _str = "sound";
                break;
            case 6:
                _str = "detect";
                break;
            case 7:
                _str = "lx";
                break;
            case 8:
                _str = "pmv";
                break;
            case 9:
                _str = "ppd";
                break;
            case 10:
                _str = "hcho";
                break;
            case 11:
                _str = "cpu_1m";
                break;
            case 12:
                _str = "cpu_5m";
                break;
            case 13:
                _str = "cpu_15m";
                break;
            case 14:
                _str = "cpu_temp";
                break;
            case 15:
                _str = "pm1";
                break;
            case 16:
                _str = "pm10";
                break;
            case 17:
                _str = "pm25";
                break;
        }

        return _str;
    }

    IEnumerator GetTexture()
    {
        while (true)
        {
            if (image.transform.parent.gameObject.activeSelf == true)
            {
                using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url))
                {
                    yield return uwr.SendWebRequest();

                    sText_Y.text = startY;
                    sText_M.text = startM;
                    sText_D.text = startD;
                    sText_Hh.text = startHh;
                    sText_Mm.text = startMm;
                    sText_Ss.text = startSs;

                    eText_Y.text = endY;
                    eText_M.text = endM;
                    eText_D.text = endD;
                    eText_Hh.text = endHh;
                    eText_Mm.text = endMm;
                    eText_Ss.text = endSs;

                    if (uwr.result != UnityWebRequest.Result.Success)
                    {
                        //Debug.Log(uwr.error);

                        yield return null;
                    }
                    else
                    {
                        // Get downloaded asset bundle
                        testImage.texture = DownloadHandlerTexture.GetContent(uwr);
                        Texture2D tex = DownloadHandlerTexture.GetContent(uwr);
                        image.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));

                        if (DropdownNumSign)
                            yield return new WaitForSeconds(3f);
                        else
                            yield return null;
                    }
                }
            }
            else
            {
                image.sprite = null;
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
}
