using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using CharacterInfo = _2_Scripts.Game.ScriptableObject.Character.CharacterInfo;
using DG.DemiEditor;
using System;

public class CharacterCreateEditorWindow : EditorWindow
{
    #region CheckAndOpen
    private static readonly HashSet<string> mScene = new HashSet<string>() { "Main", "Test", "Challenge" };

    [MenuItem("HotSixTool/CreateCharacter %g")]
    static void Open()
    {
        var win = GetWindow<CharacterCreateEditorWindow>();
        win.titleContent.text = "Character Create Tool";
    }

    //[MenuItem("HotSixTool/CreateCharacter %g", true)]
    private bool ValidateCheck()
    {
        return mScene.Contains(EditorSceneManager.GetActiveScene().name);
    }
    #endregion

    private Dictionary<int, Texture2D> mCharacterTextures = new Dictionary<int, Texture2D>();
    private Vector2 scrollPos = Vector2.zero;

    private GUIStyle mTitle;
    private GUIStyle mCharacterName;
    private Action mDrawAction;

    private void OnGUI()
    {
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        mDrawAction?.Invoke();
        EditorGUILayout.EndScrollView();
    }

    private void PlayModeInit(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.EnteredPlayMode)
        {
            Init();
        }

        else if (state == PlayModeStateChange.EnteredEditMode)
        {
            CheckValidateConditionOfScnene(EditorSceneManager.GetActiveScene(), EditorSceneManager.GetActiveScene());
        }
    }

    private void Init()
    {
        EditorSceneManager.activeSceneChanged -= CheckValidateConditionOfScnene;
        EditorSceneManager.activeSceneChanged += CheckValidateConditionOfScnene;

        Texture2D tex = null;
        int num = 0;
        do
        {
            tex = AssetDatabase.LoadAssetAtPath<Texture2D>($"Assets/3_Resources/Art/Sprite/CharacterIcon/{num}.png");
            if (tex == null) break;
            mCharacterTextures.TryAdd(num, tex);
            ++num;
        } while (true);
    }

    private void Awake()
    {
        CheckValidateConditionOfScnene(EditorSceneManager.GetActiveScene(), EditorSceneManager.GetActiveScene());
        EditorSceneManager.activeSceneChanged -= CheckValidateConditionOfScnene;
        EditorSceneManager.activeSceneChanged += CheckValidateConditionOfScnene;

        EditorApplication.playModeStateChanged -= PlayModeInit;
        EditorApplication.playModeStateChanged += PlayModeInit;

        Init();

        mTitle = new GUIStyle();
        mTitle.fontSize = 64;
        mTitle.padding.top = 20;
        mTitle.padding.bottom = 20;
        mTitle.normal.textColor = Color.white;
        
        mCharacterName = new GUIStyle();
        mCharacterName.fixedHeight = 35;
        mCharacterName.fixedWidth = 100;
        mCharacterName.fontSize = 24;
        mCharacterName.padding.top = 15;
        mCharacterName.padding.bottom = 15;
        mCharacterName.alignment = TextAnchor.MiddleCenter;
        mCharacterName.normal.textColor = Color.white;
    }

    private void ValidateDraw()
    {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        GUILayout.Label("캐릭터 생성", mTitle);
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        for (int i = 0; i < GameManager.Instance.UserCharacterList.Count; ++i)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            for (int j = 1; j < 4; ++j)
            {
                GUILayout.BeginVertical();
                var data = GameManager.Instance.GetCharacterInfo(i + 1).CharacterEvolutions[j].GetData;

                if (GUILayout.Button(mCharacterTextures[i * 3 + j - 1], GUILayout.Width(100), GUILayout.Height(100)))
                {
                    MapManager.Instance.CreateUnit(data);
                }

                GUILayout.Label(data.GetCharacterName(), mCharacterName);
                GUILayout.EndVertical();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
    }

    private void InvalidDraw()
    {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        GUILayout.Label("캐릭터 생성 불가", mTitle);
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }

    private void OnDestroy()
    {
        EditorSceneManager.activeSceneChangedInEditMode -= CheckValidateConditionOfScnene;
        EditorApplication.playModeStateChanged -= PlayModeInit;
        mCharacterTextures.Clear();
        mCharacterTextures = null;
    }

    private void CheckValidateConditionOfScnene(Scene prev, Scene next)
    {
        if (ValidateCheck())
        {
            mDrawAction -= InvalidDraw;
            mDrawAction -= ValidateDraw;
            mDrawAction += ValidateDraw;
        }

        else
        {
            mDrawAction -= InvalidDraw;
            mDrawAction -= ValidateDraw;
            mDrawAction += InvalidDraw;
        }
    }
}
