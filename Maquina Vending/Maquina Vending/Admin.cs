using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maquina_Vending {
    internal class Admin : Usuario {

        public Admin() { }
        public Admin(List <Producto> productos) : base(productos) { }
        public Admin(string nombre, string pin, List<Producto> productos) : base(nombre, pin, productos) { }

        public override void Menu() {
            int opcion = 0;
            do {
                Console.Clear();
                Console.WriteLine("1. Añadir producto");
                Console.WriteLine("2. Eliminar producto por ID");
                Console.WriteLine("3. Listar productos");
                Console.WriteLine("4. Salir");
                Console.Write("Elige una opción: ");
                try {
                    opcion = int.Parse(Console.ReadLine());
                    switch (opcion) {
                        case 1:
                            Console.WriteLine("");
                            break;
                        case 2:
                            Console.WriteLine("");
                            /*Console.Write("ID del contenido a eliminar: ");
                            int id_contenido = int.Parse(Console.ReadLine());
                            Contenido contenidoTemp = BuscarContenido(id_contenido);
                            EliminarContenido(contenidoTemp);*/
                            break;
                        case 3:
                            Console.WriteLine("");
                            break;
                        case 4:
                            Salir();
                            Console.WriteLine("Saliendo...");
                            break;
                        default:
                            Console.WriteLine("Opción no válida");
                            break;
                    }
                }
                catch (FormatException) {
                    Console.WriteLine("Error: Opción inválida. Por favor ingrese un número válido. ");
                }
                catch (Exception ex) {
                    Console.WriteLine("Error: " + ex.Message);
                }
                Console.ReadKey();
            } while (opcion != 4);
        }

        public void AddProducto() {
            int opcion = 0;
            do {
                Console.Clear();
                Console.WriteLine("1. Nuevo material precioso");
                Console.WriteLine("2. Nuevo producto alimenticio");
                Console.WriteLine("3. Nuevo producto electrónico");
                Console.WriteLine("4. Volver al menú principal");
                Console.Write("Introduzca la opción: ");
                try {
                    opcion = int.Parse(Console.ReadLine());
                    switch (opcion) {
                        case 1:
                            MaterialesPreciosos p1 = new MaterialesPreciosos(listaProductos);
                            p1.SolicitarDetalles();
                            listaProductos.Add(p1);
                            break;
                        case 2:
                            ProductoAlimenticio p2 = new ProductoAlimenticio(listaProductos);
                            p2.SolicitarDetalles();
                            listaProductos.Add(p2);
                            break;
                        case 3:
                            ProductoElectronico p3 = new ProductoElectronico(listaProductos);
                            p3.SolicitarDetalles();
                            listaProductos.Add(p3);
                            break;
                        default:
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
                Console.WriteLine("Presiona una tecla para continuar...");
            } while (opcion != 4);
        }










        public override void ComprarProductos() {
            throw new NotImplementedException();
        }
        public override void MostrarInformacion() {
            throw new NotImplementedException();
        }
        public override void Salir() { }
    }
}

