using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectImpact : MonoBehaviour
{
    [Header("Pollution Impact")]
    public float airPperCycle = 0, waterPperCycle = 0, landPperCycle = 0;
    [Header("Establishment Thresholds")]
    public float airQuality = 0, waterQuality = 0, landQuality = 0;
}
