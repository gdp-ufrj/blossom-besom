using UnityEngine;
using UnityEngine.Events;
using System;
[System.Serializable]
public class HealEvent : UnityEvent<int, Action> { };
