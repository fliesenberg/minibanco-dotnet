public abstract class Conta
{
    public string Titular { get; private set; }

    public int Numero { get; private set; }

    public decimal Saldo { get; protected set; }

    protected List<Transacao> Extrato { get; set; }

    public Conta(string titular, int numero)
    {
        this.Titular = titular;
        this.Numero = numero;
        this.Saldo = 0.0M;
        this.Extrato = new List<Transacao>();
    }

    protected void RegistrarTransacao(string descricao, string tipo, decimal valor)
    {
        var transacao = new Transacao(descricao, tipo, valor);
        this.Extrato.Add(transacao);            
    }

    public void Depositar(decimal valor, string descricao)
    {
        try
        {
            RegistrarTransacao("Depósito", "Deposito", valor);
            this.Saldo += valor;            
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

    public virtual void Sacar(decimal valor)
    {
        if (valor > this.Saldo)
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

    public virtual void AplicarFechamentoMensal()
    {
    }
}