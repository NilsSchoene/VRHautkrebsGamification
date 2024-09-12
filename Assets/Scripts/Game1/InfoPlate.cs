using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoPlate : MonoBehaviour
{
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text itemInfo;
    private Game1Item itemOnPanel = null;

    void Awake()
    {
        InitializeInfoText();
    }

    private void InitializeInfoText()
    {
        if(itemOnPanel == null)
        {
            itemName.text = "Info Panel";
            itemInfo.text = "";
        }
        else
        {
            itemName.text = itemOnPanel.game1ItemData.itemName;
            itemInfo.text = itemOnPanel.game1ItemData.itemInfo;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Game1Item" && itemOnPanel == null)
        {
            itemOnPanel = other.GetComponent<Game1Item>();
            InitializeInfoText();
        }
        else if (other.tag == "Game1Item" && itemOnPanel != null)
        {
            Game1Item item = other.GetComponent<Game1Item>();
            item.ResetItem();
            InitializeInfoText();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Game1Item")
        {
            Game1Item item = other.GetComponent<Game1Item>();
            if (item == itemOnPanel)
            {
                itemOnPanel = null;
                InitializeInfoText();
            }
            else
            {
                InitializeInfoText();
            }
        }
    }
}
