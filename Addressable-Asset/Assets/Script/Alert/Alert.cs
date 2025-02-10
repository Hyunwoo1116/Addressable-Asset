using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public delegate Task<bool> DownloadAddressable();
public class Alert : MonoBehaviour
{
    public Text Message;

    private bool? isDownload = null;

    public async Task<bool> ShowDownloadAlert(string message, DownloadAddressable downloadAddressable)
    {
        isDownload = null;
        this.gameObject.SetActive(true);
        Message.text = message;
        LayoutRebuilder.ForceRebuildLayoutImmediate(this.GetComponent<RectTransform>());

        while( isDownload == null)
        {
            await Task.Yield();
        }

        if ( isDownload is true)
        {
            bool result = await downloadAddressable.Invoke();
            Debug.Log($"Result{result}");
        }
        else
        {
            Debug.Log("Download하지 않음");
        }
        this.gameObject.SetActive(false);
        return (bool) isDownload;
    }
    public void ClickCancel()
    {
        isDownload = false;
    }

    public void ClickConfirm()
    {
        isDownload = true;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
