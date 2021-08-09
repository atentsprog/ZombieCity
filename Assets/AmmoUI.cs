using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoUI : GaugeUI<AmmoUI>
{
    internal void SetGuage(int bulletCountInClip, int maxBulletCountInClip, int allBulletCount, int maxBulletCount)
    {
        SetGuage(bulletCountInClip, maxBulletCountInClip);
        countText.text = $"{allBulletCount}/{maxBulletCount}";
    }
}
