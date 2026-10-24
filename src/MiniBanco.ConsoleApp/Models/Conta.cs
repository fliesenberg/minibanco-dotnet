public abstract class Conta
{
    public string Titular { get; private set; }

    public int Numero { get; private set; }

    public decimal Saldo { get; protected set; }

    public Conta(string titular, int numero)
    {
        this.Titular = titular;
        this.Numero = numero;
        this.Saldo = 0.0M;
    }

    public void Depositar(decimal valor, string descricao)
    {
        this.Saldo += valor;
    }

    public virtual void Sacar(decimal valor)
    {
        if (valor > this.Saldo)
        {
            Console.WriteLine("Saldo insuficiente!");
            return;
        }

        this.Saldo -= valor;
    }

    public virtual void AplicarFechamentoMensal()
    {
    }
}