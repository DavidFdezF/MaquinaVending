using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maquina_Vending {
    internal class Cliente : Usuario {

        public Cliente(List<Producto> productos) : base(productos) { }
        public Cliente(string nombre, List<Producto> productos) : base(nombre, productos) { }

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

