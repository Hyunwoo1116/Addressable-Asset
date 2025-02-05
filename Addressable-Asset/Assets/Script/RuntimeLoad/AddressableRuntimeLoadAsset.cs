using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableRuntimeLoadAsset : MonoBehaviour
{
    public List<string> Resources = new List<string>()
    {
        "Audio", "Effects", "FBX", "Textures"
    };

    void Start()
    {
        AsyncOperationHandle<long> handle = Addressables.GetDownloadSizeAsync("LargeMap");

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
