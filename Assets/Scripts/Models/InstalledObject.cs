public class InstalledObject {
    #region Fields

    Tile owningTile;

    #endregion

    #region Properties

    public Tile OwningTile {
        get {
            return this.owningTile;
        }
    }

    #endregion

    #region Constructors

    public InstalledObject(Tile owningTile) {
        this.owningTile = owningTile;
    }

    #endregion
}
