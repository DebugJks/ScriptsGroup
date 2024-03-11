using UnityEngine;
using StringSpace;

public class QuestObjs : AObjs
{
    [SerializeField] QuestSO curQuest;

    public override void Action()
    {
        curQuest.curNum++;

                        // 퀘스트 조건 달성한지 검사
        if(curQuest.curNum >= curQuest.targetNum)
        {
            curQuest.isComplete = true;
                    // 비활성화돼서 문제 생김
            GameManagers.instance.Alert_Show("퀘스트 완료!");
        }

        this.gameObject.SetActive(false);
    }


    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag(StringSpaces.Player))
        {
            Action();
        }
    }
}