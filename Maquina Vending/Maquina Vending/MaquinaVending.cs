using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maquina_Vending {
    internal class MaquinaVending {

        private const string CONTRASENA = "admin";
        public List<Producto> listaProductos;

        public MaquinaVending() {
            listaProductos = new List<Producto>();
            CargarContenidosDeArchivo();
        }

        public void ComprarProducto() {
            Cliente cliente = new Cliente(listaProductos);
            cliente.ComprarProductos();
        }

        public void MostrarInformacionProducto() {
            Cliente cliente = new Cliente(listaProductos);
            cliente.MostrarInformacionProducto();
        }

        public void CargarIndividualProducto() {
            Admin admin = new Admin(listaProductos, CONTRASENA);
            admin.Menu();
        }

        public void CargarCompletaProducto() {
            Admin admin = new Admin(listaProductos, CONTRASENA);
            admin.CargaCompleta();
        }
        public bool CargarContenidosDeArchivo() {
            bool contenidosCargados = false;
            try {
                if (File.Exists("productos.csv")) {
                    StreamReader sr = new StreamReader("productos.csv");
                    string linea;
                    while ((linea = sr.ReadLine()) != null) {
                        contenidosCargados = true;
                        string[] datos = linea.Split(';');
                        if (datos.Length == 8 && datos[7] == "MaterialesPreciosos") {
                            MaterialesPreciosos p = new MaterialesPreciosos(int.Parse(datos[0]), datos[1], int.Parse(datos[2]), double.Parse(datos[3]), datos[4], datos[5], double.Parse(datos[6]));
                            listaProductos.Add(p);
                        }
                        else if (datos.Length == 7 && datos[6] == "ProductoAlimenticio") {
                            ProductoAlimenticio p = new ProductoAlimenticio(int.Parse(datos[0]), datos[1], int.Parse(datos[2]), double.Parse(datos[3]), datos[4], datos[5]);
                            listaProductos.Add(p);
                        }
                        else if (datos.Length == 9 && datos[8] == "ProductoElectronico") {
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
            catch (IndexOutOfRangeException) {
                Console.WriteLine("Error: Índice fuera del rango en la matriz");
            }
            return contenidosCargados;
        }
    }
}


