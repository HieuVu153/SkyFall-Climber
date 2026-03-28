using UnityEngine;
using UnityEngine.UI;

public sealed class SettingManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private Slider volumeSlider;

    private void Start()
    {
        // Mặc định âm lượng là Max khi bắt đầu
        if (volumeSlider != null)
        {
            volumeSlider.value = volumeSlider.maxValue;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    // Hàm chỉnh âm lượng
    public void SetVolume(float value)
    {
        // value sẽ chạy từ 0 đến 1 (hoặc MaxValue của Slider)
        AudioListener.volume = value;
        Debug.Log("Âm lượng hiện tại: " + value);
    }

    // Hàm mở Setting
    public void OpenSetting()
    {
        settingPanel.SetActive(true);
    }

    // Hàm thoát Setting (Đóng Panel)
    public void CloseSetting()
    {
        settingPanel.SetActive(false);
    }
}