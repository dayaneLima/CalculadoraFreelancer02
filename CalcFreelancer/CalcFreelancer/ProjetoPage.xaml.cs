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
	public partial class ProjetoPage : ContentPage
	{
		public ProjetoPage ()
		{
			InitializeComponent ();
            GravarButton.Clicked += GravarButton_Clicked;
            LimparButton.Clicked += LimparButton_Clicked;
        }

        private void LimparButton_Clicked(object sender, EventArgs e)
        {
            Nome.Text = string.Empty;
            ValorPorHora.Text = string.Empty;
            HorasPorDia.Text = string.Empty;
            DiasDuracaoProjeto.Text = string.Empty;
            ValorTotal.Text = string.Empty;
        }


        private void GravarButton_Clicked(object sender, EventArgs e)
        {
            var valorTotal = double.Parse(ValorPorHora.Text) * int.Parse(HorasPorDia.Text) * int.Parse(DiasDuracaoProjeto.Text);
            ValorTotal.Text = $"{valorTotal.ToString("C")} / hora";
            Gravar(valorTotal);
        }

        private async void Gravar(double valorTotal)
        {
            var projetoAzureClient = new AzureProjetoRepository();

            projetoAzureClient.Insert(new Models.Projeto()
            {
                Nome = Nome.Text,
                ValorPorHora = double.Parse(ValorPorHora.Text),
                HorasPorDia = int.Parse(HorasPorDia.Text),
                DiasDuracaoProjeto = int.Parse(DiasDuracaoProjeto.Text),
                ValorTotal = valorTotal
            });

            await App.Current.MainPage.DisplayAlert("Sucesso", "Projeto gravado!", "Ok");
        }
    }
}