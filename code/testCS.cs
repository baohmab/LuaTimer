using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XLua;
using System;

[System.Serializable]
public class Injection
{
    public string name;
    public GameObject value;
}

[LuaCallCSharp]
public class LuaBehaviour : MonoBehaviour {
    public TextAsset luaScript;
    public Injection[] injections;

    internal static LuaEnv luaEnv = new LuaEnv(); 

    private Action<double, double> luaUpdate;

    private LuaTable scriptEnv;
    void Awake()
    {
        luaEnv.DoString(System.Text.Encoding.UTF8.GetString(luaScript.bytes));
        luaEnv.Global.Get("update", out luaUpdate);
    }

	  // Update is called once per frame
	  void Update ()
    {
        if (luaUpdate != null)
        {
            luaUpdate(Time.unscaledTime, Time.time);
        }
        luaEnv.Tick();
	  }
}
