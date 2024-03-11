using UnityEngine;
using StringSpace;

public class PlayerMover : MonoBehaviour
{

    public PlayerActions inputActions;
    public PlayerActions.PActionActions playerActions;

    
    public Transform mainCamTransform;

            // 즉각적으로 회전이 아닌 자연스럽게 회전하기 위함
    Vector3 curTargetRot;
    Vector3 timeTargetRot;
    Vector3 curDampTargetRotVel;
    Vector3 dampTargetRotTime;


    [SerializeField] float speed;
    [SerializeField] float speedDamp;

    // 점프관련
    public LayerMask groundLayer;

    bool isKeepRot;

    [HideInInspector] public Vector2 moveInput;

    [HideInInspector] public Rigidbody rigid;


    Players player;

    void Awake()
    {
        player = GetComponent<Players>();
        rigid = GetComponent<Rigidbody>();

        inputActions = new PlayerActions();
        playerActions = inputActions.PAction;

        timeTargetRot.y = 0.15f;   
    }
    
    void OnEnable() => inputActions.Enable();

    void OnDisable() => inputActions.Disable();

    void Update()
    {
        moveInput = playerActions.Move.ReadValue<Vector2>();
        isKeepRot = moveInput != Vector2.zero;
    }

    void FixedUpdate()
    {
        Move();

        if(isKeepRot)
        {
            RotateTargetRotation();
        }
    }

    void Move()
    {
        if(moveInput == Vector2.zero || player.isTalk)
        {
                    // 살짝 미끄러지면서 정지할 수 있도록
            rigid.velocity = Vector3.Lerp(rigid.velocity,Vector3.zero, speedDamp * Time.deltaTime);
            player.changeAnimation(StringSpaces.Idle);
                        // IDLE 애니메이션 재생
            player.idleTime += Time.deltaTime;
            if(player.idleTime >= 5.5f)
            {
                player.changeAnimation(StringSpaces.IdleB);
                player.idleTime = 0;
            }
            return;
        }

        player.changeAnimation(StringSpaces.Run);
           // 캐릭터 추락하는 동안 카메라 회전시 해당 방향으로 플레이어 경로 다시 설정하기 위함
        float targetRotate_YAngle = Rotate(moveInput); 

        Vector3 targetRotDir = GetTargetRotDirection(targetRotate_YAngle);
        Vector3 curHorizonVelocity = GetHorizonVelocity();
        rigid.AddForce(speed * targetRotDir - curHorizonVelocity, ForceMode.VelocityChange);
    }

            // 현재 수평방향 속도 추출 >> 계속 앞으로 나아가는 거 방지
    Vector3 GetHorizonVelocity()
    {
        Vector3 HorizonVelocity = rigid.velocity;
        HorizonVelocity.y = 0f;
        return HorizonVelocity;
    }


    public float Rotate(Vector3 direction)
    {
        float directionAngle = UpdateTargetRotation(direction);
        RotateTargetRotation();

        return directionAngle;
    }

    float GetDirectionAngle(Vector3 direction)
    {
        float dirAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

            // Atan은 -180 ~ 180을 반환하는데 음수일 경우 회전이 이상해지는 경우가 있어
                    //이를 보정하기 위함
        if(dirAngle < 0f)
        {
            dirAngle += 360f;
        }
        return dirAngle;
    }

    float GetCamRotationAngle(float direction)
    {
        direction += mainCamTransform.eulerAngles.y;
                // eulerAngles 값 360도 이상 가능해서 보정하기 위함
        if(direction > 360f)
        {
            direction -= 360f;
        }
        return direction;
    }


    void RotateTargetRotation()
    {
        float curY_Angle = rigid.rotation.eulerAngles.y;

        if(curY_Angle == curTargetRot.y)        // 이미 회전 끝난 경우
            return;
        
        float smoothY_Angle = Mathf.SmoothDampAngle(curY_Angle, curTargetRot.y
            ,ref curDampTargetRotVel.y,timeTargetRot.y - dampTargetRotTime.y);

                            // FixedUpdate에서 호출시 자동으로 fixedDeltatime 반환
        dampTargetRotTime.y += Time.deltaTime;
        Quaternion targetRot = Quaternion.Euler(0f, smoothY_Angle, 0f);
        rigid.MoveRotation(targetRot);
    }

    float UpdateTargetRotation(Vector3 direction, bool isCamRot = true)
    {
        float directionAngle = GetDirectionAngle(direction);
        if(isCamRot)
        {
            directionAngle = GetCamRotationAngle(directionAngle);
        }
                // 회전 후 값 새로 설정
        if(directionAngle != curTargetRot.y)
        {
            curTargetRot.y = directionAngle;
            dampTargetRotTime.y = 0f;
        }

        return directionAngle;
    }


    Vector3 GetTargetRotDirection(float targetAngle)
    {
        return Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
    }
}