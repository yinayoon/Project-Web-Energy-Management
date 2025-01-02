using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_EventHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Color _originalColor = new Color(1.0f, 0.0f, 0.0f, 0.6f);
    Color _changeColor = new Color(0.0f, 0.0f, 0.0f, 0.6f);

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Enter");
        this.transform.GetComponent<Image>().color = _changeColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Exit");
        this.transform.GetComponent<Image>().color = _originalColor;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
