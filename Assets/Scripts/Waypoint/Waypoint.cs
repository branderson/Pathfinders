using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {
    public int ID;
    public int NextID;
    public int PreviousID;

    public int Next() {
        if (NextID == 0)
            return -PreviousID;
        else
            return NextID;
    }

    public int Previous()
    {
        if (PreviousID == 0)
            return -NextID;
        else
            return PreviousID;
    }
}
