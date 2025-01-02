using UnityEngine;
using UnityEngine.UI;

public class CameraManagerManualScene : MonoBehaviour
{
    [SerializeField] CameraController _cameraController;

    [Header("- GUI")]
    [SerializeField] GameObject _sensorGuiGroup;
    [SerializeField] Toggle Anim3dToggle;
    [SerializeField] Text Anim3dToggleText;

    // Start is called before the first frame update
    void Start()
    {
        CsState(false);

        Anim3dToggle.isOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeState(Anim3dToggle.isOn);
    }

    void ChangeState(bool state)
    {
        switch (state)
        {
            case true:
                Anim3dToggleText.text = "¸ðµ¨¸µ È¸Àü : On";
                _sensorGuiGroup.SetActive(!state);
                CsState(state);
                break;
            case false:
                Anim3dToggleText.text = "¸ðµ¨¸µ È¸Àü : Off";
                transform.rotation = Quaternion.identity;
                _sensorGuiGroup.SetActive(!state);
                CsState(state);
                break;
        }
    }

    void CsState(bool state)
    {
        _cameraController.enabled = state;
    }
}
