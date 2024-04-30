using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maquina_Vending {
    internal class Program {

        static void Main(string[] args) {
            MaquinaVending maquinaVending = new MaquinaVending();

            int opcion = 0;
            do {
                Console.Clear();
                Console.WriteLine("1. Comprar producto");
                Console.WriteLine("2. Ver información de producto");
                Console.WriteLine("3. Carga individual de producto (Admin)");
                Console.WriteLine("4. Carga completa de producto (Admin)");
                Console.WriteLine("5. Salir");
                Console.Write("Opción: ");
                try {
                    opcion = int.Parse(Console.ReadLine());
                    Console.Clear();
                    switch (opcion) {
                        case 1:
                            maquinaVending.ComprarProducto();
                            break;
                        case 2:
                            maquinaVending.MostrarInformacionProducto();
                            break;
                        case 3:
                            maquinaVending.CargarIndividualProducto();
                            break;
                        case 4:
                            maquinaVending.CargarCompletaProducto();
                            break;
                        case 5:
                            Console.WriteLine("Saliendo...");
                            break;
                        default:
                            Console.WriteLine("Opción no válida. Por favor, ingrese un número válido.");
                            break;
                    }
                }
                catch (FormatException) {
                    Console.WriteLine("Error: Opción inválida. Por favor, ingrese un número válido.");
                }
                catch (Exception ex) {
                    Console.WriteLine("Error: " + ex.Message);
                }
                Console.WriteLine("Presiona una tecla para continuar...");
                Console.ReadKey();
            } while (opcion != 5);
        }
    }
}