using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    // Enums e classes base
    public enum ProdutoTipo { Normal, Ilegal }
    public enum CategoriaEvento { Economico, Politico, Ambiental, Sanitario, Militar, Cultural, Financeiro, Tecnologico }

    public class Produto
    {
        public int Id;
        public string Nome;
        public decimal PrecoBase;
        public decimal Peso;
        public ProdutoTipo Tipo;
    }

    public class Pais
    {
        public int Id;
        public string Nome;
        public string Descricao;
        public List<int> ForcaDeVenda;
        public List<int> NecessidadeDeCompra;
        public List<int> ProdutosAceitos;
        public decimal TaxaImportacao;
        public string ForcaComercial;
        public string FraquezaComercial;
        public string TendenciaEconomica;
        public Dictionary<int, string> RelacoesDiplomaticas;
    }

    public class EventoEncadeado
    {
        public int Id { get; set; }
        public int EventoOrigemId { get; set; }
        public string Condicao { get; set; }
        public Evento EventoResultado { get; set; }
    }

    public class Evento
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Gravidade { get; set; }
        public CategoriaEvento Categoria { get; set; }
        public List<int> PaisesAfetadosDiretos { get; set; }
        public Dictionary<int, decimal> ProdutosAfetados { get; set; }
        public Dictionary<int, Dictionary<int, decimal>> PaisesFavorecidos { get; set; }
        public List<int> PaisesComViagemBloqueada { get; set; }
        public Dictionary<int, decimal> RiscoComercial { get; set; }
        public Dictionary<int, decimal> ModificadorReputacao { get; set; }
        public int TurnosAtivo { get; set; } = 0;
    }

    public class InventarioItem
    {
        public int ProdutoId;
        public string Nome;
        public int Quantidade;
        public decimal PesoUnitario;
        public decimal PrecoCompra;
    }

    public class Jogador
    {
        public int PaisAtualId;
        public decimal Dinheiro;
        public List<InventarioItem> Inventario;
        public Dictionary<int, int> ReputacaoPorPais;
        public decimal CapacidadeCarga;

        public Jogador()
        {
            Inventario = new List<InventarioItem>();
            ReputacaoPorPais = new Dictionary<int, int>();
            Dinheiro = 1000;
            CapacidadeCarga = 100;
        }

        public decimal CargaAtual() => Inventario.Sum(i => i.PesoUnitario * i.Quantidade);
    }

    public class MotorDoJogo
    {
        public List<Evento> EventosAtivos { get; set; }
        public List<Evento> EventosFinalizados { get; set; }
        public List<Evento> EventosPendentes { get; set; }
        public int TurnoAtual { get; private set; } = 1;

        private Random rnd = new Random();

        public MotorDoJogo(List<Evento> eventosBase)
        {
            EventosAtivos = new List<Evento>();
            EventosFinalizados = new List<Evento>();
            EventosPendentes = eventosBase;
        }

        public void AtualizarTurno()
        {
            // Processa eventos ativos
            foreach (var evento in EventosAtivos.ToList())
            {
                evento.TurnosAtivo--;
                if (evento.TurnosAtivo <= 0)
                {
                    EventosAtivos.Remove(evento);
                    EventosFinalizados.Add(evento);
                }
            }

            // Ativa novo evento se houver pendentes
            if (EventosPendentes.Any())
            {
                var escolhido = EventosPendentes[rnd.Next(EventosPendentes.Count)];
                escolhido.TurnosAtivo = rnd.Next(1, 4);
                EventosAtivos.Add(escolhido);
                EventosPendentes.Remove(escolhido);
            }

            TurnoAtual++;
        }
    }

    // Classe para gerenciar a UI do menu principal
    public static class MenuPrincipalUI
    {
        private static Dictionary<string, string> emojiPaises = new Dictionary<string, string>
        {
            {"Brasil", "🇧🇷"}, {"EUA", "🇺🇸"}, {"Japão", "🇯🇵"},
            {"Alemanha", "🇩🇪"}, {"China", "🇨🇳"}, {"Rússia", "🇷🇺"}
        };

        public static void Exibir(Jogador jogador, MotorDoJogo motor, List<Pais> paises, List<Produto> produtos)
        {
            var paisAtual = paises.First(p => p.Id == jogador.PaisAtualId);

            Console.Clear();
            Console.WriteLine("════════════ 🎮 MERCADOR GLOBAL – MENU PRINCIPAL ════════════");
            Console.WriteLine($"📆 Turno: {motor.TurnoAtual} / 10");
            Console.WriteLine($"📍 País Atual: {ObterEmoji(paisAtual.Nome)} {paisAtual.Nome}");
            Console.WriteLine($"💰 Dinheiro: R${jogador.Dinheiro:0.00}");
            Console.WriteLine($"🎒 Carga: {jogador.CargaAtual():0.0}kg / {jogador.CapacidadeCarga}kg");

            ExibirEventosAtivosResumo(motor, paisAtual, paises, produtos);
            ExibirDestaquesEstrategicos(jogador, motor, paises, produtos);

            Console.WriteLine("\n1. Ver Inventário");
            Console.WriteLine("2. Comprar Produtos");
            Console.WriteLine("3. Vender Produtos");
            Console.WriteLine("4. Viajar entre Países");
            Console.WriteLine("5. Ver Eventos Ativos");
            Console.WriteLine("6. Finalizar Turno");
            Console.WriteLine("7. Compre Dicas");
            Console.Write("Escolha uma opção: ");
        }

        private static string ObterEmoji(string nomePais) =>
            emojiPaises.TryGetValue(nomePais, out var emoji) ? emoji : "🌎";

        private static void ExibirEventosAtivosResumo(MotorDoJogo motor, Pais paisAtual, List<Pais> paises, List<Produto> produtos)
        {
            if (motor.EventosAtivos.Any())
            {
                Console.WriteLine("\n════════════ 📢 EVENTOS ATIVOS – RESUMO ═════════════");
                foreach (var evento in motor.EventosAtivos)
                {
                    Console.WriteLine($"🌀 {evento.Nome} ({evento.TurnosAtivo} turno(s) restante(s)");
                }
            }
        }

        private static void ExibirDestaquesEstrategicos(Jogador jogador, MotorDoJogo motor, List<Pais> paises, List<Produto> produtos)
        {
            var paisAtual = paises.First(p => p.Id == jogador.PaisAtualId);

            Console.WriteLine("\n════════════ 🔎 DESTAQUES ESTRATÉGICOS ════════════");

            if (jogador.Inventario.Any())
            {
                var maisValorizado = jogador.Inventario
                    .OrderByDescending(i => CalculadoraComercio.CalcularPrecoFinal(produtos.First(p => p.Id == i.ProdutoId), paisAtual, motor.EventosAtivos, jogador) / i.PrecoCompra)
                    .First();
                Console.WriteLine($"📈 Produto em alta: {maisValorizado.Nome}");
            }

            Console.WriteLine($"⚠️ Carga: {jogador.CargaAtual():0.0}/{jogador.CapacidadeCarga}kg");
        }
    }

    // Classe para gerenciar a UI do inventário
    public static class InventarioUI
    {
        public static void Exibir(Jogador jogador, List<Produto> produtos, List<Pais> paises, List<Evento> eventos)
        {
            var paisAtual = paises.First(p => p.Id == jogador.PaisAtualId);

            Console.Clear();
            Console.WriteLine("════════════ 📦 INVENTÁRIO ════════════");
            Console.WriteLine($"📍 País atual: {paisAtual.Nome}");
            Console.WriteLine($"💰 R${jogador.Dinheiro:0.00} 🎒 {jogador.CargaAtual():0.0}kg/{jogador.CapacidadeCarga}kg");

            Console.WriteLine("\n┌────┬────────────────────┬────┬──────┬──────────────┬──────────────┐");
            Console.WriteLine("│ ID │ Produto            │Qtd │Peso  │ Preço Local  │ Preço Compra │");
            Console.WriteLine("├────┼────────────────────┼────┼──────┼──────────────┼──────────────┤");

            foreach (var item in jogador.Inventario)
            {
                var produto = produtos.First(p => p.Id == item.ProdutoId);
                decimal precoLocal = CalculadoraComercio.CalcularPrecoFinal(produto, paisAtual, eventos, jogador);

                Console.WriteLine($"│ {produto.Id,2} │ {produto.Nome,-18} │ {item.Quantidade,3} │ {item.PesoUnitario * item.Quantidade,4:0.0} │ R${precoLocal,8:0.00} │ R${item.PrecoCompra,8:0.00} │");
            }

            Console.WriteLine("└────┴────────────────────┴────┴──────┴──────────────┴──────────────┘");
            Console.WriteLine("\nPressione qualquer tecla para voltar...");
            Console.ReadKey();
        }
    }

    // Classe para gerenciar a UI de compra
    public static class CompraUI
    {
        public static void Exibir(Jogador jogador, List<Produto> produtos, List<Pais> paises, List<Evento> eventos)
        {
            var paisAtual = paises.First(p => p.Id == jogador.PaisAtualId);

            Console.Clear();
            Console.WriteLine("════════════ 🛒 COMPRAR PRODUTOS ════════════");
            Console.WriteLine($"📍 País: {paisAtual.Nome}");
            Console.WriteLine($"💰 R${jogador.Dinheiro:0.00} 🎒 {jogador.CargaAtual():0.0}kg/{jogador.CapacidadeCarga}kg");

            Console.WriteLine("\n┌────┬────────────────────┬────────────┬────────────┐");
            Console.WriteLine("│ ID │ Produto            │ Preço      │ Peso       │");
            Console.WriteLine("├────┼────────────────────┼────────────┼────────────┤");

            foreach (var produto in produtos)
            {
                decimal preco = CalculadoraComercio.CalcularPrecoFinal(produto, paisAtual, eventos, jogador);
                Console.WriteLine($"│ {produto.Id,2} │ {produto.Nome,-18} │ R${preco,8:0.00} │ {produto.Peso,4:0.0}kg │");
            }

            Console.WriteLine("└────┴────────────────────┴────────────┴────────────┘");
            Console.WriteLine("\nDigite o ID do produto para comprar ou 0 para voltar: ");

            if (int.TryParse(Console.ReadLine(), out int idProduto) && idProduto > 0)
            {
                var produto = produtos.FirstOrDefault(p => p.Id == idProduto);
                if (produto != null)
                {
                    ProcessarCompra(jogador, produto, paisAtual, eventos);
                }
            }
        }

        private static void ProcessarCompra(Jogador jogador, Produto produto, Pais paisAtual, List<Evento> eventos)
        {
            decimal preco = CalculadoraComercio.CalcularPrecoFinal(produto, paisAtual, eventos, jogador);

            Console.Write("Quantidade: ");
            if (int.TryParse(Console.ReadLine(), out int quantidade) && quantidade > 0)
            {
                decimal custoTotal = quantidade * preco;
                decimal pesoTotal = quantidade * produto.Peso;

                if (jogador.Dinheiro >= custoTotal && jogador.CargaAtual() + pesoTotal <= jogador.CapacidadeCarga)
                {
                    jogador.Dinheiro -= custoTotal;
                    AdicionarAoInventario(jogador, produto, quantidade, preco);
                    Console.WriteLine($"✅ Comprou {quantidade}x {produto.Nome} por R${custoTotal:0.00}");
                }
                else
                {
                    Console.WriteLine("⚠️ Recursos insuficientes (dinheiro ou espaço)");
                }
                Console.ReadKey();
            }
        }

        private static void AdicionarAoInventario(Jogador jogador, Produto produto, int quantidade, decimal preco)
        {
            var itemExistente = jogador.Inventario.FirstOrDefault(i => i.ProdutoId == produto.Id);

            if (itemExistente != null)
            {
                itemExistente.Quantidade += quantidade;
            }
            else
            {
                jogador.Inventario.Add(new InventarioItem
                {
                    ProdutoId = produto.Id,
                    Nome = produto.Nome,
                    Quantidade = quantidade,
                    PesoUnitario = produto.Peso,
                    PrecoCompra = preco
                });
            }
        }
    }

    // Classe para gerenciar a UI de venda
    public static class VendaUI
    {
        public static void Exibir(Jogador jogador, List<Produto> produtos, List<Pais> paises, List<Evento> eventos)
        {
            var paisAtual = paises.First(p => p.Id == jogador.PaisAtualId);

            Console.Clear();
            Console.WriteLine("════════════ 💰 VENDER PRODUTOS ════════════");
            Console.WriteLine($"📍 País: {paisAtual.Nome}");
            Console.WriteLine($"💰 R${jogador.Dinheiro:0.00} 🎒 {jogador.CargaAtual():0.0}kg/{jogador.CapacidadeCarga}kg");

            Console.WriteLine("\n┌────┬────────────────────┬────┬──────────────┬──────────────┐");
            Console.WriteLine("│ ID │ Produto            │Qtd │ Preço Venda  │ Preço Compra │");
            Console.WriteLine("├────┼────────────────────┼────┼──────────────┼──────────────┤");

            foreach (var item in jogador.Inventario.Where(i => i.Quantidade > 0))
            {
                var produto = produtos.First(p => p.Id == item.ProdutoId);
                decimal precoVenda = CalculadoraComercio.CalcularPrecoFinal(produto, paisAtual, eventos, jogador);

                Console.WriteLine($"│ {produto.Id,2} │ {produto.Nome,-18} │ {item.Quantidade,3} │ R${precoVenda,8:0.00} │ R${item.PrecoCompra,8:0.00} │");
            }

            Console.WriteLine("└────┴────────────────────┴────┴──────────────┴──────────────┘");
            Console.WriteLine("\nDigite o ID do produto para vender ou 0 para voltar: ");

            if (int.TryParse(Console.ReadLine(), out int idProduto) && idProduto > 0)
            {
                var item = jogador.Inventario.FirstOrDefault(i => i.ProdutoId == idProduto);
                if (item != null && item.Quantidade > 0)
                {
                    ProcessarVenda(jogador, item, produtos, paisAtual, eventos);
                }
            }
        }

        private static void ProcessarVenda(Jogador jogador, InventarioItem item, List<Produto> produtos, Pais paisAtual, List<Evento> eventos)
        {
            var produto = produtos.First(p => p.Id == item.ProdutoId);
            decimal precoVenda = CalculadoraComercio.CalcularPrecoFinal(produto, paisAtual, eventos, jogador);

            Console.Write("Quantidade: ");
            if (int.TryParse(Console.ReadLine(), out int quantidade) && quantidade > 0 && quantidade <= item.Quantidade)
            {
                decimal total = quantidade * precoVenda;
                jogador.Dinheiro += total;
                item.Quantidade -= quantidade;

                if (item.Quantidade == 0)
                {
                    jogador.Inventario.Remove(item);
                }

                Console.WriteLine($"✅ Vendeu {quantidade}x {item.Nome} por R${total:0.00}");
                Console.ReadKey();
            }
        }
    }

    // Classe para gerenciar a UI de viagem
    public static class ViagemUI
    {
        public static void Exibir(Jogador jogador, List<Pais> paises, MotorDoJogo motor)
        {
            var paisAtual = paises.First(p => p.Id == jogador.PaisAtualId);
            var destinos = paises.Where(p => p.Id != paisAtual.Id).ToList();

            Console.Clear();
            Console.WriteLine("════════════ ✈️ VIAGEM ENTRE PAÍSES ════════════");
            Console.WriteLine($"📍 Atual: {paisAtual.Nome}");
            Console.WriteLine($"💰 R${jogador.Dinheiro:0.00}");

            Console.WriteLine("\n┌────┬────────────────────┬────────────┬────────────┐");
            Console.WriteLine("│ ID │ País               │ Custo      │ Relação    │");
            Console.WriteLine("├────┼────────────────────┼────────────┼────────────┤");

            foreach (var destino in destinos)
            {
                decimal custo = CalculadoraComercio.CalcularCustoViagem(destino, jogador, motor.EventosAtivos);
                string relacao = paisAtual.RelacoesDiplomaticas.ContainsKey(destino.Id) ?
                    paisAtual.RelacoesDiplomaticas[destino.Id] : "Neutra";

                Console.WriteLine($"│ {destino.Id,2} │ {destino.Nome,-18} │ R${custo,8:0.00} │ {relacao,-10} │");
            }

            Console.WriteLine("└────┴────────────────────┴────────────┴────────────┘");
            Console.WriteLine("\nDigite o ID do país para viajar ou 0 para voltar: ");

            if (int.TryParse(Console.ReadLine(), out int idDestino) && idDestino > 0)
            {
                var destino = paises.FirstOrDefault(p => p.Id == idDestino);
                if (destino != null)
                {
                    ProcessarViagem(jogador, destino, motor);
                }
            }
        }

        private static void ProcessarViagem(Jogador jogador, Pais destino, MotorDoJogo motor)
        {
            decimal custo = CalculadoraComercio.CalcularCustoViagem(destino, jogador, motor.EventosAtivos);

            if (jogador.Dinheiro >= custo)
            {
                jogador.Dinheiro -= custo;
                jogador.PaisAtualId = destino.Id;
                motor.AtualizarTurno();
                Console.WriteLine($"✈️ Viajou para {destino.Nome} por R${custo:0.00}");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("❌ Dinheiro insuficiente para viagem");
                Console.ReadKey();
            }
        }
    }

    // Classe para gerenciar a UI de eventos
    public static class EventosUI
    {
        public static void Exibir(List<Evento> eventos, List<Pais> paises, List<Produto> produtos)
        {
            Console.Clear();
            Console.WriteLine("════════════ ⚠️ EVENTOS ATIVOS ════════════");

            if (!eventos.Any())
            {
                Console.WriteLine("\nNenhum evento ativo no momento");
            }
            else
            {
                foreach (var evento in eventos)
                {
                    Console.WriteLine($"\n🌀 {evento.Nome} (Turnos restantes: {evento.TurnosAtivo})");
                    Console.WriteLine($"📖 {evento.Descricao}");

                    if (evento.PaisesAfetadosDiretos.Any())
                    {
                        Console.WriteLine("🌍 Países afetados: " +
                            string.Join(", ", evento.PaisesAfetadosDiretos.Select(id => paises.First(p => p.Id == id).Nome)));
                    }
                }
            }

            Console.WriteLine("\nPressione qualquer tecla para voltar...");
            Console.ReadKey();
        }
    }

    // Classe para gerenciar o fim do turno
    public static class TurnoUI
    {
        public static void Finalizar(Jogador jogador, MotorDoJogo motor, List<Produto> produtos, List<Pais> paises)
        {
            Console.Clear();
            Console.WriteLine($"════════════ 🏁 FIM DO TURNO {motor.TurnoAtual} ════════════");

            var paisAtual = paises.First(p => p.Id == jogador.PaisAtualId);
            Console.WriteLine($"📍 País: {paisAtual.Nome}");
            Console.WriteLine($"💰 R${jogador.Dinheiro:0.00} 🎒 {jogador.CargaAtual():0.0}kg/{jogador.CapacidadeCarga}kg");

            // Simular confisco de itens ilegais
            var itensIlegais = jogador.Inventario
                .Where(i => produtos.First(p => p.Id == i.ProdutoId).Tipo == ProdutoTipo.Ilegal)
                .ToList();

            if (itensIlegais.Any())
            {
                Console.WriteLine("\n🚨 Itens ilegais no inventário:");
                foreach (var item in itensIlegais)
                {
                    Console.WriteLine($"- {item.Quantidade}x {item.Nome}");
                }
            }

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }

    public static class CalculadoraComercio
    {
        /// <summary>
        /// Calcula o preço final de um produto considerando todas as variáveis do mercado
        /// </summary>
        public static decimal CalcularPrecoFinal(Produto produto, Pais pais, List<Evento> eventos, Jogador jogador)
        {
            decimal preco = produto.PrecoBase;

            // Ajustes baseados nas características do país
            if (pais.ForcaDeVenda.Contains(produto.Id))
                preco *= 0.8m;  // Desconto para produtos que o país tem em abundância
            if (pais.NecessidadeDeCompra.Contains(produto.Id))
                preco *= 1.3m;  // Acréscimo para produtos que o país precisa

            // Aplicar efeitos de eventos ativos
            foreach (var ev in eventos)
            {
                if (ev.ProdutosAfetados.ContainsKey(produto.Id))
                {
                    preco *= (1 + ev.ProdutosAfetados[produto.Id]);
                }
            }

            // Aplicar taxa de importação do país
            preco *= (1 + pais.TaxaImportacao);

            // Garantir preço mínimo de 1.00
            return Math.Max(Math.Round(preco, 2), 1.00m);
        }

        /// <summary>
        /// Calcula o custo de viagem para um país destino
        /// </summary>
        public static decimal CalcularCustoViagem(Pais destino, Jogador jogador, List<Evento> eventos)
        {
            decimal baseCusto = 50;

            // Calcular risco total baseado em eventos ativos
            decimal risco = eventos
                .Where(e => e.RiscoComercial.ContainsKey(destino.Id))
                .Sum(e => e.RiscoComercial[destino.Id]);

            // Aplicar risco ao custo base (com mínimo de 10)
            return Math.Max(baseCusto + (baseCusto * risco), 10);
        }

        /// <summary>
        /// Verifica se um produto é legal para ser comercializado em um país específico
        /// </summary>
        public static bool ProdutoEstaLegalNoPais(Produto produto, Pais pais, List<Evento> eventos)
        {
            // Verificar se o país normalmente aceita este produto
            bool legalBase = pais.ProdutosAceitos.Contains(produto.Id);

            // Verificar se há algum embargo ativo contra este produto
            bool embargado = eventos.Any(ev =>
                ev.ProdutosAfetados.TryGetValue(produto.Id, out decimal impacto) &&
                impacto == -1 &&  // -1 indica embargo total
                ev.PaisesAfetadosDiretos.Contains(pais.Id));

            return legalBase && !embargado;
        }
    }


    public enum DicaTipo
    {
        ProdutoValorizado,
        ProdutoLegalAqui,
        PrecoBaixo,
        QuedaDePreco,
        ProdutoIlegalTentador,
        RotaLimpa,
        ProdutoGlobal,
        ReputacaoBaixa,
        PaisEsquecido,
        HistoricoAlta,
        RotaSeguraMultipla,
        RiscoAlto
    }

    public static class EmojiHelper
    {
        private static readonly Dictionary<string, string> emojiPaises = new Dictionary<string, string>
        {
            {"Brasil", "🇧🇷"},
            {"EUA", "🇺🇸"},
            {"Japão", "🇯🇵"},
            {"Alemanha", "🇩🇪"},
            {"China", "🇨🇳"},
            {"Rússia", "🇷🇺"},
            {"Índia", "🇮🇳"},
            {"Reino Unido", "🇬🇧"},
            {"França", "🇫🇷"},
            {"Argentina", "🇦🇷"}
        };

        public static string ObterEmoji(string nomePais)
        {
            return emojiPaises.TryGetValue(nomePais, out var emoji) ? emoji : "🌍";
        }
    }


    public static class DicasManager
    {
        private static List<(DicaTipo Tipo, string Texto)> _dicasDisponiveis = new();
        private static List<string> _dicasCompradas = new();
        private static decimal _precoDica = 2.00m;

        public static void CarregarDicasParaTurno(Jogador jogador, MotorDoJogo motor, List<Pais> paises,
                                                List<Produto> produtos, Dictionary<int, List<decimal>> historicoPrecos)
        {
            _dicasDisponiveis.Clear();
            _dicasCompradas.Clear();

            var paisAtual = paises.First(p => p.Id == jogador.PaisAtualId);
            var eventosRelevantes = motor.EventosAtivos
                .Where(e =>
                    e.PaisesAfetadosDiretos.Contains(paisAtual.Id) ||
                    (e.PaisesFavorecidos?.ContainsKey(paisAtual.Id) ?? false) ||
                    (e.RiscoComercial?.ContainsKey(paisAtual.Id) ?? false))
                .ToList();
            var dicas = new List<(DicaTipo Tipo, string Texto)>();


            // 1. Produtos valorizados
            var produtosValorizados = eventosRelevantes
                .SelectMany(e => e.ProdutosAfetados)
                .Where(kv => kv.Value > 0)
                .Select(kv => InputDados.ListaDeProdutos.FirstOrDefault(p => p.Id == kv.Key)?.Nome)
                .Where(nome => nome != null)
                .Distinct()
                .ToList();

            if (produtosValorizados.Any())
            {
                dicas.Add((DicaTipo.ProdutoValorizado, $"🔼 Jogue firme: venda {string.Join(", ", produtosValorizados)} em regiões quentes!"));
            }

            // 2. Produtos temporariamente legais
            foreach (var produto in InputDados.ListaDeProdutos)
            {
                bool legalAqui = CalculadoraComercio.ProdutoEstaLegalNoPais(produto, paisAtual, motor.EventosAtivos);
                if (!legalAqui) continue;

                var paisesIlegais = InputDados.ListaDePaises
                   .Where(p => p.Id != paisAtual.Id && !CalculadoraComercio.ProdutoEstaLegalNoPais(produto, p, motor.EventosAtivos))
                   .Select(p => EmojiHelper.ObterEmoji(p.Nome))
                    .ToList();

                if (paisesIlegais.Any())
                {
                    dicas.Add((DicaTipo.ProdutoLegalAqui, $"🟢 {produto.Nome}: tá legal aqui, mas proibido em {string.Join(", ", paisesIlegais)}"));
                }
            }

            // 3. Produtos muito baratos
            var produtosMuitoBaratos = InputDados.ListaDeProdutos
                .Where(p => CalculadoraComercio.CalcularPrecoFinal(p, paisAtual, motor.EventosAtivos, jogador) <= p.PrecoBase * 0.7m)
                .ToList();

            foreach (var p in produtosMuitoBaratos)
            {
                var sugestoes = InputDados.ListaDePaises
                    .Where(pais => pais.Id != paisAtual.Id && CalculadoraComercio.ProdutoEstaLegalNoPais(p, pais, motor.EventosAtivos))
                    .Take(3)
                    .Select(pais => EmojiHelper.ObterEmoji(pais.Nome));

                if (sugestoes.Any())
                {
                    dicas.Add((DicaTipo.PrecoBaixo, $"💰 {p.Nome} tão baratinhos - compra agora e exporta pra {string.Join(", ", sugestoes)}!"));
                }
            }

            // 4. Produtos com queda recente
            var produtosComQuedaRecente = InputDados.ListaDeProdutos
                .Where(p => historicoPrecos.ContainsKey(p.Id) && historicoPrecos[p.Id].Count >= 3)
                .Where(p =>
                {
                    var hist = historicoPrecos[p.Id];
                    return hist[^1] < hist[^2] && hist[^2] < hist[^3];
                })
                .ToList();

            foreach (var p in produtosComQuedaRecente)
            {
                var ultima = historicoPrecos[p.Id][^1];
                var mediaAnterior = historicoPrecos[p.Id].Take(historicoPrecos[p.Id].Count - 1).Average();
                var variacao = (mediaAnterior - ultima) / mediaAnterior * 100;
                if (variacao >= 20)
                {
                    dicas.Add((DicaTipo.QuedaDePreco, $"📉 {p.Nome} caiu {variacao:0.#}% - estoque agora, vai valorizar!"));
                }
            }

            // 5. Produtos ilegais tentadores
            var tentacoes = InputDados.ListaDeProdutos
                .Where(p => p.Tipo == ProdutoTipo.Ilegal && !CalculadoraComercio.ProdutoEstaLegalNoPais(p, paisAtual, motor.EventosAtivos))
                .ToList();

            foreach (var p in tentacoes)
            {
                dicas.Add((DicaTipo.ProdutoIlegalTentador, $"🕶️ {p.Nome} tá bombando, mas é ilegal aqui. Se topar, segura firme!"));
            }

            // 6. Rotas limpas
            var rotaLimpa = InputDados.ListaDeProdutos
                .Where(p => CalculadoraComercio.ProdutoEstaLegalNoPais(p, paisAtual, motor.EventosAtivos))
                .Select(p => new
                {
                    Produto = p,
                    Destinos = InputDados.ListaDePaises
                        .Where(pais => CalculadoraComercio.ProdutoEstaLegalNoPais(p, pais, motor.EventosAtivos) && pais.NecessidadeDeCompra.Contains(p.Id))
                        .Where(pais =>
                            !motor.EventosAtivos.Any(ev => ev.RiscoComercial.ContainsKey(pais.Id) && ev.RiscoComercial[pais.Id] > 0.2m))
                        .Select(pais => EmojiHelper.ObterEmoji(pais.Nome))
                        .ToList()
                })
                .Where(x => x.Destinos.Any())
                .ToList();

            foreach (var rota in rotaLimpa)
            {
                dicas.Add((DicaTipo.RotaLimpa, $"✈️ {rota.Produto.Nome} é valorizado em {string.Join(", ", rota.Destinos)} e tá sem risco. Leva e lucra!"));
            }

            // 7. Produtos globais
            var produtosGlobais = InputDados.ListaDeProdutos.Where(p =>
                InputDados.ListaDePaises.Count(pa =>
                    CalculadoraComercio.ProdutoEstaLegalNoPais(p, pa, motor.EventosAtivos) && pa.NecessidadeDeCompra.Contains(p.Id)) >= 5).ToList();

            foreach (var p in produtosGlobais)
            {
                var destinos = InputDados.ListaDePaises
                    .Where(pa => CalculadoraComercio.ProdutoEstaLegalNoPais(p, pa, motor.EventosAtivos) && pa.NecessidadeDeCompra.Contains(p.Id))
                    .Select(pa => EmojiHelper.ObterEmoji(pa.Nome));
                dicas.Add((DicaTipo.ProdutoGlobal, $"🌐 {p.Nome} tá com moral em tudo quanto é canto. Lucra fácil no {string.Join(", ", destinos)} - vai sem medo!"));
            }

            // 8. Reputação ruim
            var reputacaoRuim = InputDados.ListaDePaises
                .Where(p =>
                    jogador.ReputacaoPorPais.TryGetValue(p.Id, out int rep) && rep < 40 &&
                    eventosRelevantes.Any(ev => ev.PaisesFavorecidos.ContainsKey(p.Id)))
                .ToList();

            foreach (var pais in reputacaoRuim)
            {
                var produtosRelevantes = eventosRelevantes
                    .Where(e => e.PaisesFavorecidos.ContainsKey(pais.Id))
                    .SelectMany(e => e.PaisesFavorecidos[pais.Id].Keys)
                    .Select(id => InputDados.ListaDeProdutos.FirstOrDefault(p => p.Id == id)?.Nome)
                    .Where(nome => nome != null);
                dicas.Add((DicaTipo.ReputacaoBaixa, $"🚷 {pais.Nome} tava pagando bem por {string.Join(", ", produtosRelevantes)}, mas sua reputação lá tá no chão. Vai melhorar essa imagem!"));
            }

            // 9. Países esquecidos
            var paisesEsquecidos = InputDados.ListaDePaises
                .Where(p =>
                    !eventosRelevantes.Any(ev => ev.PaisesAfetadosDiretos.Contains(p.Id) ||
                                                 ev.PaisesFavorecidos.ContainsKey(p.Id) ||
                                                 ev.RiscoComercial.ContainsKey(p.Id)) &&
                    p.NecessidadeDeCompra.Any())
                .ToList();

            foreach (var pais in paisesEsquecidos)
            {
                var produtosNecess = pais.NecessidadeDeCompra
                    .Select(id => InputDados.ListaDeProdutos.FirstOrDefault(p => p.Id == id)?.Nome)
                    .Where(nome => nome != null);
                dicas.Add((DicaTipo.PaisEsquecido, $"🔎 {pais.Nome} quer {string.Join(", ", produtosNecess)} e ninguém tá indo pra lá. É sua chance de dominar o mercado!"));
            }

            // 10. Histórico de alta
            var produtosHistoricoAlta = InputDados.ListaDeProdutos
                .Where(p => historicoPrecos.ContainsKey(p.Id) && historicoPrecos[p.Id].Count >= 3)
                .Where(p =>
                {
                    var h = historicoPrecos[p.Id];
                    return h[^1] > h[^2] && h[^2] > h[^3];
                })
                .ToList();

            foreach (var p in produtosHistoricoAlta)
            {
                dicas.Add((DicaTipo.HistoricoAlta, $"📊 {p.Nome} tá subindo firme nos últimos turnos. Pode apostar!"));
            }

            // 11. Rotas seguras com múltiplos produtos
            var rotaSeguraMultiProdutos = InputDados.ListaDePaises
                .Select(p => new
                {
                    Pais = p,
                    Produtos = InputDados.ListaDeProdutos.Where(prod =>
                        CalculadoraComercio.ProdutoEstaLegalNoPais(prod, p, motor.EventosAtivos) &&
                        p.NecessidadeDeCompra.Contains(prod.Id) &&
                        (!motor.EventosAtivos.Any(e => e.RiscoComercial.ContainsKey(p.Id) && e.RiscoComercial[p.Id] > 0.2m))
                    ).ToList()
                })
                .Where(x => x.Produtos.Count >= 2)
                .ToList();

            foreach (var destino in rotaSeguraMultiProdutos)
            {
                var emoji = EmojiHelper.ObterEmoji(destino.Pais.Nome);
                dicas.Add((DicaTipo.RotaSeguraMultipla, $"🚚 {emoji} {destino.Pais.Nome}: aceita bem {string.Join(", ", destino.Produtos.Select(p => p.Nome))} sem risco aparente!"));
            }

            // Gera e atribui as dicas
            _dicasDisponiveis = dicas;
        }

        
        public static bool ComprarDica(Jogador jogador)
        {
            if (jogador.Dinheiro < _precoDica)
            {
                Console.WriteLine("❌ Dinheiro insuficiente para comprar dica!");
                return false;
            }

            if (!_dicasDisponiveis.Any())
            {
                Console.WriteLine("❌ Não há mais dicas disponíveis neste turno!");
                return false;
            }

            var dica = _dicasDisponiveis.First();
            _dicasDisponiveis.RemoveAt(0);
            _dicasCompradas.Add(dica.Texto);
            jogador.Dinheiro -= _precoDica;

            Console.WriteLine($"✅ Dica comprada por R${_precoDica:0.00}:");
            Console.WriteLine(dica.Texto);
            return true;
        }

        public static void ExibirMenuDicas(Jogador jogador)
        {
            Console.Clear();
            Console.WriteLine("════════════ 💡 LOJA DE DICAS COMERCIAIS ════════════");
            Console.WriteLine($"💰 Dinheiro: R${jogador.Dinheiro:0.00} | Preço por dica: R${_precoDica:0.00}");
            Console.WriteLine($"📚 Dicas disponíveis: {_dicasDisponiveis.Count} | Dicas compradas: {_dicasCompradas.Count}");
            Console.WriteLine();

            if (_dicasCompradas.Any())
            {
                Console.WriteLine("════════════ 📖 DICAS COMPRADAS ════════════");
                foreach (var dica in _dicasCompradas)
                {
                    Console.WriteLine($"• {dica}");
                }
                Console.WriteLine();
            }

            Console.WriteLine("════════════ 🛒 DICAS DISPONÍVEIS ════════════");
            if (_dicasDisponiveis.Any())
            {
                Console.WriteLine($"1. Comprar próxima dica (R${_precoDica:0.00})");
                Console.WriteLine($"   {_dicasDisponiveis.First().Texto}");
            }
            else
            {
                Console.WriteLine("Nenhuma dica disponível no momento");
            }

            Console.WriteLine("\n0. Voltar ao menu principal");
            Console.Write("Escolha uma opção: ");

            var opcao = Console.ReadLine();
            if (opcao == "1" && _dicasDisponiveis.Any())
            {
                ComprarDica(jogador);
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
                ExibirMenuDicas(jogador); // Mostra o menu novamente após comprar
            }
        }

    }

    // Classe principal do programa
    class Program
    {
        private static MotorDoJogo motor;
        public static Dictionary<int, List<decimal>> GerarHistoricoPrecos(List<Produto> produtos, List<Pais> paises, List<Evento> eventos, Jogador jogador)
        {
            var historico = new Dictionary<int, List<decimal>>();

            foreach (var produto in produtos)
            {
                var pais = paises.First(p => p.Id == jogador.PaisAtualId);
                historico[produto.Id] = new List<decimal>
        {
            CalculadoraComercio.CalcularPrecoFinal(produto, pais, eventos, jogador)
        };
            }

            return historico;
        }


        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var produtos = InputDados.ListaDeProdutos;
            var paises = InputDados.ListaDePaises;
            var eventosBase = InputDados.ListaDeEventos;

            motor = new MotorDoJogo(eventosBase);
            var jogador = new Jogador { PaisAtualId = 1 };
            jogador.ReputacaoPorPais[1] = 60;

            int turno = 1;
            while (turno <= 10)
            {
                // No início de cada turno:
                var historicoPrecos = GerarHistoricoPrecos(produtos, paises, motor.EventosAtivos, jogador);
                DicasManager.CarregarDicasParaTurno(jogador, motor, paises, produtos, new Dictionary<int, List<decimal>>());

                MenuPrincipalUI.Exibir(jogador, motor, paises, produtos);
                var opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        InventarioUI.Exibir(jogador, produtos, paises, motor.EventosAtivos);
                        break;
                    case "2":
                        CompraUI.Exibir(jogador, produtos, paises, motor.EventosAtivos);
                        break;
                    case "3":
                        VendaUI.Exibir(jogador, produtos, paises, motor.EventosAtivos);
                        break;
                    case "4":
                        ViagemUI.Exibir(jogador, paises, motor);
                        break;
                    case "5":
                        EventosUI.Exibir(motor.EventosAtivos, paises, produtos);
                        break;
                    case "6":
                        motor.AtualizarTurno();
                        TurnoUI.Finalizar(jogador, motor, produtos, paises);
                        turno++;
                        break;
                    case "7":
                        DicasManager.ExibirMenuDicas(jogador);
                        break;
                    default:
                        Console.WriteLine("Opção inválida");
                        Console.ReadKey();
                        break;
                }
            }

            // Exibir tela de fim de jogo
            Console.Clear();
            Console.WriteLine("════════════ 🏁 FIM DO JOGO ════════════");
            Console.WriteLine($"💰 Dinheiro final: R${jogador.Dinheiro:0.00}");
            Console.WriteLine($"📦 Itens no inventário: {jogador.Inventario.Sum(i => i.Quantidade)}");
            Console.WriteLine("\nObrigado por jogar!");
            Console.ReadKey();
        }


    }
}