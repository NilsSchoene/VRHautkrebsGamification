using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Game1Item : MonoBehaviour
{
    public Game1ItemData game1ItemData;
    private XRGrabInteractable itemGrabInteractable;
    private Vector3 startPos;

    void Awake()
    {
        startPos = gameObject.transform.position;
        itemGrabInteractable = GetComponent<XRGrabInteractable>();
    }

    public void ResetItem()
    {
        itemGrabInteractable.enabled = false;
        gameObject.transform.position = startPos;
        itemGrabInteractable.enabled = true;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            ResetItem();
        }
    }
}
