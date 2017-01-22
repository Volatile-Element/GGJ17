using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Hello. I help during presentations. I'll do abnormal things.
/// </summary>
public class DebugCommander : MonoBehaviour
{
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (!Input.GetKey(KeyCode.LeftControl) && !Input.GetKeyDown(KeyCode.RightControl))
        {
            return;
        }

        foreach (var input in GetDebugInputs())
        {
            if (Input.GetKeyDown(input.Key))
            {
                input.Function();
            }
        }
    }

    private IEnumerable<DebugInput> GetDebugInputs()
    {
        return new[] {
            new DebugInput() { Key = KeyCode.C, Description = "Spawns a city.", Function = SpawnCity },
            new DebugInput() { Key = KeyCode.W, Description = "Finish current wave.", Function = FinishWave },
            new DebugInput() { Key = KeyCode.D, Description = "Destroys all ships.", Function = DestroyAllShips },
            new DebugInput() { Key = KeyCode.G, Description = "Goes to main menu.", Function = GoToMainMenu },
            new DebugInput() { Key = KeyCode.G, Description = "Goes to game over screen.", Function = GoToGameOver },
            new DebugInput() { Key = KeyCode.M, Description = "Goes to Multplayer split screen.", Function = MultiplayerSplitScreen }
        };
    }

    private void MultiplayerSplitScreen()
    {
        FindObjectOfType<CameraControl>().MultiplayerSplitScreen = true;
    }

    private void SpawnCity()
    {
        FindObjectOfType<CitySpawner>().SpawnCity();
    }

    private void FinishWave()
    {
        FindObjectOfType<WaveController>().ForceWaveEnd();
    }

    private void DestroyAllShips()
    {
        var shipSpawner = FindObjectOfType<ShipSpawner>();

        while (shipSpawner.SpawnedShips.Count > 0)
        {
            shipSpawner.DestoryShip(shipSpawner.SpawnedShips[0]);
        }
    }

    private void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    private void GoToGameOver()
    {
        SceneManager.LoadScene("Game Over");
    }
}

public class DebugInput
{
    public delegate void InputDelegate();

    public KeyCode Key;
    public string Description;
    public InputDelegate Function;
}