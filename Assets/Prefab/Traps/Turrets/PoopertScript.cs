using UnityEngine;

public class PoopertScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject gunTransform;
    
    private Quaternion gunrotation;
    private Vector3 gunposition;
    
    public GameObject playerTransform;
    public float rotationSpeed;
    public Animator animator;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void LookAt()
    {
        Vector3 direction = playerTransform.transform.position - gunTransform.transform.position;
        gunrotation = Quaternion.Slerp(
            gunrotation,
            Quaternion.LookRotation(direction),
            Time.deltaTime * rotationSpeed
        );
        gunTransform.transform.rotation = gunrotation;


    }
    void LateUpdate()
    {
        float distance = Vector3.Distance(gunTransform.transform.position, playerTransform.transform.position);
        if(distance <= 30)
            LookAt(); 
    }

}
