using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpIcons : MonoBehaviour
{
    public PowerUpIcon[] list;


    [Serializable]
    public struct PowerUpIcon
    {
        public string name;
        public Sprite prefab;
    }
}
