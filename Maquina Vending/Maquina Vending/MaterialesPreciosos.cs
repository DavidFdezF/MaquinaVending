using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maquina_Vending {
    internal class MaterialesPreciosos : Producto{

        public string TipoMaterial {  get; set; }
        public double Peso {  get; set; }

        public MaterialesPreciosos() { }
        public MaterialesPreciosos(int id, string nombre, int unidades, double precioUnidad, 
            string descripcion,string tipoMaterial, double peso) : base (id, nombre, unidades,
                precioUnidad, descripcion) {
            TipoMaterial = tipoMaterial;
            Peso = peso;
        }
        public override string MostrarInformacion() {
            return $"{ID}\n\t{Nombre}\n\tUnidades: {Unidades}\n\tPrecio por Unidad {PrecioUnidad}\n\t" +
                $"Descripción: {Descripcion}\n\tTipo de Material: {TipoMaterial}\n\tPeso: {Peso} kg";
        }
        public override void ToFile() {

        }
    }
}
