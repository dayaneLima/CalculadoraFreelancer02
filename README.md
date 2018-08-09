# CalculadoraFreelancer02

Se trata da continuação do app <a href="https://github.com/dayaneLima/CalculadoraFreelancer01">CalculadoraFreelancer01</a>

## Vamos criar uma TabbedPage 

Temos a tela de cálculo do valor da hora do profissional e teremos outra para calcular o orçamento de projetos, então vamos trabalhar com tabbedPage, no qual terá uma "aba" com o cálculo do valor da hora e outra com o cálculo do valor do projeto.

Vamos criar uma Page chamada HomePage, clique com o botão direito sobre o projeto principal (chamado CalculadoraFreelancer01), vá em Add -> new item, escolha Xamarin Forms e a direita escolha a escrita Tabbed Page.

![Criação TabbedPage](https://github.com/dayaneLima/CalculadoraFreelancer02/blob/master/Docs/Gifs/TabbedPage.gif)

No arquivo HomePage.xaml vamos adicionar a tela CalculoValorHoraPage como sendo a primeira tab. Para isso precisamos referenciar o nosso projeto para acessar nossas telas, para isso adicione o seguinte trecho de código dentro da tag \<TabbedPage\> (o valor do namespace será o nome do seu projeto):

```xml
xmlns:local="clr-namespace:CalcFreelancer"
````

Vamos aproveitar e dar um Title para a nossa TabbedPage, adicionando também o seguinte trecho de código:

```xml
Title="Calculadora Freelancer"
````

Ficará assim então:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<TabbedPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:local="clr-namespace:CalcFreelancer"
             Title="Calculadora Freelancer"
             x:Class="CalcFreelancer.HomePage">
  <!--Pages can be added as references or inline-->
  <ContentPage Title="Tab 1" />
  <ContentPage Title="Tab 2" />
  <ContentPage Title="Tab 3" />
</TabbedPage>
````

Então vamos remover esses ContentPages que não será utilizado:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<TabbedPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:local="clr-namespace:CalcFreelancer"
             Title="Calculadora Freelancer"
             x:Class="CalcFreelancer.HomePage">

</TabbedPage>
````

Agora vamos referenciar a nossa tela:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<TabbedPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:local="clr-namespace:CalcFreelancer"
             Title="Calculadora Freelancer"
             x:Class="CalcFreelancer.HomePage">
    
    <local:CalculoValorHoraPage/>
    
</TabbedPage>
````

Para testar altere o arquivo App.xaml.cs alterando para a nossa tela inicial ser a HomePage, altere então o construtor da classe e troque o  MainPage = new NavigationPage(new CalculoValorHoraPage()) por  MainPage = new NavigationPage(new HomePage()) : 


```c#
   public App ()
   {
      InitializeComponent();
      MainPage = new NavigationPage(new HomePage());
   }
````

Agora execute o projeto, ele deverá gerar uma tela similar a essa:

<img src="https://github.com/dayaneLima/CalculadoraFreelancer02/blob/master/Docs/Imgs/HomePage.png" alt="Tela home" width="260">

## Vamos adicionar a biblioteca do Azure no nosso app

Clique com o botão direito sobre a Solution e vá em Manage NuGet Packages for Solution...

<img src="https://github.com/dayaneLima/CalculadoraFreelancer02/blob/master/Docs/Imgs/managerNugetPackagesForSolution.png" alt="Abrir gerenciamento nuget" width="260">

Vá em Browse, pesquise por Microsoft.Azure.Mobile.Client, ao encontrar a biblioteca a selecione.
Na direita, selecione todos os projetos e clique em instalar.

<img src="https://github.com/dayaneLima/CalculadoraFreelancer02/blob/master/Docs/Imgs/Azure01_azureXamarinForms01.png" alt="Instalar o Microsoft.Azure.Mobile.Client" width="100%">

Abaixo segue a animação da instalação completa:

![Instalação do Microsoft.Azure.Mobile.Client](https://github.com/dayaneLima/CalculadoraFreelancer02/blob/master/Docs/Gifs/AzureGif01_AddMicrosoft.Azure.Mobile.Client.gif)

## Criação da Model Profissional

Vamos criar uma pasta no projeto CalculadoraFreelancer01 chamada Models. 

Para isso clique com o botão direito sobre o projeto CalculadoraFreelancer01, vá em Add -> New Folder e dê o nome de Models.

![Criação pasta Models](https://github.com/dayaneLima/CalculadoraFreelancer02/blob/master/Docs/Gifs/AzureGif02_CriacaoPastaModels.gif)

Agora vamos adicionar a model Profissional ao nosso projeto. Clique com o botão direito sobre a pasta Models criada, vá em Add -> Class, e dê o nome de Profissional.

![Criação model Profissional](https://github.com/dayaneLima/CalculadoraFreelancer02/blob/master/Docs/Gifs/AzureGif03_CriacaoClasseProfissional.gif)

Vamos adicionar nela os atributos Id, ValorGanhoMes, HorasTrabalhadasPorDia, DiasTrabalhadosPorMes, DiasFeriasPorAno, ValorPorHora, CreatedAt, UpdatedAt e Version.

Em cima do nome da classe Profissional, adicione o nome da tabela referente a criada no Easy Table do Azure, dessa forma:  [DataTable("Profissional")].

A classe deverá ficar assim:

```c#
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
````

## Criação das chamadas ao Azure

Vamos criar uma outra pasta, agora chamada Repository.

Dentro dela, adicione uma classe chamada AzureRepository:

```c#
  class AzureRepository
  {
  }
````

Não se esqueça de colocá-la como pública:

```c#
  public class AzureRepository
  {
  }
````

Nela vamos criar duas propriedades:

- A primeira representa a conexão com o Azure, ela se chamará Client  e será do tipo IMobileServiceClient.
- A segunda representa a nossa tabela Profissional do Easy Table, então chamaremos de Table e será do tipo IMobileServiceTable<Profissional>.

```c#
        private IMobileServiceClient Client;
        private IMobileServiceTable<Profissional> Table;
````

O Visual Studio marcará como erro pois não importamos as classes necessárias, ele tem um atalho para isso. Clique sobre o item que está com o erro, pressione Ctrl + . e ele te dará as sugestões, a primeira delas é o using na classe necessária, então clique sobre ela.

Caso queira adicionar manualmente, vá no início do arquivo AzureRepository e adicione esses dois usings:

```c#
using CalcFreelancer.Models;
using Microsoft.WindowsAzure.MobileServices;
````

Vamos criar um construtor para essa classe e iniciar essas variáveis: 

```c#
  public AzureRepository()
  {
      string MyAppServiceURL = "sua url do Azure";
      Client = new MobileServiceClient(MyAppServiceURL);
      Table = Client.GetTable<Profissional>();
  }
````

Agora vamos criar as funções de manipulação dos dados da nossa tabela Profissional no Easy table. 

Criaremos as funções para inserir, atualizar, excluir, obter todos os registros, obter por id e obter o primeiro item da tabela.

A classe pronta ficará assim:

```c#
    public class AzureRepository
    {
        private IMobileServiceClient Client;
        private IMobileServiceTable<Profissional> Table;

        public AzureRepository()
        {
            string MyAppServiceURL = "sua url do Azure";
            Client = new MobileServiceClient(MyAppServiceURL);
            Table = Client.GetTable<Profissional>();
        }

        public async Task<IEnumerable<Profissional>> GetAll()
        {
            var empty = new Profissional[0];
            try
            {
                return await Table.ToEnumerableAsync();
            }
            catch (Exception)
            {
                return empty;
            }
        }

        public async void Insert(Profissional profissional)
        {
            await Table.InsertAsync(profissional);
        }

        public async Task Update(Profissional profissional)
        {
            await Table.UpdateAsync(profissional);
        }

        public async void Delete(Profissional profissional)
        {
            await Table.DeleteAsync(profissional);
        }

        public async Task<Profissional> Find(string id)
        {
            var itens = await Table.Where(i => i.Id == id).ToListAsync();
            return (itens.Count > 0) ? itens[0] : null;
        }

        public async Task<Profissional> GetFirst()
        {
            var itens = await Table.ToListAsync();
            return (itens.Count > 0) ? itens[0] : null;
        }
    }
````

## Alteração da Tela

Vamos alterar o texto do nosso botão de Calcular para Gravar (Alterar somente o valor do atributo Text), no arquivo CalculoValorHoraPage.xaml:

```xml
      <Button Text="Gravar"
                    BackgroundColor="#6699ff"
                    TextColor="White"
                    x:Name="CalcularValorHoraButton"/>
````

Agora vamos alterar o Code Behing para gravar no Azure os dados. Edite o arquivo  CalculoValorHoraPage.xaml.cs.

Crie a função chamada Gravar. Ela receberá por parâmetro o valor double chamado valorHora, que é o valor calculado da hora do profissional.

```c#
  private async void Gravar(double valorHora)
  {
  }
````

Vamos instanciar nessa classe um objeto do tipo AzureRepository.

```c#
  private async void Gravar(double valorHora)
  {
      var profissionalAzureClient = new AzureRepository();
  }
````

Agora vamos chamar a função de Insert do nosso AzureRepository passando um objeto do tipo Profissional, com os dados obtidos da tela.

```c#
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
  }
