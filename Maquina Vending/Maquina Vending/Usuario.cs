using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maquina_Vending {
    internal abstract class Usuario {

        //Definición de los parámetros
        public string Nombre {  get; set; }

        protected List<Producto> listaProductos;

        //COnstructores
        public Usuario() { }
        public Usuario(List<Producto> contenidos) {
            listaProductos = contenidos;
        }

        public Usuario(string nombre, List<Producto> productos) {
            Nombre = nombre;
            listaProductos = productos;
        }

        //Métodos abstractos que van a implementar las clases hijas (Cliente y Admin)
        public abstract void ComprarProductos();
        public abstract void ListarProductos();
        public abstract void Menu();
        public abstract void Salir();
    }
}
