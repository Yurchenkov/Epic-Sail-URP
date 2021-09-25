using UnityEngine;
using UnityEngine.UI;

public class BoatsStern : BoatsPart
{
   [SerializeField] private Transform mastMountingPoint;

   public Transform GetMastMountingPoint => mastMountingPoint;
}
