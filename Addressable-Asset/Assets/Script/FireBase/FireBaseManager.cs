using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Storage;
using System.Collections;
using System.Data;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class FireBaseManager : MonoBehaviour
{

    public void Start()
    {

        FirebaseStorage storage = FirebaseStorage.DefaultInstance;

        /*Addressables.InitializeAsync();
        Addressables.InstantiateAsync("Cube", transform);
*/
    }
}
