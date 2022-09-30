using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameAreaBuilder))]
public class GameAreaBuilderInspector : Editor
{
    GameAreaBuilder _target;

    private void OnEnable()
    {
        _target = (GameAreaBuilder)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Build"))
            _target.Build();
    }
}
