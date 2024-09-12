using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;


public class Game1Bag : MonoBehaviour
{
    [SerializeField] private TMP_Text bagItemCount;
    [SerializeField] private TMP_Text item1;
    [SerializeField] private TMP_Text item2;
    [SerializeField] private TMP_Text item3;
    [SerializeField] private TMP_Text item4;
    [SerializeField] private TMP_Text item5;
    [SerializeField] private TMP_Text item6;
    [SerializeField] private TMP_Text errorText;
    [SerializeField] private AudioClip dropInBagClip;
    [SerializeField] private AudioClip errorClip;
    private List<Game1Item> itemsInBag;

    void Awake()
    {
        itemsInBag = new List<Game1Item>();
        UpdateBagCanvas();
    }

    private void UpdateBagCanvas()
    {
        bagItemCount.text = itemsInBag.Count + "/6 Gegenstände";
        errorText.text = "";

        if (itemsInBag.Count > 0) { item1.text = itemsInBag[0].game1ItemData.itemName; }
        else { item1.text = ""; }
        if (itemsInBag.Count > 1) { item2.text = itemsInBag[1].game1ItemData.itemName; }
        else { item2.text = ""; }
        if (itemsInBag.Count > 2) { item3.text = itemsInBag[2].game1ItemData.itemName; }
        else { item3.text = ""; }
        if (itemsInBag.Count > 3) { item4.text = itemsInBag[3].game1ItemData.itemName; }
        else { item4.text = ""; }
        if (itemsInBag.Count > 4) { item5.text = itemsInBag[4].game1ItemData.itemName; }
        else { item5.text = ""; }
        if (itemsInBag.Count > 5) { item6.text = itemsInBag[5].game1ItemData.itemName; }
        else { item6.text = ""; }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Game1Item" && itemsInBag.Count < 5)
        {
            Game1Item item = other.GetComponent<Game1Item>();
            RemoveDuplicatesInBag(item);
            AudioManager.Instance.PlayClipOnce(dropInBagClip);
            item.game1ItemData.itemInBag = true;
            itemsInBag.Add(item);
            UpdateBagCanvas();
        }
        else if (other.tag == "Game1Item" && itemsInBag.Count == 5)
        {
            Game1Item item = other.GetComponent<Game1Item>();
            RemoveDuplicatesInBag(item);
            AudioManager.Instance.PlayClipOnce(dropInBagClip);
            item.game1ItemData.itemInBag = true;
            itemsInBag.Add(item);
            UpdateBagCanvas();
        }
        else if (other.tag == "Game1Item" && itemsInBag.Count > 5)
        {
            Game1Item item = other.GetComponent<Game1Item>();
            AudioManager.Instance.PlayClipOnce(errorClip);
            ThrowException("FULL");
            item.ResetItem();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Game1Item")
        {
            Game1Item item = other.GetComponent<Game1Item>();
            item.game1ItemData.itemInBag = false;
            itemsInBag.Remove(item);
            UpdateBagCanvas();
        }
    }

    public float CalculateItemScore()
    {
        float itemScore = itemsInBag[0].game1ItemData.itemScore + itemsInBag[1].game1ItemData.itemScore + itemsInBag[2].game1ItemData.itemScore + itemsInBag[3].game1ItemData.itemScore + itemsInBag[4].game1ItemData.itemScore + itemsInBag[5].game1ItemData.itemScore;
        return itemScore;
    }

    public bool CheckBagStatus()
    {
        if(itemsInBag.Count == 6)
        {
            return true;
        }
        else
        {
            ThrowException("NOTFULL");
            return false;
        }
    }

    private void ThrowException(string bagStatus)
    {
        switch(bagStatus)
        {
            case "NOTFULL":
                errorText.text = "Tasche noch nicht voll!";
                break;
            case "FULL":
                errorText.text = "Tasche voll!";
                break;
            default:
                Debug.Log("Game1Bag Error: unknown error string");
                break;
        }
    }

    private void RemoveDuplicatesInBag(Game1Item itemToCheck)
    {
        foreach(Game1Item i in itemsInBag)
        {
            if(i == itemToCheck)
            {
                itemsInBag.Remove(itemToCheck);
            }
        }
    }
}
