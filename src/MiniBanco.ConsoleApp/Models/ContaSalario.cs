public class ContaSalario : Conta
{
    private const decimal TARIFA_LIMITE_SAQUES = 5.0M;

    public string Empregador { get; private set; }

    public decimal LimiteSaquesMensais { get; private set; }

    private decimal ValorSaquesMesAtual { get; set; }

    public ContaSalario(string titular, int numero, string empregador, decimal limitesSaquesMensais) : base(titular, numero)
    {
        this.Empregador = empregador;
        this.LimiteSaquesMensais = limitesSaquesMensais;
    }

    public override void Sacar(decimal valor)
    {
        base.Sacar(valor);

        this.ValorSaquesMesAtual += valor;

        if (this.ValorSaquesMesAtual > this.LimiteSaquesMensais)
        {
            this.Saldo -= TARIFA_LIMITE_SAQUES;
        }
    }

    public override void AplicarFechamentoMensal()
    {
        this.ValorSaquesMesAtual = 0.0M;
    }
}