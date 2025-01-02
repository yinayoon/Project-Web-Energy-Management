using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChoceFloor : MonoBehaviour
{
    GameObject target;

    [Header("- GUI")]
    [SerializeField]
    Toggle tog;
    [SerializeField]
    Text floorText;
    [SerializeField]
    Text floor3Text;
    [SerializeField]
    Text floor4Text;

    [Header ("- GameObject")]
    [SerializeField]
    GameObject floor4Ceiling;
    [SerializeField]
    GameObject choice3FloorGUI;
    [SerializeField]
    GameObject choice4FloorGUI;
    [SerializeField]
    GameObject clickRoomCubeGroup3;
    [SerializeField]
    GameObject clickRoomCubeGroup4;
    [SerializeField]
    GameObject humanManagerGroup;
    [SerializeField]
    GameObject sensorsGroup;
    [SerializeField]
    List<GameObject> sensor3thFloorGameObjects;
    [SerializeField]
    List<GameObject> sensor4thFloorGameObjects;
    [SerializeField]
    List<GameObject> sensor3thFloorSphereGameObjects;
    [SerializeField]
    List<GameObject> sensor4thFloorSphereGameObjects;

    [Header("- Class")]
    [SerializeField]
    AnimFloorModel animFloorModel;

    Color selectCol = new Color(0.2117647f, 0.7802606f, 0.8784314f, 0.8235294f);

    Color sensorSphereOriginCol = new Color(1, 0.03915456f, 0, 0.6f);
    Color sensorOriginCol = new Color(0, 0, 0, 1);

    // Start is called before the first frame update
    void Start()
    {
        choice3FloorGUI.SetActive(false);
        choice4FloorGUI.SetActive(false);

        clickRoomCubeGroup3.SetActive(false);
        clickRoomCubeGroup4.SetActive(false);

        humanManagerGroup.SetActive(false);
        sensorsGroup.SetActive(false);

        floor3Text.transform.parent.transform.gameObject.SetActive(false);
        floor4Text.transform.parent.transform.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ChoiceFloorToggle();

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        LayerMask mask = LayerMask.GetMask("3Floor") | LayerMask.GetMask("4Floor");

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, mask))
        {
            if (target != null && hit.transform != target.transform)
            {
                Transform[] targetChildren = target.transform.GetComponentsInChildren<Transform>();
                foreach (var child in targetChildren)
                {
                    child.transform.GetComponent<MeshRenderer>().material.color = Color.white;
                }
            }

            target = hit.transform.gameObject;

            Transform[] allChildren = target.transform.GetComponentsInChildren<Transform>();

            foreach (var child in allChildren)
            {
                child.transform.GetComponent<MeshRenderer>().material.color = selectCol;//Color.red;
            }
            
            if (target.gameObject.layer == LayerMask.NameToLayer("3Floor"))
            {
                floor3Text.color = selectCol;//Color.blue;
                floor4Text.color = Color.black;

                for (int i = 0; i < sensor3thFloorSphereGameObjects.Count; i++)
                    sensor3thFloorSphereGameObjects[i].GetComponent<MeshRenderer>().material.color = selectCol;
                for (int i = 0; i < sensor4thFloorSphereGameObjects.Count; i++)
                    sensor4thFloorSphereGameObjects[i].GetComponent<MeshRenderer>().material.color = sensorSphereOriginCol;

                if (tog.isOn)
                    floorText.text = $"柳府包 3摸 {ChoiceRoom.RoomNum}龋";
                else
                    floorText.text = $"柳府包 3摸";

                if (Input.GetMouseButtonDown(0))
                {
                    choice3FloorGUI.SetActive(true);
                }
            }
            else if (target.gameObject.layer == LayerMask.NameToLayer("4Floor"))
            {
                floor4Text.color = selectCol;// Color.blue;
                floor3Text.color = Color.black;

                for (int i = 0; i < sensor3thFloorSphereGameObjects.Count; i++)
                    sensor3thFloorSphereGameObjects[i].GetComponent<MeshRenderer>().material.color = sensorSphereOriginCol;
                for (int i = 0; i < sensor4thFloorSphereGameObjects.Count; i++)
                    sensor4thFloorSphereGameObjects[i].GetComponent<MeshRenderer>().material.color = selectCol;

                if (tog.isOn)
                    floorText.text = $"柳府包 4摸 {ChoiceRoom.RoomNum}龋";
                else
                    floorText.text = $"柳府包 4摸";

                if (Input.GetMouseButtonDown(0))
                {
                    choice4FloorGUI.SetActive(true);
                }
            }
        }
        else
        {
            floor3Text.color = Color.black;
            floor4Text.color = Color.black;

            for (int i = 0; i < sensor3thFloorSphereGameObjects.Count; i++)
                sensor3thFloorSphereGameObjects[i].GetComponent<MeshRenderer>().material.color = sensorSphereOriginCol;
            for (int i = 0; i < sensor4thFloorSphereGameObjects.Count; i++)
                sensor4thFloorSphereGameObjects[i].GetComponent<MeshRenderer>().material.color = sensorSphereOriginCol;
            

            floorText.text = "Dankook";

            if (target != null)
            {
                Transform[] allChildren = target.transform.GetComponentsInChildren<Transform>();
                foreach (var child in allChildren)
                {
                    child.transform.GetComponent<MeshRenderer>().material.color = Color.white;
                }

                target = null;
            }
        }

        floor3Text.transform.parent.transform.gameObject.transform.rotation = Camera.main.transform.rotation;
        floor4Text.transform.parent.transform.gameObject.transform.rotation = Camera.main.transform.rotation;
    }

    bool sign = false;
    public void ChoiceFloorToggle()
    {
        if (!tog.isOn)
        {
            if (sign)
            {
                animFloorModel.Fold();
                floor4Ceiling.SetActive(true);
                clickRoomCubeGroup3.SetActive(false);
                clickRoomCubeGroup4.SetActive(false);

                humanManagerGroup.SetActive(false);
                sensorsGroup.SetActive(false);

                floor3Text.transform.parent.transform.gameObject.SetActive(false);
                floor4Text.transform.parent.transform.gameObject.SetActive(false);
                sign = false;
            }
        }
        else if (tog.isOn)
        {
            if (!sign)
            {
                animFloorModel.Unfold();
                floor4Ceiling.SetActive(false);
                clickRoomCubeGroup3.SetActive(true);
                clickRoomCubeGroup4.SetActive(true);

                humanManagerGroup.SetActive(true);
                sensorsGroup.SetActive(true);

                floor3Text.transform.parent.transform.gameObject.SetActive(true);
                floor4Text.transform.parent.transform.gameObject.SetActive(true);
                sign = true;
            }
        }
    }
}