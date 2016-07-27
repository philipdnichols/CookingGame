using System;

public class World {
    #region Fields

    Tile[,] tiles;

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

    #endregion

    #region Contructors

    public World(int width = 100, int height = 100) {
        this.width = width;
        this.height = height;

        this.tiles = new Tile[width, height];

        // Instantiate our empty tiles for this world.
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                tiles[x, y] = new Tile(this, x, y);
            }
        }
    }

    #endregion

    #region Helper Methods

    public Tile GetTileAt(int x, int y) {
        if (x >= Width || x < 0) {
            throw new ArgumentOutOfRangeException("x");
        } else if (y >= Height || y < 0) {
            throw new ArgumentOutOfRangeException("y");
        }
        return tiles[x, y];
    }

    #endregion
}
