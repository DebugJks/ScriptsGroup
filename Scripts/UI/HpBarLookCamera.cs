using UnityEngine;

public class HpBarLookCamera : MonoBehaviour
{

    Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;    
    }

    void Update()
    {
        transform.LookAt(transform.position + mainCam.transform.rotation * Vector3.forward
                    , mainCam.transform.rotation * Vector3.up);
    }
}
