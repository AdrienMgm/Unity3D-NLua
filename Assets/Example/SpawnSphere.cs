using UnityEngine;
using System.Collections;

using NLua;

public class SpawnSphere : NLuaBehaviour
{
    protected override string LuaScriptFileName
    {
        get { return "SpawnSphere.lua"; }
    }

	public GameObject sphere;

	public override void Awake()
    {
        base.Awake();

		env["this"] = this;
		env["transform"] = transform;
		env["sphere"] = sphere; // Give the script access to the prefab.
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
