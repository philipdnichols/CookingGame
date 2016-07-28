using System;
using UnityEngine;

public class Character {
    #region Fields

    Tile currentTile;
    Tile __destinationTile;

    float movementProgress;
    float distanceToTravel;

    float speed;

    #endregion

    #region Properties

    public float X {
        get {
            return Mathf.Lerp(currentTile.X, this.__destinationTile.X, movementProgress);
        }
    }

    public float Y {
        get {
            return Mathf.Lerp(currentTile.Y, this.__destinationTile.Y, movementProgress);
        }
    }

    public Tile DestinationTile {
        get {
            return this.__destinationTile;
        }

        set {
            if (this.currentTile == this.__destinationTile) {
                if (this.__destinationTile != value) {
                    Tile previousDestinationTile = this.__destinationTile;
                    this.__destinationTile = value;

                    this.distanceToTravel = Mathf.Sqrt(
                        Mathf.Pow(currentTile.X - this.__destinationTile.X, 2) +
                        Mathf.Pow(currentTile.Y - this.__destinationTile.Y, 2)
                    );

                    this.movementProgress = 0.0f;

                    if (destinationTileChangedCallback != null) {
                        destinationTileChangedCallback(this, previousDestinationTile);
                    }
                }
            } else {
                // TODO: Maybe throw an error?
            }
        }
    }

    #endregion

    #region Events

    public event Action<Character> movedCallback;
    public event Action<Character, Tile> destinationTileChangedCallback;

    #endregion

    #region Constructors

    public Character(Tile tile, float speed) {
        this.currentTile = this.__destinationTile = tile;

        this.speed = speed;

        this.distanceToTravel = 0.0f;
    }

    #endregion

    #region Behavior

    public void Update(float deltaTime) {
        Update_DoMovement(deltaTime);
    }

    void Update_DoMovement(float deltaTime) {
        if (currentTile == DestinationTile) {
            return;
        }

        // How much distance can we travel this update?
        float distanceThisFrame = speed * deltaTime;

        // How much is that in terms of progress to our destination?
        float progressThisFrame = distanceThisFrame / distanceToTravel;
        
        movementProgress += progressThisFrame;

        if (movementProgress >= 1.0f) {
            // We have reached our destination

            currentTile = DestinationTile;
            movementProgress = 0.0f;
        }

        if (movedCallback != null) {
            movedCallback(this);
        }
    }

    #endregion
}
