using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class StartScenePanelTests
{
    [OneTimeSetUp]
     public void LoadScene()
     {
         SceneManager.LoadScene("StartScene");
     }
     [UnityTest]
     public IEnumerator VerifySceneLoadedCorrectly()
     {
        var check = GameObject.Find("StartScreenController").GetComponent<StartScreenController>();
        Assert.That(check, Is.Not.Null);
        var tag = "StartScreenController";
        Assert.That(check.name, Is.EqualTo(tag));
        yield return null;
     }
    [UnityTest]
     public IEnumerator VerifyMainCameraWork()
     {
        var check = GameObject.Find("Main Camera");
        Assert.That(check, Is.Not.Null);
        var tag = "Main Camera";
        Assert.That(check.name, Is.EqualTo(tag));
        yield return null;
     }
}
