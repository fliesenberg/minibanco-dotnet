public class ContaCorrente : Conta
{
    private const decimal TARIFA = 10.0M;

    private decimal LimiteChequeEspecial { get; set; }

    public ContaCorrente(string titular, int numero, decimal limiteChequeEspecial) : base(titular, numero)
    {
        this.LimiteChequeEspecial = limiteChequeEspecial;
    }

    public override void Sacar(decimal valor)
    {
        decimal limiteSaque = this.Saldo + this.LimiteChequeEspecial;

        if (valor > limiteSaque)
        {
            throw new SaldoInsuficienteException();
        }

        try
        {
            RegistrarTransacao("Saque", "Saque", valor);
            this.Saldo -= valor;            
        }
        catch (ArgumentException)
        {
            throw;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public override void AplicarFechamentoMensal()
    {
        try
        {
            RegistrarTransacao("Tarifa Mensal", "Tarifa", TARIFA);
            this.Saldo -= TARIFA;            
        }
        catch (ArgumentException a)
        {
            Console.WriteLine("Erro: " + a.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine("Erro: " + e.Message);
        }
    }

    public override void MostrarDadosConta()
    {
        Console.WriteLine("> DADOS DA CONTA: ");
        Console.WriteLine("  - Tipo: Conta Corrente");
        Console.WriteLine("  - Titular: " + this.Titular + " | Conta: " + this.Numero);
        Console.WriteLine("  - Atributo Específico: Limite Cheque Especial: R$ " + this.LimiteChequeEspecial);
        Console.WriteLine("  - Atributo Específico: Tarifa: R$ " + TARIFA);
        Console.WriteLine("  - Saldo Atual: R$ " + this.Saldo);
        Console.WriteLine("");
    }
}