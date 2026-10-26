public class Transacao
{
    public string Descricao { get; private set; }

    public string Tipo { get; private set; }

    public int Pontos { get; private set; }

    public decimal Valor { get; private set; }

    public Transacao(string descricao, string tipo, decimal valor)
    {
        if (tipo != "Deposito" && tipo != "Saque" && tipo != "Tarifa")
        {
            throw new ArgumentException("Tipo de transação inválida!");
        }

        if (valor <= 0.0M)
        {
            throw new ArgumentException("Valor da transação deve ser maior que zero!");
        }

        this.Descricao = descricao;
        this.Tipo = tipo;
        this.Valor = valor;

        AtribuirPontos();
    }

    private void AtribuirPontos()
    {
        if (this.Tipo == "Deposito")
        {
            this.Pontos = 10;
        }
        else if (this.Tipo == "Saque")
        {
            this.Pontos = 5;
        }
        else if (this.Tipo == "Tarifa")
        {
            this.Pontos = 0;
        }
        else
        {
            this.Pontos = 0;
        }
    }
}