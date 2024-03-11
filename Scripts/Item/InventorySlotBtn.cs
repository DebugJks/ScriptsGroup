using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotBtn : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image icon;
    [SerializeField] Text countText;        // 아이템 수량 표시

    int slotIndex;  // 인벤토리 슬롯 인덱스  

            // 해당 슬롯에 인덱스 초기화
    public void SetIndex(int index)
    {
        slotIndex = index;
    }

        // 해당 슬롯 아이콘, 수량 표시
    public void Set(ItemSlot slot)
    {
        icon.sprite = slot.item.icon;
        icon.gameObject.SetActive(true);
        
        countText.text = slot.count.ToString();    // 아이템 수량 증가표시
        countText.gameObject.SetActive(true);
        
    }

        // 슬롯 비우기
    public void Clean()
    {
        icon.sprite = null;
        icon.gameObject.SetActive(false);

        countText.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ItemGroupSO inventory = GameManagers.instance.inventory;
        GameManagers.instance.itemMover.OnClick(inventory.slots[slotIndex]);
        GameManagers.instance.inventoryLogic.Show();    // 변화 이후 인벤토리 보여주기
    }
}
