using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimFloorModel : MonoBehaviour
{
    [SerializeField]
    GameObject floor3;
    [SerializeField]
    GameObject floor4;

    public void Fold()
    {
        StartCoroutine("FoldAnim");
    }

    public void Unfold()
    {
        StartCoroutine("UnfoldAnim");
    }

    IEnumerator FoldAnim()
    {
        for (int i = 0; i < 11; i++)
        {
            floor3.transform.Translate(Vector3.forward);
            floor4.transform.Translate(-Vector3.forward);

            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator UnfoldAnim()
    {
        for (int i = 0; i < 11; i++)
        {
            floor3.transform.Translate(-Vector3.forward);
            floor4.transform.Translate(Vector3.forward);

            yield return new WaitForSeconds(0.01f);
        }
    }
}
