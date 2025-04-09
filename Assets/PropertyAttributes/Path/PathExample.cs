using UnityEngine;

public class PathExample : MonoBehaviour
{
    [Path] public string SelectPath;

    [Path] public string[] SelectPaths;

    [Path] public int SelectPathError;

    [Path] public int[] SelectPathsError;
}