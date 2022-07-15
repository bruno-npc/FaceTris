using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigFrameRate : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = 60;
    }
}
