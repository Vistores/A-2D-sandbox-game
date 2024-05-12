using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class CharacterManager : MonoBehaviour
{
    public InputField nameInputField;
    public Dropdown characterDropdown;
    public Button createCharacterButton;
    public Button deleteCharacterButton;
    public GameObject confirmDeletePanel;
    public Button confirmYesButton;
    public Button confirmNoButton;
    public Button nextSceneButton;
    public string nextSceneName;

    private List<Character> characters = new List<Character>();

    void Start()
    {
        LoadCharacters();
        UpdateCharacterDropdown();

        createCharacterButton.onClick.AddListener(CreateCharacter);
        deleteCharacterButton.onClick.AddListener(DeleteCharacter);
        confirmYesButton.onClick.AddListener(ConfirmDelete);
        confirmNoButton.onClick.AddListener(CancelDelete);

        deleteCharacterButton.interactable = false;

        characterDropdown.onValueChanged.AddListener(delegate {
            OnCharacterSelected(characterDropdown);
        });

        nextSceneButton.onClick.AddListener(NextScene);
    }

    public void CreateCharacter()
    {
        string name = nameInputField.text;
        characters.Add(new Character(name, characters.Count + 1));
        SaveCharacters();
        UpdateCharacterDropdown();
        nameInputField.text = "";
        deleteCharacterButton.interactable = true;
    }

    private void UpdateCharacterDropdown()
    {
        characterDropdown.ClearOptions();
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();

        foreach (Character character in characters)
        {
            options.Add(new Dropdown.OptionData(character.name));
        }

        characterDropdown.AddOptions(options);
        deleteCharacterButton.interactable = characterDropdown.value != -1;
    }

    public void DeleteCharacter()
    {
        confirmDeletePanel.SetActive(true);
    }

    public void ConfirmDelete()
    {
        int selectedIndex = characterDropdown.value;
        characters.RemoveAt(selectedIndex);
        SaveCharacters();
        UpdateCharacterDropdown();
        confirmDeletePanel.SetActive(false);

        if (characters.Count == 0)
        {
            deleteCharacterButton.interactable = false;
        }
    }

    public void CancelDelete()
    {
        confirmDeletePanel.SetActive(false);
    }

    private void SaveCharacters()
    {
        string json = JsonUtility.ToJson(new CharacterData(characters));
        PlayerPrefs.SetString("Characters", json);

        // Зберегти дані інвентаря для кожного персонажа
        foreach (Character character in characters)
        {
            string characterInventoryKey = character.name + "_Inventory";
            string inventoryJson = JsonUtility.ToJson(character.inventory);
            PlayerPrefs.SetString(characterInventoryKey, inventoryJson);
        }
    }

    private void LoadCharacters()
    {
        if (PlayerPrefs.HasKey("Characters"))
        {
            string json = PlayerPrefs.GetString("Characters");
            CharacterData data = JsonUtility.FromJson<CharacterData>(json);
            characters = data.characters;

            // Завантаження інвентаря для кожного персонажа
            foreach (Character character in characters)
            {
                string characterInventoryKey = character.name + "_Inventory";
                if (PlayerPrefs.HasKey(characterInventoryKey))
                {
                    string inventoryJson = PlayerPrefs.GetString(characterInventoryKey);
                    Inventory inventory = JsonUtility.FromJson<Inventory>(inventoryJson);
                    character.inventory = inventory;
                }
            }
        }
        else
        {
            characters = new List<Character>();
        }
    }

    public void OnCharacterSelected(Dropdown dropdown)
    {
        int selectedIndex = dropdown.value;
        Character selectedCharacter = characters[selectedIndex];
        deleteCharacterButton.interactable = true;
    }

    public void NextScene()
    {
        if (characterDropdown.value != -1)
        {
            PlayerPrefs.SetString("SelectedCharacter", characters[characterDropdown.value].name);
            SceneManager.LoadScene(nextSceneName);
        }
    }
}

[System.Serializable]
public class Character
{
    public string name;
    public int number;
    public Inventory inventory;

    public Character(string _name, int _number)
    {
        name = _name;
        number = _number;
        inventory = new Inventory();
    }
}

[System.Serializable]
public class CharacterData
{
    public List<Character> characters;

    public CharacterData(List<Character> _characters)
    {
        characters = _characters;
    }
}

[System.Serializable]
public class Inventory
{
    public List<Item> items;

    public Inventory()
    {
        items = new List<Item>();
    }
}

[System.Serializable]
public class Item
{
    public string itemName;
    public int quantity;

    public Item(string _itemName, int _quantity)
    {
        itemName = _itemName;
        quantity = _quantity;
    }
}
