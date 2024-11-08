using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManagerScript : MonoBehaviour
{
    public int enemies = 5;
    public Text enemiesText;

    private void Awake()
    {
        // Corrected Tostring to ToString
        enemiesText.text = enemies.ToString();

        // Corrected the typo for the Enemy class
        Enemy.OnEnemyKilled += OnEnemyKilledAction;
    }

    // Event handler for when an enemy is killed
    void OnEnemyKilledAction()
    {
        enemies--; // Decrease the enemy count
        enemiesText.text = enemies.ToString(); // Update the UI text
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event to avoid memory leaks
        Enemy.OnEnemyKilled -= OnEnemyKilledAction;
    }
}

