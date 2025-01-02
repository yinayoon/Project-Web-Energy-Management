using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GetSensorData : MonoBehaviour
{
    // http://europa.energyiotlab.com:30101/v1/sensors/dku-jinri.env

    string jsonText;
    string url;

    public static string sensorName;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("GetRequest"); // 단국대 진리관
        StartCoroutine("CoGetJsonData");
        //Debug.Log("Dataget");
    }

    IEnumerator GetRequest()
    {
        while (true)
        {
            url = "http://europa.energyiotlab.com:30101/v1/sensors/dku-jinri.env/floe" + sensorName;
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                string[] pages = url.Split('/');
                int page = pages.Length - 1;

                switch (webRequest.result)
                {
                    case UnityWebRequest.Result.ConnectionError:
                    case UnityWebRequest.Result.DataProcessingError:
                        //UnityEngine.Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                        //UnityEngine.Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                        break;
                    case UnityWebRequest.Result.Success:
                        //Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                        jsonText = webRequest.downloadHandler.text;
                        break;
                }
            }
        }
    }

    #region 센서 데이터 필드
    // - Service"
    string _timestamp;
    string _datetime;

    // - Sensors
    string _id;
    string _ip;
    string _cpu_temp;
    string _vap;
    string _seq;
    string _cpu_5m;
    string _dmesg;
    string _cpu_15m;
    string _cpu_1m;
    string _mac;
    string _mode;
    string _next_hop;

    // - Node_info"
    string _sound;
    string _pm25;
    string _pm10;
    string _detect;
    string _humidity;
    string _temperature;
    string _hcho;
    string _co2;
    string _lx;
    string _pm1;
    string _dewpoint;
    #endregion

    #region 센서 데이터 Static 필드
    public static string Timestamp;
    public static string Datetime;

    public static string Id;
    public static string Ip;
    public static string Cpu_temp;
    public static string Vap;
    public static string Seq;
    public static string Cpu_5m;
    public static string Dmesg;
    public static string Cpu_15m;
    public static string Cpu_1m;
    public static string Mac;
    public static string Mode;
    public static string Next_hop;

    public static string Sound;
    public static string Pm25;
    public static string Pm10;
    public static string Detect;
    public static string Humidity;
    public static string Temperature;
    public static string Hcho;
    public static string Co2;
    public static string Lx;
    public static string Pm1;
    public static string Dewpoint;
    #endregion

    JObject jObject;
    IEnumerator CoGetJsonData()
    {
        while (true)
        {
            if (jsonText != null && sensorName != null)
            {
                jObject = JObject.Parse(jsonText);

                foreach (var fElement in jObject.First)
                {
                    if (fElement.ToString().Contains("timestamp"))
                    {
                        var timestamp = fElement["timestamp"].ToString();
                        Timestamp = _timestamp = timestamp;
                        //Debug.Log(Timestamp);
                    }
                    if (fElement.ToString().Contains("datetime"))
                    {
                        var datetime = fElement["datetime"].ToString();
                        Datetime = _datetime = datetime;
                        //Debug.Log(datetime);
                    }
                    if (fElement.ToString().Contains("node_info") || fElement.ToString().Contains("sensors"))
                    {
                        JObject jObj = JObject.Parse(fElement.ToString());

                        foreach (var f1 in jObj)
                        {
                            if (f1.Key == "sensors")
                            {
                                if (f1.ToString().Contains("\"pm1\":"))
                                {
                                    var pm25 = f1.Value["pm25"]["value"].ToString();
                                    Pm25 = _pm25 = pm25;
                                    //Debug.Log($"pm25 : {pm25}");
                                }
                                if (f1.ToString().Contains("\"temperature\":"))
                                {
                                    var temperature = f1.Value["temperature"]["value"].ToString();
                                    Temperature = _temperature = temperature;
                                    //Debug.Log($"temperature : {temperature}");
                                }
                                if (f1.ToString().Contains("\"humidity\":"))
                                {
                                    var humidity = f1.Value["humidity"]["value"].ToString();
                                    Humidity = _humidity = humidity;
                                    //Debug.Log($"humidity : {humidity}");
                                }
                                if (f1.ToString().Contains("\"dewpoint\":"))
                                {
                                    var dewpoint = f1.Value["dewpoint"]["value"].ToString();
                                    Dewpoint = _dewpoint = dewpoint;
                                    //Debug.Log($"dewpoint : {dewpoint}");
                                }
                                if (f1.ToString().Contains("\"hcho\":"))
                                {
                                    var hcho = f1.Value["hcho"]["value"].ToString();
                                    Hcho = _hcho = hcho;
                                    //Debug.Log($"hcho : {hcho}");
                                }
                                if (f1.ToString().Contains("\"sound\":"))
                                {
                                    var sound = f1.Value["sound"]["value"].ToString();
                                    Sound = _sound = sound;
                                    //Debug.Log($"sound : {sound}");
                                }
                                if (f1.ToString().Contains("\"detect\":"))
                                {
                                    var detect = f1.Value["detect"]["value"].ToString();
                                    Detect = _detect = detect;
                                    //Debug.Log($"detect : {detect}");
                                }
                                if (f1.ToString().Contains("\"pm10\":"))
                                {
                                    var pm10 = f1.Value["pm10"]["value"].ToString();
                                    Pm10 = _pm10 = pm10;
                                    //Debug.Log($"pm10 : {pm10}");
                                }
                                if (f1.ToString().Contains("\"co2\":"))
                                {
                                    var co2 = f1.Value["co2"]["value"].ToString();
                                    Co2 = _co2 = co2;
                                    //Debug.Log($"co2 : {co2}");
                                }
                                if (f1.ToString().Contains("\"lx\":"))
                                {
                                    var lx = f1.Value["lx"]["value"].ToString();
                                    Lx = _lx = lx;
                                    //Debug.Log($"lx : {lx}");
                                }
                                if (f1.ToString().Contains("\"pm1\":"))
                                {
                                    var pm1 = f1.Value["pm1"]["value"].ToString();
                                    Pm1 = _pm1 = pm1;
                                    //Debug.Log($"pm1 : {pm1}");
                                }
                            }
                            if (f1.ToString().Contains("\"id\":"))
                            {
                                var id = f1.Value.First["id"].ToString();
                                Id = _id = id;
                                //Debug.Log($"id : {id}");
                            }
                            if (f1.ToString().Contains("\"info\":"))
                            {
                                var info = f1.Value.Last["info"];

                                if (info.ToString().Contains("\"vap\":"))
                                {
                                    var vap = info["vap"].ToString();
                                    Vap = _vap = vap;
                                    //Debug.Log($"vap : {vap}");
                                }
                                if (info.ToString().Contains("\"mode\":"))
                                {
                                    var mode = info["mode"].ToString();
                                    Mode = _mode = mode;
                                    //Debug.Log($"mode : {mode}");
                                }
                                if (info.ToString().Contains("\"cpu_temp\":"))
                                {
                                    var cpu_temp = info["cpu_temp"].ToString();
                                    Cpu_temp = _cpu_temp = cpu_temp;
                                    //Debug.Log($"cpu_temp : {cpu_temp}");
                                }
                                if (info.ToString().Contains("\"next_hop\":"))
                                {
                                    var next_hop = info["next_hop"].ToString();
                                    Next_hop = _next_hop = next_hop;
                                    //Debug.Log($"next_hop : {next_hop}");
                                }
                                if (info.ToString().Contains("\"cpu_5m\":"))
                                {
                                    var cpu_5m = info["cpu_5m"].ToString();
                                    Cpu_5m = _cpu_5m = cpu_5m;
                                    //Debug.Log($"cpu_5m : {cpu_5m}");
                                }
                                if (info.ToString().Contains("\"cpu_15m\":"))
                                {
                                    var cpu_15m = info["cpu_15m"].ToString();
                                    Cpu_15m = _cpu_15m = cpu_15m;
                                    //Debug.Log($"cpu_15m : {cpu_15m}");
                                }
                                if (info.ToString().Contains("\"mac\":"))
                                {
                                    var mac = info["mac"].ToString();
                                    Mac = _mac = mac;
                                    //Debug.Log($"mac : {mac}");
                                }
                                if (info.ToString().Contains("\"cpu_1m\":"))
                                {
                                    var cpu_1m = info["cpu_1m"].ToString();
                                    Cpu_1m = _cpu_1m = cpu_1m;
                                    //Debug.Log($"cpu_1m : {cpu_1m}");
                                }
                                if (info.ToString().Contains("\"seq\":"))
                                {
                                    var seq = info["seq"].ToString();
                                    Seq = _seq = seq;
                                    //Debug.Log($"seq : {seq}");
                                }
                                if (info.ToString().Contains("\"ip\":"))
                                {
                                    var ip = info["ip"].ToString();
                                    Ip = _ip = ip;
                                    //Debug.Log($"ip : {ip}");
                                }
                                if (info.ToString().Contains("\"dmesg\":"))
                                {
                                    var dmesg = info["dmesg"].ToString();
                                    Dmesg = _dmesg = dmesg;
                                    //Debug.Log($"dmesg : {dmesg}");
                                }
                            }
                        }
                    }
                }

                foreach (var fElement in jObject.Last)
                {
                    if (fElement.ToString().Contains("timestamp"))
                    {
                        var timestamp = fElement["timestamp"].ToString();
                        Timestamp = _timestamp = timestamp;
                        //Debug.Log(Timestamp);
                    }
                    if (fElement.ToString().Contains("datetime"))
                    {
                        var datetime = fElement["datetime"].ToString();
                        Datetime = _datetime = datetime;
                        //Debug.Log(datetime);
                    }
                    if (fElement.ToString().Contains("node_info") || fElement.ToString().Contains("sensors"))
                    {
                        JObject jObj = JObject.Parse(fElement.ToString());

                        foreach (var f1 in jObj)
                        {
                            if (f1.Key == "sensors")
                            {
                                if (f1.ToString().Contains("\"pm1\":"))
                                {
                                    var pm25 = f1.Value["pm25"]["value"].ToString();
                                    Pm25 = _pm25 = pm25;
                                    //Debug.Log($"pm25 : {pm25}");
                                }
                                if (f1.ToString().Contains("\"temperature\":"))
                                {
                                    var temperature = f1.Value["temperature"]["value"].ToString();
                                    Temperature = _temperature = temperature;
                                    //Debug.Log($"temperature : {temperature}");
                                }
                                if (f1.ToString().Contains("\"humidity\":"))
                                {
                                    var humidity = f1.Value["humidity"]["value"].ToString();
                                    Humidity = _humidity = humidity;
                                    //Debug.Log($"humidity : {humidity}");
                                }
                                if (f1.ToString().Contains("\"dewpoint\":"))
                                {
                                    var dewpoint = f1.Value["dewpoint"]["value"].ToString();
                                    Dewpoint = _dewpoint = dewpoint;
                                    //Debug.Log($"dewpoint : {dewpoint}");
                                }
                                if (f1.ToString().Contains("\"hcho\":"))
                                {
                                    var hcho = f1.Value["hcho"]["value"].ToString();
                                    Hcho = _hcho = hcho;
                                    //Debug.Log($"hcho : {hcho}");
                                }
                                if (f1.ToString().Contains("\"sound\":"))
                                {
                                    var sound = f1.Value["sound"]["value"].ToString();
                                    Sound = _sound = sound;
                                    //Debug.Log($"sound : {sound}");
                                }
                                if (f1.ToString().Contains("\"detect\":"))
                                {
                                    var detect = f1.Value["detect"]["value"].ToString();
                                    Detect = _detect = detect;
                                    //Debug.Log($"detect : {detect}");
                                }
                                if (f1.ToString().Contains("\"pm10\":"))
                                {
                                    var pm10 = f1.Value["pm10"]["value"].ToString();
                                    Pm10 = _pm10 = pm10;
                                    //Debug.Log($"pm10 : {pm10}");
                                }
                                if (f1.ToString().Contains("\"co2\":"))
                                {
                                    var co2 = f1.Value["co2"]["value"].ToString();
                                    Co2 = _co2 = co2;
                                    //Debug.Log($"co2 : {co2}");
                                }
                                if (f1.ToString().Contains("\"lx\":"))
                                {
                                    var lx = f1.Value["lx"]["value"].ToString();
                                    Lx = _lx = lx;
                                    //Debug.Log($"lx : {lx}");
                                }
                                if (f1.ToString().Contains("\"pm1\":"))
                                {
                                    var pm1 = f1.Value["pm1"]["value"].ToString();
                                    Pm1 = _pm1 = pm1;
                                    //Debug.Log($"pm1 : {pm1}");
                                }
                            }
                            else if (f1.Key == "node_info")
                            {
                                if (f1.ToString().Contains("\"id\":"))
                                {
                                    var id = f1.Value.First["id"].ToString();
                                    Id = _id = id;
                                    //Debug.Log($"id : {id}");
                                }
                                if (f1.ToString().Contains("\"info\":"))
                                {
                                    var info = f1.Value.Last["info"];

                                    if (info.ToString().Contains("\"vap\":"))
                                    {
                                        var vap = info["vap"].ToString();
                                        Vap = _vap = vap;
                                        //Debug.Log($"vap : {vap}");
                                    }
                                    if (info.ToString().Contains("\"mode\":"))
                                    {
                                        var mode = info["mode"].ToString();
                                        Mode = _mode = mode;
                                        //Debug.Log($"mode : {mode}");
                                    }
                                    if (info.ToString().Contains("\"cpu_temp\":"))
                                    {
                                        var cpu_temp = info["cpu_temp"].ToString();
                                        Cpu_temp = _cpu_temp = cpu_temp;
                                        //Debug.Log($"cpu_temp : {cpu_temp}");
                                    }
                                    if (info.ToString().Contains("\"next_hop\":"))
                                    {
                                        var next_hop = info["next_hop"].ToString();
                                        Next_hop = _next_hop = next_hop;
                                        //Debug.Log($"next_hop : {next_hop}");
                                    }
                                    if (info.ToString().Contains("\"cpu_5m\":"))
                                    {
                                        var cpu_5m = info["cpu_5m"].ToString();
                                        Cpu_5m = _cpu_5m = cpu_5m;
                                        //Debug.Log($"cpu_5m : {cpu_5m}");
                                    }
                                    if (info.ToString().Contains("\"cpu_15m\":"))
                                    {
                                        var cpu_15m = info["cpu_15m"].ToString();
                                        Cpu_15m = _cpu_15m = cpu_15m;
                                        //Debug.Log($"cpu_15m : {cpu_15m}");
                                    }
                                    if (info.ToString().Contains("\"mac\":"))
                                    {
                                        var mac = info["mac"].ToString();
                                        Mac = _mac = mac;
                                        //Debug.Log($"mac : {mac}");
                                    }
                                    if (info.ToString().Contains("\"cpu_1m\":"))
                                    {
                                        var cpu_1m = info["cpu_1m"].ToString();
                                        Cpu_1m = _cpu_1m = cpu_1m;
                                        //Debug.Log($"cpu_1m : {cpu_1m}");
                                    }
                                    if (info.ToString().Contains("\"seq\":"))
                                    {
                                        var seq = info["seq"].ToString();
                                        Seq = _seq = seq;
                                        //Debug.Log($"seq : {seq}");
                                    }
                                    if (info.ToString().Contains("\"ip\":"))
                                    {
                                        var ip = info["ip"].ToString();
                                        Ip = _ip = ip;
                                        //Debug.Log($"ip : {ip}");
                                    }
                                    if (info.ToString().Contains("\"dmesg\":"))
                                    {
                                        var dmesg = info["dmesg"].ToString();
                                        Dmesg = _dmesg = dmesg;
                                        //Debug.Log($"dmesg : {dmesg}");
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                _timestamp = null;
                _datetime = null;

                _id = null;
                _ip = null;
                _cpu_temp = null;
                _vap = null;
                _seq = null;
                _cpu_5m = null;
                _dmesg = null;
                _cpu_15m = null;
                _cpu_1m = null;
                _mac = null;
                _mode = null;
                _next_hop = null;

                _sound = null;
                _pm25 = null;
                _pm10 = null;
                _detect = null;
                _humidity = null;
                _temperature = null;
                _hcho = null;
                _co2 = null;
                _lx = null;
                _pm1 = null;
                _dewpoint = null;
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
}
