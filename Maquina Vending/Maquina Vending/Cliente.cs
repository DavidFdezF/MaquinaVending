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
            //Mostramos productos disponibles
            Console.WriteLine("-- Productos Disponibles --");
            foreach (Producto producto in listaProductos) {
                Console.WriteLine($"ID: {producto.ID}, Nombre: {producto.Nombre}, Unidades: {producto.Unidades}, " +
                    $"Precio: {producto.PrecioUnidad}");
            }
            //Almacenamos los productos 
            List<Producto> productosComprados = new List<Producto>();

            // Bucle para que el cliente seleccione los productos que desea comprar
            bool seguirComprando = true;
            while (seguirComprando) {
                Console.Write("Ingrese el ID del producto que desea comprar o '0' para terminar: ");
                int idProducto = int.Parse(Console.ReadLine());

                if (idProducto == 0) {
                    seguirComprando = false;
                    continue;
                }
                Producto productoSeleccionado = null;
                foreach (Producto producto in listaProductos) {
                    if (producto.ID == idProducto) {
                        productoSeleccionado = producto;
                        break;
                    }
                }

                if (productoSeleccionado != null) {
                    productosComprados.Add(productoSeleccionado);
                    Console.WriteLine($"Producto '{productoSeleccionado.Nombre}' agregado al carrito.");
                }
                else {
                    Console.WriteLine("ID de producto no válido.");
                }
            }
            //Pago
            Console.WriteLine("Seleccione la forma de pago:");
            Console.WriteLine("1. Pago en efectivo");
            Console.WriteLine("2. Pago con tarjeta");
            Console.Write("Opción: ");
            int opcionPago = int.Parse(Console.ReadLine());

            // Procesar el pago según la opción seleccionada
            switch (opcionPago) {
                case 1:
                    // Proceso de pago en efectivo
                    Console.WriteLine("Proceso de pago en efectivo...");
                    break;
                case 2:
                    // Proceso de pago con tarjeta
                    Console.WriteLine("Proceso de pago con tarjeta...");
                    break;
                default:
                    Console.WriteLine("Opción de pago no válida.");
                    break;
            }
        }
        public override void MostrarInformacionProducto() {
            // Mostrar los productos disponibles
            Console.WriteLine("Productos disponibles:");
            foreach (Producto producto in listaProductos) {
                Console.WriteLine($"ID: {producto.ID}, Nombre: {producto.Nombre}, Unidades: {producto.Unidades}, Precio: {producto.PrecioUnidad}");
            }

            // Pedir al usuario el ID del producto que desea ver
            Console.Write("Ingrese el ID del producto que desea ver: ");
            int idProducto = int.Parse(Console.ReadLine());

            // Buscar el producto en la lista y mostrar su información si existe
            Producto productoMostrado = null;
            foreach (Producto producto in listaProductos) {
                if (producto.ID == idProducto) {
                    productoMostrado = producto;
                    break;
                }
            }

            if (productoMostrado != null) {
                Console.WriteLine($"Nombre: {productoMostrado.Nombre}");
                Console.WriteLine($"Precio: {productoMostrado.PrecioUnidad}");
                Console.WriteLine($"Descripción: {productoMostrado.Descripcion}");
                Console.WriteLine($"Cantidad disponible: {productoMostrado.Unidades}");
            }
            else {
                Console.WriteLine("ID de producto no válido.");
            }
        }
    }
}

