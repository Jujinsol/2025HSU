using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public static Storage inst;

    public float food, water;
    public int oxygentank, bordgame, medicine, fixtool, map, research, book, battery, yellow, blue, white;

    private void Start()
    {
        inst = this;
    }

    public void ReceiveItemsFrom(player p)
    {
        food += p.food;
        water += p.water;
        oxygentank += p.oxygentank;
        bordgame += p.bordgame;
        medicine += p.medicine;
        fixtool += p.fixtool;
        map += p.map;
        research += p.research;
        book += p.book;
        battery += p.battery;
        yellow += p.yellow;
        blue += p.blue;
        white += p.white;

        p.food = 0;
        p.water = 0;
        p.oxygentank = 0;
        p.bordgame = 0;
        p.medicine = 0;
        p.fixtool = 0;
        p.map = 0;
        p.research = 0;
        p.book = 0;
        p.battery = 0;
        p.yellow = 0;
        p.blue = 0;
        p.white = 0;
    }
}