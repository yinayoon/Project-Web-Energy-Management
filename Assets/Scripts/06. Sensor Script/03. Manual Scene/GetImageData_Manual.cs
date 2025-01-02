using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Text;
using System;

public class GetImageData_Manual : MonoBehaviour
{
    string url;

    [Header ("- GameObject for Get Image Data")]
    [SerializeField] Image panel;
    [SerializeField] Image graphImage;
    [SerializeField] Dropdown dropdown;

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
    [SerializeField] InputField sInputField_Y;
    [SerializeField] InputField sInputField_M;
    [SerializeField] InputField sInputField_D;
    [SerializeField] InputField sInputField_Hh;
    [SerializeField] InputField sInputField_Mm;
    [SerializeField] InputField sInputField_Ss;

    [Header("- End Input Field")]
    [SerializeField] InputField eInputField_Y;
    [SerializeField] InputField eInputField_M;
    [SerializeField] InputField eInputField_D;
    [SerializeField] InputField eInputField_Hh;
    [SerializeField] InputField eInputField_Mm;
    [SerializeField] InputField eInputField_Ss;

    [Header("- ETC GameObject")]
    [SerializeField] GameObject errorMessage;

    string str;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetTexture());
    }

    bool flag = false;
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

        if (panel.transform.gameObject.activeSelf == false)// && flag == false)
        {
            dropdown.value = 0;

            sInputField_Y.text = startY;
            sInputField_M.text = startM;
            sInputField_D.text = startD;
            sInputField_Hh.text = startHh;
            sInputField_Mm.text = startMm;
            sInputField_Ss.text = startSs;

            eInputField_Y.text = endY;
            eInputField_M.text = endM;
            eInputField_D.text = endD;
            eInputField_Hh.text = endHh;
            eInputField_Mm.text = endMm;
            eInputField_Ss.text = endSs;

            errorMessage.SetActive(false);
            flag = false;
        }
        else if (panel.transform.gameObject.activeSelf == true && flag == false)
        {
            SetData();
            url = str;
            StartCoroutine("GetTexture");
            flag = true;
        }

        SetData();

        if (url == null || url != str)
        {
            url = str;
            StartCoroutine("GetTexture");
        }
    }

    public void SetData()
    {
        #region 데이터가 숫자가 아닐 경우 0으로 변환하는 코드
        int sYval;
        bool sYsign = int.TryParse(sInputField_Y.text, out sYval);
        if (sYsign == false)
            sInputField_Y.text = "0";

        int sMval;
        bool sMsign = int.TryParse(sInputField_M.text, out sMval);
        if (sMsign == false)
            sInputField_M.text = "0";

        int sDval;
        bool sDsign = int.TryParse(sInputField_D.text, out sDval);
        if (sDsign == false)
            sInputField_D.text = "0";

        int sHhval;
        bool sHhsign = int.TryParse(sInputField_Hh.text, out sHhval);
        if (sHhsign == false)
            sInputField_Hh.text = "0";

        int sMmval;
        bool sMmsign = int.TryParse(sInputField_Mm.text, out sMmval);
        if (sMmsign == false)
            sInputField_Mm.text = "0";

        int sSsval;
        bool sSssign = int.TryParse(sInputField_Ss.text, out sSsval);
        if (sSssign == false)
            sInputField_Ss.text = "0";


        int eYval;
        bool eYsign = int.TryParse(eInputField_Y.text, out eYval);
        if (eYsign == false)
            eInputField_Y.text = "0";

        int eMval;
        bool eMsign = int.TryParse(eInputField_M.text, out eMval);
        if (eMsign == false)
            eInputField_M.text = "0";

        int eDval;
        bool eDsign = int.TryParse(eInputField_D.text, out eDval);
        if (eDsign == false)
            eInputField_D.text = "0";

        int eHhval;
        bool eHhsign = int.TryParse(eInputField_Hh.text, out eHhval);
        if (eHhsign == false)
            eInputField_Hh.text = "0";

        int eMmval;
        bool eMmsign = int.TryParse(eInputField_Mm.text, out eMmval);
        if (eMmsign == false)
            eInputField_Mm.text = "0";

        int eSsval;
        bool eSssign = int.TryParse(eInputField_Ss.text, out eSsval);
        if (eSssign == false)
            eInputField_Ss.text = "0";
        #endregion
        string sy = int.Parse(sInputField_Y.text).ToString("D4");
        
        string sm = sInputField_M.text;
        string sd = sInputField_D.text;
        string shh = sInputField_Hh.text;
        string smm = sInputField_Mm.text;
        string sss = sInputField_Ss.text;

        string ey = eInputField_Y.text;
        string em = eInputField_M.text;
        string ed = eInputField_D.text;
        string ehh = eInputField_Hh.text;
        string emm = eInputField_Mm.text;
        string ess = eInputField_Ss.text;

        if (sm.Length == 1)
            sm = sm.PadLeft(2, '0'); //= int.Parse(sm).ToString("D2");
        else
            sm = sInputField_M.text;

        if (sd.Length == 1)
            sd = sd.PadLeft(2, '0'); // = int.Parse(sd).ToString("D2");
        else
            sd = sInputField_D.text;

        if (shh.Length == 1)
            shh = shh.PadLeft(2, '0'); // = int.Parse(shh).ToString("D2");
        else
            shh = sInputField_Hh.text;

        if (smm.Length == 1)
            smm = smm.PadLeft(2, '0'); // = int.Parse(smm).ToString("D2");
        else
            smm = sInputField_Mm.text;

        if (sss.Length == 1)
            sss = sss.PadLeft(2, '0'); // = int.Parse(smm).ToString("D2");
        else
            sss = sInputField_Ss.text;


        if (em.Length == 1)
            em = em.PadLeft(2, '0'); // = int.Parse(em).ToString("D2");
        else
            em = eInputField_M.text;

        if (ed.Length == 1)
            ed = ed.PadLeft(2, '0'); // = int.Parse(ed).ToString("D2");
        else
            ed = eInputField_D.text;

        if (ehh.Length == 1)
            ehh = ehh.PadLeft(2, '0'); // = int.Parse(ehh).ToString("D2");
        else
            ehh = eInputField_Hh.text;

        if (emm.Length == 1)
            emm = emm.PadLeft(2, '0'); // = int.Parse(emm).ToString("D2");
        else
            emm = eInputField_Mm.text;

        if (ess.Length == 1)
            ess = ess.PadLeft(2, '0'); // = int.Parse(ess).ToString("D2");
        else
            ess = eInputField_Ss.text;

        string s = $"http://io.energyiotlab.com:54242/q?start=2023/01/12-00:00:00&$end=2023/01/12-01:00:00&m=sum:dku-jinri.env%7Bmac=floeD494,sensor=not_literal_or(co2)%7D&o=&m=sum:dku-jinri.env%7Bmac=floeD494,sensor=co2%7D&o=axis%20x1y2&yrange=%5B0:%5D&y2range=%5B0:%5D&key=out%20center%20top%20horiz%20box&wxh=1900x743&style=linespoint&png";


        if (GetDataNameFroms(dropdown.value) == "*")
        {
            str = "http://io.energyiotlab.com:54242/q?" +
            $"start={sy}/{sm}/{sd}-{shh}:{smm}:{sss}&" + // 시작시간
            $"end={ey}/{em}/{ed}-{ehh}:{emm}:{ess}" + // 끝시간
            "&m=sum:dku-jinri.env%7Bmac=" +
            $"floe{TriggerSensor.SensorName}," + // 센서 이름
            "sensor=not_literal_or(co2)%7D&o=&m=sum:dku-jinri.env%7Bmac=" +
            $"floe{TriggerSensor.SensorName}," + // 센서 이름
            "sensor=co2%7D&o=axis%20x1y2&yrange=%5B0:%5D&y2range=%5B0:%5D&key=out%20center%20top%20horiz%20box&wxh=1900x743&style=linespoint&png";
        }
        else
        {
            str = "http://io.energyiotlab.com:54242/q?" +
                $"start={sy}/{sm}/{sd}-{shh}:{smm}:{sss}&" +
                $"end={ey}/{em}/{ed}-{ehh}:{emm}:{ess}" +
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
        if (graphImage.transform.parent.gameObject.activeSelf == true)
        {
            yield return new WaitForSeconds(0.5f);
            using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url))
            {
                yield return uwr.SendWebRequest();

                if (uwr.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(uwr.error);
                    errorMessage.SetActive(true);
                }
                else
                {
                    // Get downloaded asset bundle
                    Texture2D tex = DownloadHandlerTexture.GetContent(uwr);
                    graphImage.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
                    errorMessage.SetActive(false);
                }
            }
        }
    }
}