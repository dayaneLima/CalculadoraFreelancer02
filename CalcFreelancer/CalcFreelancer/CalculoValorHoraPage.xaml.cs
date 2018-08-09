using CalcFreelancer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalcFreelancer
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CalculoValorHoraPage : ContentPage
	{
		public CalculoValorHoraPage ()
		{
			InitializeComponent ();
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

            Gravar(valorHora);
        }

        private async void Gravar(double valorHora)
        {
            var profissionalAzureClient = new AzureRepository();

            profissionalAzureClient.Insert(new Models.Profissional()
            {
                ValorGanhoMes = double.Parse(ValorGanhoMes.Text),
                HorasTrabalhadasPorDia = int.Parse(HorasTrabalhadasPorDia.Text),
                DiasTrabalhadosPorMes = int.Parse(DiasTrabalhadosPorMes.Text),
                DiasFeriasPorAno = int.Parse(DiasFeriasPorAno.Text),
                ValorPorHora = valorHora
            });

            await App.Current.MainPage.DisplayAlert("Sucesso", "Valor por hora gravado!", "Ok");
        }
    }
}