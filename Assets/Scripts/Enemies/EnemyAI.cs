using Assets.LevelElements;
using Assets.Managers;
using Assets.Utility;
using Assets.VR;
using UnityEngine;

namespace Assets.Enemies
{
    public class EnemyAI : CustomMonoBehaviour, IAddressable
    {
        [SerializeField] private int _id;
        [SerializeField] private float move_speed = 1.5f;        // The walking speed between waypoints
        public int Destination;

        private Waypoint _destination;            // Current waypoint

        bool forward = true;            // direction
        double turn = 6.0;                // Turn speed
//        float pause_time = 0;        // Pause at waypoint
        bool pause = false;

        private CharacterController character;
        private float cur_time;

        void Pause() { pause = true; }

        void Unpause() { pause = false; }

        void Reverse()
        {
            forward = !forward;
            ReachedWaypoint();
        }

        public void SetSpeed(long speed)
        {
            switch (speed)
            {
                case 1:
                    move_speed = .5f;
                    break;
                case 2:
                    move_speed = 1.5f;
                    break;
                case 3:
                    move_speed = 3f;
                    break;
            }
        }

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private void Start()
        {
            _destination = WaypointManager.Instance.GetWaypoint(Destination);
            character = GetComponent<CharacterController>();

            EventManager.Instance.StartListening("Pause", Pause);
            EventManager.Instance.StartListening("Unpause", Unpause);
            EventManager.Instance.StartListening("Reverse" + _id, Reverse);
            EventManager.Instance.StartListening("EnemySpeed" + _id, SetSpeed);
        }

        private void Update()
        {
            if (pause) return;
            Patrol();
        }

        private void ReachedWaypoint()
        {
//            Debug.Log("Enemy " + _id + " reached waypoint " + _destination._id);
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

        public void WaypointTouched(Waypoint waypoint)
        {
            if (waypoint == _destination) ReachedWaypoint();
        }

        private void OnTriggerEnter(Collider collider)
        {
            // Collision on attack radius
            VRPlayerController player = collider.GetComponentInParent<VRPlayerController>();
            if (player == null) return;
            player.Die();
        }

        private void Patrol()
        {
            Vector3 target = _destination.transform.position;
      
            target.y = transform.position.y; // Keep waypoint at character's height
            Vector3 move_dir = target - transform.position;

            Quaternion rotation = Quaternion.LookRotation(target - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * (float)turn);
            character.Move(move_dir.normalized * move_speed * Time.deltaTime);
        }
    }
}
