using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cargold;


[System.Serializable, InlineProperty, HideLabel]
public partial class DefineKey : Cargold.FrameWork.TableDataKeyDropdown
{
    [LabelWidth(DropdownDefine.LabelWidth)]
    [SerializeField, LabelText("Define Key"), ValueDropdown("CallEdit_KeyDropdown_Func")] private string key = null;

    public string GetKey => this.key;

    [ShowInInspector, ReadOnly, HideLabel, FoldoutGroup("Preview")]
    public DefineData GetData
    {
        get
        {
            DataBase_Manager.Instance.GetDefine.TryGetData_Func(this.key, out DefineData _defineData);

            return _defineData;
        }
    }

    public DefineKey(string _keyStr = null)
    {
        this.key = _keyStr;
    }

#if UNITY_EDITOR
    private IEnumerable<string> CallEdit_KeyDropdown_Func()
    {
        return DataBase_Manager.Instance.GetDefine.GetKeyArr;
    }
    public bool CallEdit_IsUnitTestDone_Func()
    {
        if(this.key.IsNullOrWhiteSpace_Func() == false)
            return DataBase_Manager.Instance.GetDefine.IsContain_Func(this.key);

        return false;
    }
#endif

    public static implicit operator string(DefineKey _key)
    {
        return _key.key;
    }
}
