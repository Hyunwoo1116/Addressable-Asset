using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Utills.Progress
{
    public class Progress : MonoBehaviour, IProgress
    {
        public TextMeshProUGUI ProgressTitle;
        public TextMeshProUGUI ProgressPercentage;
        public Slider Progressbar;
        private float currentSliderValue = 0f;
        private float currentPercentage = 0f;
        public void Hide()
        {
            this.gameObject.SetActive(false);
            Progressbar.value = 0f;
            currentSliderValue = 0f;
            ProgressTitle.text = string.Empty;
            ProgressPercentage.text = string.Empty;
        }

        public string SetProgressName(string ProgressName) => ProgressTitle.text = ProgressName;
        public float SetProgress(float percentage) => currentPercentage = percentage;
        public void Show()
        {
            this.gameObject.SetActive(true);
        }

        private void Update()
        {
            UpdatePercentage();
        }

        private void UpdatePercentage()
        {
            float targetValue = Mathf.Lerp(currentSliderValue, currentPercentage, 0.8f);
            currentSliderValue = targetValue;
            Progressbar.value = targetValue;
            ProgressPercentage.text = $"{(int)(targetValue * 100)} %";
        }

        public void Initalize()
        {
            Progressbar.value = 0f;
            ProgressPercentage.text = "0 %";
            ProgressTitle.text = string.Empty;
        }
    }
}