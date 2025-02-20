using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Utills.Progress;
using static UnityEngine.AddressableAssets.Addressables;
using Progress = Utills.Progress.Progress;

public class AddressableRuntimeLoadAsset : MonoBehaviour
{

    public Alert Alert;

    DownloadAddressable downloadAddressable;

    public List<string> Keys;

    public float TotalDownloadDependency;

    public Progress progress;

    private void Awake()
    {
        Keys = new List<string>() { "Sounds", "Prefabs", "FBXs" };
    }

    public async void DownloadAudioResource()
    {
        Debug.Log("DownloadButtonClick");
        Addressables.InitializeAsync();
        AsyncOperationHandle<long> handle = Addressables.GetDownloadSizeAsync(Keys);

        while (!handle.IsDone)
        {
            await Task.Yield();
        }
        if (handle.Result > 0)
        {
            Debug.Log($"Download 받을것이 있음.{handle.Result}Bytes");
            string messgaeBody = $"다운로드 받으시겠습니까?\n{handle.Result}Bytes";
            TotalDownloadDependency = handle.Result;
            //downloadAddressable = DownloadDependencies; MultipleDownload
            downloadAddressable = DownloadDependencySingle;
            await Alert.ShowDownloadAlert(messgaeBody, downloadAddressable);
        }
        else
        {
            Debug.Log($"Download 받을 것이 없음.{handle.Result}Bytes");
        }
    }

    public void ClearAddressableDependency()
    {
        Addressables.ClearDependencyCacheAsync(Keys);
    }
    public async Task<bool> DownloadDependencySingle()
    {
        foreach (string key in Keys)
        {
            progress.Initalize();
            progress.SetProgressName(key);
            progress.Show();
            try
            {
                AsyncOperationHandle handle = Addressables.DownloadDependenciesAsync(key, false);

                while (!handle.IsDone)
                {
                    DownloadStatus status = handle.GetDownloadStatus();
                    progress.SetProgress(status.Percent);
                    Debug.Log($"Key{key}\n{key}TotalBytes{status.TotalBytes}\nCurrentDownloadedBytes{status.DownloadedBytes}\nNeedDownloadBytes{status.TotalBytes - status.DownloadedBytes}");
                    await Task.Yield();
                }
                handle.Release();
            }
            catch (Exception error)
            {
                Debug.LogError(error.Message);
                progress.Hide();
                return false;
            }
        }
        progress.Hide();
        return true;
    }
    public async Task<bool> DownloadDependencies() // Mutliple
    {
        AsyncOperationHandle handle = Addressables.DownloadDependenciesAsync(Keys, Addressables.MergeMode.Union, false);

        while (!handle.IsDone)
        {
            DownloadStatus downloadStatus = handle.GetDownloadStatus();

            Debug.Log($"Totalbytes{downloadStatus.TotalBytes}");
            Debug.Log($"DownloadedBytes{downloadStatus.DownloadedBytes}");
            Debug.Log($"Need To be DownloadBytes{downloadStatus.TotalBytes - downloadStatus.DownloadedBytes} ");
            Debug.Log($"DownloadPercentage{downloadStatus.Percent}");
            Debug.Log($"handle.Status{handle.Status}");
            Debug.Log($"handle.PercenetComplte{handle.PercentComplete}");
            await Task.Yield();
        }
        handle.Release();
        Debug.Log("DownloadCompleted");
        return true;
    }

    public void CreateAddressableRuntime(string key)
    {
        // Key : Assets/AssetStore/FBXs/Composition_50.fbx
        AsyncOperationHandle handle = Addressables.InstantiateAsync(key, transform);
    }
    public void CreateCube()
    {
        AsyncOperationHandle handle = Addressables.InstantiateAsync("Cube", transform);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
