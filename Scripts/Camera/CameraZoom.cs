using UnityEngine;
using Cinemachine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] float defaultDis;
    [SerializeField] float minDIs;
    [SerializeField] float maxDis;

    [SerializeField] float smoothing = 4f;
    [SerializeField] float zoomSensivity = 1f;

    CinemachineFramingTransposer framingTransposer;
    CinemachineInputProvider inputProvider;

    float curTargetDis;

    void Awake()
    {
        framingTransposer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
        inputProvider = GetComponent<CinemachineInputProvider>();
        curTargetDis = defaultDis;
    }

    void Update()
    {
        Zoom();
    }

    void Zoom()
    {
        float zoomValue = inputProvider.GetAxisValue(2) * zoomSensivity;

        curTargetDis = Mathf.Clamp(curTargetDis + zoomValue, minDIs, maxDis);
        float curDistance = framingTransposer.m_CameraDistance;

        if (curDistance == curTargetDis)
        {
            return;
        }

        float lerpZoomValue = Mathf.Lerp(curDistance, curTargetDis, smoothing * Time.deltaTime);
        framingTransposer.m_CameraDistance = lerpZoomValue;
    }
}
