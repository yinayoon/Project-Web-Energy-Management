using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Data_Human : MonoBehaviour
{
    [Header("- Prefab")]
    public GameObject Human;

    [Header("- Floor Num")]
    public int floorNum;

    [Header("- RoomPos")]
    [SerializeField] GameObject R1_Pos;
    [SerializeField] GameObject R2_Pos;
    [SerializeField] GameObject R3_Pos;
    [SerializeField] GameObject R4_Pos;
    [SerializeField] GameObject R5_Pos;
    [SerializeField] GameObject R6_Pos;
    [SerializeField] GameObject R7_Pos;
    [SerializeField] GameObject R8_Pos;
    [SerializeField] GameObject R9_Pos;
    [SerializeField] GameObject R10_Pos;
    [SerializeField] GameObject R11_Pos;
    [SerializeField] GameObject R12_Pos;

    [HideInInspector] public int r1Num;
    [HideInInspector] public int r2Num;
    [HideInInspector] public int r3Num;
    [HideInInspector] public int r4Num;
    [HideInInspector] public int r5Num;
    [HideInInspector] public int r6Num;
    [HideInInspector] public int r7Num;
    [HideInInspector] public int r8Num;
    [HideInInspector] public int r9Num;
    [HideInInspector] public int r10Num;
    [HideInInspector] public int r11Num;
    [HideInInspector] public int r12Num;

    string[] timestampArray;

    [HideInInspector] public string[] valueArray;
    [HideInInspector] public string[] placeArray;

    KetiCamData[] DataClassArray;
    KetiCamData[] DataClassArray_sort;

    JArray jArray;

    public static int R1Num;
    public static int R2Num;
    public static int R3Num;
    public static int R4Num;
    public static int R5Num;
    public static int R6Num;
    public static int R7Num;
    public static int R8Num;
    public static int R9Num;
    public static int R10Num;
    public static int R11Num;
    public static int R12Num;

    string jsonText;
    string url_3 = "http://io.energyiotlab.com:54242/api/query/last?timeseries=dankook-ac.camera.occ%7Bplace=3-3,type=count,sensor=nop-cam%7D&timeseries=dankook-ac.camera.occ%7Bplace=3-4,type=count,sensor=nop-cam%7D&timeseries=dankook-ac.camera.occ%7Bplace=3-5,type=count,sensor=nop-cam%7D&timeseries=dankook-ac.camera.occ%7Bplace=3-6,type=count,sensor=nop-cam%7D&timeseries=dankook-ac.camera.occ%7Bplace=3-7,type=count,sensor=nop-cam%7D&timeseries=dankook-ac.camera.occ%7Bplace=3-8,type=count,sensor=nop-cam%7D&timeseries=dankook-ac.camera.occ%7Bplace=3-9,type=count,sensor=nop-cam%7D&timeseries=dankook-ac.camera.occ%7Bplace=3-10,type=count,sensor=nop-cam%7D&timeseries=dankook-ac.camera.occ%7Bplace=3-11,type=count,sensor=nop-cam%7D&timeseries=dankook-ac.camera.occ%7Bplace=3-12,type=count,sensor=nop-cam%7D&back_scan=1&resolve=true";
    
    string url_4 = "http://io.energyiotlab.com:54242/api/query/last?timeseries=dankook-ac.camera.occ%7Bplace=4-3,type=count,sensor=nop-cam%7D&timeseries=dankook-ac.camera.occ%7Bplace=4-4,type=count,sensor=nop-cam%7D&timeseries=dankook-ac.camera.occ%7Bplace=4-5,type=count,sensor=nop-cam%7D&timeseries=dankook-ac.camera.occ%7Bplace=4-6,type=count,sensor=nop-cam%7D&timeseries=dankook-ac.camera.occ%7Bplace=4-7,type=count,sensor=nop-cam%7D&timeseries=dankook-ac.camera.occ%7Bplace=4-8,type=count,sensor=nop-cam%7D&timeseries=dankook-ac.camera.occ%7Bplace=4-9,type=count,sensor=nop-cam%7D&timeseries=dankook-ac.camera.occ%7Bplace=4-10,type=count,sensor=nop-cam%7D&timeseries=dankook-ac.camera.occ%7Bplace=4-11,type=count,sensor=nop-cam%7D&timeseries=dankook-ac.camera.occ%7Bplace=4-12,type=count,sensor=nop-cam%7D&back_scan=1&resolve=true";
    
    string url;
    string str;

    int[] humanNum;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("GetRequest"); // 단국대 진리관
        StartCoroutine("CoGetJsonData");

        humanNum = new int[12];
        for (int i = 0; i < humanNum.Length; i++)
        {
            humanNum[i] = int.MaxValue;
        }
    }

    private void OnEnable()
    {
        StartCoroutine("GetRequest"); // 단국대 진리관
        StartCoroutine("CoGetJsonData");

        humanNum = new int[12];
        for (int i = 0; i < humanNum.Length; i++)
        {
            humanNum[i] = int.MaxValue;
        }
    }

    IEnumerator GetRequest()
    {
        while (true)
        {
            if (floorNum == 3)
                url = url_3;
            else if (floorNum == 4)
                url = url_4;

            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                if (url != null)
                {
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
    }

    struct KetiCamData : IComparable<KetiCamData>
    {
        public int Id;
        public string timestamp;
        public string value;
        public string place;

        public int CompareTo(KetiCamData other)
        {
            if (Id == other.Id)
                return 0;
            return Id < other.Id ? 1 : -1;
        }
    }

    PriorityQueue<KetiCamData> pq = new PriorityQueue<KetiCamData>();

    IEnumerator CoGetJsonData()
    {
        while (true)
        {
            if (jsonText != null)
            {                
                str = jsonText;
                jArray = JArray.Parse(jsonText);

                List<JToken> jTokenGroup = new List<JToken>();

                List<string> timestampList = new List<string>();
                List<string> valueList = new List<string>();
                List<string> placeList = new List<string>();

                foreach (JToken jChild in jArray)
                {
                    string Timestamp = jChild["timestamp"].ToString();
                    string Value = jChild["value"].ToString();
                    string Place = jChild["tags"]["place"].ToString();

                    timestampList.Add(Timestamp);
                    valueList.Add(Value);
                    placeList.Add(Place);
                }

                timestampArray = new string[jArray.Count];
                valueArray = new string[jArray.Count];
                placeArray = new string[jArray.Count];
                DataClassArray = new KetiCamData[jArray.Count];
                DataClassArray_sort = new KetiCamData[jArray.Count];
                pq = new PriorityQueue<KetiCamData>(); ;

                for (int i = 0; i < jArray.Count; i++)
                {
                    timestampArray[i] = timestampList[i];
                    valueArray[i] = valueList[i];
                    placeArray[i] = placeList[i];

                    int idx = placeList[i].IndexOf($"{floorNum}-");
                    string idStr = placeList[i].Substring(idx + 2);
                    DataClassArray[i].Id = int.Parse(idStr);
                    DataClassArray[i].timestamp = timestampList[i];
                    DataClassArray[i].value = valueList[i];
                    DataClassArray[i].place = placeList[i];
                }

                foreach (var child in DataClassArray)
                {
                    pq.Push(child);
                }

                for (int i = 0; i < jArray.Count; i++)
                {
                    DataClassArray_sort[i] = pq.Pop();
                }

                CreatHuman();                
            }
            else
            {
                timestampArray = null;
                valueArray = null;
                placeArray = null;
                DataClassArray = null;
                DataClassArray_sort = null;
                pq = null;
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void Update()
    {
        if (SceneManager.GetActiveScene().name == "Main Scene (Choice Floor)")
        {
            CountHumanForGui();
        }
        else if (SceneManager.GetActiveScene().name != "Main Scene (Choice Floor)")
        {
            CreatHuman();
        }
    }

    void CountHumanForGui()
    {
        if (DataClassArray_sort == null)
            return;

        foreach (var child in DataClassArray_sort)
        {
            switch (child.Id)
            {
                case 1:
                        R1Num = int.Parse(child.value);
                    r1Num = int.Parse(child.value);
                    //Debug.Log(R1Num);

                    break;
                case 2:
                        R2Num = int.Parse(child.value);
                    r2Num = int.Parse(child.value);
                    //Debug.Log(R2Num);

                    break;
                case 3:
                        R3Num = int.Parse(child.value);
                    r3Num = int.Parse(child.value);
                    //Debug.Log(R3Num);

                    break;
                case 4:
                        R4Num = int.Parse(child.value);
                    r4Num = int.Parse(child.value);

                    //Debug.Log(R4Num);

                    break;
                case 5:
                        R5Num = int.Parse(child.value);
                    r5Num = int.Parse(child.value);

                    //Debug.Log(R5Num);

                    break;
                case 6:
                        R6Num = int.Parse(child.value);
                    r6Num = int.Parse(child.value);
                    //Debug.Log(R6Num);

                    break;
                case 7:
                        R7Num = int.Parse(child.value);
                    r7Num = int.Parse(child.value);
                    //Debug.Log(R7Num);

                    break;
                case 8:
                        R8Num = int.Parse(child.value);
                    r8Num = int.Parse(child.value);
                    //Debug.Log(R8Num);
                    break;
                case 9:
                        R9Num = int.Parse(child.value);
                    r9Num = int.Parse(child.value);
                    //Debug.Log(R9Num);

                    break;
                case 10:
                        R10Num = int.Parse(child.value);
                    r10Num = int.Parse(child.value);
                    //Debug.Log(R10Num);

                    break;
                case 11:
                        R11Num = int.Parse(child.value);
                    r11Num = int.Parse(child.value);
                    //Debug.Log(R11Num);

                    break;
                case 12:
                        R12Num = int.Parse(child.value);
                    r12Num = int.Parse(child.value);
                    //Debug.Log(R12Num);

                    break;
                default:
                    break;
            }
        }
    }

    public void CreatHuman()
    {
        if (DataClassArray_sort == null)
            return;

        foreach (var child in DataClassArray_sort)
        {
            switch (child.Id)
            {
                case 1:
                    if (humanNum[0] != int.Parse(child.value))
                    {
                        DestroyHuman($"{floorNum}-1");

                        R1Num = humanNum[0] = int.Parse(child.value);
                        CreatHumanCore(humanNum[0], R1_Pos, $"{floorNum}-1");
                    }

                    break;
                case 2:
                    if (humanNum[1] != int.Parse(child.value))
                    {
                        DestroyHuman($"{floorNum}-2");

                        R2Num = humanNum[1] = int.Parse(child.value);
                        CreatHumanCore(humanNum[1], R2_Pos, $"{floorNum}-2");
                    }

                    break;
                case 3:
                    if (humanNum[2] != int.Parse(child.value))
                    {
                        DestroyHuman($"{floorNum}-3");

                        R3Num = humanNum[2] = int.Parse(child.value);
                        CreatHumanCore(humanNum[2], R3_Pos, $"{floorNum}-3");
                    }

                    break;
                case 4:
                    if (humanNum[3] != int.Parse(child.value))
                    {
                        DestroyHuman($"{floorNum}-4");

                        R4Num = humanNum[3] = int.Parse(child.value);
                        CreatHumanCore(humanNum[3], R4_Pos, $"{floorNum}-4");
                    }

                    break;
                case 5:
                    if (humanNum[4] != int.Parse(child.value))
                    {
                        DestroyHuman($"{floorNum}-5");

                        R5Num = humanNum[4] = int.Parse(child.value);
                        CreatHumanCore(humanNum[4], R5_Pos, $"{floorNum}-5");
                    }

                    break;
                case 6:
                    if (humanNum[5] != int.Parse(child.value))
                    {
                        DestroyHuman($"{floorNum}-6");

                        R6Num = humanNum[5] = int.Parse(child.value);
                        CreatHumanCore(humanNum[5], R6_Pos, $"{floorNum}-6");
                    }

                    break;
                case 7:
                    if (humanNum[6] != int.Parse(child.value))
                    {
                        DestroyHuman($"{floorNum}-7");

                        R7Num = humanNum[6] = int.Parse(child.value);
                        CreatHumanCore(humanNum[6], R7_Pos, $"{floorNum}-7");
                    }

                    break;
                case 8:
                    if (humanNum[7] != int.Parse(child.value))
                    {
                        DestroyHuman($"{floorNum}-8");

                        R8Num = humanNum[7] = int.Parse(child.value);
                        CreatHumanCore(humanNum[7], R8_Pos, $"{floorNum}-8");
                    }

                    break;
                case 9:
                    if (humanNum[8] != int.Parse(child.value))
                    {
                        DestroyHuman($"{floorNum}-9");

                        R9Num = humanNum[8] = int.Parse(child.value);
                        CreatHumanCore(humanNum[8], R9_Pos, $"{floorNum}-9");
                    }

                    break;
                case 10:
                    if (humanNum[9] != int.Parse(child.value))
                    {
                        DestroyHuman($"{floorNum}-10");

                        R10Num = humanNum[9] = int.Parse(child.value);
                        CreatHumanCore(humanNum[9], R10_Pos, $"{floorNum}-10");
                    }

                    break;
                case 11:
                    if (humanNum[10] != int.Parse(child.value))
                    {
                        DestroyHuman($"{floorNum}-11");

                        R11Num = humanNum[10] = int.Parse(child.value);
                        CreatHumanCore(humanNum[10], R11_Pos, $"{floorNum}-11");
                    }

                    break;
                case 12:
                    if (humanNum[11] != int.Parse(child.value))
                    {
                        DestroyHuman($"{floorNum}-12");
                        R12Num = humanNum[11] = int.Parse(child.value);
                        CreatHumanCore(humanNum[11], R12_Pos, $"{floorNum}-12");
                    }

                    break;
                default:
                    break;
            }
        }
    }

    public void DestroyHuman(string tag)
    {
        GameObject[] obj = GameObject.FindGameObjectsWithTag(tag);
        if (obj != null)
        {
            foreach (var objChild in obj)
            {
                GameObject.Destroy(objChild);
            }
        }
    }

    public void CreatHumanCore(int Id, GameObject R, string tag)
    {
        for (int i = 0; i < Id; i++)
        {
            GameObject gb = Instantiate<GameObject>(Human);
            gb.tag = tag;

            gb.transform.GetComponent<ModelCollisionProtection>().originPos = R.transform.position;
            Vector3 vec = R.transform.position;
            vec = vec + new Vector3(UnityEngine.Random.Range(-1f, 1f), 0, UnityEngine.Random.Range(-1f, 1f));

            gb.transform.position = vec;

            gb.transform.parent = R.transform;
         }

    }
}
