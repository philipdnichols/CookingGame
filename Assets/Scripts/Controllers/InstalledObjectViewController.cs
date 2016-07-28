using UnityEngine;
using System.Collections.Generic;

public class InstalledObjectViewController : MonoBehaviour {
    public static InstalledObjectViewController Instance { get; protected set; }

    #region Fields

    public GameObject installedObjectPrefab;

    Dictionary<string, Sprite> installedObjectSpriteMap;

    #endregion

    #region MonoBehavior Implementations

    void Start() {
        if (Instance != null) {
            Debug.LogError("There should never be two instances of CharacterViewController.");
        }
        Instance = this;

        // Load the sprites
        Sprite[] sprites = Resources.LoadAll<Sprite>("Images/InstalledObjects");
        installedObjectSpriteMap = new Dictionary<string, Sprite>();
        foreach (Sprite sprite in sprites) {
            installedObjectSpriteMap[sprite.name] = sprite;
        }

        WorldController.Instance.World.installedObjectCreatedCallback += OnInstalledObjectCreated;
    }

    #endregion

    #region Event Handlers

    void OnInstalledObjectCreated(InstalledObject installedObject) {
        GameObject installedObjectGO = (GameObject)Instantiate(installedObjectPrefab, new Vector3(installedObject.OwningTile.X, installedObject.OwningTile.Y, 0.0f), Quaternion.identity);
        //InstalledObjectView installedObjectView = installedObjectGO.GetComponent<InstalledObjectView>();

        installedObjectGO.transform.SetParent(this.transform);

        SpriteRenderer installedObjectSR = installedObjectGO.GetComponent<SpriteRenderer>();
        installedObjectSR.sprite = SpriteForInstalledObject(installedObject);

        // Setup Character model event handlers

        // Setup Character view event handlers
    }

    #endregion

    #region Helper Methods

    Sprite SpriteForInstalledObject(InstalledObject installedObject) {
        return installedObjectSpriteMap[installedObject.Type];
    }

    #endregion
}
