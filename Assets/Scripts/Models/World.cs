public class World {
    #region Private Fields

    private Tile[,] tiles;

    private int width;
    private int height;

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
