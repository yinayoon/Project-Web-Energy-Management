using UnityEngine;
using UnityEngine.UI;

public class TriggerSensor : MonoBehaviour
{
    [Tooltip("Doesn't have to be assigned in the manual scene.")]
    [SerializeField] Text sensorNameText;

    public static string SensorName;
    GameObject targetObj;

    public void Update()
    {
        if (SensorDataPreview.DataPanelActiveBoolean)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        LayerMask mask = LayerMask.GetMask("SensorEx");

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100.0f, mask))
        {            
            targetObj = hit.transform.gameObject;
            
            string sensorName = targetObj.GetComponent<SensorInfo>().Name;
            SensorName = sensorName;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sensor")
        {
            if (sensorNameText != null)
                sensorNameText.text = other.transform.GetChild(0).transform.GetComponent<SensorInfo>().Name;
    
            SensorName = other.transform.GetChild(0).transform.GetComponent<SensorInfo>().Name;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Sensor")
        {
            if (sensorNameText != null)
                sensorNameText.text = "";

            SensorName = "";
        }
    }
}
