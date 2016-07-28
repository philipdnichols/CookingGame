using UnityEngine;

// HACK: InstallObject is temporary for now, installing objects in the game is to be done by Characters
public enum PlayerInteractionMode { MoveMainCharacter, InstallObject }

public class PlayerInteractionManager : MonoBehaviour {
    #region Fields

    PlayerInteractionMode currentPlayerInteractionMode = PlayerInteractionMode.MoveMainCharacter;

    World world;
    Character mainCharacter;

    Camera mainCamera;

    #endregion

    #region MonoBehavior Implementations

    void Start() {
        world = WorldController.Instance.World;
        mainCamera = Camera.main;

        world.mainCharacterCreatedCallback += OnCharacterCreated;
	}
	
	void Update() {
	    switch (currentPlayerInteractionMode) {
            case PlayerInteractionMode.MoveMainCharacter:
                Update_MoveMainCharacterMode(Time.deltaTime);
                break;

            default:
                break;
        }
	}

    #endregion

    #region Event Handlers

    void OnCharacterCreated(Character character) {
        mainCharacter = character;
    }

    #endregion

    #region Helper Methods

    void Update_MoveMainCharacterMode(float deltaTime) {
        // HACK: Just a temporary way to test out character pathfinding.
        if (Input.GetMouseButtonUp(1)) {
            Tile tile = WorldController.Instance.GetTileAtWorldPosition(mainCamera.ScreenToWorldPoint(Input.mousePosition));
            if (tile != null) {
                mainCharacter.DestinationTile = tile;
            }
        }
    }

    #endregion
}
