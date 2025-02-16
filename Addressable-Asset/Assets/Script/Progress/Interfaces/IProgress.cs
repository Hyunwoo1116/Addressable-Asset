using UnityEngine;

public interface IProgress 
{
    void Show();
    string SetProgressName(string ProgressName);
    float SetProgress(float percentage);
    void Hide();
}
