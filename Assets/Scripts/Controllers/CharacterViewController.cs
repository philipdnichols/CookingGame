using System.Collections.Generic;
using UnityEngine;

public class CharacterViewController : MonoBehaviour {
    public static CharacterViewController Instance { get; protected set; }

    #region Fields

    public GameObject characterViewPrefab;

    Dictionary<string, Sprite> characterSpriteMap;

    #endregion

    #region Properties

    #endregion

    #region MonoBehavior Implementations

    // TODO: Check to see if there is a better place to be putting all these things Start? Awake? OnEnable?
    void Start() {
        if (Instance != null) {
            Debug.LogError("There should never be two instances of CharacterViewController.");
        }
        Instance = this;

        // Load the sprites
        Sprite[] sprites = Resources.LoadAll<Sprite>("Images/Characters");
        characterSpriteMap = new Dictionary<string, Sprite>();
        foreach (Sprite sprite in sprites) {
            characterSpriteMap[sprite.name] = sprite;
        }

        WorldController.Instance.World.mainCharacterCreatedCallback += OnCharacterCreated;
    }

    #endregion

    #region Event Handlers

    void OnCharacterCreated(Character character) {
        GameObject characterGO = (GameObject)Instantiate(characterViewPrefab, new Vector3(character.X, character.Y, 0.0f), Quaternion.identity);
        //CharacterView characterView = characterGO.GetComponent<CharacterView>();

        characterGO.transform.SetParent(this.transform);

        SpriteRenderer characterSR = characterGO.GetComponent<SpriteRenderer>();
        characterSR.sprite = SpriteForCharacter(character);

        // Setup Character model event handlers
        character.movedCallback += (c) => OnCharacterMoved(c, characterGO);

        // Setup Character view event handlers
    }

    void OnCharacterMoved(Character character, GameObject characterGO) {
        characterGO.transform.position = new Vector3(character.X, character.Y, 0.0f);
    }

    #endregion

    #region Helper Methods

    // TODO: Change the way this is implemented
    Sprite SpriteForCharacter(Character character) {
        return characterSpriteMap["character1"];
    }

    #endregion
}
