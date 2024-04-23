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
            Console.WriteLine("Productos disponibles:");
            foreach (Producto producto in listaProductos) {
                Console.WriteLine($"ID: {producto.ID}, Nombre: {producto.Nombre}");
            }

            List<Producto> productosComprados = new List<Producto>();

            bool seguirComprando = true;
            while (seguirComprando) {
                Console.Write("Ingrese el ID del producto que desea comprar: ");
                int idProducto = int.Parse(Console.ReadLine());

                Producto productoSeleccionado = listaProductos.FirstOrDefault(p => p.ID == idProducto);
                if (productoSeleccionado != null) {
                    productosComprados.Add(productoSeleccionado);
                    Console.WriteLine($"Producto '{productoSeleccionado.Nombre}' agregado al carrito.");
                }
                else {
                    Console.WriteLine("ID de producto no válido.");
                }

                Console.Write("¿Desea comprar otro producto? (S/N): ");
                string respuesta = Console.ReadLine().ToUpper();

                if (respuesta != "S") {
                    seguirComprando = false;
                }
            }
            Console.WriteLine("Proceso de pago...");
        }
        public override void ListarProductos() {
            Console.WriteLine("Productos disponibles:");
            foreach (Producto producto in listaProductos) {
                Console.WriteLine($"ID: {producto.ID}, Nombre: {producto.Nombre}");
            }

            Console.Write("Ingrese el ID del producto que desea ver: ");
            int idProducto = int.Parse(Console.ReadLine());

            Producto productoMostrado = null;
            foreach (Producto producto in listaProductos) {
                if (producto.ID == idProducto) {
                    productoMostrado = producto;
                    break;
                }
                else {
                    Console.WriteLine("ID de producto no válido.");
                }
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

