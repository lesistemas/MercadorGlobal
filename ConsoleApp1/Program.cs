using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
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


        // Novo campo para controle interno da duração
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

        public decimal CargaAtual()
        {
            return Inventario.Sum(i => i.PesoUnitario * i.Quantidade);
        }
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
            // Reduz turnos ativos e remove eventos encerrados
            foreach (var evento in EventosAtivos.ToList())
            {
                evento.TurnosAtivo--;
                if (evento.TurnosAtivo <= 0)
                {
                    EventosAtivos.Remove(evento);
                    EventosFinalizados.Add(evento);
                    Console.WriteLine($"✅ Evento encerrado: {evento.Nome}");
                }
            }

            // Ativa no máximo 1 evento novo por turno (se houver pendentes)
            if (EventosPendentes.Any())
            {
                var candidatos = EventosPendentes.ToList();
                var escolhido = candidatos[rnd.Next(candidatos.Count)];

                // Define duração aleatória entre 1 e 3 turnos
                escolhido.TurnosAtivo = rnd.Next(1, 4);

                EventosAtivos.Add(escolhido);
                EventosPendentes.Remove(escolhido);

                Console.WriteLine($"🚨 Novo evento ativado: {escolhido.Nome} (duração: {escolhido.TurnosAtivo} turno(s))");
            }

            TurnoAtual++;
        }
    }

    class Program
    {
        // Variáveis globais
        private static Dictionary<string, string> emojiPaises = new Dictionary<string, string>
        {
            {"Brasil", "🇧🇷"}, {"EUA", "🇺🇸"}, {"Japão", "🇯🇵"},
            {"Alemanha", "🇩🇪"}, {"China", "🇨🇳"}, {"Rússia", "🇷🇺"}
        };

        private static string estabilidadeVisual = "Média";
        private static int riscoConfisco = 15;
        private static Produto melhorProduto;
        private static decimal precoMelhorProduto;
        private static string produtoMaisLucrativo = "Nenhum";
        private static decimal lucroMaior = 0;
        private static bool ehIlegal = false;
        private static string dicaGlobalVenda = "Considere vender itens com lucro acima de 20%";
        private static string produtoMenorLucro = "Nenhum";
        private static Pais paisAtual;
        private static string produtoMaisValorizado = "Nenhum";
        private static string produtoIlegalEmEstoque = "Nenhum";
        private static decimal lucroTurno = 0;
        private static string melhorVendaTurnoAnterior = "Nenhuma";
        private static List<string> acoesDoTurno = new List<string>();
        private static Dictionary<int, List<decimal>> historicoPrecos = new Dictionary<int, List<decimal>>();

        // Métodos auxiliares
        static string ObterEmoji(string nomePais)
        {
            return emojiPaises.TryGetValue(nomePais, out var emoji) ? emoji : "🌎";
        }

        static string ObterTendenciaProduto(Produto produto)
        {
            string[] tendencias = { "Alta forte ▲▲", "Alta moderada ▲", "Estável →", "Queda moderada ▼", "Queda forte ▼▼" };
            return tendencias[(produto.Id + DateTime.Now.Second) % tendencias.Length];
        }

        static string HistoricoPreco(Produto produto)
        {
            if (!historicoPrecos.ContainsKey(produto.Id))
            {
                historicoPrecos[produto.Id] = new List<decimal>();
                for (int i = 0; i < 3; i++)
                {
                    historicoPrecos[produto.Id].Add(produto.PrecoBase * (decimal)(0.8 + new Random().NextDouble() * 0.4));
                }
            }

            var historico = historicoPrecos[produto.Id];
            string resultado = "";
            for (int i = 0; i < Math.Min(3, historico.Count - 1); i++)
            {
                decimal variacao = (historico[i + 1] - historico[i]) / historico[i] * 100;
                resultado += variacao >= 0 ? $"↑{variacao:0.#}% " : $"↓{-variacao:0.#}% ";
            }
            return resultado.Trim();
        }

        static string GerarDicaCompra(Produto produto, decimal preco, string status, string tendencia)
        {
            if (status.Contains("Ilegal")) return "Não compre - produto ilegal!";
            if (status.Contains("Embargo")) return "Embargo ativo - evite comprar";

            if (tendencia.Contains("Alta forte")) return "Ótima oportunidade - compre agora!";
            if (tendencia.Contains("Alta moderada")) return "Boa oportunidade - considere comprar";
            if (tendencia.Contains("Queda")) return "Preço em queda - espere para comprar";

            return "Preço estável - compre se necessário";
        }

        static string GerarDicaVenda(Produto produto, decimal precoVenda, decimal precoCompra, string status)
        {
            if (status.Contains("Ilegal")) return "Venda rápido - risco de confisco!";

            decimal lucroPercentual = (precoVenda - precoCompra) / precoCompra * 100;

            if (lucroPercentual > 30) return "Lucro excelente - venda agora!";
            if (lucroPercentual > 15) return "Bom lucro - considere vender";
            if (lucroPercentual < 0) return "Prejuízo - espere valorizar";

            return "Lucro moderado - avalie mercado";
        }

        static string GerarDicaInventario(Produto produto, decimal precoLocal, decimal precoCompra, string status)
        {
            if (status.Contains("Ilegal")) return "Prioridade venda - risco alto!";

            decimal lucroPercentual = (precoLocal - precoCompra) / precoCompra * 100;
            string tendencia = ObterTendenciaProduto(produto);

            if (tendencia.Contains("Queda") && lucroPercentual > 10)
                return "Venda antes da queda!";

            if (tendencia.Contains("Alta") && lucroPercentual < 5)
                return "Segure para valorizar";

            return "Mantenha ou venda parcial";
        }

        static string ObterImportacoes(Pais pais)
        {
            if (pais.NecessidadeDeCompra.Count > 0)
                return $"{pais.NecessidadeDeCompra.Count} itens";
            return "Poucas";
        }

        static string ObterExportacoes(Pais pais)
        {
            if (pais.ForcaDeVenda.Count > 0)
                return $"{pais.ForcaDeVenda.Count} itens";
            return "Poucas";
        }

        static decimal CalcularPrecoFinal(Produto produto, Pais pais, List<Evento> eventos, Jogador jogador)
        {
            decimal preco = produto.PrecoBase;
            Random rnd = new Random();

            if (pais.ForcaDeVenda.Contains(produto.Id))
                preco *= 0.8m;
            if (pais.NecessidadeDeCompra.Contains(produto.Id))
                preco *= 1.3m;

            foreach (var ev in eventos)
            {
                if (ev.ProdutosAfetados.ContainsKey(produto.Id))
                {
                    preco *= (1 + ev.ProdutosAfetados[produto.Id]);
                }
            }

            if (!jogador.ReputacaoPorPais.ContainsKey(pais.Id))
            {
                jogador.ReputacaoPorPais[pais.Id] = 50;
            }

            decimal modificadorReputacao = (jogador.ReputacaoPorPais[pais.Id] - 50) / 1000.0m;
            preco *= (1 + modificadorReputacao);

            preco *= (1 + pais.TaxaImportacao);
            preco *= (decimal)(1 + (rnd.NextDouble() * 0.2 - 0.1));

            // Atualiza histórico de preços
            if (!historicoPrecos.ContainsKey(produto.Id))
            {
                historicoPrecos[produto.Id] = new List<decimal>();
            }
            historicoPrecos[produto.Id].Add(preco);
            if (historicoPrecos[produto.Id].Count > 5)
            {
                historicoPrecos[produto.Id].RemoveAt(0);
            }

            return Math.Max(Math.Round(preco, 2), 1.00m);
        }

        static decimal CalcularCustoViagem(Pais destino, Jogador jogador, List<Evento> eventos)
        {
            decimal baseCusto = 50;
            decimal risco = eventos.Where(e => e.RiscoComercial.ContainsKey(destino.Id)).Sum(e => e.RiscoComercial[destino.Id]);
            decimal adicional = baseCusto * risco;
            decimal desconto = 0;

            if (jogador.ReputacaoPorPais.TryGetValue(destino.Id, out int reputacao))
            {
                desconto = baseCusto * ((reputacao - 50) / 200.0m);
            }

            return Math.Max(baseCusto + adicional - desconto, 10);
        }

        static void ExibirMenuCompra(Jogador jogador, List<Produto> produtos, List<Pais> paises, List<Evento> eventos)
        {
            paisAtual = paises.First(p => p.Id == jogador.PaisAtualId);
            var eventosAtivos = eventos.Where(e => e.TurnosAtivo > 0).ToList();

            // Atualiza melhor produto para compra
            melhorProduto = produtos
                .Where(p => p.Tipo == ProdutoTipo.Normal)
                .OrderBy(p => CalcularPrecoFinal(p, paisAtual, eventosAtivos, jogador) / p.PrecoBase)
                .FirstOrDefault();

            precoMelhorProduto = melhorProduto != null ?
                CalcularPrecoFinal(melhorProduto, paisAtual, eventosAtivos, jogador) : 0;

            Console.Clear();
            Console.WriteLine("════════════ 🛒 MERCADO LOCAL – COMPRAR PRODUTOS ════════════");
            Console.WriteLine($"📍 País: {ObterEmoji(paisAtual.Nome)} {paisAtual.Nome}       💰 R${jogador.Dinheiro:0.00}       🎒 Carga: {jogador.CargaAtual():0.0}kg / {jogador.CapacidadeCarga}kg");
            Console.WriteLine($"🌟 Reputação: {jogador.ReputacaoPorPais[paisAtual.Id]}/100    🧊 Estabilidade: {estabilidadeVisual}     🚨 Confisco: {riscoConfisco}%");
            Console.WriteLine();

            Console.WriteLine("┌────┬────────────────────┬────────────┬──────────────────────┬────────────┬──────┬────────────┬────────────┬────────────────────────────────────────┐");
            Console.WriteLine("│ ID │ Produto            │ 💰 Local   │ Tendência            │ Situação   │ Peso │ Seu Estoque│ Últimos    │ Dica Estratégica                       │");
            Console.WriteLine("├────┼────────────────────┼────────────┼──────────────────────┼────────────┼──────┼────────────┼────────────┼────────────────────────────────────────┤");

            foreach (var produto in produtos)
            {
                decimal preco = CalcularPrecoFinal(produto, paisAtual, eventosAtivos, jogador);
                bool legal = ProdutoEstaLegalNoPais(produto, paisAtual, eventosAtivos);
                string status = legal ? "✅ Legal" : "❌ Ilegal";

                string tendencia = ObterTendenciaProduto(produto);
                var estoqueJogador = jogador.Inventario.FirstOrDefault(i => i.ProdutoId == produto.Id);
                string estoqueStr = estoqueJogador != null ? $"{estoqueJogador.Quantidade} un. (R${estoqueJogador.PrecoCompra:0.00})" : "0 un.";
                string ultimosPrecos = HistoricoPreco(produto);

                string dica = GerarDicaCompra(produto, preco, status, tendencia);

                Console.WriteLine($"│ {produto.Id,2} │ {produto.Nome,-18} │ R${preco,8:0.00} │ {tendencia,-20} │ {status,-10} │ {produto.Peso,4:0.0} │ {estoqueStr,-10} │ {ultimosPrecos,-10} │ {dica,-38} │");
            }

            Console.WriteLine("└────┴────────────────────┴────────────┴──────────────────────┴────────────┴──────┴────────────┴────────────┴────────────────────────────────────────┘");
            Console.WriteLine();
            Console.WriteLine("════════════ 📊 ANÁLISE ESTRATÉGICA DO MERCADO ════════════");
            Console.WriteLine($"📈 Melhor oportunidade: {(melhorProduto != null ? melhorProduto.Nome : "Nenhum")} (R${precoMelhorProduto:0.00} – valorização em alta)");
            Console.WriteLine($"⚠️ Você já tem {jogador.CargaAtual():0.0}kg ocupados – escolha com sabedoria (máx: {jogador.CapacidadeCarga - jogador.CargaAtual():0.0}kg)");
            Console.WriteLine("❗ Itens Ilegais NÃO podem ser comprados");
            Console.WriteLine("💡 Use 'max' na quantidade para saber quanto pode comprar com dinheiro e espaço");
            Console.WriteLine();
            Console.Write("Digite o ID do produto para COMPRAR ou 0 para voltar: ");

            if (int.TryParse(Console.ReadLine(), out int idProduto) && idProduto > 0)
            {
                var prod = produtos.FirstOrDefault(p => p.Id == idProduto);
                if (prod == null || prod.Tipo == ProdutoTipo.Ilegal)
                {
                    Console.WriteLine("❌ Produto ilegal ou inválido. Não pode ser comprado.");
                    Console.ReadKey();
                    return;
                }

                decimal preco = CalcularPrecoFinal(prod, paisAtual, eventosAtivos, jogador);
                Console.Write("Quantidade: ");
                string input = Console.ReadLine();

                int quantidade;
                if (input.ToLower() == "max")
                {
                    decimal pesoDisponivel = jogador.CapacidadeCarga - jogador.CargaAtual();
                    int maxPorPeso = (int)(pesoDisponivel / prod.Peso);
                    int maxPorDinheiro = (int)(jogador.Dinheiro / preco);
                    quantidade = Math.Min(maxPorPeso, maxPorDinheiro);
                    Console.WriteLine($"Quantidade máxima possível: {quantidade}");
                }
                else if (!int.TryParse(input, out quantidade) || quantidade <= 0)
                {
                    Console.WriteLine("Quantidade inválida.");
                    Console.ReadKey();
                    return;
                }

                decimal pesoTotal = quantidade * prod.Peso;
                decimal custoTotal = quantidade * preco;

                if (jogador.CargaAtual() + pesoTotal > jogador.CapacidadeCarga)
                {
                    Console.WriteLine("⚠️ Excesso de carga.");
                }
                else if (jogador.Dinheiro < custoTotal)
                {
                    Console.WriteLine("⚠️ Dinheiro insuficiente.");
                }
                else
                {
                    jogador.Dinheiro -= custoTotal;
                    var existente = jogador.Inventario.FirstOrDefault(i => i.ProdutoId == prod.Id);
                    if (existente != null)
                    {
                        existente.Quantidade += quantidade;
                    }
                    else
                    {
                        jogador.Inventario.Add(new InventarioItem
                        {
                            ProdutoId = prod.Id,
                            Nome = prod.Nome,
                            Quantidade = quantidade,
                            PesoUnitario = prod.Peso,
                            PrecoCompra = preco
                        });
                    }
                    Console.WriteLine($"✅ Comprou {quantidade}x {prod.Nome} por R${custoTotal:0.00}");
                    acoesDoTurno.Add($"Compra: {quantidade}x {prod.Nome} por R${custoTotal:0.00}");
                }
                Console.ReadKey();
            }
        }

        static void ExibirMenuVenda(Jogador jogador, List<Produto> produtos, List<Pais> paises, List<Evento> eventos)
        {
            paisAtual = paises.First(p => p.Id == jogador.PaisAtualId);
            var eventosAtivos = eventos.Where(e => e.TurnosAtivo > 0).ToList();

            // Atualiza informações de lucro para exibição
            var itensComLucro = jogador.Inventario
                .Where(i => i.Quantidade > 0)
                .Select(i =>
                {
                    var prod = produtos.First(p => p.Id == i.ProdutoId);
                    decimal precoVenda = CalcularPrecoFinal(prod, paisAtual, eventosAtivos, jogador);
                    decimal lucroPercentual = (precoVenda - i.PrecoCompra) / i.PrecoCompra * 100;
                    return new { Produto = prod, Item = i, Lucro = lucroPercentual };
                })
                .ToList();

            if (itensComLucro.Any())
            {
                var maisLucrativo = itensComLucro.OrderByDescending(x => x.Lucro).First();
                produtoMaisLucrativo = maisLucrativo.Produto.Nome;
                lucroMaior = maisLucrativo.Lucro;
                ehIlegal = maisLucrativo.Produto.Tipo == ProdutoTipo.Ilegal;

                var menosLucrativo = itensComLucro.OrderBy(x => x.Lucro).First();
                produtoMenorLucro = menosLucrativo.Produto.Nome;
            }

            Console.Clear();
            Console.WriteLine("════════════ 💰 VENDA DE PRODUTOS – INVENTÁRIO COMPLETO ════════════");
            Console.WriteLine($"📍 País atual: {ObterEmoji(paisAtual.Nome)} {paisAtual.Nome}     💰 R${jogador.Dinheiro:0.00}     🎒 {jogador.CargaAtual():0.0}kg / {jogador.CapacidadeCarga}kg");
            Console.WriteLine($"🌟 Reputação: {jogador.ReputacaoPorPais[paisAtual.Id]}/100     🚨 Confisco de ilegais: {riscoConfisco}% por turno");
            Console.WriteLine();

            Console.WriteLine("┌────┬────────────────────┬────┬──────┬──────────────┬──────────────┬──────────────┬────────────┬────────────────────────────────────────┐");
            Console.WriteLine("│ ID │ Produto            │Qtd │Peso  │ 💰 Preço Local│ 💵 Preço Compra│ Lucro/Perda %│ Histórico   │ Dica Estratégica                       │");
            Console.WriteLine("├────┼────────────────────┼────┼──────┼──────────────┼──────────────┼──────────────┼────────────┼────────────────────────────────────────┤");

            foreach (var item in jogador.Inventario.Where(i => i.Quantidade > 0))
            {
                var produto = produtos.First(p => p.Id == item.ProdutoId);
                decimal precoVenda = CalcularPrecoFinal(produto, paisAtual, eventosAtivos, jogador);
                decimal lucro = precoVenda - item.PrecoCompra;
                decimal lucroPercentual = ((precoVenda - item.PrecoCompra) / item.PrecoCompra) * 100;
                bool legal = ProdutoEstaLegalNoPais(produto, paisAtual, eventosAtivos);
                string status = legal ? "✅ Legal" : "❌ Ilegal"; 
                string historico = HistoricoPreco(produto);
                string dica = GerarDicaVenda(produto, precoVenda, item.PrecoCompra, status);

                Console.WriteLine($"│ {produto.Id,2} │ {item.Nome,-18} │ {item.Quantidade,3} │ {item.PesoUnitario * item.Quantidade,4:0.0} │ R${precoVenda,8:0.00} │ R${item.PrecoCompra,8:0.00} │ {lucroPercentual,+6:0.##}%      │ {historico,-10} │ {dica,-38} │");
            }

            Console.WriteLine("└────┴────────────────────┴────┴──────┴──────────────┴──────────────┴──────────────┴────────────┴────────────────────────────────────────┘");
            Console.WriteLine();
            Console.WriteLine("════════════ 📊 RESUMO TÁTICO DE VENDA ════════════");
            Console.WriteLine($"🏆 Maior lucro: {produtoMaisLucrativo} (+{lucroMaior:0.#}%) {(ehIlegal ? "— mas ilegal!" : "")}");
            Console.WriteLine($"📉 Produto com menor valor atual: {produtoMenorLucro}");
            Console.WriteLine($"📈 Recomendação: {dicaGlobalVenda}");
            Console.WriteLine();
            Console.Write("Digite o ID do produto para VENDER ou 0 para voltar: ");

            if (int.TryParse(Console.ReadLine(), out int idVenda) && idVenda > 0)
            {
                var item = jogador.Inventario.FirstOrDefault(i => i.ProdutoId == idVenda);
                if (item == null || item.Quantidade == 0)
                {
                    Console.WriteLine("Produto não disponível.");
                    Console.ReadKey();
                    return;
                }

                var produto = produtos.First(p => p.Id == item.ProdutoId);
                decimal preco = CalcularPrecoFinal(produto, paisAtual, eventosAtivos, jogador);

                Console.Write("Quantidade para vender: ");
                if (int.TryParse(Console.ReadLine(), out int qtdVenda) && qtdVenda > 0)
                {
                    if (qtdVenda > item.Quantidade)
                    {
                        Console.WriteLine("Quantidade excede o estoque.");
                        Console.ReadKey();
                        return;
                    }

                    decimal total = qtdVenda * preco;
                    item.Quantidade -= qtdVenda;
                    jogador.Dinheiro += total;
                    Console.WriteLine($"✅ Vendeu {qtdVenda}x {item.Nome} por R${total:0.00}");
                    acoesDoTurno.Add($"Venda: {qtdVenda}x {item.Nome} por R${total:0.00}");

                    // Remove item se quantidade zerou
                    if (item.Quantidade == 0)
                    {
                        jogador.Inventario.Remove(item);
                    }
                    Console.ReadKey();
                }
            }
        }

        static void ExibirMenuViagem(Jogador jogador, List<Pais> paises, List<Evento> eventos)
        {
            paisAtual = paises.First(p => p.Id == jogador.PaisAtualId);
            var eventosAtivos = eventos.Where(e => e.TurnosAtivo > 0).ToList();
            var destinosDisponiveis = paises.Where(p => p.Id != paisAtual.Id).ToList();

            Console.Clear();
            Console.WriteLine("════════════ ✈️ ANÁLISE DE PAÍSES E OPÇÕES DE VIAGEM ════════════");
            Console.WriteLine($"📍 País atual: {ObterEmoji(paisAtual.Nome)} {paisAtual.Nome}     💰 R${jogador.Dinheiro:0.00}     🎒 Carga: {jogador.CargaAtual():0.0}kg / {jogador.CapacidadeCarga}kg");
            Console.WriteLine($"🌟 Reputação no país: {jogador.ReputacaoPorPais[paisAtual.Id]}/100     🧊 Estabilidade: {estabilidadeVisual}");
            Console.WriteLine();

            Console.WriteLine("════════════ DISPONÍVEIS PARA VIAGEM ════════════");
            Console.WriteLine("┌────┬────────────┬──────────────┬────────────┬────────────┬────────────┬────────────┬────────────┐");
            Console.WriteLine("│ ID │ País       │ Relações     │ Importa     │ Exporta     │ Risco      │ Viagem     │ Status     │");
            Console.WriteLine("├────┼────────────┼──────────────┼────────────┼────────────┼────────────┼────────────┼────────────┤");

            foreach (var destino in destinosDisponiveis)
            {
                string relacao = paisAtual.RelacoesDiplomaticas.ContainsKey(destino.Id) ?
                    paisAtual.RelacoesDiplomaticas[destino.Id] : "Desconhecida";
                decimal risco = eventosAtivos.Where(ev => ev.RiscoComercial.ContainsKey(destino.Id)).Sum(ev => ev.RiscoComercial[destino.Id]);
                decimal custo = CalcularCustoViagem(destino, jogador, eventosAtivos);
                string status = risco > 0.2m ? "🔴 Crise" : risco > 0.1m ? "🟡 Tensão" : "🟢 Estável";
                bool bloqueado = eventosAtivos.Any(ev => ev.PaisesComViagemBloqueada.Contains(destino.Id));
                if (bloqueado) status = "⛔ Bloqueado";

                string importa = ObterImportacoes(destino);
                string exporta = ObterExportacoes(destino);

                Console.WriteLine($"│ {destino.Id,2} │ {ObterEmoji(destino.Nome)} {destino.Nome,-10} │ {relacao,-12} │ {importa,-10} │ {exporta,-10} │ {risco * 100,4:0}% │ R${custo,6:0.00} │ {status,-10} │");
            }

            Console.WriteLine("└────┴────────────┴──────────────┴────────────┴────────────┴────────────┴────────────┴────────────┘");

            Console.WriteLine();
            Console.WriteLine("════════════ 🧭 ANÁLISE DETALHADA – EXEMPLO: País Selecionado ════════════");
            Console.WriteLine("📦 Produtos que você pode vender: (baseado na legalidade e valorização)");
            Console.WriteLine("💼 Força comercial: ...");
            Console.WriteLine("🧱 Fraqueza comercial: ...");
            Console.WriteLine("🌀 Evento ativo: ...");
            Console.WriteLine("🚨 Risco comercial: ...");
            Console.WriteLine("🌐 Relação diplomática com atual: ...");
            Console.WriteLine("💰 Custo de viagem: ...");
            Console.WriteLine("🔁 A viagem consumirá o turno atual");

            Console.WriteLine();
            Console.Write("Digite o ID do país para VIAJAR ou 0 para voltar: ");

            if (int.TryParse(Console.ReadLine(), out int idDestino) && idDestino > 0)
            {
                var destino = paises.FirstOrDefault(p => p.Id == idDestino);
                if (destino == null || destino.Id == paisAtual.Id)
                {
                    Console.WriteLine("Destino inválido.");
                    Console.ReadKey();
                    return;
                }

                bool bloqueado = eventosAtivos.Any(ev => ev.PaisesComViagemBloqueada.Contains(destino.Id));
                if (bloqueado)
                {
                    Console.WriteLine("⛔ Viagem bloqueada por evento.");
                    Console.ReadKey();
                    return;
                }

                decimal custo = CalcularCustoViagem(destino, jogador, eventosAtivos);
                if (jogador.Dinheiro < custo)
                {
                    Console.WriteLine("❌ Dinheiro insuficiente para viagem.");
                    Console.ReadKey();
                    return;
                }

                jogador.Dinheiro -= custo;
                jogador.PaisAtualId = destino.Id;
                Console.WriteLine($"✈️ Viajou para {destino.Nome} por R${custo:0.00}");

                motor.AtualizarTurno();
                var produtos = InputDados.ListaDeProdutos;
                FinalizarTurno(jogador, motor.EventosAtivos, produtos, motor.TurnoAtual);

                Console.ReadKey();
            }
        }

        static void ExibirEventosAtivos(List<Evento> eventos, List<Pais> paises, List<Produto> produtos)
        {
            Console.Clear();
            Console.WriteLine("════════════ ⚠️ EVENTOS ATIVOS NO MUNDO ════════════");
            Console.WriteLine($"📆 Turno atual: {motor.TurnoAtual} / 10     Total de eventos ativos: {eventos.Count}");
            Console.WriteLine();

            int count = 1;
            foreach (var evento in eventos)
            {
                Console.WriteLine($"🌀 {count}. {evento.Nome}     (⭐ Gravidade {evento.Gravidade} – faltam {evento.TurnosAtivo} turno(s))");
                Console.WriteLine($"📖 {evento.Descricao}");

                if (evento.PaisesAfetadosDiretos.Any())
                {
                    Console.Write("🌍 Países afetados diretamente: ");
                    Console.WriteLine(string.Join(", ", evento.PaisesAfetadosDiretos.Select(id => ObterEmoji(paises.First(p => p.Id == id).Nome) + " " + paises.First(p => p.Id == id).Nome)));
                }

                if (evento.ProdutosAfetados.Any())
                {
                    Console.WriteLine("📦 Produtos impactados:");
                    foreach (var kv in evento.ProdutosAfetados)
                    {
                        var nome = produtos.FirstOrDefault(p => p.Id == kv.Key)?.Nome ?? "Desconhecido";
                        string variacao = kv.Value > 0 ? $"+{kv.Value * 100:0}%" : $"{kv.Value * 100:0}%";
                        Console.WriteLine($"   - {nome}: {variacao}");
                    }
                }

                if (evento.PaisesFavorecidos.Any())
                {
                    Console.WriteLine("🌟 Países favorecidos:");
                    foreach (var kv in evento.PaisesFavorecidos)
                    {
                        var paisNome = paises.FirstOrDefault(p => p.Id == kv.Key)?.Nome ?? "Desconhecido";
                        var produtosBonus = kv.Value.Select(pid => produtos.FirstOrDefault(p => p.Id == pid.Key)?.Nome + $" (+{pid.Value * 100:0}%)");
                        Console.WriteLine($"   - {paisNome}: {string.Join(", ", produtosBonus)}");
                    }
                }

                if (evento.RiscoComercial.Any())
                {
                    Console.WriteLine("🔻 Risco comercial:");
                    foreach (var kv in evento.RiscoComercial)
                    {
                        var paisNome = paises.FirstOrDefault(p => p.Id == kv.Key)?.Nome ?? "Desconhecido";
                        Console.WriteLine($"   - {paisNome}: {kv.Value * 100:0}%");
                    }
                }

                if (evento.ModificadorReputacao.Any())
                {
                    Console.WriteLine("🧭 Reputação:");
                    foreach (var kv in evento.ModificadorReputacao)
                    {
                        var paisNome = paises.FirstOrDefault(p => p.Id == kv.Key)?.Nome ?? "Desconhecido";
                        Console.WriteLine($"   - {paisNome}: {(kv.Value >= 0 ? "+" : "")}{kv.Value}");
                    }
                }

                if (evento.PaisesComViagemBloqueada.Any())
                {
                    Console.WriteLine("⛔ Viagens bloqueadas: " +
                        string.Join(", ", evento.PaisesComViagemBloqueada.Select(id => ObterEmoji(paises.First(p => p.Id == id).Nome) + " " + paises.First(p => p.Id == id).Nome)));
                }

                Console.WriteLine(new string('═', 90));
                count++;
            }

            Console.WriteLine();
            Console.WriteLine("💡 Eventos afetam preços, riscos e reputação de forma dinâmica.");
            Console.WriteLine("🔁 Todos os efeitos duram por turnos definidos e desaparecem ao final.");
            Console.WriteLine();
            Console.WriteLine("Pressione qualquer tecla para voltar ao menu.");
            Console.ReadKey();
        }

        static void FinalizarTurno(Jogador jogador, List<Evento> eventos, List<Produto> produtos, int turnoAtual)
        {
            Console.Clear();
            Console.WriteLine($"════════════ 🏁 FIM DO TURNO {turnoAtual} – RELATÓRIO COMPLETO ════════════");

            // Acessa o país atual com base no ID do jogador
            var paisAtual = InputDados.ListaDePaises.FirstOrDefault(p => p.Id == jogador.PaisAtualId);

            // Confisco de produtos ilegais no país atual
            var ilegais = jogador.Inventario
                .Where(i =>
                    !ProdutoEstaLegalNoPais(produtos.First(p => p.Id == i.ProdutoId), paisAtual, eventos) &&
                    produtos.First(p => p.Id == i.ProdutoId).Tipo == ProdutoTipo.Ilegal &&
                    i.Quantidade > 0)
                .ToList();


            Random rng = new Random();
            List<string> confiscados = new();

            int baseRisco = 20;
            int reputacao;
            if (!jogador.ReputacaoPorPais.TryGetValue(jogador.PaisAtualId, out reputacao))
            {
                reputacao = 50; // Valor padrão neutro
                jogador.ReputacaoPorPais[jogador.PaisAtualId] = reputacao;
            }
            if (reputacao < 30)
                baseRisco += 15;

            bool fiscalizacaoReforcada = eventos.Any(e =>
                e.TurnosAtivo > 0 &&
                e.Categoria == CategoriaEvento.Politico &&
                e.PaisesAfetadosDiretos.Contains(jogador.PaisAtualId));

            if (fiscalizacaoReforcada)
                baseRisco += 10;

            foreach (var item in ilegais)
            {
                if (rng.Next(1, 101) <= Math.Clamp(baseRisco, 5, 70))
                {
                    confiscados.Add($"{item.Quantidade}x {item.Nome} (Risco: {baseRisco}%)");
                    jogador.Inventario.Remove(item);
                }
            }

            // Mostrar inventário final
            Console.WriteLine($"📍 Local atual: {paisAtual.Nome}        💰 Dinheiro: R${jogador.Dinheiro:0.00}        🎒 Carga: {jogador.CargaAtual():0.0}kg / {jogador.CapacidadeCarga}kg");
            Console.WriteLine($"🌟 Reputação no país: {jogador.ReputacaoPorPais[jogador.PaisAtualId]}/100");
            Console.WriteLine($"📦 Itens totais: {jogador.Inventario.Sum(i => i.Quantidade)}");

            Console.WriteLine("\n════════════ ✅ AÇÕES DO TURNO ════════════");
            foreach (var acao in acoesDoTurno)
            {
                Console.WriteLine($"• {acao}");
            }

            if (confiscados.Any())
            {
                Console.WriteLine("\n🚨 Produtos confiscados:");
                foreach (var item in confiscados)
                    Console.WriteLine($"• {item}");
            }
            else
            {
                Console.WriteLine("\n✅ Nenhum item ilegal foi confiscado neste turno.");
            }

            Console.WriteLine("\n🔄 Eventos atualizados:");
            foreach (var evento in eventos.Where(e => e.TurnosAtivo == 0))
            {
                Console.WriteLine($"🌀 Evento encerrado: {evento.Nome}");
            }

            Console.WriteLine("\nPressione qualquer tecla para iniciar o próximo turno...");
            acoesDoTurno.Clear();
            Console.ReadKey();
        }

        static void ExibirFimDeJogo(Jogador jogador, List<Evento> eventos, List<Pais> paises, List<Produto> produtos, int turnoFinal)
        {
            Console.Clear();
            Console.WriteLine("════════════ 🏁 FIM DO JOGO – RELATÓRIO FINAL ════════════");

            var paisFinal = paises.First(p => p.Id == jogador.PaisAtualId);
            int totalItens = jogador.Inventario.Sum(i => i.Quantidade);
            int ilegais = jogador.Inventario.Count(i =>
                !ProdutoEstaLegalNoPais(produtos.First(p => p.Id == i.ProdutoId), paisFinal, eventos) &&
                produtos.First(p => p.Id == i.ProdutoId).Tipo == ProdutoTipo.Ilegal); decimal pesoTotal = jogador.CargaAtual();
            double reputacaoMedia = jogador.ReputacaoPorPais.Values.Any() ? jogador.ReputacaoPorPais.Values.Average() : 50;

            Console.WriteLine($"📆 Duração da campanha: {turnoFinal} turnos");
            Console.WriteLine($"🧳 País final: {paisFinal.Nome}        💰 Dinheiro final: R${jogador.Dinheiro:0.00}");
            Console.WriteLine($"📦 Inventário final: {totalItens} produtos ({ilegais} ilegais) – {pesoTotal:0.0}kg / {jogador.CapacidadeCarga}kg");
            Console.WriteLine($"🌟 Reputação média global: {reputacaoMedia:0.0}/100");

            // Lucros
            decimal lucroTotal = jogador.Inventario.Sum(i => i.Quantidade * (CalcularPrecoFinal(produtos.First(p => p.Id == i.ProdutoId), paisFinal, eventos, jogador) - i.PrecoCompra));

            var itemMaisLucrativo = jogador.Inventario
                .Where(i => i.Quantidade > 0)
                .OrderByDescending(i => CalcularPrecoFinal(produtos.First(p => p.Id == i.ProdutoId), paisFinal, eventos, jogador) - i.PrecoCompra)
                .FirstOrDefault();

            var itemMaisArriscado = jogador.Inventario
                .Where(i => produtos.First(p => p.Id == i.ProdutoId).Tipo == ProdutoTipo.Ilegal)
                .OrderByDescending(i => i.Quantidade)
                .FirstOrDefault();

            Console.WriteLine("\n════════════ 📊 DESEMPENHO FINANCEIRO ════════════");
            Console.WriteLine($"💸 Lucro total acumulado: R${lucroTotal:0.00}");
            Console.WriteLine($"📈 Produto mais lucrativo: {(itemMaisLucrativo != null ? itemMaisLucrativo.Nome : "Nenhum")}");
            Console.WriteLine($"📉 Produto mais arriscado: {(itemMaisArriscado != null ? itemMaisArriscado.Nome : "Nenhum")}");
            Console.WriteLine($"🛒 Total de compras: {totalItens}");
            Console.WriteLine($"💰 Total de vendas: {(int)(totalItens * 0.85)} (estimativa)");

            Console.WriteLine("\n════════════ 🧠 DESTAQUES ESTRATÉGICOS ════════════");
            if (itemMaisLucrativo != null)
                Console.WriteLine($"✅ Melhor decisão: Vender {itemMaisLucrativo.Nome} com lucro");
            if (itemMaisArriscado != null)
                Console.WriteLine($"⚠️ Pior decisão: Manter {itemMaisArriscado.Nome} ilegal até o fim");
            if (ilegais > 0)
                Console.WriteLine($"🏆 Jogada mais ousada: Conservar itens ilegais com risco");

            Console.WriteLine("\n════════════ 🌍 INTERAÇÃO COM O MUNDO ════════════");
            var eventosEnfrentados = eventos.Where(e => e.TurnosAtivo < e.Gravidade).ToList();
            Console.WriteLine($"🌀 Eventos enfrentados: {eventosEnfrentados.Count}");
            foreach (var e in eventosEnfrentados)
                Console.WriteLine($"• {e.Nome}");

            Console.WriteLine($"✈️ Países visitados: {string.Join(", ", jogador.ReputacaoPorPais.Keys.Select(id => paises.First(p => p.Id == id).Nome))}");
            Console.WriteLine($"⛔ Viagens bloqueadas enfrentadas: {eventos.SelectMany(e => e.PaisesComViagemBloqueada).Distinct().Count()}");

            Console.WriteLine("\n════════════ 🧾 ANÁLISE FINAL ════════════");
            Console.WriteLine("🔹 Estratégia equilibrada, com boas decisões táticas.");
            if (ilegais > 0) Console.WriteLine("🔹 Itens ilegais impactaram o risco – avalie reduzir em futuras rodadas.");
            if (pesoTotal >= jogador.CapacidadeCarga * 0.9m) Console.WriteLine("🔹 Melhor uso da carga poderia gerar mais lucro.");

            Console.WriteLine("\n════════════ 🧠 CLASSIFICAÇÃO FINAL ════════════");
            Console.WriteLine($"⭐ DESEMPENHO: {(lucroTotal >= 1500 ? "AVANÇADO" : lucroTotal >= 800 ? "INTERMEDIÁRIO" : "INICIANTE")}");
            Console.WriteLine("🎯 TÁTICA: ALTA PRECISÃO");
            Console.WriteLine($"🧪 RISCO: {(ilegais > 0 ? "MODERADO" : "BAIXO")}");
            Console.WriteLine("💡 DECISÃO: EFICIENTE");

            Console.WriteLine("\nObrigado por jogar o Mercador Global!");
            Console.WriteLine("Pressione qualquer tecla para sair...");
            Console.ReadKey();
        }

        private static MotorDoJogo motor;
        static bool ProdutoEstaLegalNoPais(Produto produto, Pais pais, List<Evento> eventos)
        {
            // Se produto está fora da lista ProdutosAceitos, é ilegal por padrão
            bool legalBase = pais.ProdutosAceitos.Contains(produto.Id);

            // Se evento ativo coloca embargo no produto, ele é ilegal mesmo se estiver aceito
            bool embargado = eventos.Any(ev =>
                ev.ProdutosAfetados.TryGetValue(produto.Id, out decimal impacto) && impacto == -1 &&
                (ev.PaisesAfetadosDiretos.Contains(pais.Id) || !ev.PaisesAfetadosDiretos.Any()) // Se afeta o país ou é global
            );

            return legalBase && !embargado;
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
                paisAtual = paises.First(p => p.Id == jogador.PaisAtualId);
                int reputacao = 0;
                if (jogador.ReputacaoPorPais.TryGetValue(jogador.PaisAtualId, out var valor))
                {
                    reputacao = valor;
                }
                Console.Clear();
                Console.WriteLine("════════════ 🎮 MERCADOR GLOBAL – MENU PRINCIPAL ════════════");
                Console.WriteLine($"📆 Turno: {turno} / 10             ⏳ Turnos restantes: {10 - turno}");
                Console.WriteLine($"📍 País Atual: {ObterEmoji(paisAtual.Nome)} {paisAtual.Nome}");
                Console.WriteLine($"💰 Dinheiro: R${jogador.Dinheiro:0.00}      🎒 Carga Atual: {jogador.CargaAtual():0.0}kg / {jogador.CapacidadeCarga}kg");
                Console.WriteLine($"📦 Produtos no inventário: {jogador.Inventario.Count} tipos / {jogador.Inventario.Sum(i => i.Quantidade)} unidades");
                Console.WriteLine($"🛡️ Reputação local: {reputacao} / 100  🧊 Estabilidade do país: {estabilidadeVisual}");
                Console.WriteLine($"🚨 Risco de confisco de ilegais: {riscoConfisco}% por turno");
                Console.WriteLine();
                // Adicione isso ao final da exibição do menu principal
                if (motor.EventosAtivos.Any())
                {
                    Console.WriteLine();
                    Console.WriteLine("════════════════════════ 📢 EVENTOS ATIVOS – RESUMO TÁTICO ════════════════════════\n");

                    var eventosRelevantes = motor.EventosAtivos
                        .Where(e =>
                            e.PaisesAfetadosDiretos.Contains(paisAtual.Id) ||
                            (e.PaisesFavorecidos?.ContainsKey(paisAtual.Id) ?? false) ||
                            (e.RiscoComercial?.ContainsKey(paisAtual.Id) ?? false))
                        .ToList();

                    foreach (var evento in eventosRelevantes)
                    {
                        Console.WriteLine($"🌀 {evento.Nome} ({evento.TurnosAtivo} turno restante{(evento.TurnosAtivo > 1 ? "s" : "")})");

                        if (evento.ProdutosAfetados.Any())
                        {
                            var afetados = evento.ProdutosAfetados.Select(kv =>
                            {
                                var nome = InputDados.ListaDeProdutos.FirstOrDefault(p => p.Id == kv.Key)?.Nome ?? "Desconhecido";
                                string variacao = kv.Value > 0 ? $"+{kv.Value * 100:0}%" : $"{kv.Value * 100:0}%";
                                return $"• 📦 {nome}: {variacao}";
                            });
                            foreach (var linha in afetados)
                                Console.WriteLine(linha);
                        }

                        if (evento.PaisesFavorecidos.Any())
                        {
                            foreach (var kv in evento.PaisesFavorecidos)
                            {
                                var paisNome = InputDados.ListaDePaises.FirstOrDefault(p => p.Id == kv.Key)?.Nome ?? "?";
                                var emoji = emojiPaises.TryGetValue(paisNome, out string e) ? e : "🌍";
                                var produtosBonus = kv.Value.Select(pv =>
                                {
                                    var nome = InputDados.ListaDeProdutos.FirstOrDefault(p => p.Id == pv.Key)?.Nome ?? "Desconhecido";
                                    return $"{nome} (+{pv.Value * 100:0}%)";
                                });
                                Console.WriteLine($"• 🌍 Venda: {emoji} {paisNome} ({string.Join(", ", produtosBonus)})");
                            }
                        }

                        if (evento.RiscoComercial.Any())
                        {
                            var paisesRiscoAlto = evento.RiscoComercial
                                .Where(r => r.Value >= 0.3m)
                                .Select(r =>
                                {
                                    var nome = InputDados.ListaDePaises.FirstOrDefault(p => p.Id == r.Key)?.Nome ?? "?";
                                    var emoji = emojiPaises.TryGetValue(nome, out string e) ? e : "⚠️";
                                    return $"{emoji} {nome}";
                                });
                            if (paisesRiscoAlto.Any())
                                Console.WriteLine($"• ⚠️ Evite: {string.Join(", ", paisesRiscoAlto)} (risco alto)");
                        }

                        Console.WriteLine();
                    }

                    Console.WriteLine("💡 DICA CRUZADA:");

                    var produtosValorizados = eventosRelevantes
                        .SelectMany(e => e.ProdutosAfetados)
                        .Where(kv => kv.Value > 0)
                        .Select(kv => InputDados.ListaDeProdutos.FirstOrDefault(p => p.Id == kv.Key)?.Nome)
                        .Where(nome => nome != null)
                        .Distinct()
                        .ToList();

                    if (produtosValorizados.Any())
                        Console.WriteLine($"🔼 Jogue firme: venda {string.Join(", ", produtosValorizados)} em regiões quentes!");

                    var produtosTemporariamenteLegais = new List<(string Produto, List<string> PaisesIlegais)>();
                    foreach (var produto in InputDados.ListaDeProdutos)
                    {
                        bool legalAqui = ProdutoEstaLegalNoPais(produto, paisAtual, motor.EventosAtivos);
                        if (!legalAqui) continue;

                        var paisesIlegais = InputDados.ListaDePaises
                            .Where(p => p.Id != paisAtual.Id && !ProdutoEstaLegalNoPais(produto, p, motor.EventosAtivos))
                            .Select(p => emojiPaises.TryGetValue(p.Nome, out var emoji) ? $"{emoji} {p.Nome}" : p.Nome)
                            .ToList();

                        if (paisesIlegais.Any())
                            produtosTemporariamenteLegais.Add((produto.Nome, paisesIlegais));
                    }

                    if (produtosTemporariamenteLegais.Any())
                    {
                        Console.WriteLine("🟢 Produto liberado só aqui – leve pra onde é proibido!");
                        foreach (var item in produtosTemporariamenteLegais)
                            Console.WriteLine($"• {item.Produto}: tá legal aqui, mas proibido em {string.Join(", ", item.PaisesIlegais)}");
                    }

                    var produtosMuitoBaratos = InputDados.ListaDeProdutos
                        .Where(p => CalcularPrecoFinal(p, paisAtual, motor.EventosAtivos, jogador) <= p.PrecoBase * 0.7m)
                        .ToList();

                    if (produtosMuitoBaratos.Any())
                    {
                        Console.WriteLine("💰 Preço de banana aqui:");
                        foreach (var p in produtosMuitoBaratos)
                        {
                            var sugestoes = InputDados.ListaDePaises
                                .Where(pais => pais.Id != paisAtual.Id && ProdutoEstaLegalNoPais(p, pais, motor.EventosAtivos))
                                .Take(3)
                                .Select(pais => emojiPaises.TryGetValue(pais.Nome, out var emoji) ? $"{emoji} {pais.Nome}" : pais.Nome);

                            Console.WriteLine($"• {p.Nome} tão baratinhos – compra agora e exporta pra {string.Join(", ", sugestoes)}!");
                        }
                    }

                    var produtosComQuedaRecente = InputDados.ListaDeProdutos
                        .Where(p => historicoPrecos.ContainsKey(p.Id) && historicoPrecos[p.Id].Count >= 3)
                        .Where(p =>
                        {
                            var hist = historicoPrecos[p.Id];
                            return hist[^1] < hist[^2] && hist[^2] < hist[^3];
                        })
                        .ToList();

                    if (produtosComQuedaRecente.Any())
                    {
                        Console.WriteLine("📉 Oportunidade rara:");
                        foreach (var p in produtosComQuedaRecente)
                        {
                            var ultima = historicoPrecos[p.Id][^1];
                            var mediaAnterior = historicoPrecos[p.Id].Take(historicoPrecos[p.Id].Count - 1).Average();
                            var variacao = (mediaAnterior - ultima) / mediaAnterior * 100;
                            if (variacao >= 20)
                                Console.WriteLine($"• {p.Nome} caiu {variacao:0.#}% – estoque agora, vai valorizar!");
                        }
                    }

                    var tentacoes = InputDados.ListaDeProdutos
                        .Where(p => p.Tipo == ProdutoTipo.Ilegal && !ProdutoEstaLegalNoPais(p, paisAtual, motor.EventosAtivos))
                        .ToList();

                    if (tentacoes.Any())
                    {
                        Console.WriteLine("🕶️ Tentação proibida:");
                        foreach (var p in tentacoes)
                            Console.WriteLine($"• {p.Nome} tá bombando, mas é ilegal aqui. Se topar, segura firme!");
                    }

                    var rotaLimpa = InputDados.ListaDeProdutos
                        .Where(p => ProdutoEstaLegalNoPais(p, paisAtual, motor.EventosAtivos))
                        .Select(p => new
                        {
                            Produto = p,
                            Destinos = InputDados.ListaDePaises
                                .Where(pais => ProdutoEstaLegalNoPais(p, pais, motor.EventosAtivos) && pais.NecessidadeDeCompra.Contains(p.Id))
                                .Where(pais =>
                                    !motor.EventosAtivos.Any(ev => ev.RiscoComercial.ContainsKey(pais.Id) && ev.RiscoComercial[pais.Id] > 0.2m))
                                .Select(pais => emojiPaises.GetValueOrDefault(pais.Nome, "🌍") + " " + pais.Nome)
                                .ToList()
                        })
                        .Where(x => x.Destinos.Any())
                        .ToList();

                    if (rotaLimpa.Any())
                    {
                        Console.WriteLine("✈️ Rota limpa:");
                        foreach (var rota in rotaLimpa)
                            Console.WriteLine($"• {rota.Produto.Nome} é valorizado em {string.Join(", ", rota.Destinos)} e tá sem risco. Leva e lucra!");
                    }


                    // Adição de dicas novas como produto global, reputação baixa e países esquecidos
                    var produtosGlobais = InputDados.ListaDeProdutos.Where(p =>
                        InputDados.ListaDePaises.Count(pa =>
                            ProdutoEstaLegalNoPais(p, pa, motor.EventosAtivos) && pa.NecessidadeDeCompra.Contains(p.Id)) >= 5).ToList();

                    if (produtosGlobais.Any())
                    {
                        Console.WriteLine("🌐 Produto global:");
                        foreach (var p in produtosGlobais)
                        {
                            var destinos = InputDados.ListaDePaises
                                .Where(pa => ProdutoEstaLegalNoPais(p, pa, motor.EventosAtivos) && pa.NecessidadeDeCompra.Contains(p.Id))
                                .Select(pa => emojiPaises.GetValueOrDefault(pa.Nome, "🌍") + " " + pa.Nome);
                            Console.WriteLine($"• {p.Nome} tão com moral em tudo quanto é canto. Lucra fácil no {string.Join(", ", destinos)} – vai sem medo!");
                        }
                    }

                    var reputacaoRuim = InputDados.ListaDePaises
                        .Where(p =>
                            jogador.ReputacaoPorPais.TryGetValue(p.Id, out int rep) && rep < 40 &&
                            eventosRelevantes.Any(ev => ev.PaisesFavorecidos.ContainsKey(p.Id)))
                        .ToList();

                    if (reputacaoRuim.Any())
                    {
                        Console.WriteLine("🚷 Perdeu a moral:");
                        foreach (var pais in reputacaoRuim)
                        {
                            var produtosRelevantes = eventosRelevantes
                                .Where(e => e.PaisesFavorecidos.ContainsKey(pais.Id))
                                .SelectMany(e => e.PaisesFavorecidos[pais.Id].Keys)
                                .Select(id => InputDados.ListaDeProdutos.FirstOrDefault(p => p.Id == id)?.Nome)
                                .Where(nome => nome != null);
                            Console.WriteLine($"• {pais.Nome} tava pagando bem por {string.Join(", ", produtos)}, mas sua reputação lá tá no chão. Vai melhorar essa imagem!");
                        }
                    }


                    var produtosHistoricoAlta = InputDados.ListaDeProdutos
                        .Where(p => historicoPrecos.ContainsKey(p.Id) && historicoPrecos[p.Id].Count >= 3)
                        .Where(p =>
                        {
                            var h = historicoPrecos[p.Id];
                            return h[^1] > h[^2] && h[^2] > h[^3];
                        })
                        .ToList();

                    if (produtosHistoricoAlta.Any())
                    {
                        Console.WriteLine("📊 Histórico de valorização:");
                        foreach (var p in produtosHistoricoAlta)
                        {
                            Console.WriteLine($"• {p.Nome} tá subindo firme nos últimos turnos. Pode apostar!");
                        }
                    }

                    var rotaSeguraMultiProdutos = InputDados.ListaDePaises
                        .Select(p => new
                        {
                            Pais = p,
                            Produtos = InputDados.ListaDeProdutos.Where(prod =>
                                ProdutoEstaLegalNoPais(prod, p, motor.EventosAtivos) &&
                                p.NecessidadeDeCompra.Contains(prod.Id) &&
                                (!motor.EventosAtivos.Any(e => e.RiscoComercial.ContainsKey(p.Id) && e.RiscoComercial[p.Id] > 0.2m))
                            ).ToList()
                        })
                        .Where(x => x.Produtos.Count >= 2)
                        .ToList();

                    if (rotaSeguraMultiProdutos.Any())
                    {
                        Console.WriteLine("🚚 Rota segura com múltiplos produtos:");
                        foreach (var destino in rotaSeguraMultiProdutos)
                        {
                            var emoji = emojiPaises.GetValueOrDefault(destino.Pais.Nome, "🌍");
                            Console.WriteLine($"• {emoji} {destino.Pais.Nome}: aceita bem {string.Join(", ", destino.Produtos.Select(p => p.Nome))} sem risco aparente!");
                        }
                    }

                    Console.WriteLine();


                    var paisesArriscados = eventosRelevantes
                        .SelectMany(e => e.RiscoComercial)
                        .Where(kv => kv.Value >= 0.3m)
                        .Select(kv => InputDados.ListaDePaises.FirstOrDefault(p => p.Id == kv.Key)?.Nome)
                        .Where(nome => nome != null)
                        .Distinct();

                    if (paisesArriscados.Any())
                    {
                        Console.WriteLine("🚫 Evita esses lugares por enquanto – confisco tá rolando solto:");
                        foreach (var nome in paisesArriscados)
                            Console.WriteLine($"• {emojiPaises.GetValueOrDefault(nome, "🌍")} {nome} – risco alto, reputação baixa, fiscalização pesada");
                    }

                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("🔕 Nenhum evento ativo no momento. Ambiente estável. Tá tudo suave.\n");
                }

                Console.WriteLine("════════════════ 🔎 DESTAQUES E ALERTAS ESTRATÉGICOS ════════════════");

                // Atualiza informações para exibição
                if (jogador.Inventario.Any())
                {
                    var maisValorizado = jogador.Inventario
                        .OrderByDescending(i => CalcularPrecoFinal(produtos.First(p => p.Id == i.ProdutoId), paisAtual, motor.EventosAtivos, jogador) / i.PrecoCompra)
                        .First();
                    produtoMaisValorizado = maisValorizado.Nome;
                    var ilegal = jogador.Inventario
                                .FirstOrDefault(i =>
                                    !ProdutoEstaLegalNoPais(produtos.First(p => p.Id == i.ProdutoId), paisAtual, motor.EventosAtivos) &&
                                    produtos.First(p => p.Id == i.ProdutoId).Tipo == ProdutoTipo.Ilegal);
                                    produtoIlegalEmEstoque = ilegal != null ? ilegal.Nome : "Nenhum";
                }

                Console.WriteLine($"📈 Produto em alta no país atual: {produtoMaisValorizado}");
                Console.WriteLine($"🚨 Produto sob risco de confisco: {produtoIlegalEmEstoque}");
                Console.WriteLine($"⚠️ Carga quase no limite! ({jogador.CargaAtual():0.0}/{jogador.CapacidadeCarga}kg)");
                Console.WriteLine($"💸 Lucro no turno atual: R${lucroTurno:0.00}");
                Console.WriteLine($"🏆 Melhor venda do turno anterior: {melhorVendaTurnoAnterior}");
                Console.WriteLine();

                if (acoesDoTurno.Any())
                {
                    Console.WriteLine("════════════════ 🔄 AÇÕES REALIZADAS NESTE TURNO ════════════════");
                    foreach (var acao in acoesDoTurno)
                    {
                        Console.WriteLine($"✅ {acao}");
                    }
                }
                Console.WriteLine();

                Console.WriteLine("1. Ver Inventário");
                Console.WriteLine("2. Comprar Produtos");
                Console.WriteLine("3. Vender Produtos");
                Console.WriteLine("4. Viajar entre Países");
                Console.WriteLine("5. Ver Eventos Ativos");
                Console.WriteLine("6. Finalizar Turno");
                Console.Write("Escolha uma opção: ");
                var opcao = Console.ReadLine();

                if (opcao == "1")
                {
                    Console.Clear();
                    Console.WriteLine("════════════ 📦 INVENTÁRIO ESTRATÉGICO ════════════");
                    Console.WriteLine($"📍 País atual: {ObterEmoji(paisAtual.Nome)} {paisAtual.Nome}     💰 R${jogador.Dinheiro:0.00}     🎒 {jogador.CargaAtual():0.0}kg / {jogador.CapacidadeCarga}kg");
                    Console.WriteLine($"🌟 Reputação: {jogador.ReputacaoPorPais[paisAtual.Id]}/100     🚨 Confisco de ilegais: {riscoConfisco}% por turno");
                    Console.WriteLine();

                    Console.WriteLine("┌────┬────────────────────┬────┬──────┬──────────────┬──────────────┬──────────────┬────────────┬────────────────────────────────────────┐");
                    Console.WriteLine("│ ID │ Produto            │Qtd │Peso  │ 💰 Local     │ 💵 Compra    │ Diferença    │ Situação   │ Dica Estratégica                       │");
                    Console.WriteLine("├────┼────────────────────┼────┼──────┼──────────────┼──────────────┼──────────────┼────────────┼────────────────────────────────────────┤");

                    foreach (var item in jogador.Inventario)
                    {
                        var produto = produtos.First(p => p.Id == item.ProdutoId);
                        decimal precoLocal = CalcularPrecoFinal(produto, paisAtual, motor.EventosAtivos, jogador);
                        decimal diferenca = ((precoLocal - item.PrecoCompra) / item.PrecoCompra) * 100;
                        bool legal = ProdutoEstaLegalNoPais(produto, paisAtual, motor.EventosAtivos);
                        string status = legal ? "✅ Legal" : "❌ Ilegal";

                        string dica = GerarDicaInventario(produto, precoLocal, item.PrecoCompra, status);

                        Console.WriteLine($"│ {produto.Id,2} │ {produto.Nome,-18} │ {item.Quantidade,3} │ {item.PesoUnitario * item.Quantidade,4:0.0} │ R${precoLocal,8:0.00} │ R${item.PrecoCompra,8:0.00} │ {diferenca,+6:0.##}%      │ {status,-10} │ {dica,-38} │");
                    }

                    Console.WriteLine("└────┴────────────────────┴────┴──────┴──────────────┴──────────────┴──────────────┴────────────┴────────────────────────────────────────┘");

                    Console.WriteLine();
                    Console.WriteLine("════════════ 🔎 RESUMO ESTRATÉGICO DO INVENTÁRIO ════════════");

                    if (jogador.Inventario.Any())
                    {
                        var maisValorizado = jogador.Inventario
                            .OrderByDescending(i => CalcularPrecoFinal(produtos.First(p => p.Id == i.ProdutoId), paisAtual, motor.EventosAtivos, jogador) / i.PrecoCompra)
                            .First();
                        var maiorLucro = ((CalcularPrecoFinal(produtos.First(p => p.Id == maisValorizado.ProdutoId), paisAtual, motor.EventosAtivos, jogador) - maisValorizado.PrecoCompra) / maisValorizado.PrecoCompra * 100);

                        Console.WriteLine($"📈 Produto mais valorizado agora: {maisValorizado.Nome} (+{maiorLucro:0.#}%)");
                    }

                    var ilegal = jogador.Inventario.FirstOrDefault(i => produtos.First(p => p.Id == i.ProdutoId).Tipo == ProdutoTipo.Ilegal);
                    Console.WriteLine($"🚨 Produto sob risco de confisco: {(ilegal != null ? ilegal.Nome : "Nenhum")}");
                    Console.WriteLine($"⚠️ Alerta de sobrepeso: Carga {jogador.CargaAtual() / jogador.CapacidadeCarga * 100:0.0}% cheia!");
                    Console.WriteLine("💡 Recomendação: Venda itens com bom lucro e descarte riscos ilegais");
                    Console.WriteLine();
                    Console.WriteLine("Pressione qualquer tecla para voltar...");
                    Console.ReadKey();
                }
                else if (opcao == "2")
                {
                    ExibirMenuCompra(jogador, produtos, paises, motor.EventosAtivos);
                }
                else if (opcao == "3")
                {
                    ExibirMenuVenda(jogador, produtos, paises, motor.EventosAtivos);
                }
                else if (opcao == "4")
                {
                    ExibirMenuViagem(jogador, paises, motor.EventosAtivos);
                }
                else if (opcao == "5")
                {
                    ExibirEventosAtivos(motor.EventosAtivos, paises, produtos);
                }
                else if (opcao == "6")
                {
                    motor.AtualizarTurno();
                    FinalizarTurno(jogador, motor.EventosAtivos, produtos, motor.TurnoAtual);
                    turno++;

                    if (turno > 10)
                    {
                        ExibirFimDeJogo(jogador, motor.EventosAtivos, paises, produtos, turno - 1);
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("Opção inválida.");
                    Console.ReadKey();
                }
            }
        }
    }
}