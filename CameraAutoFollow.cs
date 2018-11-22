using UnityEngine;
using System.Collections;

public class CameraAutoFollow : MonoBehaviour
{

    private GameObject player;

    [SerializeField] float zCameraOffset;
    [SerializeField] float yCameraOffset;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, zCameraOffset);
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + yCameraOffset, zCameraOffset);
    }
}