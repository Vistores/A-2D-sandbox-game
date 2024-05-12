using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static System.Net.Mime.MediaTypeNames;

public class MainMenu : MonoBehaviour
{
    public Button startButton; // Кнопка для переходу до наступної сцени
    public Button exitButton; // Кнопка для виходу з гри
    public string nextSceneName; // Назва наступної сцени

    void Start()
    {
        // Додаємо функції до подій кнопок
        startButton.onClick.AddListener(StartGame);
        exitButton.onClick.AddListener(ExitGame);
    }

    public void StartGame()
    {
        // Перехід до наступної сцени (якщо така є)
        SceneManager.LoadScene(nextSceneName);
    }

    public void ExitGame()
    {
        // Закриття гри
        UnityEngine.Application.Quit();
    }
}
