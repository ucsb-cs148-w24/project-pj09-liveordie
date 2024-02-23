using System.Collections;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class CharacterHealthTest
{
    [Test]
    public void Test_DecreaseHealth_ReducesHealthAndUpdatesBar()
    {
        // GameObject go = new GameObject();
        // go.AddComponent<CharacterHealth>();
        // var health1 = go.GetComponent<CharacterHealth>();
        // // Start with full health
        // health1.setHealth(50f);

        // // Simulate enemy collision
        // health1.DecreaseHealth();

        // // Assert health and bar update
        // Assert.AreEqual(49f, health1.getHealth());
        // Assert.AreEqual(0.98f, health1.getHealthBarFillAmount());
    }

    [Test]
    public void Test_HealthBarColor_ChangesWithHealthLevels()
    {
        // GameObject go = new GameObject();
        // go.AddComponent<CharacterHealth>();
        // var health = go.GetComponent<CharacterHealth>();
        // // Start with full health (green)
        // health.setHealth(50f);
        // Assert.AreEqual(Color.green, health.getHealthBarColor());

        // // Reduce health to yellow threshold
        // health.setHealth(25f);
        // Assert.AreEqual(Color.yellow, health.getHealthBarColor());

        // // Reduce health to red threshold
        // health.setHealth(10f);
        // Assert.AreEqual(Color.red, health.getHealthBarColor());
    }

    [Test]
    public void Test_ZeroHealth_TriggersDeathAndCleanup()
    {
        // GameObject go = new GameObject();
        // go.AddComponent<CharacterHealth>();
        // var health = go.GetComponent<CharacterHealth>();
        // // Set low health
        // health.setHealth(1f);

        // // Simulate enemy collision
        // health.DecreaseHealth();

        // // Assert death flag and movement lock
        // Assert.IsTrue(health.checkDeath());
    }

    // A Test behaves as an ordinary method
    [Test]
    public void CharacterHealthTestSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // // `yield return null;` to skip a frame.
    // [UnityTest]
    // public IEnumerator CharacterHealthTestWithEnumeratorPasses()
    // {
    //     // Use the Assert class to test conditions.
    //     // Use yield to skip a frame.
    //     yield return null;
    // }
}
