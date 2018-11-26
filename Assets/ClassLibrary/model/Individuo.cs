using System;
using Random = System.Random;

namespace ClassLibrary.model
{
    public class Individuo
    {
        private static int ultimoId;
        private byte tamano;
        private byte velocidad;
        private byte esperanzaVida;
        private byte descendencia;
        private DateTime finEsterilidad;
        private DateTime muerte;
        private int id;

        public Individuo()
        {
            individuoAleatorio();
            setDerivadas();
        }
        
        public Individuo(Individuo madre, Individuo padre)
        {
            tamano = Convert.ToByte((madre.tamano + padre.tamano)/ 2);
            velocidad = Convert.ToByte((madre.velocidad + padre.velocidad)/ 2);
            esperanzaVida = Convert.ToByte((madre.esperanzaVida + padre.esperanzaVida)/ 2);
            descendencia = Convert.ToByte((madre.descendencia + padre.descendencia)/ 2);
            setDerivadas();
        }

        public byte Tamano
        {
            get { return tamano; }
            set { tamano = value; }
        }

        public byte Velocidad
        {
            get { return velocidad; }
            set { velocidad = value; }
        }

        public byte EsperanzaVida
        {
            get { return esperanzaVida; }
            set { esperanzaVida = value; }
        }

        public byte Descendencia
        {
            get { return descendencia; }
            set { descendencia = value; }
        }
        
        public int Id
        {
            get { return id; }
        }

        private void setDerivadas()
        {
            ultimoId++;
            id = ultimoId;
            muerte = DateTime.Now.AddSeconds(esperanzaVida);
        }

        private void individuoAleatorio()
        {
            Random random = new Random(DateTime.Now.Millisecond);
            int randNum;
            while (tamano+velocidad+esperanzaVida+descendencia < 100)
            {
                randNum = random.Next(1, 5);
                switch (randNum)
                {
                        case 1:
                            tamano++;
                            break;
                        case 2:
                            velocidad++;
                            break;
                        case 3:
                            esperanzaVida++;
                            break;
                        case 4:
                            descendencia++;
                            break;
                }
            }
        }

        public void volverseEsteril()
        {
            finEsterilidad = DateTime.Now.AddSeconds(100 - descendencia);
        }

        public bool esFertil()
        {
            return finEsterilidad < DateTime.Now;
        }

        public bool haMuerto()
        {
            return muerte < DateTime.Now;
        }

    }
}