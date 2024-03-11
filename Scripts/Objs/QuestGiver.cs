using UnityEngine;
using StringSpace;

public class QuestGiver : AObjs
{
    public QuestSO quest;

        // 대화관련
    public Dialog dialog;
    public GameObject npcCam;
    public string npcName;
    public bool isTalk;

    public override void Action()
    {
        GiveQuest();
        isTalk = true;
        GameManagers.instance.dialogueManager.questGiverLogic = this;
        StartCoroutine(GameManagers.instance.dialogueManager.ShowDialog(dialog, 1));
        
    }


    void OnTriggerStay(Collider other) 
    {
        if(other.CompareTag(StringSpaces.Player) && Input.GetKeyDown(KeyCode.F)
                     && !isTalk)
        {
            Action();
        }
    }

    void GiveQuest()
    {
            // 부여할 퀘스트 정보
        GameManagers.instance.questLists.Set(quest, quest.quest_Title, quest.quest_Info);
    }
}
