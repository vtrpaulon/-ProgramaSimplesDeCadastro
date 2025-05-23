using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProgramaSimplesDeCadastro
{
    class Program
    {
        [System.Serializable]
        struct Cliente
        {
            public string nome;
            public string email;
            public string cpf;
        }
        static List<Cliente> clientes = new List<Cliente>();
        enum Menu { Listagem = 1, Adicionar = 2, Remover = 3, Sair = 4 }
        public static void Main(string[] args)
        {
            Carregar();
            bool escolheuSair = false;
            while (!escolheuSair)
            {
                Console.WriteLine("Sistema de clientes - Bem vindo!");
                Console.WriteLine("1-Listagem\n2-Adicionar\n3-Remover\n4-Sair");
                int intOp = int.Parse(Console.ReadLine());
                Menu opcao = (Menu)intOp;

                switch (opcao)
                {
                    case Menu.Adicionar:
                        Adicionar();
                        break;
                    case Menu.Listagem:
                        Listagem();
                        break;
                    case Menu.Remover:
                        Remover();
                        break;
                    case Menu.Sair:
                        escolheuSair = true;
                        break;
                }
                Console.Clear();
            }
        }
        static void Adicionar()
        {
            Cliente cliente = new Cliente();
            Console.WriteLine("Cadastro de cliente: ");
            Console.WriteLine("Nome do cliente: ");
            cliente.nome = Console.ReadLine();
            Console.WriteLine("Email do cliente: ");
            cliente.email = Console.ReadLine();
            Console.WriteLine("CPF do cliente:");
            cliente.cpf = Console.ReadLine();

            clientes.Add(cliente);
            Salvar();
            Console.WriteLine("Cadastro concluído, aperte enter para sair.");
            Console.ReadLine();
        }
        static void Listagem()
        {
            if (clientes.Count > 0) // SE tem pelo menos um cliente
            {
                Console.WriteLine("Lista de clientes: ");
                int i = 0;
                foreach (Cliente cliente in clientes)
                {
                    Console.WriteLine($"ID: {i}");
                    Console.WriteLine($"Nome: {cliente.nome}");
                    Console.WriteLine($"E-mail: {cliente.email}");
                    Console.WriteLine($"CPF: {cliente.cpf}");
                    Console.WriteLine("====================================");
                    i++;
                }
            }
            else
            {
                Console.WriteLine("Nenhum cliente cadastrado!");
            }
            Console.WriteLine("Aperte enter para sair.");
            Console.ReadLine();
        }
        static void Remover()
        {
            Listagem();
            Console.WriteLine("Digite o ID do cliente que você quer remover:");
            int id = int.Parse(Console.ReadLine());
            if (id >= 0 && id < clientes.Count)
            {
                clientes.RemoveAt(id);
                Salvar();
            }
            else
            {
                Console.WriteLine("Id digitado é inválido, tente novamente!");
                Console.ReadLine();
            }
        }
        static void Salvar()
        {
            string json = JsonSerializer.Serialize(clientes);
            File.WriteAllText("clientes.json", json);
        }
        static void Carregar()
        {
            if (File.Exists("clientes.json"))
            {
                try
                {
                    string json = File.ReadAllText("clientes.json");
                    clientes = JsonSerializer.Deserialize<List<Cliente>>(json) ?? new List<Cliente>();
                }
                catch
                {
                    clientes = new List<Cliente>();
                }
            }
            else
            {
                clientes = new List<Cliente>();
            }
        }
    }
}
