using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DialogueNode))]
public class DialogueNodeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SerializedProperty dialogueText = serializedObject.FindProperty("dialogueText");
        SerializedProperty needResponse = serializedObject.FindProperty("NeedResponse");
        SerializedProperty responses = serializedObject.FindProperty("responses");

        // Tampilkan List DialogueText
        EditorGUILayout.PropertyField(dialogueText, true);

        // Tampilkan Checkbox NeedResponse
        EditorGUILayout.PropertyField(needResponse);

        // Jika NeedResponse aktif, tampilkan list Responses
        if (needResponse.boolValue)
        {
            EditorGUILayout.PropertyField(responses, true);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
