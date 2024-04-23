using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Maquina_Vending {
    internal class Admin : Usuario {

        public string Pin { get; set; }

        public Admin() { }
        public Admin(List<Producto> productos, string pin) : base(productos) {
            Pin = pin;
        }
        public Admin(List<Producto> productos) : base(productos) { }
        public Admin(string nombre, string pin, List<Producto> productos) : base(nombre, productos) {
            Pin = pin;
        }
        public bool Login() {
            bool inicioSesion = false;
            Console.Write("Ingrese la clave de administrador: ");
            string clave = Console.ReadLine();

            if (clave == Pin) {
                Console.WriteLine("Acceso concedido.");
                inicioSesion = true;            }
            else {
                Console.WriteLine("Clave incorrecta. Acceso denegado.");
            }
            return inicioSesion;
        }

        public override void Menu() {
            int opcion = 0;
            if (Login()) {
                do {
                    Console.Clear();
                    Console.WriteLine("1. Añadir producto");
                    Console.WriteLine("2. Eliminar producto por ID");
                    Console.WriteLine("3. Listar productos");
                    Console.WriteLine("4. Añadir existencias a un producto");
                    Console.WriteLine("5. Salir");
                    Console.Write("Elige una opción: ");
                    try {
                        opcion = int.Parse(Console.ReadLine());
                        switch (opcion) {
                            case 1:
                                AddProducto();
                                break;
                            case 2:
                                ListarProductos();
                                Console.Write("ID del producto a eliminar: ");
                                int id_producto = int.Parse(Console.ReadLine());
                                Producto productoTemp = BuscarProducto(id_producto);
                                EliminarProducto(productoTemp);
                                break;
                            case 3:
                                ListarProductos();
                                break;
                            case 4:
                                ListarProductos();
                                Console.Write("ID del producto al que deseas añadir existencias: ");
                                int id_producto_existencias = int.Parse(Console.ReadLine());
                                Producto productoExistencias = BuscarProducto(id_producto_existencias);
                                if (productoExistencias != null) {
                                    Console.Write("Cantidad de existencias a añadir: ");
                                    int cantidad = int.Parse(Console.ReadLine());
                                    productoExistencias.AñadirExistencias(cantidad);
                                    Console.WriteLine("Existencias añadidas correctamente.");
                                }
                                else {
                                    Console.WriteLine("Producto no encontrado.");
                                }
                                break;
                            case 5:
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
                } while (opcion != 5);
            }
        }
        public override void ComprarProductos() {
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

        public void AddProducto() {
            if (listaProductos.Count < 12) {
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
            else {
                Console.WriteLine("No hay ranuras disponibles en la máquina de vending.");
            }
        }
            public void AñadirExistencias(List<Producto> listaProductos) {
            Console.WriteLine("Añadir existencias a un producto existente:");
            Console.Write("ID del producto: ");
            int idProducto = int.Parse(Console.ReadLine());

            Producto producto = BuscarProducto(idProducto);
            if (producto != null) {
                Console.Write("Cantidad de existencias a añadir: ");
                int cantidad = int.Parse(Console.ReadLine());
                producto.AñadirExistencias(cantidad);
                Console.WriteLine("Existencias añadidas correctamente.");
            }
            else {
                Console.WriteLine("Producto no encontrado.");
            }
        }


        public Producto BuscarProducto(int id) {
            Producto productoTemp = null;
            foreach (Producto p in listaProductos) {
                if (p.ID == id) {
                    productoTemp = p;
                    break;
                }
            }
            return productoTemp;
        }
        public void EliminarProducto(Producto p) {
            if (p != null) {
                Console.WriteLine("Producto encontrado:");
                Console.WriteLine(p.MostrarInformacion());
                Console.Write("Cantidad a reducir: ");
                int cantidadReducir = int.Parse(Console.ReadLine());

                if (cantidadReducir <= p.Unidades) {
                    p.Unidades -= cantidadReducir;
                    Console.WriteLine($"Se han reducido {cantidadReducir} unidades del producto.");

                    if (p.Unidades == 0) {
                        listaProductos.Remove(p);
                        Console.WriteLine("El producto ha sido eliminado porque su cantidad llegó a 0.");
                        string archivo = "productos.csv";
                        string[] lineas = File.ReadAllLines(archivo);
                        List<string> lineasNuevas = new List<string>();

                        foreach (string linea in lineas) {
                            string[] campos = linea.Split(';');
                            int id = int.Parse(campos[0]);
                            if (id != p.ID) {
                                lineasNuevas.Add(linea);
                            }
                        }

                        File.WriteAllLines(archivo, lineasNuevas);
                    }
                }
                else {
                    Console.WriteLine("La cantidad a reducir es mayor que la cantidad actual del producto.");
                }
            }
            else {
                Console.WriteLine("No se ha encontrado ningún producto con el ID introducido");
            }
        }

    public override void ListarProductos() {
            Console.WriteLine(" --- Listado de productos --- ");
            Console.WriteLine();
            foreach (Producto p in listaProductos) {
                Console.WriteLine(p.MostrarInformacion());
            }
        }
    }
}


