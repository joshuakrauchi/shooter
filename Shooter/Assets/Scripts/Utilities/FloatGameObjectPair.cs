using System;
using UnityEngine;

[Serializable]
public class FloatGameObjectPair
{
    [field: SerializeField] public float Quantity { get; private set; }
    [field: SerializeField] public GameObject GameObject { get; private set; }
}