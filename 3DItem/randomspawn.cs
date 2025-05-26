using UnityEngine;

[System.Serializable]
public class SpawnData
{
    public GameObject itemPrefab;             // 등장할 아이템 프리팹
    public Transform[] spawnPoints;           // 해당 아이템이 등장할 수 있는 위치들
    public int spawnCount;                    // 실제로 등장시킬 개수
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
        // 위치 배열 복사 후 셔플
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