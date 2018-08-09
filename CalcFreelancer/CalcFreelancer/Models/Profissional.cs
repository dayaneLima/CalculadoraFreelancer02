using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalcFreelancer.Models
{
    [DataTable("Profissional")]
    public class Profissional
    {
        public string Id { get; set; }
        public double ValorGanhoMes { get; set; }
        public int HorasTrabalhadasPorDia { get; set; }
        public int DiasTrabalhadosPorMes { get; set; }
        public int DiasFeriasPorAno { get; set; }
        public double ValorPorHora { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [Version]
        public string Version { get; set; }
    }
}
