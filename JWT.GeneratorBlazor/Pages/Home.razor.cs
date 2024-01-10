namespace JWT.GeneratorBlazor.Pages;

public partial class Home
{
    private JsonWebToken _jsonWebToken { get; set; } = new JsonWebToken();
    private string? _novaClaimChave;
    private string? _novaClaimValor;
    private Modal _modal = default!;
    private string _token { get; set; } = string.Empty;
    private List<ToastMessage> _mensagens = new List<ToastMessage>();

    [Inject]
    private ITokenService? _tokenService { get; set; }
    [Inject]
    private IClipboard? _clipboardService { get; set; }

    protected override Task OnInitializedAsync()
    {
        IniciarDadosTela();

        return base.OnInitializedAsync();
    }

    private async Task EnviarJWT()
    {
        _token = await _tokenService!.CriarTokenAsync(_jsonWebToken);
        if (string.IsNullOrEmpty(_token) == false)
            await _modal.ShowAsync();
    }

    private async Task FecharModal()
    {
        _token = string.Empty;
        await _modal.HideAsync();
    }

    private async Task CopiarTokenAsync() => await _clipboardService!.SetTextAsync(_token!);

    private void AdicionarClaim()
    {
        var chaveOuValorNulos = string.IsNullOrEmpty(_novaClaimChave) || string.IsNullOrEmpty(_novaClaimValor);
        if (chaveOuValorNulos)
        {
            MostrarMensagem(ToastType.Warning, "Aviso", "O valor e a chave do token não podem ser vazios!");
            return;
        }

        _jsonWebToken.Claims!.Add(new JWT.GeneratorLib.Models.Claim { Chave = _novaClaimChave, Valor = _novaClaimValor });
        _novaClaimChave = string.Empty;
        _novaClaimValor = string.Empty;
    }

    private void IniciarDadosTela()
    {
        var dataAtual = DateTime.Now;

        _jsonWebToken = new JsonWebToken();
        _jsonWebToken.Claims = new ObservableCollection<Claim>();
        _jsonWebToken.IssuedDay = new DateOnly(dataAtual.Year, dataAtual.Month, dataAtual.Day);
        _jsonWebToken.IssuedTime = new TimeOnly(dataAtual.Hour, dataAtual.Minute);
        _jsonWebToken.ExpirationDay = _jsonWebToken.IssuedDay.Value.AddYears(1);
        _jsonWebToken.ExpirationTime = _jsonWebToken.IssuedTime;
    }

    private void MostrarMensagem(ToastType toastType, string titulo, string mensagem) => _mensagens.Add(CriarToastMessage(toastType, titulo, mensagem));

    private ToastMessage CriarToastMessage(ToastType toastType, string titulo, string mensagem)
    {
        return new ToastMessage
        {
            Type = toastType,
            Title = titulo,
            HelpText = $"{DateTime.Now}",
            Message = mensagem,
        };
    }
}
