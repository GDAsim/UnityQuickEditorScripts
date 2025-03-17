using System;
using UnityEngine;

public class EnumFlagExample : MonoBehaviour
{
    [EnumFlag]
    [SerializeField] Fruits fruit;

    [Flags]
    public enum Fruits
    {
        None,
        Orange,
        Potato
    }

    void Update()
    {
        print(fruit);
    }
}