using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoUI : GaugeUI<AmmoUI>
{
    internal void SetBulletCount(int v1, int v2, int v3, int v4)
    {
        SetGauge(v1, v2);
        //v3, int v4

    }
}
