using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameSettings : MonoBehaviour
{
    public Scriptable settings;
    #region Singleton
    public static GameSettings Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
        {
            Instance = this;
        }
    }
    #endregion
} 
#region Tags Class
public static class Tags
{
    public const string knifeTag = "Knife";
    public const string slicableTag = "Slicable";
    public const string groundTag = "Ground";
    public const string gameOverTag = "GameOver";
    public const string levelEndTag = "LevelEnd";
}
#endregion
