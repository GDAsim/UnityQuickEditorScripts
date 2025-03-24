
//Unity Default Inspector For Vector4 fields, uses an internal function "MultiFieldPrefixLabel" to draw the intentation padding etc
//PropertyDrawer is unable to access this. as a result. any EditorGUI.PropertyField that draws a foldout in front will have the margin draw differently than default
