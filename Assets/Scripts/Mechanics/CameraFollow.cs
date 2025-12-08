using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{

    [SerializeField] private float minXPos = -4.5f;
    [SerializeField] private float maxXPos = 233.3f;

    [SerializeField] private Transform target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!target)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (!player)
            {
                Debug.LogError("CameraFollow: No GameObject with tag 'Player' found in the scene.");
                return;
            }

            target = player.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //store our target's position
        if (!target) return;
        Vector3 pos = transform.position;
        //only update the x position of the camera
        pos.x = Mathf.Clamp(target.position.x, minXPos, maxXPos);
        //apply the position back to the camera
        transform.position = pos;
    }
}
