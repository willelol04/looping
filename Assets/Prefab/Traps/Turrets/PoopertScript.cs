using UnityEngine;

public class PoopertScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject gunTransform;
    public GameObject playerTransform;
    public float rotationSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(gunTransform.transform.position, playerTransform.transform.position);
        Debug.Log("Distance to player: " + distance);

        if(distance <= 30)
            LookAt();
    }

    private void LookAt()
    {
        Vector3 direction = playerTransform.transform.position - gunTransform.transform.position;
        gunTransform.transform.rotation = Quaternion.Slerp(
            gunTransform.transform.rotation,
            Quaternion.LookRotation(direction),
            Time.deltaTime * rotationSpeed
        );


    }
}
