using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(AppEntry))]
public class AppEntryDrawer : PropertyDrawer
{
    private const float ToggleWidth = 18f;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        Rect appRect = new Rect(position.x, position.y, position.width - ToggleWidth - 4f, position.height);
        Rect toggleRect = new Rect(position.xMax - ToggleWidth, position.y, ToggleWidth, position.height);

        SerializedProperty appProp = property.FindPropertyRelative("app");
        SerializedProperty visibleProp = property.FindPropertyRelative("visibleInUpgrade");

        EditorGUI.PropertyField(appRect, appProp, GUIContent.none);
        EditorGUI.PropertyField(toggleRect, visibleProp, GUIContent.none);

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight;
    }
}
