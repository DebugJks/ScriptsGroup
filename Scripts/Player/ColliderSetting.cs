using UnityEngine;

public class ColliderSetting : MonoBehaviour
{
    CapsuleCollider colliders;
    Vector3 colliderCenterSpace;

    [Header("----Default_콜라이더-----")]
    [SerializeField] float height_Default;
    [SerializeField] float center_Y_Default;
    [SerializeField] float radius_Default;

    [SerializeField] float heightPer = 0.2f;
    [SerializeField] float rayDis = 2f;
    [SerializeField] float reachForce = 25f;

    Players player;
    
    void Awake()
    {
        player = GetComponent<Players>();
        InitCollider();
        CalCapsuleColliderDim();
    }

    void FixedUpdate()
    {
        Floating();
    }


    public void InitCollider()
    {
        colliders = GetComponent<CapsuleCollider>();
        colliderCenterSpace = colliders.center;
    }

    void CalCapsuleColliderDim()
    {
        colliders.radius = radius_Default;
        colliders.height = height_Default * (1f - heightPer);
        UpdateCapsulCollider_Center();

        float halfColHeight = colliders.height * 0.5f;
        if(halfColHeight < colliders.radius) 
        {
            colliders.radius = halfColHeight;
        }
        colliderCenterSpace = colliders.center;
    }

    void UpdateCapsulCollider_Center()
    {
        float heightDiff = height_Default - colliders.height;
        Vector3 newCenter = new Vector3(0f, center_Y_Default + (heightDiff * 0.5f) ,0f);
        colliders.center = newCenter;
    }


    void Floating()
    {
        Vector3 capsulColliderCenter = colliders.bounds.center;
        Ray centerRay = new Ray(capsulColliderCenter, Vector3.down);
        if(Physics.Raycast(centerRay, out RaycastHit hit, rayDis, player.playerMover.groundLayer
                    ,QueryTriggerInteraction.Ignore))
        {
            float floatPoint = colliderCenterSpace.y 
                    * player.gameObject.transform.localScale.y - hit.distance;
            if(floatPoint == 0)
                return;
            float liftAmount = floatPoint * reachForce - player.playerMover.rigid.velocity.y;
            Vector3 liftForce = new Vector3(0f, liftAmount, 0f);
            player.playerMover.rigid.AddForce(liftForce, ForceMode.VelocityChange);
        }
    }
}