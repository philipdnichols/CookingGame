using System.Collections.Generic;
using UnityEngine;

public class TileViewController : MonoBehaviour {
    public static TileViewController Instance { get; protected set; }

    #region Fields

    public GameObject tileViewPrefab;

    // FIXME: Implement this in a better way
    Dictionary<string, Sprite> tileSpriteMap;

    #endregion

    #region MonoBehavior Implementations

    void Start() {
        if (Instance != null) {
            Debug.LogError("There should never be two instances of TileViewController.");
        }
        Instance = this;

        // Load the sprites
        Sprite[] sprites = Resources.LoadAll<Sprite>("Images/Tiles");
        tileSpriteMap = new Dictionary<string, Sprite>();
        foreach (Sprite sprite in sprites) {
            tileSpriteMap[sprite.name] = sprite;
        }

        WorldController.Instance.World.tileCreatedCallback += OnTileCreated;
    }

    #endregion

    #region Event Handlers

    void OnTileCreated(Tile tile) {
        GameObject tileGO = (GameObject)Instantiate(tileViewPrefab, new Vector3(tile.X, tile.Y, 0.0f), Quaternion.identity);
        //TileView tileView = tileGO.GetComponent<TileView>();

        tileGO.name = tileViewPrefab.name + "_" + tile.X + "_" + tile.Y;
        tileGO.transform.SetParent(this.transform);

        SpriteRenderer tileSR = tileGO.GetComponent<SpriteRenderer>();
        tileSR.sprite = SpriteForTile(tile);

        // Setup Tile model event handlers
        tile.typeChangedCallback += (t, previousType) => OnTileChanged(t, previousType, tileGO);

        // Setup Tile view event handlers
    }

    void OnTileChanged(Tile tile, TileType previousType, GameObject tileGO) {
        SpriteRenderer tileSR = tileGO.GetComponent<SpriteRenderer>();
        tileSR.sprite = SpriteForTile(tile, previousType);
    }

    #endregion

    #region Helper Methods

    // FIXME: Implement a better way to do this.
    Sprite SpriteForTile(Tile tile) {
        return SpriteForTile(tile, tile.Type);
    }

    // FIXME: Implement a better way to do this.
    Sprite SpriteForTile(Tile tile, TileType previousType) {
        switch (tile.Type) {
            case TileType.Dirt:
                return tileSpriteMap["dirt"];

            case TileType.Grass:
                return tileSpriteMap["grass"];

            default:
                return tileSpriteMap["dirt"];
        }
    }

    #endregion
}
