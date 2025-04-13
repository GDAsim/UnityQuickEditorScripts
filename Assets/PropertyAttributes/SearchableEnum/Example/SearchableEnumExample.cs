using UnityEngine;

public class SearchableEnumExample : MonoBehaviour
{
    public enum Fruits { a, b, c, d, e, f }

    [SearchableEnum] public Fruits fruits;
    [SearchableEnum] public Fruits[] EnumArray;

    [SearchableEnum] public string Error;
}
