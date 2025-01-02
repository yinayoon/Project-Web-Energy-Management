using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveScopeRay : MonoBehaviour
{
    RaycastHit hit;
    public static bool MoveSign;

    // Start is called before the first frame update
    void Start()
    {
        MoveSign = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 right = transform.TransformDirection(Vector3.right);
        Vector3 left = transform.TransformDirection(Vector3.right);
        Vector3 front = transform.TransformDirection(Vector3.right);
        Vector3 back = transform.TransformDirection(Vector3.right);

        Debug.DrawRay(transform.position, right * 0.5f, Color.red);

        if (Physics.Raycast(transform.position, right, out hit, 0.08f))
        {
            MoveSign = false;
            //Debug.Log(hit.transform.name);
        }
        else if (Physics.Raycast(transform.position, left, out hit, 0.08f))
        {
            MoveSign = false;
            //Debug.Log(hit.transform.name);
        }
        else if (Physics.Raycast(transform.position, front, out hit, 0.08f))
        {
            MoveSign = false;
            //Debug.Log(hit.transform.name);
        }
        else if (Physics.Raycast(transform.position, back, out hit, 0.08f))
        {
            MoveSign = false;
            //Debug.Log(hit.transform.name);
        }
        else
        {
            MoveSign = true;
        }
    }
}
