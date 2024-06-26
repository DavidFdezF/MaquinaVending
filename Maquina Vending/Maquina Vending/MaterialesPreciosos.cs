﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Maquina_Vending {
    internal class MaterialesPreciosos : Producto {

        public string TipoMaterial { get; set; }
        public double Peso { get; set; }

        public MaterialesPreciosos() { }
        public MaterialesPreciosos(List<Producto> listaProductos) : base(listaProductos) { }
        public MaterialesPreciosos(int id, string nombre, int unidades, double precioUnidad,
            string descripcion, string tipoMaterial, double peso) : base(id, nombre, unidades,
                precioUnidad, descripcion) {
            TipoMaterial = tipoMaterial;
            Peso = peso;
        }
        public override string MostrarInformacion() {
            return base.MostrarInformacion() +
                $"{TipoMaterial}\n\tPeso: {Peso:F2} kg";
        }
        public override void SolicitarDetalles() {
            try {
                base.SolicitarDetalles();
                Console.Write("Tipo de Material: ");
                TipoMaterial = Console.ReadLine();
                Console.Write("Peso (en kg): ");
                Peso = double.Parse(Console.ReadLine());
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
                $"{TipoMaterial};{Peso};MaterialesPreciosos");
            sw.Close();
        }
    }
}
