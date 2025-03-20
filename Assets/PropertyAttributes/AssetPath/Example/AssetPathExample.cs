using UnityEngine;

public class AssetPathExample : MonoBehaviour
{
    [AssetPath(typeof(AssetPathExample))]
    public string Prefab;

#if UNITY_EDITOR
    [AssetPath(typeof(UnityEditor.SceneAsset))]
#endif
    public string Scene;

    [AssetPath(typeof(GameObject))]
    public int integer;

    void Start()
    {
        Debug.Log(Prefab);
        Debug.Log(Scene);
    }
}
