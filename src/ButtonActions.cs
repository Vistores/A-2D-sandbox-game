using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonActions : MonoBehaviour
{
    public Button closeButton; // Кнопка для закриття інтерфейсу
    public Button menuButton; // Кнопка для переходу до меню
    public GameObject menuCanvas; // Посилання на Canvas з меню
    public string menuSceneName; // Назва сцени головного меню

    void Start()
    {
        //функції до подій кнопок
        closeButton.onClick.AddListener(CloseInterface);
        menuButton.onClick.AddListener(GoToMenu);
    }

    public void CloseInterface()
    {
        // Закриття інтерфейсу (відображенням меню)
        menuCanvas.SetActive(false);
    }

    public void GoToMenu()
    {
        // Перехід до вказаної сцени (головного меню)
        SceneManager.LoadScene(menuSceneName);
    }
}
