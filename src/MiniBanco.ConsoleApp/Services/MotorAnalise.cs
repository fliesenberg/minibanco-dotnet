public class MotorAnalise
{
    private void MostrarMensagemInicial()
    {
        Console.WriteLine("=============================================");
        Console.WriteLine(" MINIBANCO .NET - MOTOR DE GESTÃO DE CONTAS ");
        Console.WriteLine("=============================================");        
    }

    private void MostrarMensagemFinal()
    {
        Console.WriteLine("=============================================");
        Console.WriteLine("       FIM DO PROCESSAMENTO DE CONTAS        ");
        Console.WriteLine("=============================================");                
    }

    private void MostrarMensagemInicialConta(int posConta, int qtdContas)
    {
        Console.WriteLine("");
        Console.WriteLine("[" + posConta + "/" + qtdContas + "] PROCESSANDO CONTA");
        Console.WriteLine("--------------------------------------------------------");        
    }

    private void MostrarMensagemDadosConta(Conta conta)
    {
        conta.MostrarDadosConta();
    }

    private string GetIndicadorTransacao(Transacao transacao)
    {
        string indicador = string.Empty;

        if (transacao.Tipo == "Deposito")
        {
            indicador = "[OK ]";
        }
        else if (transacao.Tipo == "Saque")
        {
            indicador = "[ ! ]";
        }
        else if (transacao.Tipo == "Tarifa")
        {
            indicador = "[ X ]";
        }

        return indicador;
    }

    private void MostrarTransacoes(Conta conta)
    {
        Console.WriteLine("> MOVIMENTAÇÕES REGISTRADAS (" + conta.Extrato.Count() + " TRANSAÇÕES):");

        foreach (Transacao transacao in conta.Extrato)
        {
            string strDescricao = transacao.Descricao.PadRight(45, '-');
            Console.WriteLine(" " + GetIndicadorTransacao(transacao) + " " + strDescricao + " " + transacao.Tipo + ": R$ " + transacao.Valor + "(" + transacao.Pontos + "pts)");
        }

        Console.WriteLine("");
    }

    private int GetQtdPontos(Conta conta)
    {
        int qtdPontos = 0;

        foreach (Transacao transacao in conta.Extrato)
        {
            qtdPontos += transacao.Pontos;
        }

        return qtdPontos;
    }

    private int GetPontosPossiveis(Conta conta)
    {
        const int PTS_MAXIMO_TRANSACAO = 10;

        return conta.Extrato.Count() * PTS_MAXIMO_TRANSACAO;
    }

    private double CalcularIndiceSaude(int qtdPontos, int pontosPossiveis)
    {
        double indiceSaude = 0.0;
        
        if (pontosPossiveis > 0)
            indiceSaude = ((double)qtdPontos / (double)pontosPossiveis) * 100;

        return indiceSaude;
    }

    private string GetClassificacaoFinal(double indiceSaude)
    {
        const double FAIXA_1 = 59.0;
        const double FAIXA_2 = 89.0;

        string classificacao = string.Empty;

        if (indiceSaude <= FAIXA_1)
        {
            classificacao = "CONTA EM RISCO";
        }
        else if (indiceSaude <= FAIXA_2)
        {
            classificacao = "CONTA EM ATENÇÃO";
        }
        else
        {
            classificacao = "CONTA SAUDÁVEL";
        }

        return classificacao;
    }

    private void MostrarResumoPontuacao(int qtdPontos, int pontosPossiveis, double indiceSaude, string classificaoFinal)
    {
        Console.WriteLine("> RESUMO DA PONTUAÇÃO: ");
        Console.WriteLine("  - Pontuação Atingida: " + qtdPontos + " de " + pontosPossiveis + " pontos possíveis");
        Console.WriteLine("  - Índice de Saúde: " + indiceSaude + " %");
        Console.WriteLine("  - Classificação Final: [" + classificaoFinal + "]");
        Console.WriteLine("");
    }

    private int MostrarItensCriticos(Conta conta)
    {
        int qtdTransacoesTarifas = 0;

        Console.WriteLine("");
        Console.WriteLine("  - ITENS CRÍTICOS (TARIFAS): ");

        foreach (Transacao transacao in conta.Extrato)
        {
            if (transacao.Tipo == "Tarifa")
            {
                ++qtdTransacoesTarifas;
                Console.WriteLine("Transacao: " + transacao.Descricao + " R$ " + transacao.Valor);
            }
        }

        return qtdTransacoesTarifas;
    }

    private int MostrarItensAtencao(Conta conta)
    {
        int qtdTransacoesSaques = 0;

        Console.WriteLine("");
        Console.WriteLine("  - ITENS DE ATENÇÃO (SAQUES): ");

        foreach (Transacao transacao in conta.Extrato)
        {
            if (transacao.Tipo == "Saque")
            {
                ++qtdTransacoesSaques;
                Console.WriteLine("Transacao: " + transacao.Descricao + " R$ " + transacao.Valor);
            }
        }

        return qtdTransacoesSaques;        
    }

    private void MostrarRecomendacoesGerencia(Conta conta, int qtdTarifas, int qtdSaques)
    {
        Console.WriteLine("> RECOMENDAÇÕES PARA A GERÊNCIA: ");

        if (qtdTarifas > 1 && (conta is ContaSalario))
        {
            Console.WriteLine("Sugerir outro tipo de conta ou revisar o pacote de tarifas!");
        }

        if (qtdSaques > 5 && (conta is ContaPoupanca))
        {
            Console.WriteLine("Sugerir outro tipo de conta");
        }

        if (conta is ContaCorrente)
        {
            if (conta.Saldo < 0.0M)
            {
                Console.WriteLine("Contatar o cliente, pois o saldo da conta está negativo");
            }
            else if (conta.Saldo > 1000.0M && qtdSaques <= 5)
            {
                Console.WriteLine("Oferecer produtos de investimento ao cliente");
            }
        }

        Console.WriteLine("-------------------------------------");
        Console.WriteLine("");
        
    }

    private void MostrarRelatorioPendenciasERecomendacoes(Conta conta)
    {
        Console.WriteLine("> RELATÓRIO DE PENDÊNCIAS: ");

        int qtdTarifas = MostrarItensCriticos(conta);
        int qtdSaques = MostrarItensAtencao(conta);

        Console.WriteLine("-------------------------------------");
        Console.WriteLine("");

        MostrarRecomendacoesGerencia(conta, qtdTarifas, qtdSaques);
    }

    private void ProcessarConta(Conta conta, int posConta, int qtdContas)
    {
        MostrarMensagemInicialConta(posConta, qtdContas);

        MostrarMensagemDadosConta(conta);

        MostrarTransacoes(conta);

        int qtdPontos = GetQtdPontos(conta);
        int pontosPossiveis = GetPontosPossiveis(conta);
        double indiceSaude = CalcularIndiceSaude(qtdPontos, pontosPossiveis);
        string classificacaoFinal = GetClassificacaoFinal(indiceSaude);

        MostrarResumoPontuacao(qtdPontos, pontosPossiveis, indiceSaude, classificacaoFinal);

        MostrarRelatorioPendenciasERecomendacoes(conta);
    }

    public void AnalisarContas(IEnumerable<Conta> contas)
    {
        MostrarMensagemInicial();

        int contador = 1;
        int qtdContas = contas.Count();

        foreach (Conta conta in contas)
        {
            ProcessarConta(conta, contador, qtdContas);

            ++contador;
        }

        MostrarMensagemFinal();
    }
}