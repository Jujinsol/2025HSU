using UnityEngine;

[System.Serializable]
public class SpawnData
{
    public GameObject itemPrefab;             // ������ ������ ������
    public Transform[] spawnPoints;           // �ش� �������� ������ �� �ִ� ��ġ��
    public int spawnCount;                    // ������ �����ų ����
}

public class randomspawn : MonoBehaviour
{
    public SpawnData[] spawnDatas;

    void Start()
    {
        foreach (SpawnData data in spawnDatas)
        {
            SpawnItems(data);
        }
    }

    void SpawnItems(SpawnData data)
    {
        // ��ġ �迭 ���� �� ����
        Transform[] points = (Transform[])data.spawnPoints.Clone();
        Shuffle(points);

        for (int i = 0; i < Mathf.Min(data.spawnCount, points.Length); i++)
        {
            Instantiate(data.itemPrefab, points[i].position, points[i].rotation);   
        }
    }

    void Shuffle(Transform[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int rand = Random.Range(0, i + 1);
            Transform temp = array[i];
            array[i] = array[rand];
            array[rand] = temp;
        }
    }
}