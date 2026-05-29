using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CustomEditorTemplate : MonoBehaviour
{
    [SerializeField] List<GameObject> handlesExampleGO = new List<GameObject>();

    public enum HandlesCapType
    {
        Arrow,
        Circle,
        Cone,
        Cube,
        Cylinder,
        Dot,
        Rectangle,
        Sphere
    }

    [Header("For Handles")]
    [SerializeField] Color handlesColor = Color.coral;

    [Header("For FreeMoveHandle")]
    [SerializeField] HandlesCapType capType = HandlesCapType.Sphere;
    [SerializeField] float size = 1;
    [SerializeField] Vector3 ctrlSnap = new Vector3(0.5f, 0.5f, 0.5f);

    void Update()
    {
        transform.position = Vector3.zero;

        for (int i = handlesExampleGO.Count - 1; i > transform.childCount - 1; i--)
        {
            DestroyImmediate(handlesExampleGO[i]);
            handlesExampleGO.RemoveAt(i);
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);

            if (handlesExampleGO.Count - 1 < i)
            {
                handlesExampleGO.Add(child.gameObject);
            }
            else
            {
                handlesExampleGO[i] = child.gameObject;
            }
        }
    }
}