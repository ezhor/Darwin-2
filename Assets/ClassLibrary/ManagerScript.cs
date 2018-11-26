using UnityEngine;

namespace ClassLibrary
{
    public class ManagerScript : MonoBehaviour
    {
        public GameObject individuoPrefab;
        private static readonly int NUMERO_ADANES = 10;
        private int adanes;

        private void Start()
        {
            Cursor.visible = false;
        }

        private void Update()
        {
            if (adanes < NUMERO_ADANES)
            {
                adanes++;
                Instantiate(individuoPrefab, new Vector3(), Quaternion.identity);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}