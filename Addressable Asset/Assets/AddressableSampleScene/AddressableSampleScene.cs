using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class AddressableSampleScene : MonoBehaviour
{
    public Button[] buttons;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach(Button button in buttons)
        {
          button.onClick.AddListener(() => CreateAddressableAsset(button));
        }
        Addressables.InitializeAsync();
    }

    private void CreateAddressableAsset(Button button)
    {
        Addressables.InstantiateAsync(button.gameObject.name, transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
