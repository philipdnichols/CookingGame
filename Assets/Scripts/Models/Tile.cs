using System;

public enum TileType { Dirt, Grass }

public class Tile {
    #region Fields

    World world;

    int x;
    int y;

    // __ indicates that this field should never be used to get or set the value outside of the Property
    TileType __type;

    InstalledObject installedObject;

    #endregion

    #region Properties

    public float MovementCost {
        get {
            switch (this.Type) {
                case TileType.Dirt:
                    return 1.0f;

                default:
                    return 1.0f;
            }
        }
    }

    public int X {
        get {
            return this.x;
        }
    }

    public int Y {
        get {
            return this.y;
        }
    }

    public TileType Type {
        get {
            return this.__type;
        }

        set {
            if (this.__type != value) {
                TileType previousType = this.__type;
                this.__type = value;

                if (typeChangedCallback != null) {
                    typeChangedCallback(this, previousType);
                }
            }
        }
    }

    #endregion

    #region Events

    // TODO: is this the proper way to use events?
    public event Action<Tile, TileType> typeChangedCallback; 

    #endregion

    #region Constructors

    public Tile(World world, int x, int y) {
        this.world = world;

        this.x = x;
        this.y = y;

        this.Type = TileType.Dirt;
    }

    #endregion
}
