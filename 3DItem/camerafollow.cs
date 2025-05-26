using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;     // 따라갈 캐릭터
    public float distance = 5f;  // 카메라가 캐릭터로부터 얼마나 뒤에 있을지
    public float height = 2f;    // 카메라 높이
    public float smoothSpeed = 10f;

    void LateUpdate()
    {
        if (target == null) return;

        // 캐릭터가 바라보는 방향으로 오프셋 계산
        Vector3 offset = -target.forward * distance + Vector3.up * height;
        Vector3 desiredPosition = target.position + offset;

        // 부드럽게 이동
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);

        // 항상 캐릭터를 바라보게
        transform.LookAt(target.position + Vector3.up * 1.5f); // 살짝 위쪽 바라보게
    }
}