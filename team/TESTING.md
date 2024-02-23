# Unit/Integration Testing Documentation
## Software: LiveOrDie (Phase: Beta)
_Purpose: document the testing library/ies and approach/es we've experiemnted with and the unit test(s) we've implemented_

[2-16-2024] Created First 2 Scene Verification (Integration) Tests
Test Coverage: SceneManagement Tests 
Location: `LiveOrDie/Assets/Tests/StartScenePanelTest.cs`

This script tests that the Start Screen Panel loads correctly. It will run these tests
  1. `VerifySceneLoadedCorrectly()`: Ensures game adds the `StartScreenController` into the Scene
  2. `VerifyMainCameraWork`: Ensures that a main camera is loaded to game

[Status]: 2/2 Passing
