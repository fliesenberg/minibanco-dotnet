public class ContaPoupanca : Conta
{
    private const double TAXA_RENDIMENTO_MENSAL = 0.5;

    public ContaPoupanca(string titular, int numero) : base(titular, numero)
    {
    }

    public override void AplicarFechamentoMensal()
    {
        decimal rendimento = this.Saldo * (decimal)TAXA_RENDIMENTO_MENSAL / 100;

        try
        {
            RegistrarTransacao("Rendimento Mensal", "Deposito", rendimento);
            this.Saldo += rendimento;            
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
        Console.WriteLine("  - Tipo: Conta Poupança");
        Console.WriteLine("  - Titular: " + this.Titular + " | Conta: " + this.Numero);
        Console.WriteLine("  - Atributo Específico: Taxa de Rendimento Mensal: " + TAXA_RENDIMENTO_MENSAL + " %");
        Console.WriteLine("  - Saldo Atual: R$ " + this.Saldo);
        Console.WriteLine("");               
    }
}