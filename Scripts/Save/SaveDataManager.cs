using System.Collections.Generic;
using System.IO;
using UnityEngine;



[System.Serializable]
public class InventorySave
{   
    public int id;
    public int count;

        // 생성자
    public InventorySave(int Id, int Count)
    {
        id = Id;
        count = Count;
    }
}

        // 인벤토리에서 저장 시도할 것이기 때문에 실제 아이템이 표시될 슬롯에 적용하기 위함
[System.Serializable]
public class SlotSave
{
    public List<InventorySave> slotData;

    public SlotSave()
    {
        slotData = new List<InventorySave>();
    }
}



public class SaveDataManager : MonoBehaviour
{
    SlotSave inventory_Save = new SlotSave();
    ItemGroupSO inventory;

    void Start()
    {
        inventory = GameManagers.instance.inventory;

        if(inventory_Save == null)
            return;
        
        Inventory_Load("inventoryData.json");       // 시작시 인벤토리 데이터 로드
    }

    public void Inventory_Save()
    {

        float slotNum = inventory.slots.Count;
        for(int i = 0; i < slotNum; i++)
        {
            if(inventory.slots[i].item == null) // 아이템이 비어있는 경우
            {
                            // id 가 -1인 경우 빈 슬롯이란 것을 알려주기 위함
                inventory_Save.slotData.Add(new InventorySave(-1,0));
            }
            else
            {
                inventory_Save.slotData.Add(new InventorySave(inventory.slots[i].item.itemId
                                    , inventory.slots[i].count));
            }
        }
                // Json파일 생성
        string inventoryJson = JsonUtility.ToJson(inventory_Save);
        string invenotryDataPath = Path.Combine(Application.dataPath, "inventoryData.json");
        File.WriteAllText(invenotryDataPath,inventoryJson);
    }

    public void Inventory_Load(string jsonName)
    {
                // json 데이터가 빈 경우
        if(jsonName == "" || jsonName == "{}")
            return;

        string invenotryDataPath = Path.Combine(Application.dataPath, "inventoryData.json");
        string inventoryJson = File.ReadAllText(invenotryDataPath);
        inventory_Save = JsonUtility.FromJson<SlotSave>(inventoryJson);

        for (int i = 0 ; i < inventory_Save.slotData.Count; i++)
        {
            if(inventory_Save.slotData[i].id == -1) //슬롯 비우기
            {
                inventory.slots[i].Clear(); 
            }
            else
            {               // id에 맞게 아이템 채우기
                inventory.slots[i].item =       
                        GameManagers.instance.itemList.items[inventory_Save.slotData[i].id];
                inventory.slots[i].count = inventory_Save.slotData[i].count;
            }
        }
    }

    // 플레이어 + 퀘스트 저장
#region  
    public void Player_Save()
    {

        PlayerData playerData = new PlayerData
        {
            curHp = GameManagers.instance.player.playerData.curHp,
            lastPos = GameManagers.instance.player.transform.position   // 플레이어 마지막 위치
        };
        string playerJson = JsonUtility.ToJson(playerData);
        string playerDataPath = Path.Combine(Application.dataPath, "PlayerData.json");
        File.WriteAllText(playerDataPath,playerJson);
    }

    public void Player_Load(string jsonName)
    {
        if(jsonName == "" || jsonName == "{}")
            return;

        string playerDataPath = Path.Combine(Application.dataPath, "PlayerData.json");
        string playerJson = File.ReadAllText(playerDataPath);
        GameManagers.instance.player.playerData = JsonUtility.FromJson<PlayerData>(playerJson);
    }

#endregion
}