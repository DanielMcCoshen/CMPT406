using UnityEngine;
using System.Collections;

public class Filter
{
    public int Id { get; }

    public Filter(bool North, bool South, bool East, bool West){
        //all doors
        if (North && East && South && West)
        {
            Id = 0;
        }
        //one door
        else if (North && !East && !South && !West)
        {
            Id = 1;
        }
        else if (!North && East && !South && !West)
        {
            Id = 2;
        }
        else if (!North && !East && South && !West)
        {
            Id = 3;
        }
        else if (!North && !East && !South && West)
        {
            Id = 4;
        }
        //two doors
        else if (North && East && !South && !West)
        {
            Id = 5;
        }
        else if (!North && East && South && !West)
        {
            Id = 6;
        }
        else if (!North && !East && South && West)
        {
            Id = 7;
        }
        else if (North && !East && !South && West)
        {
            Id = 8;
        }
        else if (North && !East && South && !West)
        {
            Id = 9;
        }
        else if (!North && East && !South && West)
        {
            Id = 10;
        }
        //three doors
        else if (North && East && !South && West)
        {
            Id = 11;
        }
        else if (North && East && South && !West)
        {
            Id = 12;
        }
        else if (!North && East && South && West)
        {
            Id = 13;
        }
        else if (North && !East && South && West)
        {
            Id = 1;
        }
        else
        {
            throw new System.ArgumentException("Invalid Door State");
        }
    }
}
