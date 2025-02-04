using Codice.Client.Common.GameUI;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterSelectEditorWindow : EditorWindow
{
    [MenuItem("HotSixTool/SelectedCharacter %g")]
    static void Open()
    {
        var win = GetWindow<CharacterSelectEditorWindow>();
        win.titleContent.text = "SelectedCharacter Tool";
    }

    private void OnGUI()
    {
        ValidateDraw();
    }

    private void ValidateDraw()
    {
        foreach (var t in Selection.GetTransforms(SelectionMode.TopLevel))
        {
            
        }
    }

    private void InvalidDraw()
    {

    }
}
