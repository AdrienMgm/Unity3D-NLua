using UnityEngine;
using System.Collections;

using NLua;

public abstract class NLuaBehaviour : MonoBehaviour
{
    protected bool initialized;
    protected Lua env;

    protected abstract string LuaScriptFileName
    {
        get;
    }

    public virtual void Awake()
    {
        env = new Lua();
        env.LoadCLRPackage();

        StartCoroutine(LoadFile(LuaScriptFileName));
    }

    public System.Object[] Call(string function, params System.Object[] args)
    {
        System.Object[] result = new System.Object[0];
        if (env == null) return result;
        LuaFunction lf = env.GetFunction(function);
        if (lf == null) return result;
        try
        {
            // Note: calling a function that does not
            // exist does not throw an exception.
            if (args != null)
            {
                result = lf.Call(args);
            }
            else
            {
                result = lf.Call();
            }
        }
        catch (NLua.Exceptions.LuaException e)
        {
            Debug.LogError(FormatException(e), gameObject);
        }
        return result;
    }

    public System.Object[] Call(string function)
    {
        return Call(function, null);
    }

    public static string FormatException(NLua.Exceptions.LuaException e)
    {
        string source = (string.IsNullOrEmpty(e.Source)) ? "<no source>" : e.Source.Substring(0, e.Source.Length - 2);
        return string.Format("{0}\nLua (at {2})", e.Message, string.Empty, source);
    }

    public IEnumerator LoadFile(string luaFileName)
    {
        string filePath = Application.streamingAssetsPath + "/" + luaFileName;

        string luaScript = null;
        if (filePath.Contains("://"))
        {
            WWW www = new WWW(filePath);
            yield return www;

            luaScript = www.text;
        }
        else
        {
            luaScript = System.IO.File.ReadAllText(filePath);
        }

        try
        {
            env.DoString(luaScript);
        }
        catch (NLua.Exceptions.LuaException e)
        {
            Debug.LogError(FormatException(e), gameObject);
        }

        initialized = true;
    }
}
