using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AnimEvent : MonoBehaviour
{
    [SerializeField]
    GameObject dataImage;
    [SerializeField]
    GameObject mapImage;
    [SerializeField]
    GameObject graphImage;

    void DataOn()
    {
        dataImage.SetActive(true);
        mapImage.SetActive(false);
    }

    void DataOff()
    {
        if (graphImage != null)
            graphImage.transform.GetComponent<Image>().sprite = null;
        
        dataImage.SetActive(false);
        mapImage.SetActive(true);
    }

    void Restart3Floor()
    {
        SceneManager.LoadScene("Automatic Scene 3");
    }

    void Restart4Floor()
    {
        SceneManager.LoadScene("Automatic Scene 4");
    }
}