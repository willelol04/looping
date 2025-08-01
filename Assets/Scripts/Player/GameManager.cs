using Unity.Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Camera;
    public GameObject player;

    private static bool gamePaused = false;
    private static bool canMove = true;
    
    public float detectionTime = 1;
    public CinemachineCamera camera_VM;

   
    private void Start()
    {
        //camera_VM.Follow = player.transform.GetChild(0);
    }

    public static bool GamePaused
    {
        get { return gamePaused; }
        set { gamePaused = value; }
    }
    public static bool CanMove
    {
        get { return canMove; }
        set { canMove = value; }
    }

    public void SpawnPlayer()
    {
        if (player != null)
            return;
        camera_VM.Follow = player.transform.GetChild(0);
    }
    private void Update()
    {
            SpawnPlayer();
    }


}
