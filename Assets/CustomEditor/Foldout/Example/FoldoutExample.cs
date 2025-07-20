using UnityEngine;

public class FoldoutExample : MonoBehaviour
{
    [Foldout("Setup")] public Transform selfTransform;

    [Foldout("Data")] public int HP;
    [Foldout("Data")] public int AT;
}