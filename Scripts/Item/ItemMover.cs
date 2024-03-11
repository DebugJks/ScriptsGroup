using UnityEngine;
using UnityEngine.UI;


        // 아이템 드래그 앤 드롭 관리
public class ItemMover : MonoBehaviour
{
    [SerializeField] ItemSlot itemSlot;
    [SerializeField] GameObject itemDragImg;    // 아이템 슬롯 이동시 마우스 따라다닐 이미지 
    RectTransform dragImgTrans;
    Image itemImg;

    void Start()
    {
        itemSlot = new ItemSlot();
        dragImgTrans = itemDragImg.GetComponent<RectTransform>();
        itemImg = itemDragImg.GetComponent<Image>();
    }

    void Update()
    {
        if(itemDragImg.activeInHierarchy)
        {
            dragImgTrans.position = Input.mousePosition;    // 아이콘 이미지 마우스 위치로
        }
    }


            // 아이템 슬롯 이동 후 비우기
    public void OnClick(ItemSlot itemSlot)
    {
        if(this.itemSlot.item == null)
        {
                    // 클릭한 슬롯의 아이템이 없는 경우
            this.itemSlot.CopyItem(itemSlot);
            itemSlot.Clear();
        }
        else
        {
            ItemSO item = itemSlot.item;
            int count = itemSlot.count;
            itemSlot.CopyItem(this.itemSlot);
            this.itemSlot.Set(item, count);
        }
        GetDragImg();
    }

    void GetDragImg()
    {
        if(itemSlot.item == null)
        {
            itemDragImg.SetActive(false);
        }
        else
        {
            itemDragImg.SetActive(true);
            itemImg.sprite = itemSlot.item.icon;
        }
    }
}
