using UnityEngine;

public class AssetPreviewExample : MonoBehaviour
{
    [AssetPreview]
    public Sprite sprite;

    [AssetPreview(96, 96)]
    public GameObject prefab;
}