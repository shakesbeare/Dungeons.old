using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private GameObject player;
    private Transform playerPosition;
    private Transform cameraPosition;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerPosition = player.GetComponent<Transform>();
        cameraPosition = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            cameraPosition.position = playerPosition.position + new Vector3(0, 0, -10);
        }
    }
}
