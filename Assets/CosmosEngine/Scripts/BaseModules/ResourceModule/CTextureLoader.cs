﻿//------------------------------------------------------------------------------
//
//      CosmosEngine - The Lightweight Unity3D Game Develop Framework
// 
//                     Version 0.8 (20140904)
//                     Copyright © 2011-2014
//                   MrKelly <23110388@qq.com>
//              https://github.com/mr-kelly/CosmosEngine
//
//------------------------------------------------------------------------------
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[CDependencyClass(typeof(CAssetFileLoader))]
public class CTextureLoader : CBaseResourceLoader
{
    public Texture Asset { get { return ResultObject as Texture; } }

    public delegate void CTextureLoaderDelegate(bool isOk, Texture tex);

    private CAssetFileLoader AssetFileBridge;
    public override float Progress
    {
        get
        {
            return AssetFileBridge.Progress;
        }
    }
    public string Path { get; private set; }

    public static CTextureLoader Load(string path, CTextureLoaderDelegate callback = null)
    {
        CLoaderDelgate newCallback = null;
        if (callback != null)
        {
            newCallback = (isOk, obj) => callback(isOk, obj as Texture);
        }
        return AutoNew<CTextureLoader>(path, newCallback);
    }
    protected override void Init(string url)
    {
        base.Init(url);

        Path = url;
        AssetFileBridge = CAssetFileLoader.Load(Path, OnAssetLoaded);
    }

    void OnAssetLoaded(bool isOk, UnityEngine.Object obj)
    {
        OnFinish(obj);
    }

    protected override void DoDispose()
    {
        base.DoDispose();
        AssetFileBridge.Release(); // all, Texture is singleton!
    }
}