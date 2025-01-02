using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerEx : MonoBehaviour
{
    [SerializeField] bool MoveSign;
    [SerializeField] GameObject SensorGui;
    [SerializeField] Image graphImage;

    Vector3 pos;

    GameObject buttonTarget;
    GameObject sensorTarget;
    GameObject sensorsTarget;

    private void Start()
    {
        if (MoveSign == false)
            SensorGui = null;

        else
        {
            transform.position = PosData.Pos;
            pos = PosData.Pos;
            SensorGui.transform.GetChild(2).gameObject.SetActive(false);

            GameObject[] sensorTags = GameObject.FindGameObjectsWithTag("Sensor");
            for (int i = 0; i < sensorTags.Length; i++)
            {
                sensorTags[i].transform.GetChild(1).transform.gameObject.SetActive(false);
                sensorTags[i].transform.GetChild(2).transform.gameObject.SetActive(false);
            }
        }
    }

    bool showData;
    private void Update()
    {
        if (MoveSign)
        {
            if (SensorGui.transform.GetChild(2).gameObject.activeSelf == false)
            {
                PlayerMove();
            }

            if (showData && sensorsTarget != null)
            {
                sensorsTarget.transform.GetChild(0).transform.GetChild(0).transform.GetComponent<Text>().text =
@$" - Name : floe{GetSensorData.sensorName}

 - Sound : {GetSensorData.Sound}
 - Detect : {GetSensorData.Detect}
 - Humidity : {GetSensorData.Humidity}
 - Temperature : {GetSensorData.Temperature}
 - Co2 : {GetSensorData.Co2}
 - Lx : {GetSensorData.Lx}
 - Dewpoint : {GetSensorData.Dewpoint}";
            }
        }
    }

    public void PlayerMove()
    {
        if (MoveSign == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 10))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (hit.collider.tag == "MoveScope")
                        pos = hit.point;                    
                }
                else if (Input.GetMouseButton(0))
                {
                    if (hit.collider.tag == "MoveScope")
                        pos = hit.point;

                    Quaternion q = MoveFunc(transform.GetChild(0).transform.rotation.eulerAngles, true);
                    Quaternion qScope = MoveFunc(transform.GetChild(1).transform.rotation.eulerAngles, false);
                    transform.GetChild(0).rotation = Quaternion.Slerp(transform.GetChild(0).rotation, q, 10); // 자연스럽게 회전
                    transform.GetChild(1).rotation = Quaternion.Slerp(transform.GetChild(1).rotation, qScope, 10); // 자연스럽게 회전
                }
            }

            // 컬러 변환 및 버튼 애니메이션
            if (Physics.Raycast(ray, out hit, 10, LayerMask.GetMask("MoveScope")))
            {   
                if (buttonTarget == null)
                    buttonTarget = hit.collider.gameObject;

                if (buttonTarget != null)
                {
                    Color col = buttonTarget.GetComponent<Renderer>().material.color;
                    col = Color.red;
                    col.a = 0.8f;
                    buttonTarget.GetComponent<Renderer>().material.color = col;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    StartCoroutine("MoveButtonClickAnim");
                }
            }
            else
            {
                if (buttonTarget != null)
                {
                    Color col = buttonTarget.GetComponent<Renderer>().material.color;
                    col = Color.black;
                    col.a = 0.8f;
                    buttonTarget.GetComponent<Renderer>().material.color = col;
                }
            }

            // 센서
            if (Physics.Raycast(ray, out hit, 1.5f, LayerMask.GetMask("Sensor")))
            {
                //Debug.Log(hit.point);
                if (sensorTarget == null)
                    sensorTarget = hit.collider.gameObject;

                if (sensorTarget != null)
                {
                    Color col = sensorTarget.GetComponent<Renderer>().material.color;
                    col = Color.red;
                    col.a = 0.8f;
                    sensorTarget.GetComponent<Renderer>().material.color = col;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    GameObject.Find("Preview Map Object").transform.GetChild(0).GetComponent<Image>().enabled = false;
                    SensorGui.transform.GetChild(2).gameObject.SetActive(true);
                    //SensorGui.transform.GetChild(1).gameObject.SetActive(true);

                    sensorsTarget.transform.GetChild(0).transform.GetChild(0).transform.GetComponent<Text>().text = "";
                    showData = false;

                    sensorsTarget.SetActive(false);
                    sensorsTarget.transform.parent.GetChild(2).gameObject.SetActive(false);
                }
            }
            else
            {
                if (sensorTarget != null)
                {
                    sensorTarget.GetComponent<Renderer>().material.color = Color.black;
                    sensorTarget = null;
                }
            }

            if (sensorsTarget != null)
                sensorsTarget.transform.rotation = Camera.main.transform.rotation;

            pos.y = 1.5f;
            if (moveScopeRay.MoveSign) 
                transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * 3);
        }
    }

    IEnumerator MoveButtonClickAnim()
    {
        Vector3 beforeVec;
        for (int i = 0; i < 5; i++)
        {
            beforeVec = buttonTarget.transform.position;
            beforeVec.y -= 0.01f;
            buttonTarget.transform.position = beforeVec;

            yield return new WaitForSeconds(0.01f);
        }

        Vector3 afterVec;
        for (int i = 0; i < 5; i++)
        {
            afterVec = buttonTarget.transform.position;
            afterVec.y += 0.01f;
            buttonTarget.transform.position = afterVec;

            yield return new WaitForSeconds(0.01f);
        }
    }

    public Quaternion MoveFunc(Vector3 vec, bool b)
    {
        if (b == true)
        {
            vec.y += Input.GetAxis("Mouse X") * 5; // 마우스 X 위치 * 회전 스피드
            //vec.x += -1 * Input.GetAxis("Mouse Y") * 5; // 마우스 Y 위치 * 회전 스피드
        }
        else
            vec.y += Input.GetAxis("Mouse X") * 5; // 마우스 X 위치 * 회전 스피드

        Quaternion q = Quaternion.Euler(vec); // Quaternion으로 변환
        q.z = 0;

        return q;
    }

    public void GuiExitButton()
    {
        graphImage.sprite = null;
        GameObject.Find("Preview Map Object").transform.GetChild(0).GetComponent<Image>().enabled = true;
        SensorGui.transform.GetChild(2).gameObject.SetActive(false);
        //SensorGui.transform.GetChild(1).gameObject.SetActive(false);
        sensorsTarget.SetActive(true);
        sensorsTarget.transform.parent.GetChild(2).gameObject.SetActive(true);

        // 넘어오는 Data로 Text 내용 변경
        showData = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sensor")
        {
            other.transform.GetChild(1).gameObject.SetActive(true);
            sensorsTarget = other.transform.GetChild(1).gameObject;
            sensorsTarget.transform.parent.GetChild(2).gameObject.SetActive(true);

            // 넘어오는 Data로 Text 내용 변경
            showData = true;

            GetSensorData.sensorName = sensorsTarget.transform.parent.GetChild(0).GetComponent<SensorInfo>().name;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Sensor")
        {
            other.transform.GetChild(1).gameObject.SetActive(false);
            if (sensorsTarget != null)
            {
                sensorsTarget.transform.GetChild(0).transform.GetChild(0).transform.GetComponent<Text>().text = "";
                sensorsTarget.transform.parent.GetChild(2).gameObject.SetActive(false);
                sensorsTarget = null;
            }
            showData = false;
            GetSensorData.sensorName = null;
        }
    }
}