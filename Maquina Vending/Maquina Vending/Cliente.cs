using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Maquina_Vending {
    internal class Cliente : Usuario {

        public Cliente(List<Producto> productos) : base(productos) { }
        public Cliente(string nombre, List<Producto> productos) : base(nombre, productos) { }

        //Métodos de la clase abstracta
        public override void ComprarProductos() {
            throw new NotImplementedException();
        }
        public override void ListarProductos() {
            Console.WriteLine(" --- Listado de productos --- ");
            Console.WriteLine();
            foreach (Producto p in listaProductos) {
                Console.WriteLine(p.MostrarInformacion());
            }
        }
        public override void Menu() {
            throw new NotImplementedException();
        }
        public override void Salir() {
            if (listaProductos.Count > 0) {
                File.Create("productos.csv").Close();
                foreach (Producto p in listaProductos) {
                    p.ToFile();
                }
            }
        }
    }
}

