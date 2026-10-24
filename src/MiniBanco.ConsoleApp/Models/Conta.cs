public class Conta
{
    public string Titular { get; private set; }

    public int Numero { get; private set; }

    public decimal Saldo { get; private set; }

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

    public void Sacar(decimal valor)
    {
        this.Saldo -= valor;
    }

    public void AplicarFechamentoMensal()
    {
    }
}