````

Vamos exibir um alerta para o nosso usuário informando que os dados foram gravados:

```c#
     private async void Gravar(double valorHora)
        {
            var profissionalAzureClient = new AzureRepository();

            profissionalAzureClient.Insert(new Models.Profissional()
            {
                ValorGanhoMes = double.Parse(ValorGanhoMes.Text),
                HorasTrabalhadasPorDia = int.Parse(HorasTrabalhadasPorDia.Text),
                DiasTrabalhadosPorMes = int.Parse(DiasTrabalhadosPorMes.Text),
                DiasFeriasPorAno = int.Parse(DiasFeriasPorAno.Text),
                DiasDoencaPorAno = int.Parse(DiasDoencaPorAno.Text),
                ValorPorHora = valorHora
            }); 

            await App.Current.MainPage.DisplayAlert("Sucesso", "Valor por hora gravado!", "Ok");
        }
````

Agora vamos alterar a função CalcularValorHoraButton_Clicked para chamar a função Gravar que acabamos de criar. No final da função adicione a chamada: Gravar(valorHora). O código da função ficará assim:

```c#
  private void CalcularValorHoraButton_Clicked(object sender, EventArgs e)
  {

            double valorGanhoAnual = double.Parse(ValorGanhoMes.Text) * 12;
            int totalDiasTrabalhadosPorAno = int.Parse(DiasTrabalhadosPorMes.Text) * 12;

            if (!string.IsNullOrEmpty(DiasFeriasPorAno.Text))
            {
                totalDiasTrabalhadosPorAno -= int.Parse(DiasFeriasPorAno.Text);
            }

            if (!string.IsNullOrEmpty(DiasDoencaPorAno.Text))
            {
                totalDiasTrabalhadosPorAno -= int.Parse(DiasDoencaPorAno.Text);
            }
            
            double valorHora = valorGanhoAnual / (totalDiasTrabalhadosPorAno * int.Parse(HorasTrabalhadasPorDia.Text));

            ValorDaHora.Text = $"{valorHora.ToString("C")} / hora";

            Gravar(valorHora);
  }
````


Prontinho, agora nossos dados estão sendo gravados no Azure.

