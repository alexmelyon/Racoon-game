using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSounds : MonoBehaviour
{
    public static AudioSounds Instance;

    private void Awake() {
        Instance = this;
    }
}
