using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using System.Collections;
using System.Data;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class FireBaseManager : MonoBehaviour
{

    public void Start()
    {
        Addressables.InitializeAsync();

        Addressables.InstantiateAsync("Cube", transform);

    }
}
