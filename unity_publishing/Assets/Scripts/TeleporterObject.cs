using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(Collider))]
    public class TeleporterObject : MonoBehaviour
    {
        private Teleportation tele;

        [SerializeField] private GameObject targetPositioner;
        private Vector3 targetPosition;

        void Start()
        {
            targetPosition = targetPositioner.transform.position;
        }

        public void Instantiate(Teleportation t)
        {
            tele = t;
        }
        
        void OnTriggerEnter(Collider other)
        {
            tele.Trigger(targetPosition);
        }
    }
}