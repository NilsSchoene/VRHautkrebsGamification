using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HumanRotation : MonoBehaviour
{
    public InputActionReference turn;
    private float rotationSpeed;

    void Update()
    {
        rotationSpeed = turn.action.ReadValue<Vector2>().x;
    }

    void FixedUpdate()
    {
        transform.Rotate(Vector3.up * (100 * (-rotationSpeed) * Time.deltaTime));
    }
}