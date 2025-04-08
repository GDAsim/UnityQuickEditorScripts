/*
 * About:
 * Custom UnityEditor Property Drawer for [Expandable] CustomAttribute
 * Allows ScriptableObject to be able to expand in the inspector and modify and edit
 * 
 * How To Use:
 * Use [Expandable] on the ScriptableObject variable
 * 
 */

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomPropertyDrawer(typeof(ExpandableAttribute))]
public class ExpandablePropertyDrawer : PropertyDrawer
{
    float totalHeight;
    ScriptableObject scriptableObject;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var targetObject = property.serializedObject.targetObject;

        if (property.objectReferenceValue == null)
        {
            EditorGUI.PropertyField(position, property, label);
            return;
        }

        scriptableObject = property.objectReferenceValue as ScriptableObject;
        if (scriptableObject == null)
        {
            EditorGUI.PropertyField(position, property, label);
            return;
        }

        var foldoutRect = new Rect()
        {
            x = position.x,
            y = position.y,
            width = EditorGUIUtility.labelWidth,
            height = EditorGUIUtility.singleLineHeight
        };
        property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, label, true);

        var propertyRect = new Rect()
        {
            x = position.x,
            y = position.y,
            width = position.width,
            height = EditorGUIUtility.singleLineHeight
        };

        EditorGUI.PropertyField(propertyRect, property, label, false);

        if (property.isExpanded)
        {
            DrawChildProperties(position, property);
        }
    }

    private void DrawChildProperties(Rect rect, SerializedProperty property)
    {
        ScriptableObject scriptableObject = property.objectReferenceValue as ScriptableObject;
        if (scriptableObject == null) return;

        var boxRect = new Rect()
        {
            x = 0.0f,
            y = rect.y + EditorGUIUtility.singleLineHeight,
            width = rect.width * 2.0f,
            height = rect.height - EditorGUIUtility.singleLineHeight
        };

        GUI.Box(boxRect, GUIContent.none);

        using (new EditorGUI.IndentLevelScope())
        {
            var serializedObject = new SerializedObject(scriptableObject);
            serializedObject.Update();

            using (var iterator = serializedObject.GetIterator())
            {
                totalHeight = EditorGUIUtility.singleLineHeight;

                if (iterator.NextVisible(true))
                {
                    do
                    {
                        SerializedProperty childProperty = serializedObject.FindProperty(iterator.name);
                        if (childProperty.name.Equals("m_Script", System.StringComparison.Ordinal))
                        {
                            continue;
                        }

                        float childHeight = EditorGUI.GetPropertyHeight(childProperty);
                        var childRect = new Rect()
                        {
                            x = rect.x,
                            y = rect.y + totalHeight,
                            width = rect.width,
                            height = childHeight
                        };

                        EditorGUI.PropertyField(childRect, childProperty);

                        totalHeight += childHeight;
                    }
                    while (iterator.NextVisible(false));
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (scriptableObject == null) return base.GetPropertyHeight(property, label);

        return totalHeight;
    }
}
#endif

public class ExpandableAttribute : PropertyAttribute { }