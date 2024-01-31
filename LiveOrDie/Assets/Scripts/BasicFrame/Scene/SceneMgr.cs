using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

#region Scene Manager
//Scene Manager has two encapsulated load methods 

//Both Load Scene sync and async allows user to pass in a call back function that is called after loading
//Load async trigger a event called ProgressBar with the progress value 
#endregion
public class SceneMgr : Singleton<SceneMgr>
{
    
    /// <summary>
    /// Load scene synchronously
    /// It may lag. Call afterLoad after scene loaded
    /// </summary>
    /// <param name="sceneName">Scene Name</param>
    /// <param name="afterLoad">Function called after scene loaded</param>
    /// <param name="loadSceneMode">load mode, default is normal mode</param>
    public void LoadScene(string sceneName, UnityAction afterLoad, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
    {
        SceneManager.LoadScene(sceneName,loadSceneMode);
        afterLoad(); //call after loading completed
    }

    /// <summary>
    /// Load scene asynchronously
    /// </summary>
    /// <param name="sceneName">Scene Name</param>
    /// <param name="afterLoad">Function called after scene loaded</param>
    /// <param name="loadSceneMode">load mode, default is normal mode</param>
    public void LoadSceneAsync(string sceneName, UnityAction afterLoad, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
    {
        MonoMgr.Instance.StartCoroutine(LoadSceneAsyncCoroutine(sceneName, afterLoad,loadSceneMode));
    }

    private IEnumerator LoadSceneAsyncCoroutine(string sceneName, UnityAction afterLoad, LoadSceneMode loadSceneMode) //协程函数
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName,loadSceneMode);

        while (!ao.isDone)
        {
            EventMgr.Instance.EventTrigger("ProgressBar", ao.progress); //trigger event every time it loads, passing new progress value

            yield return ao.progress; //update progress value 
        }

        afterLoad(); //call after loading completed
    }
}
