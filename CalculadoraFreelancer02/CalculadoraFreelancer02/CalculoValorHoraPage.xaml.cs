using CalculadoraFreelancer02.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalculadoraFreelancer02
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalculoValorHoraPage : ContentPage
    {
        public CalculoValorHoraPage()
        {
            InitializeComponent();

            CalcularValorHoraButton.Clicked += CalcularValorHoraButton_Clicked;
        }

        private void CalcularValorHoraButton_Clicked(object sender, EventArgs e)
        {

            double valorGanhoAnual = double.Parse(ValorGanhoMes.Text) * 12;
            int totalDiasTrabalhadosPorAno = int.Parse(DiasTrabalhadosPorMes.Text) * 12;

            if (!string.IsNullOrEmpty(DiasFeriasPorAno.Text))
            {
                totalDiasTrabalhadosPorAno -= int.Parse(DiasFeriasPorAno.Text);
            }
            
            double valorHora = valorGanhoAnual / (totalDiasTrabalhadosPorAno * int.Parse(HorasTrabalhadasPorDia.Text));

            ValorDaHora.Text = $"{valorHora.ToString("C")} / hora";

            Gravar();
        }

        private void Gravar()
        {
            var profissionalAzureClient = new ProfissionalAzureClient();
            profissionalAzureClient.Insert(new Models.Profissional()
            {
                ValorGanhoMes = double.Parse(ValorGanhoMes.Text),
                HorasTrabalhadasPorDia = int.Parse(HorasTrabalhadasPorDia.Text),
                DiasTrabalhadosPorMes = int.Parse(DiasTrabalhadosPorMes.Text),
                DiasFeriasPorAno = int.Parse(DiasFeriasPorAno.Text),
            });
        }
    }
}