using System;
using System.Drawing;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.AdaptivePerformance.Provider;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;
using static UnityEngine.Audio.ProcessorInstance;

[CustomEditor(typeof(CustomEditorTemplate), true)]

public class CustomEditorTemplateEditor : Editor
{
    void OnEnable()
    {
        Tools.hidden = true;
    }
    void OnDisable()
    {
        Tools.hidden = false;
    }
    void OnSceneGUI()
    {
        var script = (CustomEditorTemplate)target;



        var handlesExampleProp = serializedObject.FindProperty("handlesExampleGO");
        if (handlesExampleProp == null) return;

        // Draw Transform Handle
        if (handlesExampleProp.arraySize >= 0)
        {
            var handleGOProp = handlesExampleProp.GetArrayElementAtIndex(0);
            var handleGO = handleGOProp.objectReferenceValue as GameObject;
            if (handleGO != null)
            {
                EditorGUI.BeginChangeCheck();

                Handles.Label(handleGO.transform.position, "Transform Handle");

                float size = HandleUtility.GetHandleSize(handleGO.transform.position);
                var pos = handleGO.transform.position;
                var rot = handleGO.transform.rotation;
                var scale = handleGO.transform.localScale;
                Handles.TransformHandle(ref pos, ref rot, ref scale);

                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(handleGO.transform, "Transform Object");
                    handleGO.transform.SetPositionAndRotation(pos, rot);
                    handleGO.transform.localScale = scale;
                }
            }
        }

