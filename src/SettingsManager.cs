using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    // UI елементи для налаштувань
    public Toggle fullscreenToggle;
    public Slider cameraZoomSlider;
    public Slider placeRadiusSlider;
    public Slider destroyRadiusSlider;
    public Toggle disableTextToggle;
    public UnityEngine.UI.Text disableTextElement;

    // Ключі для зберігання налаштувань у PlayerPrefs
    private const string FullscreenPrefsKey = "Fullscreen";
    private const string CameraZoomPrefsKey = "CameraZoom";
    private const string PlaceRadiusPrefsKey = "PlaceRadius";
    private const string DestroyRadiusPrefsKey = "DestroyRadius";
    private const string TextElementEnabledPrefsKey = "TextElementEnabled";

    public Camera mainCamera; // Посилання на головну камеру

    void Start()
    {
        // Підписка на події зміни флажка і текстового елемента
        fullscreenToggle.onValueChanged.AddListener(OnFullscreenToggleChanged);
        cameraZoomSlider.onValueChanged.AddListener(OnCameraZoomChanged);
        placeRadiusSlider.onValueChanged.AddListener(OnPlaceRadiusChanged);
        destroyRadiusSlider.onValueChanged.AddListener(OnDestroyRadiusChanged);
        disableTextToggle.onValueChanged.AddListener(OnDisableTextToggleChanged);

        // Завантаження налаштувань при запуску
        LoadSettings();
    }

    private void LoadSettings()
    {
        // Завантаження налаштувань з PlayerPrefs
        bool isFullscreen = GetBoolSetting(FullscreenPrefsKey, true);
        float cameraZoom = GetFloatSetting(CameraZoomPrefsKey, 1f);
        float placeRadius = GetFloatSetting(PlaceRadiusPrefsKey, 0f);
        float destroyRadius = GetFloatSetting(DestroyRadiusPrefsKey, 0f);
        bool isTextElementEnabled = GetBoolSetting(TextElementEnabledPrefsKey, true);

        // Встановлення значень у відповідні UI елементи
        fullscreenToggle.isOn = isFullscreen;
        cameraZoomSlider.value = cameraZoom;
        placeRadiusSlider.value = placeRadius;
        destroyRadiusSlider.value = destroyRadius;
        disableTextToggle.isOn = isTextElementEnabled;
        disableTextElement.gameObject.SetActive(isTextElementEnabled);

        // Встановлення посилання на головну камеру
        mainCamera = Camera.main;
    }

    private void OnFullscreenToggleChanged(bool isOn)
    {
        // Зміна налаштувань за допомогою флажка повноекранного режиму
        Screen.fullScreen = isOn;
    }

    private void OnCameraZoomChanged(float value)
    {
        // Логіка зміни налаштувань для збільшення камери
        if (mainCamera != null)
        {
            mainCamera.orthographicSize = value; // Зміна зуму камери
        }
    }

    private void OnPlaceRadiusChanged(float value)
    {
        // Логіка зміни налаштувань для радіусу встановлення блоків
        // Тут можна виконати необхідні дії для зміни радіусу встановлення блоків
    }

    private void OnDestroyRadiusChanged(float value)
    {
        // Логіка зміни налаштувань для радіусу руйнування блоків
        // Тут можна виконати необхідні дії для зміни радіусу руйнування блоків
    }

    private void OnDisableTextToggleChanged(bool isOn)
    {
        // Зміна стану текстового елемента за допомогою флажка
        disableTextElement.gameObject.SetActive(!isOn);
    }

    // Статичні методи для збереження та завантаження налаштувань

    public static void SetBoolSetting(string key, bool value)
    {
        PlayerPrefs.SetInt(key, value ? 1 : 0);
    }

    public static bool GetBoolSetting(string key, bool defaultValue)
    {
        return PlayerPrefs.GetInt(key, defaultValue ? 1 : 0) == 1;
    }

    public static void SetFloatSetting(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
    }

    public static float GetFloatSetting(string key, float defaultValue)
    {
        return PlayerPrefs.GetFloat(key, defaultValue);
    }
}
