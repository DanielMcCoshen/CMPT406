using UnityEngine;
using System.Collections;


[System.Serializable]
public class Filter
{
    [SerializeField]
    private int id = -1;
    public int Id { get => id; }

    public Filter(bool North, bool South, bool East, bool West){
        //all doors
        if (North && East && South && West)
        {
            id = 0;
        }
        //one door
        else if (North && !East && !South && !West)
        {
            id = 1;
        }
        else if (!North && !East && !South && West)
        {
            id = 2;
        }
        else if (!North && !East && South && !West)
        {
            id = 3;
        }
        else if (!North && East && !South && !West)
        {
            id = 4;
        }
        //two doors
        else if (North && !East && !South && West)
        {
            id = 5;
        }
        else if (!North && !East && South && West)
        {
            id = 6;
        }
        else if (!North && East && South && !West)
        {
            id = 7;
        }
        else if (North && East && !South && !West)
        {
            id = 8;
        }
        else if (North && !East && South && !West)
        {
            id = 9;
        }
        else if (!North && East && !South && West)
        {
            id = 10;
        }
        //three doors
        else if (North && East && !South && West)
        {
            id = 11;
        }
        else if (North && !East && South && West)
        {
            id = 12;
        }
        else if (!North && East && South && West)
        {
            id = 13;
        }
        else if (North && East && South && !West)
        {
            id = 1;
        }
        else
        {
            throw new System.ArgumentException("Invalid Door State");
        }
    }
}
