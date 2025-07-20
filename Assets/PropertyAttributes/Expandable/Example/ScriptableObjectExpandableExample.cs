using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjectExample", menuName = "ScriptableObjects/Test/ExpandableExample", order = 0)]
public class ScriptableObjectExpandableExample : ScriptableObject
{
    public int a;
    public float b;
    public string c;
    public bool d;
    public List<float> e;
}