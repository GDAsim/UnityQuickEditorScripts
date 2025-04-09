using UnityEngine;
using static StatsBarAttribute;

public class StatsBarExample : MonoBehaviour
{
    public int IntMax = 100;
    public float FloatMax = 100;

    [StatsBar(null, StatsBarColor.Green)] public float FloatStatsEmpty = 90;

    [StatsBar(nameof(IntMax), StatsBarColor.Red)] public int IntegerStats = 25;

    [StatsBar(nameof(FloatMax), StatsBarColor.Blue)] public float FloatStats = 50;

    [StatsBar(nameof(FloatMax), StatsBarColor.Yellow)] public bool BoolStats = true;
}