using UnityEngine;

public class FollowCamera : MonoBehaviour
{ 

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z));      
    }
}
