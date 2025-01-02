using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LoadSceneEx : MonoBehaviour
{
    public GameObject automaticFloorUIGo;
    public GameObject manualFloorUIGo;

    public GameObject loadScenePanel3;
    public GameObject loadScenePanel4;

    public Dropdown dropdown;

    public void LoadNextScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Load3FloorScene()
    {
        if (dropdown.value == 0)
            SceneManager.LoadScene("Automatic Scene 3");
        if (dropdown.value == 1)
            SceneManager.LoadScene("Manual Scene 3");
    }

    public void Load4FloorScene()
    {
        if (dropdown.value == 0)
            SceneManager.LoadScene("Automatic Scene 4");
        if (dropdown.value == 1)
            SceneManager.LoadScene("Manual Scene 4");
    }

    public void Update()
    {
        if (automaticFloorUIGo == null || manualFloorUIGo == null)
            return;

        if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0))
        {
            automaticFloorUIGo.SetActive(false);
            manualFloorUIGo.SetActive(false);
        }
    }

    public void OnOffAutomaticFloorUIGo()
    {
        automaticFloorUIGo.SetActive(true);
        manualFloorUIGo.SetActive(false);
    }

    public void OnOffManualFloorUIGo()
    {
        automaticFloorUIGo.SetActive(false);
        manualFloorUIGo.SetActive(true);
    }

    public void ExitPanelFloor3(int idx)
    {
        if (idx == 3)
            loadScenePanel3.SetActive(false);
        else if (idx == 4)
            loadScenePanel4.SetActive(false);
    }
}
