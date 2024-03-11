using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManagers : MonoBehaviour
{
    [SerializeField] QuestGroupSO questGroup;
    [SerializeField] List<QuestSlotPanel> questSlots;

    [SerializeField] Text questTitle_Text;
    [SerializeField] Text questInfo_Text;

    void Start()
    {
        SetIndex();
    }

    void OnEnable() => Show_Quest();

    void SetIndex()
    {
        for(int i = 0; i < questGroup.questList.Count; i++)
        {
            questSlots[i].SetIndex(i); 
        }
    }

    public void Show_Quest()
    {
        for(int i = 0; i < questGroup.questList.Count; i++)
        {
                        // 퀘스트 미완료인 경우
            if(questGroup.questList[i].quest.isComplete == false)
            {
                questSlots[i].Set(questGroup.questList[i].quest);
            }
            else if (questGroup.questList[i].quest == null)
            {
                //퀘스트가 없는 경우
                Debug.Log("진행중 퀘스트 없는 칸");
            }
            else
            {
                        // 완료된 퀘스트 경우
                questSlots[i].Clean();
            }
        }
    }
}