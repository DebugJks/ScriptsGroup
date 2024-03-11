using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] ItemGroupSO inventory;
    [SerializeField] List<InventorySlotBtn> slotsBtn;

    void Start()
    {
        SetIndex();
    }

    void OnEnable()
    {
        Show();
    }

        // 인벤토리 슬롯에 인덱스 부여
    void SetIndex() 
    {
        for(int i = 0; i < inventory.slots.Count; i++)
        {
            slotsBtn[i].SetIndex(i); 
        }
    }

        // 인벤토리 아이템 보여주기
    public void Show()
    {
        for (int i  = 0; i < inventory.slots.Count; i++)
        {
            if(inventory.slots[i].item == null)
            {
                slotsBtn[i].Clean();      // 슬롯에 아이템이 없다면 clean
            }
            else
            {
                slotsBtn[i].Set(inventory.slots[i]);
            }
        }
    }
}
