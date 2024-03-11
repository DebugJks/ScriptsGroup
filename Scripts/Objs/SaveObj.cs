using System.Collections;
using UnityEngine;
using StringSpace;

        // 게임 저장할 수 있는 npc
public class SaveObj : AObjs
{
    [SerializeField] GameObject saveCam;
    WaitForSeconds seconds = new WaitForSeconds(3.5f);

    public override void Action()
    {
        StartCoroutine(SaveAction());
        GameManagers.instance.Alert_Show("게임 저장 성공!");
        GameManagers.instance.saveDataManager.Inventory_Save();
        GameManagers.instance.saveDataManager.Player_Save();
    }

    IEnumerator SaveAction()
    {
        GameManagers.instance.player.isTalk = true;
        saveCam.gameObject.SetActive(true);
        yield return seconds;
        
        saveCam.gameObject.SetActive(false);
        GameManagers.instance.player.isTalk = true;
    }

    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag(StringSpaces.Player) && Input.GetKeyDown(KeyCode.F))
        {
            Action();
        }
    }
}