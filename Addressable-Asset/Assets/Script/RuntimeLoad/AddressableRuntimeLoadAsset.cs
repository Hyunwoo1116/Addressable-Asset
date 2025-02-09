using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableRuntimeLoadAsset : MonoBehaviour
{

    async void Start()
    {
        
    }

    public async void DownloadAudioResource()
    {
        AsyncOperationHandle<long> handle = Addressables.GetDownloadSizeAsync("Assets/AssetStore/AS_Casual_Island_Game_Sounds_FREE/Jingles/Jingle_2.wav");

        while (!handle.IsDone)
        {
            await Task.Yield();
        }
        if (handle.Result > 0 )
        {
            Debug.Log($"Download 받을것이 있음.{handle.Result}Bytes");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
