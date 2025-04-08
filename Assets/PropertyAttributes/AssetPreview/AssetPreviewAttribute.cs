/*
 * About:
 * Custom UnityEditor Property Drawer for [AssetPreview] CustomAttribute
 * Draws an Asset Preview right in the inspector window
 * 
 * How To Use:
 * Use [AssetPreview(width,height)] on the asset refrence variables such as GameObject, Sprite
 * e.g [AssetPreview(96, 96)]
 * 
 */

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomPropertyDrawer(typeof(AssetPreviewAttribute))]
public class AssetPreviewPropertyDrawer : PropertyDrawer
{
    Texture2D previewTexture;
    float heightForTexture;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var assetPreviewAttribute = attribute as AssetPreviewAttribute;
        var targetObject = property.serializedObject.targetObject;

        if (property.propertyType != SerializedPropertyType.ObjectReference)
        {
            Debug.LogError("Only ObjectReference Types are Allowed for [AssetPreview] Attribute", targetObject);

            EditorGUIUtilities.DrawErrorField(position, property, label);

            return;
        }

        var propertyRect = position;
        propertyRect.height = EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(propertyRect, property, label);

        if (property.objectReferenceValue == null) return;

        previewTexture = AssetPreview.GetAssetPreview(property.objectReferenceValue);
        if (previewTexture != null)
        {
            Rect indentRect = EditorGUI.IndentedRect(position);
            float indentLength = indentRect.x - position.x;

            var targetHeight = assetPreviewAttribute.PreviewHeight;
            heightForTexture = Mathf.Clamp(targetHeight, 0, previewTexture.height);

            var previewRect = new Rect()
            {
                x = position.x + indentLength,
                y = position.y + EditorGUIUtility.singleLineHeight,
                width = position.width,
                height = heightForTexture
            };
            GUI.Label(previewRect, previewTexture);
        }
    }
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (previewTexture != null)
        {
            return base.GetPropertyHeight(property, label) + heightForTexture;
        }

        return base.GetPropertyHeight(property, label);
    }
}
#endif

public class AssetPreviewAttribute : PropertyAttribute
{
    public readonly int PreviewWidth;
    public readonly int PreviewHeight;

    public AssetPreviewAttribute(int previewWidth = 64, int previewHeight = 64)
    {
        PreviewWidth = previewWidth;
        PreviewHeight = previewHeight;
    }
}