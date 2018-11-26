using System.Collections;
using UnityEngine;

namespace ClassLibrary
{
    public class Destructor : MonoBehaviour
    {
        void Start()
        {
            StartCoroutine(destruir(10));
        }

        private IEnumerator destruir(int segundos)
        {
            yield return new WaitForSeconds(segundos);
            Destroy(gameObject);
        }
    }
}