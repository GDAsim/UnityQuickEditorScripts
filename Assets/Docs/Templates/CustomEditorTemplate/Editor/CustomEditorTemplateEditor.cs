using System;
using System.Drawing;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.AdaptivePerformance.Provider;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static GluonGui.WorkspaceWindow.Views.WorkspaceExplorer.Configuration.ConfigurationTreeNodeCheck;
using static UnityEditor.PlayerSettings;
using static UnityEngine.Audio.ProcessorInstance;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

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
                var sizeProp = serializedObject.FindProperty("freeMoveSize");
                var snapProp = serializedObject.FindProperty("freeMoveSnap");
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

                var size = serializedObject.FindProperty("discSize").floatValue;
                var axis = Vector3.up;
                var snap = 25f; // in degree
                var newDiscRotation = Handles.Disc(handleGO.transform.rotation, handleGO.transform.position, axis, size, false, snap);

                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(handleGO.transform, "Rotate Object");
                    handleGO.transform.rotation = newDiscRotation;
                }
            }
        }

        // Draw Button Handle
        if (handlesExampleProp.arraySize >= 8)
        {
            var handleGOProp = handlesExampleProp.GetArrayElementAtIndex(8);
            var handleGO = handleGOProp.objectReferenceValue as GameObject;
            if (handleGO != null)
            {
                EditorGUI.BeginChangeCheck();

                var size = serializedObject.FindProperty("discSize").floatValue;
                var isClicked = Handles.Button(handleGO.transform.position, handleGO.transform.rotation, size, size, Handles.SphereHandleCap);

                if (isClicked)
                {
                    Handles.Label(handleGO.transform.position, "Clicking! Button Handle");
                }
                else
                {
                    Handles.Label(handleGO.transform.position, "Click Me! Button Handle");
                }
            }
        }

        // Draw ScaleSlider Handle
        if (handlesExampleProp.arraySize >= 9)
        {
            var handleGOProp = handlesExampleProp.GetArrayElementAtIndex(9);
            var handleGO = handleGOProp.objectReferenceValue as GameObject;
            if (handleGO != null)
            {
                EditorGUI.BeginChangeCheck();

                var scale = handleGO.transform.localScale.x;
                var size = HandleUtility.GetHandleSize(handleGO.transform.position);
                var snap = 25f; // in degree
                var newScaleSliderValue = Handles.ScaleSlider(scale, handleGO.transform.position, handleGO.transform.right, handleGO.transform.rotation, size, snap);

                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(handleGO.transform, "Scale Object");
                    handleGO.transform.localScale = new Vector3(newScaleSliderValue, newScaleSliderValue, newScaleSliderValue);
                }
            }
        }

        // Draw Slider Handle
        if (handlesExampleProp.arraySize >= 10)
        {
            var handleGOProp = handlesExampleProp.GetArrayElementAtIndex(10);
            var handleGO = handleGOProp.objectReferenceValue as GameObject;
            if (handleGO != null)
            {
                EditorGUI.BeginChangeCheck();

                var newSliderValue = Handles.Slider(handleGO.transform.position, handleGO.transform.right);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(handleGO.transform, "Move Object");
                    handleGO.transform.position = newSliderValue;
                }
            }
        }

        // Draw ScaleValueHandle Handle
        if (handlesExampleProp.arraySize >= 11)
        {
            var handleGOProp = handlesExampleProp.GetArrayElementAtIndex(11);
            var handleGO = handleGOProp.objectReferenceValue as GameObject;
            if (handleGO != null)
            {
                EditorGUI.BeginChangeCheck();

                var scale = handleGO.transform.localScale.x;
                var size = scale * 2;
                var snap = 0;
                var newScaleValueValue = Handles.ScaleValueHandle(scale, handleGO.transform.position, handleGO.transform.rotation, size, Handles.CircleHandleCap, snap);

                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(handleGO.transform, "Scale Object");
                    handleGO.transform.localScale = new Vector3(newScaleValueValue, newScaleValueValue, newScaleValueValue);
                }
            }
        }

        // not interactable
        //Handles.DrawSolidDisc(handleGO.transform.position, axis, size);
        //Handles.DrawWireDisc(handleGO.transform.position, axis, size);
        //Handles.DrawWireArc 
        //Handles.DrawWireCube 
        //Handles.DrawSelectionFrame 
        //Handles.DrawSolidArc

        //DrawLine
        //DrawLines
        //DrawDottedLine
        //DrawDottedLines
        //Handles.DrawPolyLine 
        //Handles.DrawAAPolyLine
        //Handles.DrawBezier //MakeBezierPoints  

        //Handles.DrawAAConvexPolygon
        //Handles.DrawOutline

        //Handles.DrawSolidRectangleWithOutline

        //DrawCamera
        //DrawTexture3DSDF 
        //DrawTexture3DSlice
        //DrawTexture3DVolume 



        //preselectionColor
        //selectedColor

        //elementColor
        //elementPreselectionColor
        //elementSelectionColor
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
