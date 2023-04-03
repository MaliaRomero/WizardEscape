using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CustomExtensions;

// Almost everything commented out is from the introducing stacks, queues and hashsets
//chapter, The reason they were commented out was they messed with the item pickups
// making the game impossible to win.

public class GameBehavior : MonoBehaviour //, IManager
{
    public string labelText = "Collect all 4 items and win your freedom!";
    public int maxItems = 4;
    public Stack<string> lootStack = new Stack<string>();
    public bool showWinScreen = false;
    public bool showLossScreen = false;
    private int _itemsCollected = 0;
    private string _state;
    public delegate void DebugDelegate(string newText);
    public DebugDelegate debug = Print;
    public int gameStartScene;

    //public string State
    //{
    //    get { return _state; }
    //    set { _state = value; } 
    //}

    public int Items
    {
        get { return _itemsCollected; }
        set
        {
            _itemsCollected = value;

            if (_itemsCollected >= maxItems)
            {
                labelText = "You've found all the items!";
                showWinScreen = true;
                Time.timeScale = 0f;
            }
            else
            {
                labelText = "Item found, only " + (maxItems - _itemsCollected)
                    + " more to go!";
            }
        }
    }
    private int _playerHP = 3;

    public int HP
    {
        get { return _playerHP; }
        set
        {
            _playerHP = value;

            if (_playerHP <= 0)
            {
                labelText = "You want another life with that?";
                showLossScreen = true;
                Time.timeScale = 0;
            }
            else
            {
                labelText = "Ouch... that's gotta hurt.";
            }
            Debug.LogFormat("Lives: {0}", _playerHP);
        }
    }

    void Start()
    {
        Initialize();
        InventoryList<string> inventoryList = new
            InventoryList<string>();
    }

    public void Initialize()
    {
        _state = "Manager Initialized...";
        _state.FancyDebug();
        // said "fancey" in textbook, typo :0
        debug(_state);
        LogWithDelegate(debug);

        GameObject player = GameObject.Find("Player");
        PlayerBehavior playerBehavior =
            player.GetComponent<PlayerBehavior>();
        playerBehavior.playerJump += HandlePlayerJump;
    }

    public void HandlePlayerJump()
    {
        debug("Player has jumped...");
    }

    public static void Print(string newText)
    {
        Debug.Log(newText);
    }

    public void LogWithDelegate(DebugDelegate del)
    {
        del("Delegating the debug task...");
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1.0f;
    }

    void OnGUI()
    {
        GUI.Box(new Rect(20, 20, 150, 25), "Player Health: " + _playerHP);

        GUI.Box(new Rect(20, 50, 150, 25), "Items Collected: " + _itemsCollected);

        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 50, 300, 50), labelText);

        if (showWinScreen)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (showLossScreen)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }
    }
}
    //public void PrintLootReport()
    //{
    //    var currentItem = lootStack.Pop();
    //    var nextItem = lootStack.Peek();
        // added semicolons, textbook did not
    //    Debug.LogFormat("You got a {0}! You've got a good chance of finding a { 1}next!", currentItem, nextItem);
    //    Debug.LogFormat("There are {0} random loot items waiting for you!", lootStack.Count);
    //}

