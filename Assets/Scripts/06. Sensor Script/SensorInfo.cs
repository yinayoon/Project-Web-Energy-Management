using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorInfo : MonoBehaviour
{
    public string Name;

    public void Awake()
    {
        Name = transform.name.Replace(":", "");
    }
}
