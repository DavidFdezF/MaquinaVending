using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maquina_Vending {
    internal class Cliente : Usuario {

        public Cliente() { }

        //El cliente no necesita ningún pin para acceder, solo pasa el nombre a la clase padre (usuario)
        public Cliente (string nombre) : base(nombre, "") {}

        //Métodos de la clase abstracta
        public override void ComprarProductos() {
            throw new NotImplementedException();
        }
        public override void MostrarInformacion() {
            throw new NotImplementedException();
        }
        public override void Menu() {
            throw new NotImplementedException();
        }
        public override void Salir() {
            throw new NotImplementedException(); 
        }
    }
}

