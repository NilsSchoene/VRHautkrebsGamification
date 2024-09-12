using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetVRButton : MonoBehaviour
{
    private Vector3 initialPos;
    private void Awake()
    {
        initialPos = transform.position;
    }

    public void ResetPos()
    {
        transform.position = initialPos;
    }
}
