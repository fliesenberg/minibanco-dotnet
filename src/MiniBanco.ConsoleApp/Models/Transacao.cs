public class Transacao
{
    public string Descricao { get; private set; }

    public string Tipo { get; private set; }

    public decimal Valor { get; private set; }

    public Transacao(string descricao, string tipo, decimal valor)
    {
        this.Descricao = descricao;
        this.Tipo = tipo;
        this.Valor = valor;
    }
}