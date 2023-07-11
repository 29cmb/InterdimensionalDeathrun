using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Configuration : MonoBehaviour
{
    public bool isFlipped = false;
    public bool isGlitched = false;
    public bool isMorphed = false;
    public int morphKey = 1;
    public int keyNumber = 0;
    public int abilityNumber = 0;
    public GameObject player;

    void Start() {
        player = GameObject.Find("Player");
    }
}
