using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomTextRotation : MonoBehaviour
{
    public string floorNum;

    public Text R1;
    public Text R2;
    public Text R3;
    public Text R4;
    public Text R5;
    public Text R6;
    public Text R7;
    public Text R8;
    public Text R9;
    public Text R10;
    public Text R11;
    public Text R12;

    // Update is called once per frame
    void Update()
    {
        R1.transform.parent.transform.rotation =
            R2.transform.parent.transform.rotation =
            R3.transform.parent.transform.rotation =
            R4.transform.parent.transform.rotation =
            R5.transform.parent.transform.rotation =
            R6.transform.parent.transform.rotation =
            R7.transform.parent.transform.rotation =
            R8.transform.parent.transform.rotation =
            R9.transform.parent.transform.rotation =
            R10.transform.parent.transform.rotation =
            R11.transform.parent.transform.rotation =
            R12.transform.parent.transform.rotation
            = Camera.main.transform.rotation;

        R1.text = $"{floorNum}01 : {Data_Human.R1Num.ToString()}";
        R2.text = $"{floorNum}02 : {Data_Human.R2Num.ToString()}";
        R3.text = $"{floorNum}03 : {Data_Human.R3Num.ToString()}";
        R4.text = $"{floorNum}04 : {Data_Human.R4Num.ToString()}";
        R5.text = $"{floorNum}05 : {Data_Human.R5Num.ToString()}";
        R6.text = $"{floorNum}06 : {Data_Human.R6Num.ToString()}";
        R7.text = $"{floorNum}07 : {Data_Human.R7Num.ToString()}";
        R8.text = $"{floorNum}08 : {Data_Human.R8Num.ToString()}";
        R9.text = $"{floorNum}09 : {Data_Human.R9Num.ToString()}";
        R10.text = $"{floorNum}10 : {Data_Human.R10Num.ToString()}";
        R11.text = $"{floorNum}11 : {Data_Human.R11Num.ToString()}";
        R12.text = $"{floorNum}12 : {Data_Human.R12Num.ToString()}";
    }
}
