using UnityEngine;
using System;

public class TitleImageMover : MonoBehaviour
{
    readonly string MouseX = "Mouse X";
    readonly string MouseY = "Mouse Y";


    [SerializeField] float moveSpeed;
    [SerializeField] float minX, minY, maxX, maxY;
    
    void Update()
    {
        MoveTitle_Icon();
    }

    void MoveTitle_Icon()
    {
        // 마우스의 움직임을 감지
        float horizontalInput = Input.GetAxis(MouseX);
        float verticalInput = Input.GetAxis(MouseY);

        // Debug.Log("ㅅㄷㄴㅅ");
        // 이미지의 현재 위치를 가져오기
        Vector3 currentPosition = transform.position;

        // 마우스의 움직임에 따라 새로운 위치 계산
        float newX = Mathf.Clamp(currentPosition.x + horizontalInput * moveSpeed * Time.deltaTime, minX, maxX);
        float newY = Mathf.Clamp(currentPosition.y + verticalInput * moveSpeed * Time.deltaTime, minY, maxY);
        
            //벽에서 마우스 계속 이동시 이미지 무한이동 버그 방지
        if(Math.Abs(currentPosition.x - newX) > maxX ||Math.Abs(currentPosition.y - newY) > maxY)
            return;

        // 새로운 위치로 이미지 이동
        transform.position = new Vector3(newX, newY, currentPosition.z);      
    }
}
