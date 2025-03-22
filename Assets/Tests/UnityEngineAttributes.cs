#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

/// <summary>
/// Automatically adds required components to the same GameObject.
/// Mark this component as dependent on required component
/// Prevents removal dependnt component
/// </summary>
[RequireComponent(typeof(Rigidbody))]

/// <summary>
/// Apply to classes sub MonoBehaviour.Requires matching class name and file name.
/// </summary>
[AddComponentMenu("Name/Name")]

/// <summary>
/// Multiple of this component cannot be added to the same GameObject.
/// </summary>
[DisallowMultipleComponent]

/// <summary>
/// Provide a custom documentation URL for a class.
/// </summary>
[HelpURL("http://example.com/docs/MyComponent.html")]

/// <summary>
/// Allows the monobehavour class to run unity events in Edit Mode.
/// e.g Awake,Update,OnGUI
/// </summary>
[ExecuteInEditMode]
[ExecuteAlways]

public class ClassName : MonoBehaviour
{
    /// <summary>
    /// Show alpha 
    /// Set HDR
    /// </summary>
    [ColorUsage(false, true)]
    public Color ColorField;

    /// <summary>
    /// Add a right click menu to the script/component to call a void function
    /// </summary>
    [ContextMenu("Do Something")]
    void DoSomething()
    {
        Debug.Log("Something");
    }

    /// <summary>
    /// Adds a right click menu to the variable to call a function using string
    /// </summary>
    [ContextMenuItem("Do Something", "DoSomething")]
    public float RightClickOnThisField;

    /// <summary>
    /// Make a string field to have multi-line, only works with string
    /// </summary>
    [Multiline(4)]
    public string MultilineString;

    /// <summary>
    /// Adds a Label "header" above the field
    /// </summary>
    [Header("Header Label Here")]
    public int HeaderLabelAbove;

    [Space(25)]//Use this PropertyAttribute on a field to add some spacing in the Inspector.
    public int SpaceAbove;

    /// <summary>
    /// make a string be edited with a height-flexible and scrollable text area.
    /// </summary>
    [TextArea]
    public string MyTextArea;

    /// <summary>
    /// Tool tip
    /// </summary>
    [Tooltip("Tooltip here")]
    public int HoverOverThis;

    /// <summary>
    /// Makes a variable not show up in the inspector but be serialized.
    /// </summary>
    [HideInInspector]
    public int HiddenVar;

    /// <summary>
    /// Force Serialize a private var
    /// </summary>
    [SerializeField]
    private bool ForceSerializedPrivateVar = true;

    /// <summary>
    /// Make a variable not update unless user press enter in inspector. float, int, or string 
    /// </summary>
    [Delayed]
    public string Somefield2;

    /// <summary>
    /// Draws a Slider in inspector for float/int vars
    /// </summary>
    [Range(0, 1)]
    public float RangeVar;

    /// <summary>
    /// OnGUI will run on specified display, Default is 0
    /// Some platform do not support multiple display
    /// </summary>
    [GUITarget(1)]
    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 300, 100), "Visible on Display 1 Only");
    }


    /// <summary>
    /// Assembly declaration only
    /// For Managing API and Libraries Versioning in older unity
    /// Marks the API as compatible with older versions
    /// </summary>
    //[UnityAPICompatibilityVersion("6.0.0", true)]

    /// <summary>
    /// Have Imageeffects to run on the Scene View
    /// </summary>
    //[ImageEffectAllowedInSceneView]

    //[ImageEffectOpaque]
    //Any Image Effect with this attribute will be rendered after opaque geometry but before transparent geometry
    //This allows for effects which extensively use the depth buffer(SSAO, etc) to affect only opaque pixels.This attribute can be used to reduce the amount of visual artifacts in a scene with post processing.

    //[ImageEffectTransformsToLDR]
    //When using HDR rendering it can sometime be desirable to switch to LDR rendering during ImageEffect rendering.
    //Using this Attribute on an image effect will cause the destination buffer to be an LDR buffer, and switch the rest of the Image Effect pipeline into LDR mode. It is the responsibility of the Image Effect that this Attribute is associated to ensure that the output is in the LDR range.

    //[PreferBinarySerialization]
    // RuntimeInitializeOnLoadMethodAttribute
    //[SharedBetweenAnimatorsAttribute]
}

/// <summary>
/// Mark the custom editor to allow editing multiple selected objects
/// </summary>
[CanEditMultipleObjects]

/// <summary>
/// Loads this editor class when unity loads and scripts recompiled
/// </summary>
[InitializeOnLoad]
public class EditorExample : Editor
{
    /// <summary>
    /// Run this method when unity loads
    /// Usually for Editor methods
    /// </summary>
    [InitializeOnLoadMethod]
    static void OnProjectLoadedInEditor()
    {
        Debug.Log("Project loaded in Unity Editor");
    }

    /// <summary>
    /// Run this method when unity enter play mode
    /// Usually for Editor methods
    /// </summary>
    [InitializeOnEnterPlayMode]
    static void OnProjectLoadedInEditor(EnterPlayModeOptions options)
    {
        Debug.Log("Entering PlayMode");
        options.HasFlag(EnterPlayModeOptions.DisableSceneReload);
    }
}

/// <summary>
/// Make the SciptableObject class an asset to be listed in assets/create submenu. 
/// to be easily created and stored in the project as ".asset" files.
/// </summary>
[CreateAssetMenu(fileName = "ScriptableObjectExample", menuName = "ScriptableObject")]
public class ScriptableObjectExample : ScriptableObject { }

#endif