using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableRuntimeLoadAsset : MonoBehaviour
{

    public Alert Alert;

    DownloadAddressable downloadAddressable;

    public List<string> Keys = new List<string>() { "Sounds", "Prefabs" };

    
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
            Debug.Log($"Download �������� ����.{handle.Result}Bytes");
            string messgaeBody = $"�ٿ�ε� �����ðڽ��ϱ�?\n{handle.Result}Bytes";
            downloadAddressable = DownloadDependency;
            await Alert.ShowDownloadAlert(messgaeBody, downloadAddressable);
        }
        else
        {
            Debug.Log($"Download ���� ���� ����.{handle.Result}Bytes");
        }
    }

    public void ClearAddressableDependency()
    {
        Addressables.ClearDependencyCacheAsync(Keys);
    }

    public async Task<bool> DownloadDependency()
    {
        AsyncOperationHandle handle = Addressables.DownloadDependenciesAsync(Keys, Addressables.MergeMode.Union);

        while(!handle.IsDone)
        {
            await Task.Yield();
        }
        Debug.Log("DownloadCompleted");
        return true;
    }

    public void CreateCube()
    {
        Addressables.InitializeAsync();
        AsyncOperationHandle handle = Addressables.InstantiateAsync("Cube", transform);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
