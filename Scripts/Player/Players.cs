using Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using StringSpace;
using System.IO;

[System.Serializable]
public class PlayerData
{
    public bool isOnQuest;  // 퀘스트 수락한 상태인지 검사
    public int curHp, maxHp;
    public Vector3 lastPos; // 저장시 위치
}


public class Players : MonoBehaviour
{
    public PlayerData playerData = new PlayerData();

    public CinemachineVirtualCamera vCam;

    public bool isTalk; // 대화중인지

    public Transform bulletPos;
    [SerializeField] bool isOnQuest;
    [SerializeField] Image hpBar;
    
    
    [HideInInspector] public float idleTime = 0; // 애니메이션 관련
    [HideInInspector] public bool isShield; // 힐 스킬 사용시 On
    [HideInInspector] public Animator anim;
    [HideInInspector] public PlayerMover playerMover;
    [HideInInspector] public SecondWeapon secondWeapon;

    WaitForSeconds seconds = new WaitForSeconds(3f);

    void Awake()
    {
        playerMover = GetComponent<PlayerMover>();
        secondWeapon = GetComponentInChildren<SecondWeapon>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        try
        {
                    // 저장된 데이터 불러오기
            GameManagers.instance.saveDataManager.Player_Load("PlayerData.json");
            transform.position = playerData.lastPos;
        }
        catch(FileNotFoundException)
        {
            Debug.Log("저장된 데이터가 없음");
        }
    }

    void LateUpdate()
    {
        ControlHpBar();
    }

    public void OnHit(int dmg)
    {
        if(isShield)
            playerData.curHp -= (int) (dmg * 0.8);
        else
            playerData.curHp -= dmg;
    }


    void ControlHpBar()
            => hpBar.fillAmount = (float) playerData.curHp / (float) playerData.maxHp;

    

                // 플레이어 애니메이션용
    public void changeAnimation(string currentAnimation)
    {
        if (currentAnimation == StringSpaces.Idle)
        {
            anim.SetBool("runFlag", false);
            anim.SetBool("idleFlag", true);
            anim.SetBool("jumpLandFlag", false);
        }

        if (currentAnimation == StringSpaces.IdleB)
        {
            anim.SetBool("idleBFlag", true);
            anim.SetBool("idleFlag", false);
        }       

        if (currentAnimation == StringSpaces.Run)
        {
            anim.SetBool("runFlag", true);
            anim.SetBool("idleFlag", false);
            anim.SetBool("jumpLandFlag", false);
            anim.SetBool("jumpAirFlag", false);
            anim.SetBool("attackFlag", false);
            anim.SetBool("idleBFlag", false);
        }

        if(currentAnimation == "attack")
        {
            anim.SetBool("attackFlag", true);
            anim.SetBool("idleFlag", false);
            anim.SetBool("idleBFlag", false);
        }
    }
}