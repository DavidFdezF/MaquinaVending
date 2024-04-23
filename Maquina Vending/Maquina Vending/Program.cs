﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maquina_Vending {
    internal class Program {

        static List<Producto> listaProductos;
        static void Main(string[] args) {

            listaProductos = new List<Producto>();

            Admin admin = new Admin(listaProductos, "admin");

            CargarContenidosDeArchivo();

            int opcion = 0;
            do {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("1. Comprar producto");
                Console.WriteLine("2. Ver información de producto");
                Console.WriteLine("3. Carga individual de producto (Admin)");
                Console.WriteLine("4. Carga  completa de producto (Admin)");
                Console.WriteLine("5. Salir");
                Console.Write("Opción: ");
                try {
                    opcion = int.Parse(Console.ReadLine());
                    Console.Clear();
                    switch (opcion) {
                        case 1:

                            break;
                        case 2:

                            break;
                        case 3:
                            admin.Menu();
                            break;
                        case 4:
                            admin.CargaCompleta();
                            break;
                        default:
                            Console.WriteLine("Opcion no valida");
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


        public static bool CargarContenidosDeArchivo() {
            bool contenidosCargados = false;
            try {
                if (File.Exists("productos.csv")) {
                    StreamReader sr = new StreamReader("productos.csv");
                    string linea;
                    while ((linea = sr.ReadLine()) != null) {
                        contenidosCargados = true;
                        string[] datos = linea.Split(';');
                        if (datos[7] == "MaterialesPreciosos") {
                            MaterialesPreciosos p = new MaterialesPreciosos(int.Parse(datos[0]), datos[1], int.Parse(datos[2]), double.Parse(datos[3]), datos[4], datos[5], double.Parse(datos[6]));
                            listaProductos.Add(p);
                        }
                        else if (datos[6] == "ProductoAlimenticio") {
                            ProductoAlimenticio p = new ProductoAlimenticio(int.Parse(datos[0]), datos[1], int.Parse(datos[2]), double.Parse(datos[3]), datos[4], datos[5]);
                            listaProductos.Add(p);
                        }
                        else if (datos[8] == "ProductoElectronico") {
                            ProductoElectronico p = new ProductoElectronico(int.Parse(datos[0]), datos[1], int.Parse(datos[2]), double.Parse(datos[3]), datos[4], datos[5], bool.Parse(datos[6]), bool.Parse(datos[7]));
                            listaProductos.Add(p);
                        }
                    }
                    sr.Close();
                }
            }
            catch (FileNotFoundException ex) {
                Console.WriteLine("No se encuentra el archivo de contenidos: " + ex.Message);
            }
            catch (IOException ex) {
                Console.WriteLine("Error de E/S: " + ex.Message);
            }
            return contenidosCargados;
        }

    }
}
