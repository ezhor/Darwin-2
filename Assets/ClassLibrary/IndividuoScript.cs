using System;
using System.Collections;
using ClassLibrary.model;
using UnityEngine;
using Random = System.Random;

namespace ClassLibrary
{
    public class IndividuoScript : MonoBehaviour
    {
        public GameObject particulasCorazon;
        public GameObject particulasMuerte;
        private Individuo individuo;
        private Vector3 vectorVelocidad;
        private GameObject mesh;
        private Individuo madre;
        private Individuo padre;
        private Vector3 posicionMadre;
        private Boolean haMuerto;

        private void Start()
        {
            if (individuo == null){
                individuo = new Individuo();
                generarPosicion();
            }
            else{
                transform.position = posicionMadre;
            }
            mesh = transform.Find("berd_mesh").gameObject;
            generarAspecto();
            generarVectorVelocidad();
            ajustarRotacion();
            name = "berd " + individuo.Id;
        }

        private void Update()
        {
            if (!haMuerto)
            {
                darPaso();
                comprobarMuerte();                
            }
            else
            {
                transform.localScale = transform.localScale / 1.1F;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            switch (other.gameObject.tag)
            {
                case "Pared":
                    rebotar(other.gameObject);
                    break;
                case "Berd":
                    reproducirse(other.gameObject);
                    break;
            }
        }

        private void generarAspecto()
        {
            Renderer rend = mesh.GetComponent<Renderer>();
            Transform transform = GetComponent<Transform>();
            float rojo = individuo.Velocidad / 100F;
            float verde = individuo.EsperanzaVida / 100F;
            float azul = individuo.Descendencia / 100F;
            rend.material.color = new Color(rojo, verde, azul);
            transform.localScale =
                new Vector3(individuo.Tamano / 100f, individuo.Tamano / 100f, individuo.Tamano / 100f);
        }

        private void generarPosicion()
        {
            Random random = new Random(DateTime.Now.Millisecond);
            float minX = GameObject.Find("Pared izquierda").transform.position.x + 1;
            float maxX = GameObject.Find("Pared derecha").transform.position.x - 1;
            float minZ = GameObject.Find("Pared cerca").transform.position.z + 1;
            float maxZ = GameObject.Find("Pared fondo").transform.position.z - 1;
            float y = GameObject.Find("Suelo").transform.position.y + individuo.Tamano / 200F;
            float x = (float) ((maxX - minX) * random.NextDouble() + minX);
            float z = (float) ((maxZ - minZ) * random.NextDouble() + minZ);

            transform.position = new Vector3(x, y, z);
        }

        private void generarVectorVelocidad()
        {
            Random random = new Random(DateTime.Now.Millisecond);
            float x = (float) random.NextDouble() * 2 - 1;
            float z = (float) (Math.Sqrt(1 - x * x) * (Math.Round(random.NextDouble())*2-1));
            vectorVelocidad = new Vector3(x, 0, z);
        }

        private void darPaso()
        {
            transform.Translate(vectorVelocidad * Time.deltaTime, Space.World);
        }

        private void rebotar(GameObject otherGameObject)
        {
            switch (otherGameObject.name)
            {
                case "Pared izquierda":
                    vectorVelocidad.x *= -1;
                    break;
                case "Pared derecha":
                    vectorVelocidad.x *= -1;
                    break;
                case "Pared cerca":
                    vectorVelocidad.z *= -1;
                    break;
                case "Pared fondo":
                    vectorVelocidad.z *= -1;
                    break;
            }

            ajustarRotacion();
        }

        private void ajustarRotacion()
        {
            float angulo = Vector3.Angle(Vector3.forward, vectorVelocidad);
            if (vectorVelocidad.x < 0)
            {
                angulo = 360 - angulo;
            }

            transform.rotation =
                Quaternion.Euler(transform.rotation.eulerAngles.x, angulo, transform.rotation.eulerAngles.z);
        }

        private void reproducirse(GameObject other)
        {
            IndividuoScript otroScript = other.GetComponent<IndividuoScript>();
            if (esFertil() && otroScript.esFertil())
            {
                otroScript.volverseEsteril();
                this.volverseEsteril();
                mostrarCorazones();
                parirHijo(other);
            }
        }

        private void parirHijo(GameObject other)
        {
            GameObject hijo = Instantiate(gameObject);
            hijo.GetComponent<IndividuoScript>().nacer(individuo, other.GetComponent<IndividuoScript>().individuo, transform.position);
        }

        private void mostrarCorazones()
        {
            particulasCorazon.transform.position = transform.position;
            Instantiate(particulasCorazon);
        }

        public void volverseEsteril()
        {
            individuo.volverseEsteril();
        }

        public bool esFertil()
        {
            return individuo.esFertil();
        }

        public void nacer(Individuo madre, Individuo padre, Vector3 posicion)
        {
            individuo = new Individuo(madre, padre);
            posicionMadre = posicion;
        }

        private void comprobarMuerte()
        {
            if (individuo.haMuerto())
            {
                mostrarCalavera();
                StartCoroutine(destruirseAlTiempo(1));
                haMuerto = true;
            }
        }
        
        private void mostrarCalavera()
        {
            particulasMuerte.transform.position = transform.position;
            particulasMuerte.GetComponent<ParticleSystem>().startColor = mesh.GetComponent<Renderer>().material.color;
            Instantiate(particulasMuerte);
        }

        private IEnumerator destruirseAlTiempo(float segundos)
        {
            yield return new WaitForSeconds(segundos);
            Destroy(gameObject);
        }
    }
}