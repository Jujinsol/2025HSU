using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CoverSpwan : MonoBehaviour
{
    public GameObject Normal;
    public GameObject Empty;
    public GameObject Event;

    public float xSize = 1f;
    public float ySize = 1f;
    int randInt = 0;

    private List<GameObject> Covers = new List<GameObject>();

    public void setCover()
    {
        for (float x = 37f; x <= 43f; x += xSize)
        {
            for (float y = -0.35f; y <= 1.65f; y += ySize)
            {
                Vector3 spawnPos = new Vector3(x, y);

                if (Mathf.Approximately(x, 37f) && Mathf.Approximately(y, 0.65f))
                    continue;

                GameObject cover = null;

                randInt = Random.Range(0, 10);
                if (randInt <= 3)
                {
                    cover = Instantiate(Empty, spawnPos, Quaternion.identity);
                }
                else if (randInt > 3 && randInt <= 6)
                {
                    cover = Instantiate(Normal, spawnPos, Quaternion.identity);
                }
                else
                {
                    cover = Instantiate(Event, spawnPos, Quaternion.identity);
                }

                if (cover != null)
                {
                    Covers.Add(cover);
                }
            }
        }
    }

    public void deleteCover()
    {
        for (int i = 0; i < Covers.Count; i++)
        {
            Destroy(Covers[i]);
        }
    }

}
