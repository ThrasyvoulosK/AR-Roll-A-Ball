using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public float angularSpeed;
    public int points;

    public static Action<int> onPickedUp;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, angularSpeed * Time.deltaTime, 0f, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            onPickedUp?.Invoke(points);

            gameObject.SetActive(false);

            Debug.Log("Score: " + ScoreManager.score);
        }
    }
}
