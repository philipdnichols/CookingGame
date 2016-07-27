public class World {
    #region Fields

    Tile[,] tiles;

    int width;
    int height;

    #endregion

    #region Properties

    public Tile[,] Tiles {
        get {
            return this.tiles;
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
}
