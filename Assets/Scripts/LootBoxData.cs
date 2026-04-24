using System;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "LootBoxData", menuName = "Data/LootBoxData")]
public class LootBoxData : ScriptableObject
{
    public ItemChance[] lootBoxData;
    public Sprite icon;
}
[Serializable]
public class ItemChance
{
    public AppData item;
    public float weight;

}
[CustomPropertyDrawer(typeof(ItemChance))]
public class ItemChanceDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        float itemWidth = position.width * 0.68f;
        float weightWidth = position.width - itemWidth - 4f;

        Rect itemRect = new Rect(position.x, position.y, itemWidth, position.height);
        Rect weightRect = new Rect(position.x + itemWidth + 4f, position.y, weightWidth, position.height);

        SerializedProperty itemProp = property.FindPropertyRelative("item");
        SerializedProperty weightProp = property.FindPropertyRelative("weight");

        EditorGUI.PropertyField(itemRect, itemProp, GUIContent.none);
        EditorGUI.PropertyField(weightRect, weightProp, GUIContent.none);

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight;
    }
}
