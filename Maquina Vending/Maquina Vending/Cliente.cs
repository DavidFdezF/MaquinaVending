using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Maquina_Vending
{
    internal class Cliente : Usuario
    {

        public Cliente(List<Producto> productos) : base(productos) { }
        public Cliente(string nombre, List<Producto> productos) : base(nombre, productos) { }

        //Métodos de la clase abstracta
        public override void ComprarProductos() {
            // Mostramos productos disponibles
            List<Producto> productosComprados = new List<Producto>();

            while (true) {
                Console.Clear();
                // Mostrar lista de productos disponibles
                Console.WriteLine("Productos disponibles:");
                foreach (Producto producto in listaProductos)
                {
                    Console.WriteLine($"ID: {producto.ID}\n\tNombre: {producto.Nombre}\n\tUnidades disponibles: {producto.Unidades}\n\t" +
                                      $"Precio: {producto.PrecioUnidad}");
                }

                // Solicitar al usuario la acción que desea realizar
                Console.WriteLine("\n¿Qué deseas hacer?");
                Console.WriteLine("1. Comprar un producto (ingresa el ID)");
                Console.WriteLine("2. Ver carrito de compras");
                Console.WriteLine("3. Terminar compra");
                Console.WriteLine("4. Salir");
                Console.Write("Opción: ");
                string opcion = Console.ReadLine();

                switch (opcion) {
                    case "1":
                        // Lógica para comprar un producto
                        Console.Write("\nIngrese el ID del producto que desea comprar: ");
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
                            if (productoSeleccionado.Unidades <= 0) {
                                Console.WriteLine("El producto no está disponible en este momento.");
                                Console.WriteLine("\nPresiona una tecla para continuar");
                                Console.ReadLine();
                                continue;
                            }

                            productosComprados.Add(productoSeleccionado);
                            Console.WriteLine($"\nProducto '{productoSeleccionado.Nombre}' agregado al carrito.");

                            // Actualizar unidades disponibles del producto
                            productoSeleccionado.Unidades -= 1;

                            // Preguntar al usuario si desea continuar comprando
                            Console.Write("\n¿Desea continuar comprando? (s/n): ");
                            string continuar = Console.ReadLine();
                            if (continuar != "s") {
                                RealizarPago(productosComprados);
                                return;
                            }
                        }
                        else {
                            Console.WriteLine("\nID de producto no válido.");
                            Console.WriteLine("\nPresiona una tecla para continuar");
                            Console.ReadLine();
                        }
                        break;
                    case "2":
                        // Lógica para ver carrito de compras
                        Console.Clear();
                        Console.WriteLine("Carrito de compras:");
                        if (productosComprados.Count > 0) {
                            foreach (Producto producto in productosComprados) {
                                Console.WriteLine($"ID: {producto.ID}\n\tNombre: {producto.Nombre}\n\tPrecio: {producto.PrecioUnidad}");
                            }
                        }
                        else {
                            Console.WriteLine("El carrito de compras está vacío.");
                        }
                        Console.WriteLine("\nPresiona una tecla para volver al menú...");
                        Console.ReadLine();
                        break;
                    case "3":
                        RealizarPago(productosComprados);
                        break;
                    case "4":
                        Salir();
                        Console.WriteLine("\nSaliendo...");
                        return;
                    default:
                        Console.WriteLine("\nOpción no válida. Por favor, intenta de nuevo.");
                        Console.WriteLine("\nPresiona una tecla para continuar");
                        Console.ReadLine();
                        break;
                }
            }
        }

        public void RealizarPago(List<Producto> productosComprados) {
            // Pago
            int opcionPago = 0;
                do {
                    Console.Clear();
                    Console.WriteLine("Seleccione la forma de pago:");
                    Console.WriteLine("1. Pago en efectivo");
                    Console.WriteLine("2. Pago con tarjeta");
                    Console.WriteLine("3. Cancelar compra por ID");
                    Console.WriteLine("4. Salir");
                    Console.Write("Opción: ");
                    try {
                        opcionPago = int.Parse(Console.ReadLine());

                        // Procesar el pago según la opción seleccionada
                        switch (opcionPago) {
                            case 1:
                            if (productosComprados.Count > 0) {
                                // Proceso de pago en efectivo
                                Console.WriteLine("\nProceso de pago en efectivo...");
                                double totalPago = 0;
                                foreach (Producto producto in productosComprados) {
                                    totalPago += producto.PrecioUnidad;
                                }
                                Console.WriteLine($"\nEl total a pagar es: {totalPago}");

                            double cantidadPagada = 0;
                            while (cantidadPagada < totalPago)
                            {
                                Console.Write("\nIngrese la cantidad a pagar: ");
                                double moneda = double.Parse(Console.ReadLine());
                                cantidadPagada += moneda;
                                Console.WriteLine($"Cantidad pagada: {cantidadPagada}");
                            }

                                if (cantidadPagada >= totalPago) {
                                    Console.WriteLine("\nPago completado. Dispensando producto...");
                                    productosComprados.Clear();
                                }
                                else {
                                    Console.WriteLine("\nPago incompleto. La compra ha sido cancelada.");
                                }
                            }
                            else {
                                Console.WriteLine("El carrito de compra está vacío");
                            }
                                break;
                            case 2:
                            if (productosComprados.Count > 0) {
                                // Proceso de pago con tarjeta
                                Console.WriteLine("\nProceso de pago con tarjeta...");
                                Console.Write("Ingrese el número de tarjeta: ");
                                string numeroTarjeta = Console.ReadLine();
                                Console.Write("Ingrese la fecha de caducidad (MM/AA): ");
                                string fechaCaducidad = Console.ReadLine();
                                Console.Write("Ingrese el código de seguridad: ");
                                string codigoSeguridad = Console.ReadLine();

                                Console.WriteLine("\nPago completado. Dispensando producto...");
                                productosComprados.Clear();
                            }
                            else {
                                Console.WriteLine("El carrito de compra está vacío");
                            }
                                break;
                            case 3:
                                // Cancelar compra por ID
                                Console.Write("\nIngrese el ID del producto que desea cancelar: ");
                                int idCancelar = int.Parse(Console.ReadLine());
                                Producto productoCancelar = null;
                                foreach (Producto producto in productosComprados) {
                                    if (producto.ID == idCancelar) {
                                        productoCancelar = producto;
                                        break;
                                    }
                                }
                            if (productoCancelar != null) {
                                    productoCancelar.Unidades += 1;
                                    productosComprados.Remove(productoCancelar);
                                    Console.WriteLine("\nProducto cancelado con éxito.");
                                }
                                else {
                                    Console.WriteLine("\nID de producto no válido.");
                                }
                                break;
                            case 4:
                                Salir();
                                Console.WriteLine("Saliendo...");
                                Console.WriteLine("Presione una tecla para continuar");
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
                    Console.ReadLine();
                } while (opcionPago != 4);
            }

        public override void MostrarInformacionProducto()
        {
            // Mostrar los productos disponibles
            Console.WriteLine("Productos disponibles:");
            foreach (Producto producto in listaProductos)
            {
                Console.WriteLine($"ID: {producto.ID}, Nombre: {producto.Nombre}, Unidades: {producto.Unidades}, Precio: {producto.PrecioUnidad}");
            }

            // Pedir al usuario el ID del producto que desea ver
            Console.Write("Ingrese el ID del producto que desea ver: ");
            int idProducto = int.Parse(Console.ReadLine());

            // Buscar el producto en la lista y mostrar su información si existe
            Producto productoMostrado = null;
            foreach (Producto producto in listaProductos)
            {
                if (producto.ID == idProducto)
                {
                    productoMostrado = producto;
                    break;
                }
            }

            if (productoMostrado != null)
            {
                if (productoMostrado is MaterialesPreciosos)
                {
                    MaterialesPreciosos materialPrecioso = (MaterialesPreciosos)productoMostrado;
                    Console.WriteLine($"ID: {materialPrecioso.ID}");
                    Console.WriteLine($"Nombre: {materialPrecioso.Nombre}");
                    Console.WriteLine($"Unidades: {materialPrecioso.Unidades}");
                    Console.WriteLine($"Precio: {materialPrecioso.PrecioUnidad}");
                    Console.WriteLine($"Tipo de Material: {materialPrecioso.TipoMaterial}");
                    Console.WriteLine($"Peso: {materialPrecioso.Peso:F2} kg");
                }
                else if (productoMostrado is ProductoAlimenticio)
                {
                    ProductoAlimenticio productoAlimenticio = (ProductoAlimenticio)productoMostrado;
                    Console.WriteLine($"ID: {productoAlimenticio.ID}");
                    Console.WriteLine($"Nombre: {productoAlimenticio.Nombre}");
                    Console.WriteLine($"Unidades: {productoAlimenticio.Unidades}");
                    Console.WriteLine($"Precio: {productoAlimenticio.PrecioUnidad}");
                    Console.WriteLine($"Información Nutricional: {productoAlimenticio.InformacionNutricional}");
                }
                else if (productoMostrado is ProductoElectronico)
                {
                    ProductoElectronico productoElectronico = (ProductoElectronico)productoMostrado;
                    Console.WriteLine($"ID: {productoElectronico.ID}");
                    Console.WriteLine($"Nombre: {productoElectronico.Nombre}");
                    Console.WriteLine($"Unidades: {productoElectronico.Unidades}");
                    Console.WriteLine($"Precio: {productoElectronico.PrecioUnidad}");
                    Console.WriteLine($"Tipo de materiales utilizados: {productoElectronico.TipoMaterial}");
                    Console.WriteLine($"¿Tiene pilas? {productoElectronico.Pilas}");
                    Console.WriteLine($"¿Está precargado? {productoElectronico.PreCargado}");
                }
            }
            else
            {
                Console.WriteLine("No se encontró ningún producto con el ID especificado.");
            }
        }

        public override void Salir()
        {
            if (listaProductos.Count > 0)
            {
                File.Create("productos.csv").Close();
                foreach (Producto p in listaProductos)
                {
                    p.ToFile();
                }
            }
        }
    }
}

