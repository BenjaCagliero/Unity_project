using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Range (0f, 100f)]
    public float playerHealth = 100f;
    public void TakeDamage(float amount)
    {
        playerHealth -= amount;

        if (playerHealth <= 0)
        {
            Debug.Log("Game over");
        }
    }
    //public static int skeletonMaxHealth;
    //public static int dummyMinHealth;
    [SerializeField]private int _score;
    [SerializeField]private int _dashes;
    //public int Score => _score;
    //public int TotalDashes => _dashes;

    public void addScore (int scoreTotal)
    {
        _score += scoreTotal;
    }
    public void addDash (int dashtotal)
    {
        _dashes += dashtotal;
    }


    private void Awake()
    {

        _score = 0;
        _dashes = 0;

        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
}
