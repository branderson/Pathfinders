using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Assets.Managers;

public class EnemyAI : MonoBehaviour
{

    public int ID;
    public int Destination;

    private Waypoint _destination;            // Current waypoint

    public float move_speed = 1.5f;        // The walking speed between waypoints
    bool forward = true;            // direction
    double turn = 6.0;                // Turn speed
    float pause_time = 0;        // Pause at waypoint
    bool pause = false;

    private CharacterController character;
    private float cur_time;

    void Pause() { pause = true; }

    void Unpause() { pause = false; }

    void Reverse() { forward = !forward; }

    // Use this for initialization
    void Start()
    {
        _destination = WaypointManager.Instance.GetWaypoint(Destination);
        character = GetComponent<CharacterController>();

        EventManager.Instance.StartListening("Pause", Pause);
        EventManager.Instance.StartListening("Unpause", Unpause);
        EventManager.Instance.StartListening("Reverse", Reverse);
    }

    // Update is called once per frame
    void Update()
    {
        if (!pause) {
            Patrol();
            Vector3 move = _destination.GetComponent<Transform>().position - transform.position;
            if (move.magnitude < .1)
                ReachedWaypoint();
        }
    }
    private void ReachedWaypoint()
    {
        if (forward)
        {
            Destination = _destination.Next();
            if (Destination < 0)
            {
                Destination *= -1;
                forward = false;
            }
        }
        else
        {
            Destination = _destination.Previous();
            if (Destination < 0)
            {
                Destination *= -1;
                forward = true;
            }
        }
        _destination = WaypointManager.Instance.GetWaypoint(Destination);
    }

    void Patrol()
    {
        Vector3 target = _destination.GetComponent<Transform>().position;
      
        target.y = transform.position.y; // Keep waypoint at character's height
        Vector3 move_dir = target - transform.position;

        Quaternion rotation = Quaternion.LookRotation(target - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * (float)turn);
        character.Move(move_dir.normalized * move_speed * Time.deltaTime);
    }
}
