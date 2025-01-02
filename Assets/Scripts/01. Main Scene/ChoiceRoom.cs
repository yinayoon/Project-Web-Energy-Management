using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChoiceRoom : MonoBehaviour
{
    [Header("- GUI")]
    [SerializeField]
    Text humanCount;
    [SerializeField]
    Text sensorCount;

    [SerializeField]
    Data_Human dataHuman3;
    [SerializeField]
    Data_Human dataHuman4;

    static public string RoomNum;
    GameObject target;
    Color clickCol = new Color(0.0f, 0.0f, 0.0f, 0.5f);
    Color originCol = new Color(0.0f, 0.0f, 0.0f, 0.0f);

    public void Start()
    {
        humanCount.transform.gameObject.SetActive(false);
        sensorCount.transform.gameObject.SetActive(false);
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        LayerMask mask = LayerMask.GetMask("RoomBox");
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, mask))
        {
            RoomData3Floor(RoomNum);
            RoomData4Floor(RoomNum);

            if (target == null)
            {
                target = hit.transform.gameObject;
                target.transform.GetComponent<Renderer>().material.color = clickCol;
            }
            else if (hit.transform.gameObject != target)
            {
                target.transform.GetComponent<Renderer>().material.color = originCol;
                target = hit.transform.gameObject;
                target.transform.GetComponent<Renderer>().material.color = clickCol;
            }

            RoomNum = hit.transform.gameObject.name;

            humanCount.gameObject.SetActive(true);
            sensorCount.gameObject.SetActive(true);
        }
        else
        {
            if (target != null)
            {
                target.transform.GetComponent<Renderer>().material.color = originCol;
                target = null;

                humanCount.gameObject.SetActive(false);
                sensorCount.gameObject.SetActive(false);
            }
        }
    }

    public void RoomData3Floor(string idx)
    {
        switch (idx)
        {
            case "303":
                //Debug.Log("303");
                humanCount.text = $"사람 수 : {dataHuman3.r3Num}";
                sensorCount.text = $"센서 수 : 2";
                break;
            case "304":
                //Debug.Log("304");
                humanCount.text = $"사람 수 : {dataHuman3.r4Num}";
                sensorCount.text = $"센서 수 : 2";
                break;
            case "305":
                //Debug.Log("305");
                humanCount.text = $"사람 수 : {dataHuman3.r5Num}";
                sensorCount.text = $"센서 수 : 2";
                break;
            case "306":
                //Debug.Log("306");
                humanCount.text = $"사람 수 : {dataHuman3.r6Num}";
                sensorCount.text = $"센서 수 : 2";
                break;
            case "307":
                //Debug.Log("307");
                humanCount.text = $"사람 수 : {dataHuman3.r7Num}";
                sensorCount.text = $"센서 수 : 2";
                break;
            case "308":
                //Debug.Log("308");
                humanCount.text = $"사람 수 : {dataHuman3.r8Num}";
                sensorCount.text = $"센서 수 : 2";
                break;
            case "309":
                //Debug.Log("309");
                humanCount.text = $"사람 수 : {dataHuman3.r9Num}";
                sensorCount.text = $"센서 수 : 2";
                break;
            case "310":
                //Debug.Log("310");
                humanCount.text = $"사람 수 : {dataHuman3.r10Num}";
                sensorCount.text = $"센서 수 : 2";
                break;
            case "311":
                //Debug.Log("311");
                humanCount.text = $"사람 수 : {dataHuman3.r11Num}";
                sensorCount.text = $"센서 수 : 2";
                break;
            case "312":
                //Debug.Log("312");
                humanCount.text = $"사람 수 : {dataHuman3.r12Num}";
                sensorCount.text = $"센서 수 : 2";
                break;
        }
    }

    public void RoomData4Floor(string idx)
    {
        switch (idx)
        {
            case "403":
                //Debug.Log("403");
                humanCount.text = $"사람 수 : {dataHuman4.r3Num}";
                sensorCount.text = $"센서 수 : 2";
                break;
            case "404":
                //Debug.Log("404");
                humanCount.text = $"사람 수 : {dataHuman4.r4Num}";
                sensorCount.text = $"센서 수 : 2";
                break;
            case "405":
                //Debug.Log("405");
                humanCount.text = $"사람 수 : {dataHuman4.r5Num}";
                sensorCount.text = $"센서 수 : 2";
                break;
            case "406":
                //Debug.Log("406");
                humanCount.text = $"사람 수 : {dataHuman4.r6Num}";
                sensorCount.text = $"센서 수 : 2";
                break;
            case "407":
                //Debug.Log("407");
                humanCount.text = $"사람 수 : {dataHuman4.r7Num}";
                sensorCount.text = $"센서 수 : 2";
                break;
            case "408":
                //Debug.Log("408");
                humanCount.text = $"사람 수 : {dataHuman4.r8Num}";
                sensorCount.text = $"센서 수 : 2";
                break;
            case "409":
                //Debug.Log("409");
                humanCount.text = $"사람 수 : {dataHuman4.r9Num}";
                sensorCount.text = $"센서 수 : 2";
                break;
            case "410":
                //Debug.Log("410");
                humanCount.text = $"사람 수 : {dataHuman4.r10Num}";
                sensorCount.text = $"센서 수 : 2";
                break;
            case "411":
                //Debug.Log("411");
                humanCount.text = $"사람 수 : {dataHuman4.r11Num}";
                sensorCount.text = $"센서 수 : 2";
                break;
            case "412":
                //Debug.Log("412");
                humanCount.text = $"사람 수 : {dataHuman4.r12Num}";
                sensorCount.text = $"센서 수 : 2";
                break;
        }
    }
}
