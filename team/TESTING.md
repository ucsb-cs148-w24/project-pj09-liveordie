# Unit/Integration Testing Documentation
## Software: LiveOrDie (Phase: Beta)
_Purpose: document the testing library/ies and approach/es we've experiemnted with and the unit test(s) we've implemented_

Links to Test PRs: [89](https://github.com/ucsb-cs148-w24/project-pj09-liveordie/pull/89) & [103](https://github.com/ucsb-cs148-w24/project-pj09-liveordie/pull/103)

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
  7. `VerifyEnemySpawnerLoadedCorrectly`: Enemy Spawn works, more detailed compared to #2
  8. `VerifyAudioSceneManagerLoadedCorrectly`: Audio loaded correctly
  9. `VerifyScoreBoardControllerLoadedCorrectly`: Ensures scoreboard exists
  10. more added as PRs were created

[Status]: ALL Passing


[2-16-2024] Created First 2 Scene Verification (Integration) Tests
Test Coverage: SceneManagement Tests 
Location: `LiveOrDie/Assets/Tests/StartScenePanelTest.cs`

This script tests that the Start Screen Panel loads correctly. It will run these tests
  1. `VerifySceneLoadedCorrectly()`: Ensures game adds the `StartScreenController` into the Scene
  2. `VerifyMainCameraWork`: Ensures that a main camera is loaded to game

[Status]: 2/2 Passing

## Retrospective/Reflections
1) Implementation Process
    - We used Unity's Built-in `NUnit.Framework`, `UnityEditor.SceneManagement` and `UnityEngine.TestTools` to load the test scenes and create UnityTest functions.
    - We also used `Linq` expressions, `Collections`, and Built-in C# Assertion syntax to compare our Expectation vs Reality checks
    - We created TestAssembly.asm files that assembled our tests so they can run in Unity's Test Runner
    - Since Unity didn't provide Mocking frameworks, we opted for Scene-Based Testing that made our tests model that of Integration Tests
3) Future Plans
    - We plan to continue with Scene-Based Integration Tests, for as of now, there exists no substantial mocking frameworks for us to create
      mock objects to use in Unit Tests. However, we do hope to incorporate Unit Tests into our Testing Workflow by adding to our SceneTest
      scripts so that when a script is loaded into the game, we call each of its public function to ensure that it works as expected 
5) Satisfaction with `component/integration/end-to-end` testing library/tools
    - Overall, Unity's Test Tools and `NUit.Framework` provides a great way to test for basic functionality of our game. Additionally, C#'s built-in
      libraries enabled us to create an organized and structured testing framework that we can continue to use easily. However, we do feel that Unity
      lacked Mocking frameworks that we can use in Unit Tests. Having mocks will drive down elaborate scene loading and runtime in replacement of
      simpler and more specific unit tests. So overall satisfaction was moderate.
7) Higher-level testing plans
    - We plan to continue with integration testing, as well as incorporate more aggressive unit tests for each function or game object we create
    - We won't be using Mock frameworks, as we realized it won't be neccessary considering current strategies we use
      can successfully test function at the same level. 
