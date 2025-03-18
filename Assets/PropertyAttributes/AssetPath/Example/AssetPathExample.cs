using UnityEditor;
using UnityEngine;

public class AssetPathExample : MonoBehaviour
{
    [AssetPath(typeof(AssetPathExample))]
    public string Prefab;

    [AssetPath(typeof(SceneAsset))]
    public string Scene;

    [AssetPath(typeof(GameObject))]
    public int integer;

    void Start()
    {
        Debug.Log(Prefab);
        Debug.Log(Scene);
    }
}
