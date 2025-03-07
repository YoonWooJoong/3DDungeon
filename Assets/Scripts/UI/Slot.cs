using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class Slot : MonoBehaviour
{
    public ItemData item;

    public Button button;
    public Image icon;
    public TextMeshProUGUI ItemCountText;
    private Outline outline;

    public UIInventory inventory;

    public int itemCount;
    public int index;
    public bool equipped;

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }
    private void OnEnable()
    {
        outline.enabled = equipped;
    }

    public void Set()
    {
        icon.gameObject.SetActive(true);
        icon.sprite = item.icon;
        ItemCountText.text = itemCount > 1 ? itemCount.ToString() : string.Empty;

        if (outline != null)
        {
            outline.enabled = enabled;
        }

    }

    public void Clear()
    {
        item = null;
        icon.gameObject.SetActive(false);
        ItemCountText.text = string.Empty;
    }

    public void OnClickButton()
    {
        inventory.SelectItem(index);
    }
}
