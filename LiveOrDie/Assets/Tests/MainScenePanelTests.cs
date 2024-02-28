using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class MainScenePanelTests
{
    [OneTimeSetUp]
     public void LoadScene()
     {
         SceneManager.LoadScene("MainScene");
     }
     [UnityTest]
     public IEnumerator VerifyPauseControllerLoadedCorrectly()
     {
        var check = GameObject.Find("PausePanelController").GetComponent<PausePanelManager>();
        Assert.That(check, Is.Not.Null);
        var tag = "PausePanelController";
        Assert.That(check.name, Is.EqualTo(tag));
        yield return null;
     }

    [UnityTest]
     public IEnumerator VerifyEnemyClonerLoadedCorrectly()
     {
        // Assertions for Existence of EnemyCloner
        var check = GameObject.Find("Enemies");
        var cloner = check.GetComponentInChildren<EnemyCloner>();
        Assert.That(cloner, Is.Not.Null);
        Assert.That(cloner.enemySize, Is.EqualTo(10));
        Assert.That(cloner.wolfPrefab.name, Is.EqualTo("Wolf"));
        yield return null;
     }

    [UnityTest]
     public IEnumerator VerifyMainSceneRopeLoadedCorrectly()
     {
        // Assertions for Existence of the Bamboo/Rope
        var check = GameObject.Find("Players");
        var rope = check.GetComponentInChildren<RopeController>();
        Assert.That(rope, Is.Not.Null);
        yield return null;
     }

     [UnityTest]
     public IEnumerator VerifyMainScenePlayersLoadedCorrectly()
     {
        // Assertions for Existence of Players
        var check = GameObject.Find("Players");
        Assert.That(check, Is.Not.Null);
        var players = check.GetComponentsInChildren<CharacterMovement>();
        Assert.That(players.Length, Is.EqualTo(2));
        Assert.That(players.Select(a => a.tag == "Player1").Where(b => b == true).Count, Is.EqualTo(1));
        Assert.That(players.Select(a => a.tag == "Player2").Where(b => b == true).Count, Is.EqualTo(1));
        yield return null;
     }
     [UnityTest]
     public IEnumerator VerifyPlayerHealthBarsLoadedCorrectly()
     {
        // var players = GameObject.Find("Players").GetComponentsInChildren<CharacterMovement>();
        // var healthbars = players.Select(p => p.GetComponentInChildren<CharacterHealth>());
        // Assert.That(healthbars, Is.Not.Null);
        // Assert.That(healthbars.Count, Is.EqualTo(2));
        // var healthLevels = healthbars.Select(h => h.getHealth()).Distinct().ToList();
        // // assert that all healthLevels are the same at the start
        // Assert.That(healthLevels.Count, Is.EqualTo(1));
        // // assert that all healthLevels are at the default health level @ start
        // Assert.That(healthLevels.FirstOrDefault, Is.EqualTo(50));
        yield return null;
     }
    [UnityTest]
     public IEnumerator VerifyBackgroundLoadedCorrectly()
     {
        var tiles = GameObject.Find("NavMesh");
        var map = GameObject.Find("MapGrid1");
        Assert.That(tiles, Is.Not.Null);
        Assert.That(map, Is.Not.Null);
        yield return null;
     }
}
