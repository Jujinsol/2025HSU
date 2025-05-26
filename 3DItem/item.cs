using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    public enum Type { Food, Water, Battery, Oxygen, Book, Bordgame, Medicine, Fixtool, Map, Research, yellow, blue, white };
    public Type type;
    public int value = 1;
    public bool collected = false;
    public Sprite itemSprite;

    private Renderer rend;
    private Outline outline;

    Rigidbody rigid;
    SphereCollider sphereCollider;

    // �������� �����ϱ� ���� ���� ����Ʈ
    public static List<item> allItems = new List<item>();

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
        outline = GetComponent<Outline>();

        // �������� allItems ����Ʈ�� �߰�
        if (!allItems.Contains(this))
        {
            allItems.Add(this);
        }
    }

    void Update()
    {
        transform.Rotate(Vector3.up * 20 * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            rigid.isKinematic = true;
            sphereCollider.enabled = false;
        }
    }

    public void SetColor(Color color)
    {
        if (outline != null)
        {
            outline.OutlineColor = color;
        }
    }
}