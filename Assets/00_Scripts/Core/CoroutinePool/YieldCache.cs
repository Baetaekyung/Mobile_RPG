using System;
using System.Collections.Generic;
using UnityEngine;

public static class YieldCache
{
    private static Dictionary<float, WaitForSeconds> wfsCache = new();

    private static readonly WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
    private static readonly WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

    public static WaitForSeconds GetWaitSec(float seconds)
    {
        if(!wfsCache.ContainsKey(seconds))
            wfsCache[seconds] = new WaitForSeconds(seconds);
        return wfsCache[seconds];
    }

    public static WaitForEndOfFrame GetEndOfFrame() => waitForEndOfFrame;
    public static WaitForFixedUpdate GetFixedUpdate() => waitForFixedUpdate;
}
