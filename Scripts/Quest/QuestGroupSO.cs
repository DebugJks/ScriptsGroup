using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class QuestSlot
{
    public QuestSO quest;
    public string questTitle;
    public string questInfo;

    public void Set(QuestSO quest, string title, string info)
    {
        this.quest = quest;
        this.questTitle = title;
        this.questInfo = info;
    }

    public void Clear(QuestSO quest, string title)
    {
        quest.quest_Title = " ";
        quest.quest_Info = " ";
    }
}

[CreateAssetMenu(menuName = "SO/QuestGroupSO")]
public class QuestGroupSO : ScriptableObject
{
    public List<QuestSlot> questList;

    public void Set(QuestSO quest, string title, string info)
    {
                    // 퀘스트가 없는 경우
                            // 여기가 문제 없는 곳을 못찾고 있음
        QuestSlot quests = questList.Find(x => x.quest == null);
        if(quests != null)  // 빈 곳이 있을 때
        {
            quests.quest = quest;
            quests.questTitle = title;
            quests.questInfo = info;
        }
    }
}