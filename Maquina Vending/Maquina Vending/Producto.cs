using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maquina_Vending {
    internal abstract class Producto {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public int Unidades { get; set; }
        public double PrecioUnidad { get; set; }
        public string Descripcion { get; set; }

        public Producto() { }
        public Producto(List<Producto> listaProductos) {
            ID += listaProductos.Count + 1;
        }
        public Producto(int iD, string nombre, int unidades, double precioUnidad, string descripcion) {
            ID = iD;
            Nombre = nombre;
            Unidades = unidades;
            PrecioUnidad = precioUnidad;
            Descripcion = descripcion;
        }
        public virtual string MostrarInformacion() {
            return $"{ID}\n\t{Nombre}\n\tUnidades: {Unidades}\n\tPrecio por Unidad {PrecioUnidad}\n\t" +
                $"Descripción: {Descripcion}";
        }
        public virtual void SolicitarDetalles() {
            try {
                Console.Clear();
                Console.WriteLine();
                Console.Write("Nombre: ");
                Nombre = Console.ReadLine();
                Console.Write("Unidades: ");
                Unidades = int.Parse(Console.ReadLine());
                Console.Write("Precio por Unidad: ");
                PrecioUnidad = double.Parse(Console.ReadLine());
                Console.Write("Descripción: ");
                Descripcion = Console.ReadLine();
            }
            catch (FormatException) {
                throw new FormatException();
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
                throw;
            }
        }
        public virtual void AñadirExistencias(int cantidad) {
            Unidades += cantidad;
        }
        public abstract void ToFile();
    }
}
