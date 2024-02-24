# Unit/Integration Testing Documentation
## Software: LiveOrDie (Phase: Beta)
_Purpose: document the testing library/ies and approach/es we've experiemnted with and the unit test(s) we've implemented_

[2-23-2024] Created Main Scene (Integration) Tests
Test Coverage: SceneManagement Tests
Location: `LiveOrDie/Assets/Tests/MainScenePanelTests.cs`

This script tests for the Main Scene of our Game Panel. It will run tests to ensure that all GameObject prefabs, Controller Scripts, and Player Properties are loading as expected and properly

List of Tests:
  1. `VerifyBackgroundLoadedCorrectly`: Ensures Background/tilemaps loaded
  2. `VerifyEnemyClonerLoadedCorrectly`: Ensures an Enemy Spawner exists
  3. `VerifyMainScenePlayersLoadedCorrectly`: Ensures Players spawned properly
  4. `VerifyMainSceneRopeLoadedCorrectly`: Ensures String spawns between players
  5. `VerifyPauseControllerLoadedCorrectly`: Ensures there exists a Pause feature
  6. `VerifyPlayerHealthBarsLoadedCorrectly`: Ensures each Player has a proper starting healthbar

[Status]: 6/6 Passing


[2-16-2024] Created First 2 Scene Verification (Integration) Tests
Test Coverage: SceneManagement Tests 
Location: `LiveOrDie/Assets/Tests/StartScenePanelTest.cs`

This script tests that the Start Screen Panel loads correctly. It will run these tests
  1. `VerifySceneLoadedCorrectly()`: Ensures game adds the `StartScreenController` into the Scene
  2. `VerifyMainCameraWork`: Ensures that a main camera is loaded to game

[Status]: 2/2 Passing
