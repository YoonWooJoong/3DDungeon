using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Equipment : MonoBehaviour
{
    public EquipTool curEquip;
    private float curSpeedValue;

    private PlayerController controller;
    private PlayerCondition condition;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
    }

    public void EquipNew(ItemData data)
    {
        //UnEquip(data);
        for (int i = 0; i < data.equipableStats.Length; i++)
        {
            switch (data.equipableStats[i].type)
            {
                case EquipableType.Speed:
                    CharactorManager.Instance.Player._playerData.moveSpeed += data.equipableStats[i].value;
                    break;
                case EquipableType.Jump:
                    CharactorManager.Instance.Player._playerData.jumpPower += data.equipableStats[i].value;
                    break;
            }
        }
        //curEquip = Instantiate(data.equipPrefab, equipParent).GetComponent<EquipTool>();
    }

    public void UnEquip(ItemData data)
    {
        //if (curEquip != null)
        {
            for (int i = 0; i < data.equipableStats.Length; i++)
            {
                switch (data.equipableStats[i].type)
                {
                    case EquipableType.Speed:
                        CharactorManager.Instance.Player._playerData.moveSpeed -= data.equipableStats[i].value;
                        break;
                    case EquipableType.Jump:
                        CharactorManager.Instance.Player._playerData.jumpPower -= data.equipableStats[i].value;
                        break;
                }
                //Destroy(curEquip.gameObject);
                //curEquip = null;
            }
        }
    }
}
