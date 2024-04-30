using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Maquina_Vending {
    internal class Admin : Usuario {

        public string Pin { get; set; }//Otra variable para guardar la contraseña y compararla para verificar

        public Admin() { }//Constructores necesarios
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
                inicioSesion = true;
            }
            else {
                Console.WriteLine("Clave incorrecta. Acceso denegado.");
            }
            return inicioSesion;
        }

        public void Menu() {
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
                                MostrarInformacionProducto();
                                Console.Write("ID del producto a eliminar: ");
                                int id_producto = int.Parse(Console.ReadLine());
                                Producto productoTemp = BuscarProducto(id_producto);
                                EliminarProducto(productoTemp);
                                break;
                            case 3:
                                MostrarInformacionProducto();
                                break;
                            case 4:
                                MostrarInformacionProducto();
                                Console.Write("ID del producto al que deseas añadir existencias: ");
                                int id_producto_existencias = int.Parse(Console.ReadLine());
                                Producto productoExistencias = BuscarProducto(id_producto_existencias);
                                if (productoExistencias != null) {
                                    Console.Write("Cantidad de existencias a añadir: ");
                                    int cantidad = int.Parse(Console.ReadLine());
                                    productoExistencias.AnadirExistencias(cantidad);
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
        public override void Salir() {
            //Guardamos los productos en un archivo csv al cerrar la sesion
            if (listaProductos.Count > 0) {
                File.Create("productos.csv").Close();
                foreach (Producto p in listaProductos) {
                    p.ToFile();
                }
            }
        }
        public bool CargaCompleta() {
            //Cargamos desde un archivo csv desde dentro de la carpeta de la solución
            bool contenidosCargados = false;
            int productosCargados = 0;
            if (Login()) {
                Console.Clear();
                Console.WriteLine("Introduzca el nombre del archivo .csv");
                Console.WriteLine("Por ejemplo, cargarproductos.csv");
                Console.Write("Nombre del archivo .csv: ");
                string archivo = Console.ReadLine();
                try {
                    if (File.Exists(archivo)) {
                        StreamReader sr = new StreamReader(archivo);
                        string linea;
                        while ((linea = sr.ReadLine()) != null && productosCargados < 12 && listaProductos.Count < 12) {
                            contenidosCargados = true;
                            string[] datos = linea.Split(';');
                            if (datos.Length <= 9) {
                                Producto productoCargado = null;
                                if (datos.Length == 8 && datos[7] == "MaterialesPreciosos") {
                                    MaterialesPreciosos p = new MaterialesPreciosos(int.Parse(datos[0]), datos[1], int.Parse(datos[2]), double.Parse(datos[3]), datos[4], datos[5], double.Parse(datos[6]));
                                    productoCargado = p;
                                }
                                else if (datos.Length == 7 && datos[6] == "ProductoAlimenticio") {
                                    ProductoAlimenticio p = new ProductoAlimenticio(int.Parse(datos[0]), datos[1], int.Parse(datos[2]), double.Parse(datos[3]), datos[4], datos[5]);
                                    productoCargado = p;
                                }
                                else if (datos.Length == 9 && datos[8] == "ProductoElectronico") {
                                    ProductoElectronico p = new ProductoElectronico(int.Parse(datos[0]), datos[1], int.Parse(datos[2]), double.Parse(datos[3]), datos[4], datos[5], bool.Parse(datos[6]), bool.Parse(datos[7]));
                                    productoCargado = p;
                                }
                                if (productoCargado != null) {
                                    int nuevoID = ObtenerNuevoID();
                                    if (nuevoID == 0) {
                                        Console.WriteLine("La máquina de vending está llena. No se pueden cargar más productos.");
                                        break;
                                    }
                                    else {
                                        productoCargado.ID = nuevoID;
                                        listaProductos.Add(productoCargado);
                                        productosCargados++;
                                    }
                                }
                            }
                            Salir();
                        }
                        if (contenidosCargados) {
                            Console.WriteLine($"Se han cargado {productosCargados} productos.");
                        }
                        else if (listaProductos.Count == 12) {
                            Console.WriteLine("Máquina llena");
                        }
                        else {
                            Console.WriteLine("Error: La línea no contiene todos los datos necesarios.");
                        }
                    }
                    else {
                        Console.WriteLine("El archivo especificado no existe.");
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
            }
            return contenidosCargados;
        }
        private int ObtenerNuevoID() {
            //Al añadir productos desde un archivo csv, comprobamos que sus ID no esten repetidos
            int maxID = 0;
            foreach (Producto p in listaProductos) {
                if (p.ID > maxID && p.ID <= 12) {
                    maxID = p.ID;
                }
            }

            if (maxID == 12) {
                for (int i = 1; i <= 12; i++) {
                    bool idLibre = false;
                    foreach (Producto p in listaProductos) {
                        if (p.ID == i) {
                            idLibre = true;
                            break;
                        }
                    }
                    if (!idLibre) {
                        return i;
                    }
                }
                return 0;
            }
            return maxID + 1;
        }
        public void AddProducto() {
            //Metodo para añadir productos
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
                                while (BuscarProducto(p1.ID) != null) {
                                    Console.WriteLine("El ID ya está en uso. Generando un nuevo ID...");
                                    p1.ID += 1;
                                }
                                listaProductos.Add(p1);
                                break;
                            case 2:
                                ProductoAlimenticio p2 = new ProductoAlimenticio(listaProductos);
                                p2.SolicitarDetalles();
                                while (BuscarProducto(p2.ID) != null) {
                                    Console.WriteLine("El ID ya está en uso. Generando un nuevo ID...");
                                    p2.ID += 1;
                                }
                                listaProductos.Add(p2);
                                break;
                            case 3:
                                ProductoElectronico p3 = new ProductoElectronico(listaProductos);
                                p3.SolicitarDetalles();
                                while (BuscarProducto(p3.ID) != null) {
                                    Console.WriteLine("El ID ya está en uso. Generando un nuevo ID...");
                                    p3.ID += 1;
                                }
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
        public void AnadirExistencias(List<Producto> listaProductos) {
            //Metod para añadir unidades a un producto ya añadido
            Console.WriteLine("Añadir existencias a un producto existente:");
            Console.Write("ID del producto: ");
            int idProducto = int.Parse(Console.ReadLine());

            Producto producto = BuscarProducto(idProducto);
            if (producto != null) {
                Console.Write("Cantidad de existencias a añadir: ");
                int cantidad = int.Parse(Console.ReadLine());
                producto.AnadirExistencias(cantidad);
                Console.WriteLine("Existencias añadidas correctamente.");
            }
            else {
                Console.WriteLine("Producto no encontrado.");
            }
        }


        public Producto BuscarProducto(int id) {
            //Buscamos el producto por id el la listaProductos 
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
            //Metodo para reducir las unidades de u producto, a su vez si las unidades llegan a 0 el producto se eliminara.
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

        public override void MostrarInformacionProducto() {
            //Metodo para mostrar todos los productos de la lista
            Console.WriteLine(" --- Listado de productos --- ");
            Console.WriteLine();
            foreach (Producto p in listaProductos) {
                Console.WriteLine($"ID: {p.ID}\n\tNombre: {p.Nombre}\n\tUnidades: {p.Unidades}\n\tPrecio: {p.PrecioUnidad}\n\tDescripción: {p.Descripcion}");
            }
        }
    }
}


