using System;
using System.Collections.Generic;

public class World {
    #region Fields

    Tile[,] tiles;
    Character mainCharacter;    // TODO: Not sure if this is how I want to do this...
    List<Character> characters;

    int width;
    int height;

    #endregion

    #region Properties

    public int Width {
        get {
            return this.width;
        }
    }

    public int Height {
        get {
            return this.height;
        }
    }

    public List<Character> Characters {
        get {
            return this.characters;
        }
    }

    public Character MainCharacter {
        get {
            return this.mainCharacter;
        }
    }

    #endregion

    #region Events

    public event Action<Tile> tileCreatedCallback;
    public event Action<Character> mainCharacterCreatedCallback;
    public event Action<InstalledObject> installedObjectCreatedCallback;

    #endregion

    #region Contructors

    public World(int width, int height) {
        // TODO: Separate out some logic here

        this.width = width;
        this.height = height;

        this.tiles = new Tile[width, height];
        this.characters = new List<Character>();
    }

    #endregion

    #region Helper Methods

    // HACK: In order to be able to have other controllers listen for creation events, we can't put those events in the constructor,
    // so we create another helper method to actually create the models, and fire the events
    // TODO: We probably need to come up with a generic event system that we can use that isn't tied directly to models.
    public void InitializeWorld() {
        // Instantiate our empty tiles for this world.
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                Tile tile = new Tile(this, x, y);
                tiles[x, y] = tile;

                if (tileCreatedCallback != null) {
                    tileCreatedCallback(tile);
                }
            }
        }

        mainCharacter = new Character(tiles[width / 2, height / 2], 4.0f);    // FIXME: manually set character speed here.

        if (mainCharacterCreatedCallback != null) {
            mainCharacterCreatedCallback(mainCharacter);
        }
    }

    // TODO: change these into generic exceptions
    public Tile GetTileAt(int x, int y) {
        if (x >= Width || x < 0) {
            throw new ArgumentOutOfRangeException("x", "World::GetTileAt");
        } else if (y >= Height || y < 0) {
            throw new ArgumentOutOfRangeException("y", "World::GetTileAt");
        }
        return tiles[x, y];
    }

    #endregion
}
