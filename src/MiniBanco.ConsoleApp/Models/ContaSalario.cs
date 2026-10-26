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
        try
        {
            base.Sacar(valor);

            this.ValorSaquesMesAtual += valor;

            if (this.ValorSaquesMesAtual > this.LimiteSaquesMensais)
            {
                RegistrarTransacao("Tarifa Limite de Saques Mensais Excedido", "Tarifa", TARIFA_LIMITE_SAQUES);
                this.Saldo -= TARIFA_LIMITE_SAQUES;
            }            
        }
        catch (SaldoInsuficienteException)
        {
            throw;
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
        this.ValorSaquesMesAtual = 0.0M;
    }

    public override void MostrarDadosConta()
    {
        Console.WriteLine("> DADOS DA CONTA: ");
        Console.WriteLine("  - Tipo: Conta Salário");
        Console.WriteLine("  - Titular: " + this.Titular + " | Conta: " + this.Numero);
        Console.WriteLine("  - Atributo Específico: Empregador: " + this.Empregador);
        Console.WriteLine("  - Atributo Específico: Limite de Saques Mensais: R$ " + this.LimiteSaquesMensais);
        Console.WriteLine("  - Atributo Específico: Tarifa Limite de Saques Mensais Excedido: R$ " + TARIFA_LIMITE_SAQUES);
        Console.WriteLine("  - Saldo Atual: R$ " + this.Saldo);
        Console.WriteLine("");              
    }
}