using UnityEngine;
using UnityEngine.UI;

public class BoatsMast : BoatsPart
{
   [SerializeField] private Transform sailMountingPoint;
    public Transform GetSailMountingPoint => sailMountingPoint;
}
