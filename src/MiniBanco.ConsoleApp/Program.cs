using System.ComponentModel;

internal class Program
{
    private static void AplicarFechamentoMensal(IEnumerable<Conta> contas)
    {
        foreach (Conta conta in contas)
        {
            conta.AplicarFechamentoMensal();
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

            tipoValido = int.TryParse(strTipoConta, out tipoConta);

        } while (!tipoValido);

        Conta conta = null;

        switch (tipoConta)
        {
            case 1:
                conta = new ContaCorrente(strTitular, numero);
                break;
            case 2:
                conta = new ContaPoupanca(strTitular, numero);
                break;
            case 3:
                conta = new ContaSalario(strTitular, numero, "Teste S.A.", 3000.0M);
                break;
            default:
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