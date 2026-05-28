using UnityEngine;
using UnityEditor;

// Drop this script into an "Editor" folder in your Unity project.
// Open it via: Tools > Handle Caps Showcase
// Then select the "HandleCapsShowcase" GameObject (auto-created) to see
// all built-in Handles cap types drawn in a row in the Scene view.

public class AllHandleCapsShowcase : EditorWindow
{
    static float capSize = 0.5f;
    static float spacing = 2f;
    static Color capColor = Color.cyan;

    [MenuItem("Tools/Handle Caps Showcase")]
    public static void ShowWindow()
    {
        GetWindow<AllHandleCapsShowcase>("Handle Caps Showcase");
    }

    void OnGUI()
    {
        GUILayout.Label("All Handles Cap Types", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox(
            "Select the 'HandleCapsShowcase' object in the scene to see " +
            "every built-in Handles.*HandleCap drawn in a row.",
            MessageType.Info);

        capSize = EditorGUILayout.Slider("Cap Size", capSize, 0.05f, 3f);
        spacing = EditorGUILayout.Slider("Spacing", spacing, 0.5f, 5f);
        capColor = EditorGUILayout.ColorField("Cap Color", capColor);

        if (GUILayout.Button("Create / Select Showcase Object"))
        {
            var go = GameObject.Find("HandleCapsShowcase");
            if (go == null)
            {
                go = new GameObject("HandleCapsShowcase");
                go.AddComponent<HandleCapsShowcaseComponent>();
            }
            Selection.activeGameObject = go;
            SceneView.FrameLastActiveSceneView();
        }

        SceneView.RepaintAll();
    }

    // Static accessors so the component's OnSceneGUI can read live window values
    public static float CapSize => capSize;
    public static float Spacing => spacing;
    public static Color CapColor => capColor;
}

// Empty MonoBehaviour just so there's a GameObject to select/frame.
public class HandleCapsShowcaseComponent : MonoBehaviour { }

[CustomEditor(typeof(HandleCapsShowcaseComponent))]
public class HandleCapsShowcaseEditor : Editor
{
    // Every built-in Handles cap function, paired with a label.
    static (string label, Handles.CapFunction cap)[] Caps =>
        new (string, Handles.CapFunction)[]
        {
            ("Arrow",     Handles.ArrowHandleCap),
            ("Circle",    Handles.CircleHandleCap),
            ("Cone",      Handles.ConeHandleCap),
            ("Cube",      Handles.CubeHandleCap),
            ("Cylinder",  Handles.CylinderHandleCap),
            ("Dot",       Handles.DotHandleCap),
            ("Rectangle", Handles.RectangleHandleCap),
            ("Sphere",    Handles.SphereHandleCap),
        };

    void OnSceneGUI()
    {
        Transform origin = ((HandleCapsShowcaseComponent)target).transform;
        float size = AllHandleCapsShowcase.CapSize;
        float spacing = AllHandleCapsShowcase.Spacing;

        Handles.color = AllHandleCapsShowcase.CapColor;

        var caps = Caps;
        for (int i = 0; i < caps.Length; i++)
        {
            Vector3 pos = origin.position + new Vector3(i * spacing, 0f, 0f);
            Quaternion rot = Quaternion.identity;

            // EventType.Repaint draws the cap; other event types handle interaction.
            caps[i].cap(
                controlID: GUIUtility.GetControlID(FocusType.Passive),
                position: pos,
                rotation: rot,
                size: HandleUtility.GetHandleSize(pos) * size,
                eventType: Event.current.type
            );

            Handles.Label(pos + Vector3.up * (size + 0.3f), caps[i].label);
        }
    }
}