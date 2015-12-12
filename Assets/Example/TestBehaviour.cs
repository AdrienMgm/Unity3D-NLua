using UnityEngine;
using System.Collections;

using NLua;

public class TestBehaviour : NLuaBehaviour
{
    protected override string LuaScriptFileName
    {
        get { return "TestBehaviour.lua"; }
    }

	public override void Awake()
    {
        base.Awake();

		env["this"] = this; // Give the script access to the gameobject.
		env["transform"] = transform;
	}

	void Start()
    {
        if (initialized == true)
        {
            Call("Start");
        }
	}

	void Update()
    {
        if (initialized == true)
        {
            Call("Update");
        }
	}

	void OnGUI()
    {
        if (initialized == true)
        {
            Call("OnGUI");
        }
	}
}
