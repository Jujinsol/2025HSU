using UnityEngine;

public class RandomMap : MonoBehaviour
{
    public SelectMap[] map;
    void Start()
    {
        string[] mapName = { "A", "B", "C" };
        makeRandom(mapName);

        for(int i = 0; i<map.Length && i<mapName.Length; i++)
        {
            if (map[i] != null)
            {
                map[i].Map = mapName[i];
            }
        }

    }

    void makeRandom(string[] str)
    {
        for (int i = str.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i +1);

            string tmp = str[i];
            str[i] = str[j];
            str[j] = tmp;
        }
    }
}
