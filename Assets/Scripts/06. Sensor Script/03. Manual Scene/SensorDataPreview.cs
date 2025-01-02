using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SensorDataPreview : MonoBehaviour
{
    // Update is called once per frame
    Color originColor = new Color(0.0f, 0.0f, 1.0f, 0.6f);
    Color changeColor = new Color(0.0f, 0.0f, 0.0f, 0.6f);
    GameObject targetObj;

    [SerializeField] GameObject sensorDataPanel;
    [SerializeField] GameObject sensorDataPreviewPanel;
    [SerializeField] Text previewTextTitle;
    [SerializeField] Text previewText_1;
    [SerializeField] Text previewText_2;
    [SerializeField] Text sensorNumText;
    [SerializeField] Image _graphImg;

    public static bool DataPanelActiveBoolean;

    private void Start()
    {
        sensorDataPanel.SetActive(false);
        sensorDataPreviewPanel.SetActive(false);

        DataPanelActiveBoolean = false;
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        LayerMask mask = LayerMask.GetMask("SensorEx");

        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, 100.0f, mask))
        {
            //Debug.Log(hit.transform.name);
            targetObj = hit.transform.gameObject;
            targetObj.transform.GetComponent<Renderer>().material.color = changeColor;

            //Debug.Log(targetObj.GetComponent<SensorInfo>().name);
            string sensorName = targetObj.GetComponent<SensorInfo>().Name;
            GetSensorData.sensorName = sensorName;

            if (Input.GetMouseButtonDown(0))
            {
                sensorDataPreviewPanel.SetActive(false);
                sensorDataPanel.SetActive(true);
                DataPanelActiveBoolean = true;
            }
            else
            {
                if (sensorDataPanel.activeSelf)
                {
                    previewTextTitle.text = "";
                    previewText_1.text = "";
                    previewTextTitle.text = "";

                    sensorDataPreviewPanel.SetActive(false);
                }
                else
                {
                    previewTextTitle.text = @$"Sensor : floe{sensorName}";
                    previewText_1.text = 
@$"- Sound : {GetSensorData.Sound} 
- Detect : {GetSensorData.Detect}
- Humidity : {GetSensorData.Humidity} 
- Temperature : {GetSensorData.Temperature}";

                    previewText_2.text =
@$"- Co2 : {GetSensorData.Co2} 
- Lx : {GetSensorData.Lx}
- Dewpoint : {GetSensorData.Dewpoint}";

                    sensorDataPreviewPanel.SetActive(true);
                    sensorNumText.text = $"floe{sensorName}";
                }
            }
        }
        else
        {
            if(targetObj != null)
            {
                sensorDataPreviewPanel.SetActive(false);
                targetObj.transform.GetComponent<Renderer>().material.color = originColor;
                targetObj = null;
            }
        }
    }

    public void ExitPanel()
    {
        _graphImg.sprite = null;
        sensorDataPanel.SetActive(false);
        DataPanelActiveBoolean = false;
    }
}
