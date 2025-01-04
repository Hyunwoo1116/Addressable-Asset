using Firebase.Extensions;
using Firebase.Storage;
using RobinBird.FirebaseTools.Storage.Addressables;
using System.Collections;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;



public class FireBaseManager : MonoBehaviour
{

    private void Start()
    {
        Addressables.InitializeAsync();
        InstanceObject();
        //StartCoroutine(CheckDownloadAddressable());
    }
    private void InstanceObject()
    {
        Addressables.InstantiateAsync("Cylinder", transform);
        Addressables.InstantiateAsync("Sphere", transform);
        Addressables.InstantiateAsync("Plane", transform);
        Addressables.InstantiateAsync("Cube", transform);
    }
    private IEnumerator CheckDownloadAddressable()
    {

        AsyncOperationHandle<long> getDownloadSize = Addressables.GetDownloadSizeAsync("Cube");
        yield return getDownloadSize;

        if (getDownloadSize.Result > 0)
        {
            Debug.Log($"Download항목 있음 {getDownloadSize.Result}");
            Addressables.InstantiateAsync("Cube", transform);
        }
        else
        {
            Debug.Log("Download 없음.");
            Addressables.InstantiateAsync("Cube", transform);
        }
    }
    #region Firebase Provider
    /* public void Start()
     {
         InitializeAddressable();
         FirebaseAddressablesManager.IsFirebaseSetupFinished = true;

         StartCoroutine(LoadGameObjectAndMaterial());

         //StartCoroutine(CheckDownloadAddressable());
     }

     IEnumerator LoadGameObjectAndMaterial()
     {
         //Load a GameObject
         AsyncOperationHandle<GameObject> goHandle = Addressables.LoadAssetAsync<GameObject>("Cube");
         yield return goHandle;

         if (goHandle.Status == AsyncOperationStatus.Succeeded)
         {
             GameObject obj = goHandle.Result;
             Instantiate(obj);
             //etc...
         }



         //Use this only when the objects are no longer needed
         // Addressables.Release(goHandle);
         // Addressables.Release(matHandle);
     }

     private void InitializeAddressable()
     {
         Addressables.ResourceManager.ResourceProviders.Add(new FirebaseStorageAssetBundleProvider());
         Addressables.ResourceManager.ResourceProviders.Add(new FirebaseStorageJsonAssetProvider());
         Addressables.ResourceManager.ResourceProviders.Add(new FirebaseStorageHashProvider());


         Addressables.InternalIdTransformFunc += FirebaseAddressablesCache.IdTransformFunc;
     }

     
 */
    #endregion
}
