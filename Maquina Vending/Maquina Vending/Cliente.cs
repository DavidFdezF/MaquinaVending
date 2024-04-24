﻿using System;
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
            List<Producto> productosComprados = new List<Producto>();
            bool seguirComprando = true;
            while (seguirComprando) {
                Console.Clear();
                // Mostrar lista de productos disponibles
                Console.WriteLine("Productos disponibles:");
                foreach (Producto producto in listaProductos) {
                    Console.WriteLine($"ID: {producto.ID}, Nombre: {producto.Nombre}, Unidades disponibles: {producto.Unidades}, Precio: {producto.PrecioUnidad}");
                }

                // Solicitar al usuario el ID del producto que desea comprar
                Console.Write("Ingrese el ID del producto que desea comprar: ");
                int idProducto = int.Parse(Console.ReadLine());

                // Buscar el producto en la lista de productos disponibles
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

                    // Preguntar al usuario si desea elegir otro producto
                    Console.WriteLine("¿Desea elegir otro producto?");
                    Console.WriteLine("1. Sí");
                    Console.WriteLine("2. No");
                    Console.Write("Opción: ");
                    int opcion = int.Parse(Console.ReadLine());
                    seguirComprando = (opcion == 1);
                }
                else {
                    Console.WriteLine("\nID de producto no válido.");
                }




                //Pago
                int opcionPago = 0;
                do {
                    Console.Clear();
                    Console.WriteLine("Seleccione la forma de pago:");
                    Console.WriteLine("1. Pago en efectivo");
                    Console.WriteLine("2. Pago con tarjeta");
                    Console.WriteLine("3. Salir");
                    Console.Write("Opción: ");
                    try {
                        opcionPago = int.Parse(Console.ReadLine());

                        // Procesar el pago según la opción seleccionada
                        switch (opcionPago) {
                            case 1:
                                // Proceso de pago en efectivo
                                Console.WriteLine("\nProceso de pago en efectivo...");
                                double totalPago = 0;
                                foreach (Producto producto in productosComprados) {
                                    totalPago += producto.PrecioUnidad;
                                }
                                Console.WriteLine($"\nEl total a pagar es: {totalPago}");

                                double cantidadPagada = 0;
                                while (cantidadPagada < totalPago) {
                                    Console.Write("\nIngrese la cantidad a pagar: ");
                                    double moneda = double.Parse(Console.ReadLine());
                                    cantidadPagada += moneda;
                                    Console.WriteLine($"Cantidad pagada: {cantidadPagada}");
                                }

                                if (cantidadPagada >= totalPago) {
                                    Console.WriteLine("\nPago completado. Dispensando producto...");
                                    foreach (Producto producto in productosComprados) {
                                        producto.Unidades -= 1;
                                    }
                                }
                                else {
                                    Console.WriteLine("\nPago incompleto. La compra ha sido cancelada.");
                                }
                                break;
                            case 2:
                                // Proceso de pago con tarjeta
                                Console.WriteLine("\nProceso de pago con tarjeta...");
                                Console.Write("Ingrese el número de tarjeta: ");
                                string numeroTarjeta = Console.ReadLine();
                                Console.Write("Ingrese la fecha de caducidad (MM/AA): ");
                                string fechaCaducidad = Console.ReadLine();
                                Console.Write("Ingrese el código de seguridad: ");
                                string codigoSeguridad = Console.ReadLine();

                                Console.WriteLine("\nPago completado. Dispensando producto...");
                                foreach (Producto producto in productosComprados) {
                                    producto.Unidades -= 1;
                                }
                                break;
                            case 3:
                                Console.WriteLine("\nSaliendo...");
                                break;
                            default:
                                Console.WriteLine("\nOpción de pago no válida.");
                                break;
                        }
                    }
                    catch (FormatException) {
                        Console.WriteLine("Error: Opción inválida. Por favor ingrese un número válido. ");
                        Console.ReadKey();
                    }
                    catch (Exception ex) {
                        Console.WriteLine("Error: " + ex.Message);
                        Console.ReadKey();
                    }
                    Console.WriteLine("\nPresiona una tecla para volver al menú...");
                    Console.ReadLine();
                } while (opcionPago != 3);
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

