using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalcFreelancer.Models
{
    [DataTable("Projeto")]
    public class Projeto
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public double ValorPorHora { get; set; }
        public int HorasPorDia { get; set; }
        public int DiasDuracaoProjeto { get; set; }
        public double ValorTotal { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [Version]
        public string Version { get; set; }
    }
}
