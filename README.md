# CalculadoraFreelancer02

Se trata da continuação do app <a href="https://github.com/dayaneLima/CalculadoraFreelancer01">CalculadoraFreelancer01</a>

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

Vamos adicionar nela os atributos Id, ValorGanhoMes, HorasTrabalhadasPorDia, DiasTrabalhadosPorMes, DiasFeriasPorAno, DiasDoencaPorAno, ValorPorHora, CreatedAt, UpdatedAt e Version.

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
        public int DiasDoencaPorAno { get; set; }
        public double ValorPorHora { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [Version]
        public string Version { get; set; }
    }
````

## Criação das chamadas ao Azure

Vamos criar uma outra pasta, agora chamada Repository.

Dentro dela, adicione uma classe chamada AzureRepository. 

Nela vamos criar duas propriedades:

- A primeira representa a conexão com o Azure, ela se chamará Client  e será do tipo IMobileServiceClient.
- A segunda representa a nossa tabela Profissional do Easy Table, então chamaremos de Table e será do tipo IMobileServiceTable<Profissional>.

```c#
        private IMobileServiceClient Client;
        private IMobileServiceTable<Profissional> Table;
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

Vamos alterar o texto do nosso botão de Calcular para Gravar, no arquivo CalculoValorHoraPage.xaml:

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
                DiasDoencaPorAno = int.Parse(DiasDoencaPorAno.Text),
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

