using System;
using UnityEngine;

/**
 * A simple pair, made into a separate class for the purposes of serialization.
 */
[Serializable]
public class FloatGameObjectPair
{
    [field: SerializeField] public float Quantity { get; private set; }
    [field: SerializeField] public GameObject GameObject { get; private set; }
}