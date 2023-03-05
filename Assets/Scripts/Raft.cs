using UnityEngine;
using Autohand;

public class Raft : MonoBehaviour
{
    [SerializeField] private Transform wayPoint;
    [SerializeField] private float raftSpeed;

    [SerializeField] private GameObject raft;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject autoHand;

    private Rigidbody rb;
    void Start()
    {
        Player.Instance.autoHandPlayer.maxMoveSpeed = 0f;
        Destroy(autoHand.GetComponent<Rigidbody>());
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, wayPoint.position, Time.deltaTime * raftSpeed);
        player.transform.position = Vector3.MoveTowards(player.transform.position, wayPoint.position, Time.deltaTime * raftSpeed);
    }
}
