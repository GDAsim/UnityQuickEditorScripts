using System.Drawing.Drawing2D;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;
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
            var handleGO = handleGOProp?.objectReferenceValue as GameObject;
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
            var handleGO = handleGOProp?.objectReferenceValue as GameObject;
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
            var handleGO = handleGOProp?.objectReferenceValue as GameObject;
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
            var handleGO = handleGOProp?.objectReferenceValue as GameObject;
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
            var handleGO = handleGOProp?.objectReferenceValue as GameObject;
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
            var handleGO = handleGOProp?.objectReferenceValue as GameObject;
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
            var handleGO = handleGOProp?.objectReferenceValue as GameObject;
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
            var handleGO = handleGOProp?.objectReferenceValue as GameObject;
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
            var handleGO = handleGOProp?.objectReferenceValue as GameObject;
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
            var handleGO = handleGOProp?.objectReferenceValue as GameObject;
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
            var handleGO = handleGOProp?.objectReferenceValue as GameObject;
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
            var handleGO = handleGOProp?.objectReferenceValue as GameObject;
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

        // Draw Non-Interactable

        // Draw Circles
        if (handlesExampleProp.arraySize >= 12)
        {
            var handleGOProp = handlesExampleProp.GetArrayElementAtIndex(12);
            var handleGO = handleGOProp?.objectReferenceValue as GameObject;
            if (handleGO != null)
            {
                var size = HandleUtility.GetHandleSize(handleGO.transform.position) / 2;
                var thickness = 1;

                var pos = handleGO.transform.position;
                var offsetPos = new Vector3(0, 0, 0);

                Handles.DrawWireDisc(pos + offsetPos, handleGO.transform.up, size, thickness);

                offsetPos.y += size;
                Handles.DrawWireArc(pos + offsetPos, Vector3.up, handleGO.transform.forward, 270f, size, thickness);

                offsetPos.y += size;
                Handles.DrawSolidDisc(pos + offsetPos, Vector3.up, size);

                offsetPos.y += size;
                Handles.DrawSolidArc(pos + offsetPos, Vector3.up, handleGO.transform.forward, 270f, size);
            }
        }
        if (handlesExampleProp.arraySize >= 15)
        {
            var handleGOProp = handlesExampleProp.GetArrayElementAtIndex(15);
            var handleGO = handleGOProp?.objectReferenceValue as GameObject;
            if (handleGO != null)
            {
                var size = HandleUtility.GetHandleSize(handleGO.transform.position);
                Handles.DrawWireCube(handleGO.transform.position, size * Vector3.one);
            }
        }
        if (handlesExampleProp.arraySize >= 16)
        {
            var handleGOProp = handlesExampleProp.GetArrayElementAtIndex(16);
            var handleGO = handleGOProp?.objectReferenceValue as GameObject;
            if (handleGO != null)
            {
                var size = HandleUtility.GetHandleSize(handleGO.transform.position);
                int controlID = GUIUtility.GetControlID(FocusType.Passive);
                Handles.DrawSelectionFrame(controlID, handleGO.transform.position, handleGO.transform.rotation, size, EventType.Repaint);
            }
        }

        // Draw lines
        if (handlesExampleProp.arraySize >= 18)
        {
            var handleGOProp = handlesExampleProp.GetArrayElementAtIndex(18);
            var handleGO = handleGOProp.objectReferenceValue as GameObject;
            if (handleGO != null)
            {
                var pos = handleGO.transform.position + new Vector3(-0.5f, 0, -0.5f);
                var size = HandleUtility.GetHandleSize(handleGO.transform.position) / 2;
                var thickness = 1;
                var seperationSize = 2 / size;

                var p1 = pos;
                var p2 = pos + new Vector3(0, 0, 1);
                Handles.DrawLine(p1, p2, thickness);

                p1.y += size;
                p2.y += size;
                Handles.DrawDottedLine(p1, p2, seperationSize);

                p1 = pos + new Vector3(0, size * 2, 0);
                p2 = pos + new Vector3(0, size * 2, 1);
                var p3 = pos + new Vector3(1, size * 2, 1);
                var p4 = pos + new Vector3(1, size * 2, 0);
                var lines = new[]
                {
                    p1, p2,
                    p2, p3,
                    p3, p4,
                };
                Handles.DrawLines(lines);

                p1 = pos + new Vector3(0, size * 3, 0);
                p2 = pos + new Vector3(0, size * 3, 1);
                p3 = pos + new Vector3(1, size * 3, 1);
                p4 = pos + new Vector3(1, size * 3, 0);
                lines = new[]
                {
                    p1, p2,
                    p2, p3,
                    p3, p4,
                };
                Handles.DrawDottedLines(lines, seperationSize);

                p1 = pos + new Vector3(0, size * 4, 0);
                p2 = pos + new Vector3(0, size * 4, 1);
                p3 = pos + new Vector3(1, size * 4, 1);
                p4 = pos + new Vector3(1, size * 4, 0);
                lines = new[]
                {
                    p1, p2, p3, p4,
                };
                Handles.DrawPolyLine(lines);

                p1 = pos + new Vector3(0, size * 5, 0);
                p2 = pos + new Vector3(0, size * 5, 1);
                p3 = pos + new Vector3(1, size * 5, 1);
                p4 = pos + new Vector3(1, size * 5, 0);
                lines = new[]
                {
                    p1, p2, p3, p4,
                };
                Handles.DrawAAPolyLine(thickness, lines);
            }
        }
        // Draw Bezier
        if (handlesExampleProp.arraySize >= 19)
        {
            var handleGOProp = handlesExampleProp.GetArrayElementAtIndex(19);
            var handleGO = handleGOProp.objectReferenceValue as GameObject;
            if (handleGO != null)
            {
                var pos = handleGO.transform.position + new Vector3(-0.5f, 0, -0.5f);
                var size = HandleUtility.GetHandleSize(handleGO.transform.position);
                var thickness = 2 / size;

                var p1 = pos;
                var p2 = pos + new Vector3(0.2f, 1, 0);
                var p3 = pos + new Vector3(0.8f, -1, 0);
                var p4 = pos + new Vector3(1, 0, 0);
                var color = Color.red;
                Handles.DrawBezier(
                   p1, p4, p2, p3,
                   color, null, thickness);
            }
        }
        // Draw AAConvexPolygon
        if (handlesExampleProp.arraySize >= 20)
        {
            var handleGOProp = handlesExampleProp.GetArrayElementAtIndex(20);
            var handleGO = handleGOProp.objectReferenceValue as GameObject;
            if (handleGO != null)
            {
                var pos = handleGO.transform.position;
                var size = HandleUtility.GetHandleSize(handleGO.transform.position);

                Handles.DrawAAConvexPolygon(
                    pos + Vector3.forward * size,
                    pos + Vector3.right * size,
                    pos + Vector3.back * size,
                    pos + Vector3.left * size);
            }
        }

        // Draw outlines
        if (handlesExampleProp.arraySize >= 21)
        {
            var handleGOProp = handlesExampleProp.GetArrayElementAtIndex(21);
            var handleGO = handleGOProp.objectReferenceValue as GameObject;
            if (handleGO != null)
            {
                var color = Color.red;
                var opacity = 0.5f;
                Handles.DrawOutline(new[] { handleGO }, color, opacity);

                var pos = handleGO.transform.position;
                var size = HandleUtility.GetHandleSize(pos);
                var rectVerts = new[]
                {
                    pos + new Vector3(-0.5f, 0, -0.5f) * size,
                    pos + new Vector3(0.5f, 0, -0.5f) * size,
                    pos + new Vector3(0.5f, 0, 0.5f) * size,
                    pos + new Vector3(-0.5f, 0, 0.5f) * size
                };
                Handles.DrawSolidRectangleWithOutline(rectVerts, new Color(0, 1, 0, 0.2f), Color.green);
            }
        }

        // DrawTexture3DSDF
        if (handlesExampleProp.arraySize >= 23)
        {
            var handleGOProp = handlesExampleProp.GetArrayElementAtIndex(23);
            var handleGO = handleGOProp.objectReferenceValue as GameObject;
            if (handleGO != null)
            {
                var texture3DProp = serializedObject.FindProperty("texture3D");
                var texture = texture3DProp?.objectReferenceValue as Texture3D;
                if (texture == null) return;

                var size = 1;
                var step = 0.5f;

                var oldMatrix = Handles.matrix;

                var matrix = handleGO.transform.localToWorldMatrix;
                var pos = new Vector4(handleGO.transform.position.x, handleGO.transform.position.y, handleGO.transform.position.z, 1);
                Handles.matrix = matrix;
                Handles.DrawTexture3DSDF(texture, step);

                matrix.SetColumn(3, pos + new Vector4(0, size, 0, 0));
                Handles.matrix = matrix;
                Handles.DrawTexture3DSlice(texture, Vector3.zero);

                matrix.SetColumn(3, pos + new Vector4(0, size * 2, 0, 0));
                Handles.matrix = matrix;
                Handles.DrawTexture3DVolume(texture, 0.5f);

                Handles.matrix = oldMatrix;
            }
        }

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
