using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ItemSlot
{
    public ItemSO item;
    public int count;

        //  드래그 드롭할 곳으로 아이템 복사
    public void CopyItem(ItemSlot slot)
    {
        item = slot.item;
        count = slot.count;
    }

        // 슬롯 정보갱신
    public void Set(ItemSO item, int count)
    {
        this.item = item;
        this.count = count;
    }
        
        // 아이템 이동 후 원래 슬롯 비우기
    public void Clear()
    {
        item = null;
        count = 0;
    }
}

        // 아이템을 관리할 그룹 인벤토리와 연동해서 사용
[CreateAssetMenu(menuName = "SO/ItemGroupSO")]
public class ItemGroupSO : ScriptableObject
{
    public List<ItemSlot> slots;    // 원하는 슬롯 수만큼 만들어두기


    public void Add(ItemSO item, int count = 1)
    {
        ItemSlot itemSlot = slots.Find(x => x.item == item);    // 동일한 아이템 슬롯 찾기
        if(itemSlot != null)        // 동일 아이템이 있는 경우
        {
            itemSlot.count += count;
        }
        else
        {
            itemSlot = slots.Find(x => x.item == null); // 빈 슬롯 찾기
            if(itemSlot != null)        // 빈 슬롯이 있을경우
            {
                itemSlot.item = item;
                itemSlot.count = count;
            }
            else
            {
                Debug.Log("인벤토리가 모두 가득 참");
            }
        }
    }
}