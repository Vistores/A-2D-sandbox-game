using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class InventoryManager : MonoBehaviour
{
    public Tile[] blockTiles; // Масив тайлів блоків, які можуть бути в інвентарі
    public UnityEngine.UI.Image[] inventorySlots; // Слоти інвентаря для відображення блоків
    public GameObject highlightPrefab; // Префаб для підсвічування обраного блоку
    private GameObject currentHighlight; // Поточна підсвітка обраного блоку
    public float placeRadius = 1.5f; // Радіус встановлення блоків
    private int selectedBlockIndex = 0; // Індекс обраного блоку

    private Transform playerTransform; // Позиція гравця

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        UpdateInventoryUI(); // Оновлення інвентаря при запуску
    }

    void Update()
    {
        // Зміна обраного блоку в інвентарі
        if (Input.GetKeyDown(KeyCode.Alpha1) && blockTiles.Length > 0) // При натисканні "1"
        {
            selectedBlockIndex = 0;
            UpdateInventoryUI();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && blockTiles.Length > 1) // При натисканні "2"
        {
            selectedBlockIndex = 1;
            UpdateInventoryUI();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && blockTiles.Length > 2) // При натисканні "3"
        {
            selectedBlockIndex = 2;
            UpdateInventoryUI();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && blockTiles.Length > 3) // При натисканні "4"
        {
            selectedBlockIndex = 3;
            UpdateInventoryUI();
        }

        // Встановлення обраного блоку в світі при натисканні правої кнопки миші
        if (Input.GetMouseButton(1)) // При тривалому зажимі ПКМ
        {
            SetSelectedBlockInWorld();
        }

        // Видалення підсвітки при натисканні клавіші "E"
        if (Input.GetKeyDown(KeyCode.E))
        {
            ClearHighlight();
        }
    }

    void SetSelectedBlockInWorld()
    {
        // Отримання позиції курсора миші в світі
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Перевірка відстані між позицією миші та позицією гравця
        if (Vector3.Distance(mouseWorldPosition, playerTransform.position) > placeRadius)
        {
            // Позиція миші занадто далеко від гравця, тому не встановлюємо блок
            return;
        }

        // Отримання тайла з обраного індексу в інвентарі
        Tile selectedBlockTile = blockTiles[selectedBlockIndex];

        // Отримання Tilemap, на якому розміщений блок
        Tilemap tilemap = FindObjectOfType<Tilemap>();

        // Перетворення позиції миші в координати Tilemap
        Vector3Int tilePosition = tilemap.WorldToCell(mouseWorldPosition);

        // Встановлення обраного блоку в світі за позицією миші
        tilemap.SetTile(tilePosition, selectedBlockTile);
    }

    void UpdateInventoryUI()
    {
        // Оновлення відображення інвентаря
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            UnityEngine.UI.Image slotImage = inventorySlots[i];
            if (i < blockTiles.Length)
            {
                // Відображення блоків у слотах
                slotImage.sprite = Sprite.Create(blockTiles[i].sprite.texture, new Rect(0, 0, blockTiles[i].sprite.texture.width, blockTiles[i].sprite.texture.height), new Vector2(0.5f, 0.5f));
            }
            else
            {
                // Очищення слоту відображення
                slotImage.sprite = null;
            }
        }

        // Видалення попередньої підсвітки, якщо вона існує
        if (currentHighlight != null)
        {
            Destroy(currentHighlight);
        }

        // Створення нової підсвітки для обраного блоку
        currentHighlight = Instantiate(highlightPrefab, inventorySlots[selectedBlockIndex].transform.position, Quaternion.identity);
        currentHighlight.transform.SetParent(inventorySlots[selectedBlockIndex].transform); // Встановлення батьківського об'єкту підсвітки
    }

    void ClearHighlight()
    {
        // Видалення підсвітки обраного блоку
        if (currentHighlight != null)
        {
            Destroy(currentHighlight);
        }
    }
}
