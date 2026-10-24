using System.ComponentModel;

internal class Program
{
    private static void ListarContas(IEnumerable<Conta> contas)
    {
        foreach (Conta conta in contas)
        {
            string strConta = "Número: " + conta.Numero + " Titular: " + conta.Titular;
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

        return new Conta(strTitular, numero);
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
                    contas.Add(conta);
                    break;
                case 2:
                    break;
                case 3:
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