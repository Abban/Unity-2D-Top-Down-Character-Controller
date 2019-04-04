# Unity 2D Top Down Character Controller
Basic example of a 2D top down controller in Unity 2D

![Sample](https://raw.githubusercontent.com/abban/Unity-2D-Top-Down-Character-Controller/master/sample.gif)

## Requirements
In order to run this example you'll need to install [Zenject](https://github.com/modesttree/Zenject). If you don't want to use that then it should be possible to pull the code into MonoBehaviours.

You also need to install the [Spine Unity Runtime](http://esotericsoftware.com/spine-unity).

## How It Works

### Input
The input controller will run once a frame on Tick and set variables an input state object depending on what buttons the player is clicking. Use WASD to move around.

### Player
The following files run the player controller:

* PlayerAnimatorHandler: Checks the player movement once a frame and sets parameters on the animator.
* PlayerCollisionHandler: Fires a bunch of rays in the direction of travel and looks for colliders that are obstructing the player.
* PlayerFacade: MonoBehaviour that acts as the main controller and a gateway to player functionality for other systems. It also contains an empty FSM as an example of how one might be used with Zenject.
* PlayerGUIHandler: Handles showing icons when a player is in a trigger.
* PlayerMovementHandler: Moves the player depending on input.
* PlayerSettings: Scriptable Object that contains some setting for the player.
* PlayerTriggerHandler: MonoBehaviour that adds and removes triggers from a list when the player collider enters and leaves them.
* PlayerModel: Acts as the base model for the other components to use.
* PlayerCollisionState: Holds the player's current collision status.
* PlayerTriggerItems: Holds the current triggers the player is inside.

### Camera
The camera just has a controller to follow the player.

