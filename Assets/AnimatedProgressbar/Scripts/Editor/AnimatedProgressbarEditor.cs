using UnityEditor;

[CustomEditor(typeof(AnimatedProgressbar))]
[CanEditMultipleObjects]
public class AnimatedProgressbarEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var barUI = serializedObject.FindProperty("m_barUI");
        var fillSpeed = serializedObject.FindProperty("m_fillSpeed");
        var fillAmount = serializedObject.FindProperty("m_fillAmount");
        var currentAmount = serializedObject.FindProperty("m_currentAmount");
        var initUvRectWidth = serializedObject.FindProperty("m_initUvRectWidth");
        var speed = serializedObject.FindProperty("m_speed");
        var autoPlay = serializedObject.FindProperty("m_autoPlay");

        EditorGUILayout.PropertyField(barUI);
        EditorGUILayout.PropertyField(fillSpeed);
        EditorGUILayout.PropertyField(fillAmount);
        EditorGUILayout.PropertyField(autoPlay);
        EditorGUILayout.PropertyField(initUvRectWidth);
        EditorGUILayout.PropertyField(speed);

        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.PropertyField(currentAmount);
        EditorGUI.EndDisabledGroup();
        serializedObject.ApplyModifiedProperties();
    }
}
