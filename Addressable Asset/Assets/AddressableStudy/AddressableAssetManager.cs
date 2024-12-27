using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableAssetManager : MonoBehaviour
{
    public string key;
    public string key2;
    AsyncOperationHandle<GameObject> opHandle;
    AsyncOperationHandle<GameObject> opHandle2;

    public void Start()
    {
        opHandle = Addressables.LoadAssetAsync<GameObject>(key);
        opHandle.Completed += LoadCompleted;
        opHandle2 = Addressables.LoadAssetAsync<GameObject>(key2);
        opHandle2.Completed += LoadCompleted;

    }
    private void LoadCompleted(AsyncOperationHandle<GameObject> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            Instantiate(obj.Result, transform);
        }

        Addressables.Release(obj);
    }
    void OnDestroy()
    {
        if (opHandle.IsValid())
            Addressables.Release(opHandle);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
