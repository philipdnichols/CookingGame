public class InstalledObject {
    #region Fields

    string type;

    Tile owningTile;

    #endregion

    #region Properties

    public string Type {
        get {
            return this.type;
        }
    }

    public Tile OwningTile {
        get {
            return this.owningTile;
        }
    }

    #endregion

    #region Constructors

    // This constructor will only ever be used for InstalledObject prototype creation
    public InstalledObject(string type) {
        this.type = type;
    }

    #endregion
}
