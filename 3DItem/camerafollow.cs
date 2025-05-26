using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;     // ���� ĳ����
    public float distance = 5f;  // ī�޶� ĳ���ͷκ��� �󸶳� �ڿ� ������
    public float height = 2f;    // ī�޶� ����
    public float smoothSpeed = 10f;

    void LateUpdate()
    {
        if (target == null) return;

        // ĳ���Ͱ� �ٶ󺸴� �������� ������ ���
        Vector3 offset = -target.forward * distance + Vector3.up * height;
        Vector3 desiredPosition = target.position + offset;

        // �ε巴�� �̵�
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);

        // �׻� ĳ���͸� �ٶ󺸰�
        transform.LookAt(target.position + Vector3.up * 1.5f); // ��¦ ���� �ٶ󺸰�
    }
}