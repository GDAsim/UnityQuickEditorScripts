using UnityEditor;
using UnityEngine;

/// <summary>
/// Apply to classes sub MonoBehaviour.Requires matching class name and file name.
/// </summary>
[AddComponentMenu("Name/Name")]
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
    /// force Serialize a private var
    /// </summary>
    [SerializeField]
    private bool hasHealthPotion = true;

    //[InitializeOnLoadAttribute]
    //[InitializeOnLoadMethodAttribute]
    //    [InitializeOnEnterPlayMode]
    //[FilePathAttribute]
}



/// <summary>
/// Mark the custom editor to allow editing multiple selected objects
/// </summary>
[CanEditMultipleObjects]
public class EditorExample : Editor
{

}

/// <summary>
/// Make the SciptableObject class an asset to be listed in assets/create submenu. to be easily created and stored in the project as ".asset" files.
/// </summary>
public class ScriptableObjectExample : ScriptableObject
{

}

[CreateAssetMenu]// Make the SciptableObject class an asset to be listed in assets/create submenu. to be easily created and stored in the project as ".asset" files.
public class ClassName2 : MonoBehaviour
{
    [Delayed]//Make a variable not update unless user press enter in inspector. float, int, or string 
    public string Somefield2;
}
[DisallowMultipleComponent]//Prevents MonoBehaviour of same type (or subtype) to be added more than once to a GameObject.
public class ClassName3 : MonoBehaviour
{

}
/// <summary>
/// - Update is only called when something in the scene changed.
///- OnGUI is called when the Game View recieves an Event.
///- OnRenderObject and the other rendering callback functions are called on every repaint of the Scene View or Game View.
/// </summary>
[ExecuteInEditMode] //Allows monobehavour to run in edit mode
public class ClassName4 : MonoBehaviour
{
    [GUITarget(1)]// Label will appear on display 0 and 1 only, use for TV and Wii U dev that has more than one gui display
    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 300, 100), "Visible on TV and Wii U GamePad only");
    }

}
[HelpURL("http://example.com/docs/MyComponent.html")]//Provide a custom documentation URL for a class.
[CanEditMultipleObjects]
public class ClassName5 : MonoBehaviour
{


    //[ImageEffectAllowedInSceneView]//Any Image Effect with this attribute can be rendered into the scene view camera.

    //[ImageEffectOpaque]
    //Any Image Effect with this attribute will be rendered after opaque geometry but before transparent geometry
    //This allows for effects which extensively use the depth buffer(SSAO, etc) to affect only opaque pixels.This attribute can be used to reduce the amount of visual artifacts in a scene with post processing.

    //[ImageEffectTransformsToLDR]
    //When using HDR rendering it can sometime be desirable to switch to LDR rendering during ImageEffect rendering.
    //Using this Attribute on an image effect will cause the destination buffer to be an LDR buffer, and switch the rest of the Image Effect pipeline into LDR mode. It is the responsibility of the Image Effect that this Attribute is associated to ensure that the output is in the LDR range.

    //[PreferBinarySerialization]

    [Range(0, 1)]//Attribute used to make a float or int variable in a script be restricted to a specific range.
    public float test;
}

[RequireComponent(typeof(Rigidbody))]/// automatically adds required components as dependencies.
public class PlayerScript : MonoBehaviour
{
    //RuntimeInitializeOnLoadMethodAttribute



    //[SharedBetweenAnimatorsAttribute]



    //UnityAPICompatibilityVersion
}