        // Draw Position Handle
        if (handlesExampleProp.arraySize >= 1)
        {
            var handleGOProp = handlesExampleProp.GetArrayElementAtIndex(1);
            var handleGO = handleGOProp.objectReferenceValue as GameObject;
            if (handleGO != null)
            {
                EditorGUI.BeginChangeCheck();
                Handles.Label(handleGO.transform.position, "Position Handle");
                var newPositionHandlePos = Handles.PositionHandle(handleGO.transform.position, handleGO.transform.rotation);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(handleGO.transform, "Move Object");
                    handleGO.transform.position = newPositionHandlePos;
                }
            }
        }

        // Draw Rotation Handle
        if (handlesExampleProp.arraySize >= 2)
        {
            var handleGOProp = handlesExampleProp.GetArrayElementAtIndex(2);
            var handleGO = handleGOProp.objectReferenceValue as GameObject;
            if (handleGO != null)
            {
                EditorGUI.BeginChangeCheck();
                Handles.Label(handleGO.transform.position, "Rotate Handle");
                var newRotationHandleRotation = Handles.RotationHandle(handleGO.transform.rotation, handleGO.transform.position);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(handleGO.transform, "Rotate Object");
                    handleGO.transform.rotation = newRotationHandleRotation;
                }
            }
        }

        //// Draw Scale Handle
        if (handlesExampleProp.arraySize >= 3)
        {
            var handleGOProp = handlesExampleProp.GetArrayElementAtIndex(3);
            var handleGO = handleGOProp.objectReferenceValue as GameObject;
            if (handleGO != null)
            {
                EditorGUI.BeginChangeCheck();
                Handles.Label(handleGO.transform.position, "Scale Handle");
                var newScaleHandleScale = Handles.ScaleHandle(handleGO.transform.localScale, handleGO.transform.position, handleGO.transform.rotation);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(handleGO.transform, "Scale Object");
                    handleGO.transform.localScale = newScaleHandleScale;
                }
            }
        }

        var handlesColorProp = serializedObject.FindProperty("handlesColor");
        Handles.color = handlesColorProp.colorValue;

        // Draw Free Move Handle
        if (handlesExampleProp.arraySize >= 4)
        {
            var handleGOProp = handlesExampleProp.GetArrayElementAtIndex(4);
            var handleGO = handleGOProp.objectReferenceValue as GameObject;
            if (handleGO != null)
            {
                EditorGUI.BeginChangeCheck();

                Handles.Label(handleGO.transform.position, "Free Move Handle");

                var capTypeProp = serializedObject.FindProperty("capType");
                var sizeProp = serializedObject.FindProperty("size");
                var snapProp = serializedObject.FindProperty("ctrlSnap");
                var capType = GetHandlesCapFunction((CustomEditorTemplate.HandlesCapType)capTypeProp.intValue);
                var size = sizeProp.floatValue;
                var snap = snapProp.vector3Value;

                var newPositionHandlePos = Handles.FreeMoveHandle(handleGO.transform.position, size, snap, capType);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(handleGO.transform, "Move Object");
                    handleGO.transform.position = newPositionHandlePos;
                }
            }
        }

        // Draw Rotation Handle
        if (handlesExampleProp.arraySize >= 5)
        {
            var handleGOProp = handlesExampleProp.GetArrayElementAtIndex(5);
            var handleGO = handleGOProp.objectReferenceValue as GameObject;
            if (handleGO != null)
            {
                EditorGUI.BeginChangeCheck();

                Handles.Label(handleGO.transform.position, "Free Rotate Handle");

                float size = HandleUtility.GetHandleSize(handleGO.transform.position);
                var newRotationHandleRot = Handles.FreeRotateHandle(handleGO.transform.rotation, handleGO.transform.position, size);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(handleGO.transform, "Rotate Object");
                    handleGO.transform.rotation = newRotationHandleRot;
                }
            }
        }

        // Draw Radius Handle
        if (handlesExampleProp.arraySize >= 6)
        {
            var handleGOProp = handlesExampleProp.GetArrayElementAtIndex(6);
            var handleGO = handleGOProp.objectReferenceValue as GameObject;
            if (handleGO != null)
            {
                EditorGUI.BeginChangeCheck();

                Handles.Label(handleGO.transform.position, "Radius Handle");

                var newRadiusHandleValue = Handles.RadiusHandle(handleGO.transform.rotation, handleGO.transform.position, handleGO.transform.localScale.x / 2);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(handleGO.transform, "Scale Object");
                    handleGO.transform.localScale = new Vector3(newRadiusHandleValue, newRadiusHandleValue, newRadiusHandleValue);
                }
            }
        }

        // Draw Disc Handle
        if (handlesExampleProp.arraySize >= 7)
        {
            var handleGOProp = handlesExampleProp.GetArrayElementAtIndex(7);
            var handleGO = handleGOProp.objectReferenceValue as GameObject;
            if (handleGO != null)
            {
                EditorGUI.BeginChangeCheck();

                Handles.Label(handleGO.transform.position, "Disc Handle");

                var size = HandleUtility.GetHandleSize(handleGO.transform.position);
                var axis = Vector3.up;
                var snap = 25f; // in degree
                var newDiscRotation = Handles.Disc(handleGO.transform.rotation, handleGO.transform.position, axis, size, false, snap);
                Handles.DrawSolidDisc(handleGO.transform.position, axis, size);
                Handles.DrawWireDisc(handleGO.transform.position, axis, size);

                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(handleGO.transform, "Rotate Object");
                    handleGO.transform.rotation = newDiscRotation;
                }
            }
        }



        //Handles.button


        //ScaleSlider
        //ScaleValueHandle
        //Slider



        //preselectionColor
        //selectedColor

        //elementColor
        //elementPreselectionColor
        //elementSelectionColor


        //DrawAAConvexPolygon
        //DrawAAPolyLine
        //DrawBezier
        //MakeBezierPoints  
        //DrawCamera

        //DrawDottedLine
        //DrawDottedLines

        //DrawLine
        //DrawLines

        //DrawOutline
        //DrawPolyLine    
        //DrawSelectionFrame 
        //DrawSolidArc 
        //DrawSolidDisc

        //DrawSolidRectangleWithOutline  
        //DrawTexture3DSDF 
        //DrawTexture3DSlice
        //DrawTexture3DVolume 
        //DrawWireArc 
        //DrawWireCube 
        //DrawWireDisc 

        //

        //Handles.DrawLine(new Vector3(

        //Handles.DoPositionHandle

        //Vector3 Handles.Slider(Vector3 position, Vector3 direction, float size, Handles.CapFunction capFunc, float snap);
        //Vector2 Handles.Slider2D(Vector3 position, Vector3 forward, Vector3 up, Vector3 right, float size, Handles.CapFunction capFunc, float snapX, float snapY);
        //Quaternion Handles.Disc(Quaternion rotation, Vector3 position, Vector3 axis, float size, bool cutoffPlane, float snap);

        //float Handles.ScaleSlider(float scale, Vector3 position, Vector3 direction, Quaternion rotation, float size, float snap);
        //float Handles.ScaleValueHandle(float value, Vector3 position, Quaternion rotation, float size, Handles.CapFunction capFunc, float snap);

        //float Handles.RadiusHandle(Quaternion rotation, Vector3 position, float radius, bool handlesOnly = false);
        //Vector3[] Handles.ScaleHandle(...); // (already listed above, same family)
    }

    Handles.CapFunction GetHandlesCapFunction(CustomEditorTemplate.HandlesCapType capType)
    {
        return capType switch
        {
            CustomEditorTemplate.HandlesCapType.Arrow => Handles.ArrowHandleCap,
            CustomEditorTemplate.HandlesCapType.Circle => Handles.CircleHandleCap,
            CustomEditorTemplate.HandlesCapType.Cone => Handles.ConeHandleCap,
            CustomEditorTemplate.HandlesCapType.Cube => Handles.CubeHandleCap,
            CustomEditorTemplate.HandlesCapType.Cylinder => Handles.CylinderHandleCap,
            CustomEditorTemplate.HandlesCapType.Dot => Handles.DotHandleCap,
            CustomEditorTemplate.HandlesCapType.Rectangle => Handles.RectangleHandleCap,
            CustomEditorTemplate.HandlesCapType.Sphere => Handles.SphereHandleCap,
            _ => Handles.SphereHandleCap,
        };
    }
}
