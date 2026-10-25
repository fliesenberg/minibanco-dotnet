public class ContaCorrente : Conta
{
    private const decimal TARIFA = 10.0M;

    private decimal LimiteChequeEspecial { get; set; }

    public ContaCorrente(string titular, int numero) : base(titular, numero)
    {    
    }

    public override void Sacar(decimal valor)
    {
        decimal limiteSaque = this.Saldo + this.LimiteChequeEspecial;

        if (valor > limiteSaque)
        {
            Console.WriteLine("Saldo insuficiente!");
            return;
        }

        RegistrarTransacao("Saque", "Saque", valor);
        this.Saldo -= valor;
    }

    public override void AplicarFechamentoMensal()
    {
        RegistrarTransacao("Tarifa Mensal", "Tarifa", TARIFA);
        this.Saldo -= TARIFA;
    }
}