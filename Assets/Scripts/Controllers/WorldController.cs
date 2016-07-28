using System;
using UnityEngine;

public class WorldController : MonoBehaviour {
    public static WorldController Instance { get; protected set; }

    #region Fields

    public int worldWidth = 100;
    public int worldHeight = 100;

    World world;

    #endregion

    #region Properties

    public World World {
        get {
            return this.world;
        }
    }

    #endregion

    #region MonoBehavior Implementations

    // FIXME: Better way to make sure the WorldController gets created before the TileViewController (for example), other than using the Script Execution Order?
    // -- Maybe have a ControllerManager that listens for events from all the controllers and then sends out a master event?
    void Start() {
        if (Instance != null) {
            Debug.LogError("There should never be two instances of WorldCntroller.");
        }
        Instance = this;

        world = new World(worldWidth, worldHeight);

        // Center the Camera
        Camera.main.transform.position = new Vector3(world.Width / 2, world.Height / 2, Camera.main.transform.position.z);
    }

    // HACK
    bool firstUpdate = true;
	void Update() {
        if (firstUpdate) {
            firstUpdate = false;

            world.InitializeWorld();
        }

        foreach (Character character in world.Characters) {
            character.Update(Time.deltaTime);
        }
	}

    #endregion

    #region Helper Methods

    public Tile GetTileAtWorldPosition(Vector3 position) {
        Tile tile = null;

        int x = Mathf.FloorToInt(position.x);
        int y = Mathf.FloorToInt(position.y);

        try {
            tile = world.GetTileAt(x, y);
        } catch (ArgumentOutOfRangeException e) {
            // No worries, just leave the tile 'null'
            Debug.LogError(e);
        }

        return tile;
    }

    #endregion
}
