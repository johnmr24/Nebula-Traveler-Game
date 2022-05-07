using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    [Header("Set In Inspector")]
    public GameObject[] levelPieces;
    public Text goText;
    public GameObject player;
    public float levelDistance = 100f;
    public GameObject leftWall;

    public int score = 0;
    public Text scoreText;

    private float totalLength = 0;
    private float milestoneDistance = 0;
    public static Main S;
    private void Awake()
    {
        S = this;
    }
    void Start()
    {
        milestoneDistance = levelDistance * 2;
        totalLength = levelDistance;
        foreach (var piece in levelPieces)
        {
            GameObject part = Instantiate(piece);
            part.transform.position = new Vector3(totalLength, 0);
            totalLength += levelDistance;
        }

        //Start the coroutine.
        StartCoroutine(GameStart());

        scoreText.text = "SCORE: " + score;
    }

    private void FixedUpdate()
    {
        if (player.transform.position.x > milestoneDistance)
        {
            milestoneDistance += levelDistance;
            AddScore(10);
        }

        if (player.transform.position.x > (totalLength - (levelPieces.Length * levelDistance)))
        {
            score++;
            GameObject part = Instantiate(levelPieces[(int)(Random.Range(0,levelPieces.Length))]);
            part.transform.position = new Vector3(totalLength, 0);
            totalLength += levelDistance;
            leftWall.transform.position = new Vector3(leftWall.transform.position.x + levelDistance, 0);

            if (EnemySpawner.E.spawnRate > 1f)
            {
                EnemySpawner.E.spawnRate -= .5f;
            }
            
            GameObject[] items = GameObject.FindGameObjectsWithTag("Ground");
            foreach (GameObject item in items)
            {
                if (item.transform.position.x < (totalLength - (levelPieces.Length * levelDistance + (2 * levelDistance))))
                {
                    Destroy(item);
                }
            }
            items = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject item in items)
            {
                if (item.transform.position.x < (totalLength - (levelPieces.Length * levelDistance + (2 * levelDistance))))
                {
                    Destroy(item);
                }
            }
        }
    }
    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(2);
        goText.enabled = true;
        yield return new WaitForSeconds(2);
        goText.enabled = false;
    }

    public void AddScore(int value)
    {
        score += value;
        scoreText.text = "SCORE: " + score;
    }
}
