using UnityEngine;

namespace ClassLibrary
{
    public class CamaraScript : MonoBehaviour
    {
        public Transform suelo;
        public float velocidadRotacion;
        public float velocidadZoom;

        private void Update()
        {
            Vector3 rotacion = new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
            transform.RotateAround(suelo.position, rotacion, velocidadRotacion*Time.deltaTime);
            transform.LookAt(suelo.transform.position);
        }
    }
}