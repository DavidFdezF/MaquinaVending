using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maquina_Vending {
    internal class ProductoAlimenticio : Producto{
        public string InformacionNutricional {  get; set; }
        public ProductoAlimenticio() { }
        public ProductoAlimenticio(int iD, string nombre, int unidades, double precioUnidad, string descripcion, string informacionNutricional)
            : base(iD, nombre, unidades, precioUnidad, descripcion) {
            InformacionNutricional = informacionNutricional;
        }
        public override string MostrarInformacion() {
            return base.MostrarInformacion() +
              $"\n\tInformación Nutricional: {InformacionNutricional}";
        }
        public override void SolicitarDetalles() {
            try {
                base.SolicitarDetalles(); ;
                Console.Write("Información Nutricional: ");
                InformacionNutricional = Console.ReadLine();
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
            throw new NotImplementedException();
        }
    }
}
