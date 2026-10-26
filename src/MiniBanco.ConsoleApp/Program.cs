internal class Program
{
    private static void AplicarFechamentoMensal(IEnumerable<Conta> contas)
    {
        foreach (Conta conta in contas)
        {
            conta.AplicarFechamentoMensal();
        }
    }

    private static Conta ObterConta(IEnumerable<Conta> contas, int numero)
    {
        if (numero <= 0)
            return null;

        Conta contaMovimentacao = null;

        foreach (Conta conta in contas)
        {
            if (conta.Numero == numero)
            {
                contaMovimentacao = conta;
                break;
            }
        }

        return contaMovimentacao;
    }

    private static void FazerMovimentacao(IEnumerable<Conta> contas)
    {
        Conta contaMovimentacao = null;

        int nrConta = 0;
        bool nrContaValida = false;

        do
        {
            Console.WriteLine("Digite o número da conta: ");
            string strNumero = Console.ReadLine() ?? string.Empty;

            nrContaValida = int.TryParse(strNumero, out nrConta);

        } while (!nrContaValida);

        contaMovimentacao = ObterConta(contas, nrConta);

        if (contaMovimentacao == null)
        {
            Console.WriteLine("Número da conta informado é inválido!");
            return;
        }

        int opcao = 0;
        bool opcaoValida = false;

        do
        {
            Console.WriteLine("Digite a opção desejada: 1 - Depósito; 2 - Saque ");
            string strOpcao = Console.ReadLine() ?? string.Empty;

            if (int.TryParse(strOpcao, out opcao))
            {
                if (opcao != 1 && opcao != 2)
                {
                    Console.WriteLine("Opção inválida!");
                }
                else
                {
                    opcaoValida = true;    
                }
            }
            
        } while (!opcaoValida);

        bool valorValido = false;
        decimal valor = 0.0M;

        do
        {
            Console.WriteLine("Digite o valor: ");
            string strValor = Console.ReadLine() ?? string.Empty;

            if (decimal.TryParse(strValor, out valor))
            {
                valorValido = true;
            }

        } while (!valorValido);

        if (contaMovimentacao != null)
        {
            try
            {
                if (opcao == 1)
                {
                    contaMovimentacao.Depositar(valor, "Depósito");

                } else if (opcao == 2)
                {
                    contaMovimentacao.Sacar(valor);
                }

                Console.WriteLine("Movimentação solicitada foi efetuada!");                            
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao fazer a movimentação: " + e.Message);
            }
        }
    }

    private static void ListarContas(IEnumerable<Conta> contas)
    {
        foreach (Conta conta in contas)
        {
            string strConta = "Número: " + conta.Numero + " Titular: " + conta.Titular + " Saldo: R$ " + conta.Saldo;
            Console.WriteLine(strConta);
        }
    }

    private static Conta AbrirConta()
    {
        Console.WriteLine("Digite o nome do titular: ");
        string strTitular = Console.ReadLine() ?? string.Empty;

        bool nrValido = false;
        int numero = 0;

        do
        {
            Console.WriteLine("Digite o número da conta: ");
            string strNumero = Console.ReadLine() ?? string.Empty;

            nrValido = int.TryParse(strNumero, out numero); 

        } while (!nrValido);

        bool tipoValido = false;
        int tipoConta = 0;

        do
        {
            Console.WriteLine("Digite o tipo de conta (1 - Conta Corrente; 2 - Conta Poupança; 3 - Conta Salário): ");
            string strTipoConta = Console.ReadLine() ?? string.Empty;

            if (int.TryParse(strTipoConta, out tipoConta))
            {
                if (tipoConta != 1 && tipoConta != 2 && tipoConta != 3)
                {
                    Console.WriteLine("Esta opção não é um tipo de conta válido!");
                }
                else
                {
                    tipoValido = true;
                }
            }

        } while (!tipoValido);

        string strEmpregador = string.Empty;
        decimal limiteSaque = 0.0M;

        if (tipoConta == 3)
        {
            Console.WriteLine("Digite o nome do empregador: ");
            strEmpregador = Console.ReadLine() ?? string.Empty;

            bool limiteValido = false;

            do
            {
                Console.WriteLine("Digite o limite de saques mensais: ");
                string strLimite = Console.ReadLine() ?? string.Empty;

                limiteValido = decimal.TryParse(strLimite, out limiteSaque);

            } while (!limiteValido);
        }

        Conta conta = CriarConta(tipoConta, strTitular, numero, strEmpregador, limiteSaque);
        return conta;
    }

    private static Conta CriarConta(int tipoConta, string titular, int numero, string empregador, decimal limiteSaque)
    {
        Conta conta = null;

        switch (tipoConta)
        {
            case 1:
                conta = new ContaCorrente(titular, numero);
                break;
            case 2:
                conta = new ContaPoupanca(titular, numero);
                break;
            case 3:
                conta = new ContaSalario(titular, numero, empregador, limiteSaque);
                break;
            default:
                Console.WriteLine("Tipo de conta inválido!");
                break;
        }

        return conta;
    }

    private static void Main(string[] args)
    {
        List<Conta> contas = new List<Conta>();

        Console.WriteLine("MiniBanco.ConsoleApp: Motor de Gestão de Contas Bancárias");

        int opcao = 0;
        bool opcaoValida = false;

        do
        {
            Console.WriteLine("1 - Abrir nova conta");
            Console.WriteLine("2 - Registrar movimentação");
            Console.WriteLine("3 - Aplicar fechamento mensal");
            Console.WriteLine("4 - Exibir relatório das contas");
            Console.WriteLine("0 - Sair");

            Console.WriteLine("Digite a opção desejada: ");

            string strOpcao = Console.ReadLine() ?? string.Empty;

            opcaoValida = int.TryParse(strOpcao, out opcao);
            
            if (!opcaoValida)
            {
                Console.WriteLine("Opção inválida!");
            }

            switch (opcao)
            {
                case 1: 
                    var conta = AbrirConta();

                    if (conta != null)
                        contas.Add(conta);
                    else
                        Console.WriteLine("Não foi possível abrir a conta!");
                    break;
                case 2:
                    FazerMovimentacao(contas);
                    break;
                case 3:
                    AplicarFechamentoMensal(contas);
                    break;
                case 4:
                    ListarContas(contas);
                    break;
                case 0:
                    break;
                default:
                    break;
            }

        } while (opcao != 0 || !opcaoValida);
    }
}