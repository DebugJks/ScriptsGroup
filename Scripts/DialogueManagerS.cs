using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class Dialog
{
    public List<string> lines;  // 대사
}



public class DialogueManagerS : MonoBehaviour
{
    public GameObject dialogBox;

    [SerializeField] Text dialogText;
    [SerializeField] Text nameText;
       
    WaitForSeconds seconds = new WaitForSeconds(0.01f);

    public Objs npcLogic;
    public QuestGiver questGiverLogic;

            // 글씨 출력
    IEnumerator TypeDialog(string dialog)
    {        
        dialogText.text = "";
        foreach(var character in dialog.ToCharArray())
        {
            dialogText.text += character;       
                    // 여기에 사운드를 넣는다면 글씨 나올때 효과음 나오게 할 수 있음
            yield return seconds;
        }
    }

    public IEnumerator ShowDialog(Dialog dialog, int type)
    {
        dialogBox.SetActive(true);
        GameManagers.instance.player.isTalk = true;
        if(type == 0 && npcLogic.npcCam != null)
        {
            npcLogic.npcCam.SetActive(true);
        }
        else if (type == 1 && questGiverLogic != null)
        {
            questGiverLogic.npcCam.SetActive(true);
        }

        foreach(var line in dialog.lines)
        {
            yield return TypeDialog(line);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Space));            
        }

        switch(type)
        {
            case 0 :
                npcLogic.isTalk = false;
                npcLogic.npcCam.SetActive(false);
                break;
            case 1:
                questGiverLogic.isTalk = false;
                questGiverLogic.npcCam.SetActive(false);
                break;
        }
        GameManagers.instance.player.isTalk = false;
        dialogBox.SetActive(false);
    }
}
