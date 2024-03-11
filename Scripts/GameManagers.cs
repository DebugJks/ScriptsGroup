using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public enum PoolState
{
    PlayerBulletA, PlayerBulletB, WeaponBullet, Boomb
}       

public class GameManagers : MonoBehaviour
{
    public static GameManagers instance;
    [HideInInspector] public ObjectManagers objectManager;
    [HideInInspector] public DialogueManagerS dialogueManager;
    [HideInInspector] public SaveDataManager saveDataManager;
    [HideInInspector] public ItemMover itemMover;
    [HideInInspector] public Inventory inventoryLogic;

    public QuestGroupSO questLists;
    public ItemGroupSO inventory;
    public ItemList itemList;

    public Players player;

    [SerializeField] GameObject questPanel;
    [SerializeField] GameObject fadeImg;
    [SerializeField] GameObject escPanel;
    [SerializeField] GameObject inventoryPanel;
    [SerializeField] GameObject alertPanel;     // 안내 패널
    [SerializeField] Text alertText;


    bool isPanel;   // panel 제어
    WaitForSeconds delay3 = new WaitForSeconds(3f);

    void Awake() 
    {
        instance = this;   

        objectManager = GetComponent<ObjectManagers>();  
        dialogueManager = GetComponent<DialogueManagerS>();
        itemMover = GetComponent<ItemMover>();
        inventoryLogic = inventoryPanel.GetComponent<Inventory>();
        saveDataManager = GetComponent<SaveDataManager>();
    }

    void Update()
    {
        KeyHandle();
    }

            // 키 입력
    void KeyHandle()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {      
            isPanel = !isPanel;
            if(!isPanel)
            {
                escPanel.GetComponent<EscPanel>().Panel_Close();
                return;
            }
            escPanel.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.I))
            inventoryPanel.SetActive(!inventoryPanel.activeInHierarchy);
        else if (Input.GetKeyDown(KeyCode.K))
            questPanel.SetActive(!questPanel.activeInHierarchy);
    }

            //DoTween 사용
        // 알람 패널로 사용
    public void Alert_Show(string alert_Text)
    {
        AudioManagerS.intance.PlaySoundEffect(AudioManagerS.Sfx.Alert);

        Vector3 orginPos = alertPanel.transform.localPosition;
        Image panelImg = alertPanel.GetComponent<Image>();
        alertText.text = alert_Text;

        panelImg.DOFade(0.75f,3f);
        alertText.DOFade(1f,3f);
        alertPanel.transform.DOLocalMove(Vector3.zero,3);
        DOVirtual.DelayedCall(3f, () =>
        {
            panelImg.DOFade(0f,3f);
            alertText.DOFade(0f,3f);
            alertPanel.transform.DOLocalMove(orginPos,3);
        });
    }
}