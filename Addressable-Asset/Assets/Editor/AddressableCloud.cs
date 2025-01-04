using UnityEditor;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets.Build;
using UnityEngine;
using Firebase.Storage;
using System.IO;
using System.Threading.Tasks;

public class AddressableCloud
{
    [MenuItem("Addressables/Build and Upload to Cloud")]
    public static void BuildAndUpload()
    {
        // Addressables ���� ����
        AddressableAssetSettings.BuildPlayerContent();

        // ����� ���� ���
        string buildPath = Path.Combine(Application.persistentDataPath, "ServerData");
        string platformFolder = GetPlatformFolder();

        string fullPath = Path.Combine(buildPath, platformFolder);

        Debug.Log(fullPath);
        if (Directory.Exists(fullPath))
        {
            Debug.Log($"Build Completed. Uploading files from {fullPath} to Firebase...");
            UploadFolderToFirebase(fullPath, platformFolder).ConfigureAwait(false);
        }
        else
        {
            Debug.LogError("Build folder not found. Please ensure Addressables are built correctly.");
        }
    }

    public static string GetPlatformFolder()
    {
        // �÷����� ���� ���� ���� ����
        switch (EditorUserBuildSettings.activeBuildTarget)
        {
            case BuildTarget.Android: return "Android";
            case BuildTarget.iOS: return "iOS";
            case BuildTarget.StandaloneWindows64: return "StandaloneWindows64";
            case BuildTarget.StandaloneOSX: return "Mac";
            default: return "Unknown";
        }
    }

    private static async Task UploadFolderToFirebase(string localFolderPath, string remoteFolderPath)
    {
        FirebaseStorage storage = FirebaseStorage.DefaultInstance;

        // ���� ������ ��� ���� ��������
        string[] files = Directory.GetFiles(localFolderPath, "*.*", SearchOption.AllDirectories);

        foreach (string file in files)
        {
            string relativePath = file.Substring(localFolderPath.Length + 1).Replace("\\", "/");
            string firebasePath = Path.Combine(remoteFolderPath, relativePath).Replace("\\", "/");

            Debug.Log($"Uploading {file} to Firebase: {firebasePath}");

            try
            {
                var storageRef = storage.GetReference(firebasePath);
                await storageRef.PutFileAsync(file);
                Debug.Log($"Uploaded {file} to {firebasePath}");
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Failed to upload {file}: {ex}");
            }
        }
    }
}