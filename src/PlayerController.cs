using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public string selectedCharacterName;
    public UnityEngine.UI.Text characterNameText;
    public GameObject inventoryCanvas;
    public Sprite[] walkSprites; // Масив спрайтів для анімації ходьби
    public float moveSpeed = 5f; // Швидкість руху гравця
    public float jumpForce = 10f; // Сила стрибка гравця

    private Rigidbody2D rb; // Rigidbody гравця
    private bool isGrounded = false; // Позначає, чи гравець знаходиться на землі
    private SpriteRenderer spriteRenderer; // Компонент для зміни спрайтів

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Завантаження імені персонажа
        selectedCharacterName = PlayerPrefs.GetString("SelectedCharacter");
        characterNameText.text = selectedCharacterName;

        // Завантаження інформації про положення гравця
        LoadPlayerPosition();
    }

    void Update()
    {
        // Відображення або приховання інвентаря при натисканні Esc або "E"
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E))
        {
            ToggleInventory();
        }

        // Рух гравця
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * horizontalInput * moveSpeed * Time.deltaTime);

        // Поворот гравця в сторону руху
        if (horizontalInput > 0)
        {
            spriteRenderer.flipX = false; // Поворот гравця вправо
        }
        else if (horizontalInput < 0)
        {
            spriteRenderer.flipX = true; // Поворот гравця вліво
        }

        // Стрибок гравця
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = Vector2.up * jumpForce;
            isGrounded = false; // Встановлюємо прапорець "isGrounded" в false, оскільки гравець зараз у повітрі
        }

        // Відтворення анімації ходьби
        if (Mathf.Abs(horizontalInput) > 0.1f)
        {
            PlayWalkAnimation();
        }
    }

    void PlayWalkAnimation()
    {
        // Змінюємо спрайт гравця для анімації ходьби
        int spriteIndex = Mathf.FloorToInt(Time.time * 10) % walkSprites.Length;
        spriteRenderer.sprite = walkSprites[spriteIndex];
    }

    private void LoadPlayerPosition()
    {
        // Отримання позиції гравця з PlayerPrefs
        float playerX = PlayerPrefs.GetFloat(selectedCharacterName + "_PlayerX", 0f);
        float playerY = PlayerPrefs.GetFloat(selectedCharacterName + "_PlayerY", 0f);
        Vector3 playerPosition = new Vector3(playerX, playerY, 0f);

        // Переміщення гравця на його позицію
        transform.position = playerPosition;
    }

    private void ToggleInventory()
    {
        // Відображення або приховання інвентаря
        inventoryCanvas.SetActive(!inventoryCanvas.activeSelf);
    }

    public void SavePlayerPosition()
    {
        // Збереження позиції гравця у PlayerPrefs
        PlayerPrefs.SetFloat(selectedCharacterName + "_PlayerX", transform.position.x);
        PlayerPrefs.SetFloat(selectedCharacterName + "_PlayerY", transform.position.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Перевірка, чи гравець дотикається до платформи
        isGrounded = true; // Встановлюємо прапорець "isGrounded" в true, оскільки гравець торкається будь-якої колізії
    }
}
