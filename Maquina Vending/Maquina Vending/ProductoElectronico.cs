using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maquina_Vending {
    internal class ProductoElectronico : Producto {
        public string TipoMaterial {  get; set; }
        public bool Pilas {  get; set; }
        public bool PreCargado { get; set; }
        public ProductoElectronico() { }
        public ProductoElectronico(List<Producto> listaProductos) : base(listaProductos) {}
        public ProductoElectronico(int iD, string nombre, int unidades, double precioUnidad, 
            string descripcion, string tipoMaterial, bool pilas, bool preCargado)
            : base(iD, nombre, unidades, precioUnidad, descripcion) {
            TipoMaterial = tipoMaterial;
            Pilas = pilas;
            PreCargado = preCargado;
        }
        public override string MostrarInformacion() {
            return base.MostrarInformacion() +
              $"\n\tTipo de materials utilizados: {TipoMaterial}\n\t¿Tiene pilas? " +
              $"{Pilas}\n\t¿Está precargado? {PreCargado}";
        }
        public override void SolicitarDetalles() {
            try {
                base.SolicitarDetalles(); ;
                Console.Write("Tipo de materiales: ");
                TipoMaterial = Console.ReadLine();

                Console.Write("¿Tiene pilas? (true/false): ");
                Pilas = Boolean.Parse((Console.ReadLine()));

                Console.Write("¿Está precargado? (true/false): ");
                PreCargado = Boolean.Parse((Console.ReadLine()));
            }
            catch (FormatException) {
                throw new FormatException();
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
                throw;
            }
        }
        public override void ToFile() {
            StreamWriter sw = new StreamWriter("productos.csv", true);
            sw.WriteLine($"{ID};{Nombre};{Unidades};{PrecioUnidad};{Descripcion};" +
                $"{TipoMaterial};{Pilas};{PreCargado};ProductoElectronico");
            sw.Close();
        }

    }
}
