Unity3D-NLua
=======

Everything you need to get started using Lua in **Unity3D v5**. Indie & Pro compatible. 

This template project implements [NLua](https://github.com/NLua/NLua) which
drives [KeraLua](https://github.com/NLua/KeraLua), a C VM wrapper. Both projects
are licensed under the MIT. This project is licensed under the Boost Software Licence.

**Unity 5 Update**

With the release of Unity 5, native dlls are now allowed under the Indie licnence.
Therefore the previous support for [KopiLua](https://github.com/NLua/KopiLua) has been **depreciated**.

## Scripting Symbols

NLua requires some scripting symbols to be defined:

`UNITY_3D` - Suppress warnings about CLSC Attributes.

`USE_KERALUA` - KeraLua is an interop wrapper to the [NLua fork](https://github.com/NLua/lua)
of the original C VM (The DLL should be placed in a `Plugins\` folder in your project). NLua
can also interface with KopiLua (USE_KOPILUA), a pure C# implementation of lua, however it is 
not supported by this project.

`LUA_CORE` & `CATCH_EXCEPTIONS` - Required by the VM.

Your **Scripting Define Symbols** list should end up looking like:

```
UNITY_3D; USE_KERALUA; LUA_CORE; CATCH_EXCEPTIONS
```

You may also notice other symbols used throughout NLua, none of these have
been tested for compatibility.

## FAQ

 **How do I Instantiate new objects?**

See the SpawnSphere example.

**How do I run C# coroutines?**

See [this comment](https://github.com/NLua/NLua/issues/110#issuecomment-59874806) for details, essentially though you either have to call Lua functions indirectly, or roll your own coroutine manager (not hugely difficult). Direct support for coroutines may be included in future releases. 

**How do I build for iOS?**

iOS is more difficult to build for because the code is compiled using AOT compilation and Unity strips all assemblies by default. To get around this, there are a few steps you must take:

- Make sure you use the NLua.dll inside of the iOS folder.
- Prevent stripping of your code. Unity strips your code by looking at all of the references inside of your assemblies. Because Lua scripts are not part of compilation process, anything not referenced inside of your C# code or plugins will be stripped.
To prevent this, make sure any Unity or .dll code is used inside your C# code or add any namespaces or assemblies to the link.xml file. See [this page] (http://docs.unity3d.com/Manual/iphone-iOS-Optimization.html) for more details.
- Make sure you have your compatibility mode set to .net 2.0 subset. There is a bug with running NLua on iOS currently that is fixed by having compatibility set to .net 2.0 subset. See [this page] (http://forum.unity3d.com/threads/unity-5-0-3f2-il2cpp-problem-attempting-to-call-method-system-reflection-monoproperty-getteradapt.332335/) for more details.

**How do I rebuild NLua.dll?**

There are two .csproj files inside of the /src/ folders: one for windows store applications (NLua_WSA.csproj) and another for all other platforms (NLua.csproj). Simple open them up with Visual Studio, select your configuration, and rebuild.
Alternatively, you can remove the NLua.dll files from the plugins folder and use the scripts from the src folder directly. iOS has problems running NLua this way, however.