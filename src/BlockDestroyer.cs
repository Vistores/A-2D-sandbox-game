using UnityEngine;
using UnityEngine.Tilemaps;

public class BlockDestroyer : MonoBehaviour
{
    public Tilemap tilemap; // Посилання на Tilemap, де знаходяться блоки для руйнування
    public float destroyRadius = 1.5f; // Радіус, в якому гравець може руйнувати блоки
    private Transform playerTransform; // Посилання на трансформ гравця
    private GameObject currentHighlightedTile; // Поточний підсвічений блок
    public GameObject highlightPrefab; // Префаб для підсвічування блоків

    void Start()
    {
        playerTransform = GetComponent<Transform>();
    }

    void Update()
    {
        Vector3 playerPosition = playerTransform.position;

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int mouseTilePosition = tilemap.WorldToCell(mouseWorldPosition);

        if (currentHighlightedTile == null || !mouseTilePosition.Equals(currentHighlightedTile.transform.position))
        {
            Destroy(currentHighlightedTile);

            float distanceToPlayer = Vector3.Distance(playerPosition, mouseWorldPosition);
            if (distanceToPlayer <= destroyRadius)
            {
                currentHighlightedTile = tilemap.GetTile(mouseTilePosition) ? Instantiate(highlightPrefab, tilemap.GetCellCenterWorld(mouseTilePosition), Quaternion.identity) : null;
            }
        }

        if (Input.GetMouseButton(0)) // Якщо натиснута ліва кнопка миші
        {
            DestroyBlockAtPosition(mouseTilePosition);
        }
    }

    void DestroyBlockAtPosition(Vector3Int position)
    {
        TileBase tile = tilemap.GetTile(position);
        if (tile != null)
        {
            tilemap.SetTile(position, null);
        }
    }
}
