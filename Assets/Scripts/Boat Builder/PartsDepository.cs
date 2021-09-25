using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsDepository : MonoBehaviour
{
    public static PartsDepository instance;

    public List<BoatsStern> sterns;
    public List<BoatsMast> masts;
    public List<BoatsSail> sails;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance == this)
            Destroy(gameObject);
    }
}
