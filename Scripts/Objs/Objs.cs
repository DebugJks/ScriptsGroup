using UnityEngine;
using StringSpace;

public class Objs : AObjs
{
    public GameObject npcCam;
    public string npcName;

    public Dialog dialog;   // 대사

        // 대사 첫마다 무한반복 방지
    public bool isTalk;

    public override void Action()
    {
        isTalk = true;
        GameManagers.instance.dialogueManager.npcLogic = this;
        StartCoroutine(GameManagers.instance.dialogueManager.ShowDialog(dialog, 0));
    }


    void OnTriggerStay(Collider other) 
    {
        if(other.CompareTag(StringSpaces.Player) && Input.GetKeyDown(KeyCode.F) && !isTalk)
        {
            Action();
        }
    }

}