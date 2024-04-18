using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maquina_Vending {
    internal class Admin : Usuario {

        public Admin() { }
        public Admin(string nombre, string pin) : base(nombre, pin) { }

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
        public override void ComprarProductos() {
            throw new NotImplementedException();
        }
        public override void MostrarInformacion() {
            throw new NotImplementedException();
        }
        public override void Salir() { }
    }
}

