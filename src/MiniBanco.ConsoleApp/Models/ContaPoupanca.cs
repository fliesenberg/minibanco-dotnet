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
}