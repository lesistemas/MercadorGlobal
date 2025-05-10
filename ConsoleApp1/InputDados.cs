// Lista de Eventos
using ConsoleApp1;
public static class InputDados
{
    public static List<Produto> ListaDeProdutos = new List<Produto>
    {
        new Produto
        {
            Id = 1,
            Nome = "Petróleo",
            PrecoBase = 10m,
            Peso = 5.0m,
            Tipo = ProdutoTipo.Normal
        },
        new Produto
        {
            Id = 2,
            Nome = "Cocaína",
            PrecoBase = 30m,
            Peso = 1.0m,
            Tipo = ProdutoTipo.Ilegal
        },
        new Produto
        {
            Id = 3,
            Nome = "Chips Eletrônicos",
            PrecoBase = 25m,
            Peso = 0.5m,
            Tipo = ProdutoTipo.Normal
        },
        new Produto
        {
            Id = 4,
            Nome = "Vacinas",
            PrecoBase = 8m,
            Peso = 0.3m,
            Tipo = ProdutoTipo.Normal
        },
        new Produto
        {
            Id = 5,
            Nome = "Cigarros",
            PrecoBase = 3m,
            Peso = 0.2m,
            Tipo = ProdutoTipo.Normal
        },
        new Produto
        {
            Id = 6,
            Nome = "Uísque",
            PrecoBase = 12m,
            Peso = 1.2m,
            Tipo = ProdutoTipo.Normal
        },
        new Produto
        {
            Id = 7,
            Nome = "Armas de Fogo",
            PrecoBase = 50m,
            Peso = 4.5m,
            Tipo = ProdutoTipo.Ilegal
        },
        new Produto
        {
            Id = 8,
            Nome = "Medicamentos Controlados",
            PrecoBase = 6m,
            Peso = 0.4m,
            Tipo = ProdutoTipo.Normal
        },
        new Produto
        {
            Id = 9,
            Nome = "Carros Elétricos",
            PrecoBase = 1500m,
            Peso = 800m,
            Tipo = ProdutoTipo.Normal
        },
        new Produto
        {
            Id = 10,
            Nome = "Ouro",
            PrecoBase = 200m,
            Peso = 1.0m,
            Tipo = ProdutoTipo.Normal
        },
        new Produto
        {
            Id = 11,
            Nome = "Tabaco em Folha",
            PrecoBase = 2m,
            Peso = 0.3m,
            Tipo = ProdutoTipo.Normal
        },
        new Produto
        {
            Id = 12,
            Nome = "Maconha",
            PrecoBase = 15m,
            Peso = 1.5m,
            Tipo = ProdutoTipo.Ilegal
        },
        new Produto
        {
            Id = 13,
            Nome = "Anabolizantes",
            PrecoBase = 18m,
            Peso = 0.4m,
            Tipo = ProdutoTipo.Ilegal
        },
        new Produto
        {
            Id = 14,
            Nome = "Explosivos Industriais",
            PrecoBase = 400m,
            Peso = 2.5m,
            Tipo = ProdutoTipo.Ilegal
        },
        new Produto
        {
            Id = 15,
            Nome = "Opioides Sintéticos",
            PrecoBase = 220m,
            Peso = 0.4m,
            Tipo = ProdutoTipo.Ilegal
        }
        };

    public static List<Pais> ListaDePaises = new List<Pais>
        {
        new Pais
        {
            Id = 1,
            Nome = "Estados Unidos",
            Descricao = "Potência ocidental, liberal em tecnologia, rígida em drogas.",
            ForcaDeVenda = new List<int> { 3, 9, 7 },
            NecessidadeDeCompra = new List<int> { 1, 8, 10 },
            ProdutosAceitos = new List<int> { 1, 3, 4, 5, 6, 7, 8, 9, 10, 11, 13 },
            TaxaImportacao = 0.1m,
            ForcaComercial = "Tecnologia e Defesa",
            FraquezaComercial = "Commodities e Farmacêuticos",
            TendenciaEconomica = "Liberal Capitalista",
            RelacoesDiplomaticas = new Dictionary<int, string> { { 3, "Concorrência" }, { 6, "Hostilidade" } }
        },
        new Pais
        {
            Id = 2,
            Nome = "Brasil",
            Descricao = "Exportador emergente, com fronteiras permeáveis e regulamentação flexível.",
            ForcaDeVenda = new List<int> { 2, 5, 8 },
            NecessidadeDeCompra = new List<int> { 3, 4, 9 },
            ProdutosAceitos = new List<int> { 1, 3, 4, 5, 6, 8, 9, 10, 11, 12, 13 },
            TaxaImportacao = 0.08m,
            ForcaComercial = "Agronegócio e Insumos Básicos",
            FraquezaComercial = "Alta tecnologia",
            TendenciaEconomica = "Instável",
            RelacoesDiplomaticas = new Dictionary<int, string> { { 1, "Dependência" }, { 6, "Ambígua" } }
        },
        new Pais
        {
            Id = 3,
            Nome = "China",
            Descricao = "Foco em produção e exportação massiva, controle rígido e estoque infinito.",
            ForcaDeVenda = new List<int> { 3, 1, 14 },
            NecessidadeDeCompra = new List<int> { 4, 6, 8 },
            ProdutosAceitos = new List<int> { 1, 3, 4, 6, 7, 8, 9, 10, 11, 13, 14 },
            TaxaImportacao = 0.06m,
            ForcaComercial = "Manufatura e Chips",
            FraquezaComercial = "Inovação farmacêutica",
            TendenciaEconomica = "Planejada e expansionista",
            RelacoesDiplomaticas = new Dictionary<int, string> { { 1, "Concorrência" }, { 6, "Aliança Comercial" } }
        },
        new Pais
        {
            Id = 4,
            Nome = "Alemanha",
            Descricao = "Centro da União Europeia, regulador, tecnológico e militarmente controlado.",
            ForcaDeVenda = new List<int> { 3, 6, 9 },
            NecessidadeDeCompra = new List<int> { 1, 10, 2 },
            ProdutosAceitos = new List<int> { 1, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
            TaxaImportacao = 0.12m,
            ForcaComercial = "Carros e Tecnologias limpas",
            FraquezaComercial = "Drogas e Substâncias",
            TendenciaEconomica = "Regulada",
            RelacoesDiplomaticas = new Dictionary<int, string> { { 7, "Cooperação" }, { 6, "Tensão" } }
        },
        new Pais
        {
            Id = 5,
            Nome = "Índia",
            Descricao = "Mercado farmacêutico global, crescimento acelerado, caos organizado.",
            ForcaDeVenda = new List<int> { 4, 8, 15 },
            NecessidadeDeCompra = new List<int> { 1, 3, 10 },
            ProdutosAceitos = new List<int> { 1, 3, 4, 6, 8, 10, 11, 13, 15 },
            TaxaImportacao = 0.07m,
            ForcaComercial = "Farmacêuticos e Genéricos",
            FraquezaComercial = "Energia e Defesa",
            TendenciaEconomica = "Emergente e populosa",
            RelacoesDiplomaticas = new Dictionary<int, string> { { 2, "Boa" }, { 3, "Neutra" }, { 6, "Fria" } }
        },
        new Pais
        {
            Id = 6,
            Nome = "Rússia",
            Descricao = "Grande em território, petróleo e sanções. Forte no mercado negro.",
            ForcaDeVenda = new List<int> { 1, 7, 14 },
            NecessidadeDeCompra = new List<int> { 3, 4, 6 },
            ProdutosAceitos = new List<int> { 1, 3, 4, 5, 6, 7, 10, 11, 14 },
            TaxaImportacao = 0.09m,
            ForcaComercial = "Petróleo e Armamentos",
            FraquezaComercial = "Medicamentos e Chips",
            TendenciaEconomica = "Sancionada",
            RelacoesDiplomaticas = new Dictionary<int, string> { { 1, "Hostilidade" }, { 3, "Parceria" }, { 4, "Ruptura" } }
        },
        new Pais
        {
            Id = 7,
            Nome = "Reino Unido",
            Descricao = "Pós-Brexit, tradicionalista e atento aos mercados regulados.",
            ForcaDeVenda = new List<int> { 5, 6 },
            NecessidadeDeCompra = new List<int> { 3, 4, 8 },
            ProdutosAceitos = new List<int> { 1, 3, 4, 5, 6, 7, 8, 9, 10 },
            TaxaImportacao = 0.13m,
            ForcaComercial = "Bens de consumo e medicamentos",
            FraquezaComercial = "Armas e Drogas",
            TendenciaEconomica = "Regulada e conservadora",
            RelacoesDiplomaticas = new Dictionary<int, string> { { 4, "Aliança" }, { 6, "Fria" } }
        },
        new Pais
        {
            Id = 8,
            Nome = "Argentina",
            Descricao = "Crises cíclicas, criatividade no mercado informal e recursos naturais.",
            ForcaDeVenda = new List<int> { 2, 11 },
            NecessidadeDeCompra = new List<int> { 3, 4, 6 },
            ProdutosAceitos = new List<int> { 1, 2, 3, 4, 5, 6, 8, 10, 11, 12 },
            TaxaImportacao = 0.15m,
            ForcaComercial = "Subprodutos agrícolas",
            FraquezaComercial = "Alta tecnologia",
            TendenciaEconomica = "Volátil",
            RelacoesDiplomaticas = new Dictionary<int, string> { { 2, "Parceria" }, { 1, "Dependente" }, { 6, "Ambígua" } }
        },
        new Pais
        {
            Id = 9,
            Nome = "África do Sul",
            Descricao = "Mineradora do mundo, riqueza subterrânea, desigualdade na superfície.",
            ForcaDeVenda = new List<int> { 10, 2 },
            NecessidadeDeCompra = new List<int> { 3, 6, 4 },
            ProdutosAceitos = new List<int> { 1, 2, 3, 4, 5, 6, 10, 11 },
            TaxaImportacao = 0.1m,
            ForcaComercial = "Minérios e Substâncias",
            FraquezaComercial = "Tecnologia e Logística",
            TendenciaEconomica = "Extrativista",
            RelacoesDiplomaticas = new Dictionary<int, string> { { 1, "Boa" }, { 3, "Aliada" } }
        },
        new Pais
        {
            Id = 10,
            Nome = "Holanda",
            Descricao = "Avançado, liberal, especialista em logística e plantas exóticas.",
            ForcaDeVenda = new List<int> { 5, 12, 4 },
            NecessidadeDeCompra = new List<int> { 3, 1 },
            ProdutosAceitos = new List<int> { 1, 3, 4, 5, 6, 8, 9, 10, 12, 13 },
            TaxaImportacao = 0.11m,
            ForcaComercial = "Distribuição e Farmacêuticos",
            FraquezaComercial = "Recursos naturais",
            TendenciaEconomica = "Comércio livre",
            RelacoesDiplomaticas = new Dictionary<int, string> { { 4, "Aliança" }, { 7, "Boa" }, { 3, "Competição" } }
        }
     };
    public static List<Evento> ListaDeEventos = new List<Evento>
    {
        new Evento
        {
            Id = 1,
            Nome = "Surto Global de Vírus – Favorece Índia",
            Descricao = "Um novo vírus se espalhou como fake news em grupo de família. **EUA**, **Brasil** e **Índia** entraram em colapso sanitário: o americano tuitou, o brasileiro fez live e o indiano acendeu incenso. Vacinas e remédios sumiram. A **Índia**, por ironia, também era a maior produtora e lucrou enquanto chorava.",
            
            Gravidade = 5,
            Categoria = CategoriaEvento.Sanitario,
            PaisesAfetadosDiretos = new List<int> { 1, 2, 5 },
            ProdutosAfetados = new Dictionary<int, decimal> { { 4, 1.8m }, { 8, 1.5m } },
            PaisesFavorecidos = new Dictionary<int, Dictionary<int, decimal>> { { 5, new Dictionary<int, decimal> { { 4, 1.2m } } } },
            PaisesComViagemBloqueada = new List<int> { 1, 5 },
            RiscoComercial = new Dictionary<int, decimal> { { 2, 0.3m }, { 1, 0.2m } },
            ModificadorReputacao = new Dictionary<int, decimal> { { 1, -1m }, { 5, 1m } }
        },
        new Evento
        {
            Id = 2,
            Nome = "Crise no Canal de Suez – Favorece China",
            Descricao = "O Canal de Suez vira um estacionamento náutico. **EUA**, **Brasil**, **Alemanha** e mais meio mundo ficaram sem chips e petróleo. A **China**, que estoca até vento, distribuiu eletrônicos com lucro de escândalo.",
            
            Gravidade = 4,
            Categoria = CategoriaEvento.Economico,
            PaisesAfetadosDiretos = new List<int> { 1, 2, 4, 5, 7, 8, 9, 10 },
            ProdutosAfetados = new Dictionary<int, decimal> { { 3, 1.7m }, { 1, 1.4m } },
            PaisesFavorecidos = new Dictionary<int, Dictionary<int, decimal>> { { 3, new Dictionary<int, decimal> { { 3, 1.3m } } } },
            PaisesComViagemBloqueada = new List<int> { 7, 8 },
            RiscoComercial = new Dictionary<int, decimal> { { 4, 0.3m }, { 7, 0.2m } },
            ModificadorReputacao = new Dictionary<int, decimal> { { 3, 2m }, { 1, -1m } }
        },
        new Evento
        {
            Id = 3,
            Nome = "Colapso do Peso Argentino – Favorece África do Sul",
            Descricao = "**Argentina** implode sua própria moeda. O peso vira papel de parede e o ouro passou a valer mais do que Wi-Fi em aeroporto. **África do Sul**, cheia de mina e pouco escândalo, lucrou vendendo pepitas enquanto o resto do mundo fazia vaquinha.",
            
            Gravidade = 4,
            Categoria = CategoriaEvento.Financeiro,
            PaisesAfetadosDiretos = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 10 },
            ProdutosAfetados = new Dictionary<int, decimal> { { 10, 1.5m } },
            PaisesFavorecidos = new Dictionary<int, Dictionary<int, decimal>> { { 9, new Dictionary<int, decimal> { { 10, 1.2m } } } },
            PaisesComViagemBloqueada = new List<int>(),
            RiscoComercial = new Dictionary<int, decimal> { { 8, 0.4m } },
            ModificadorReputacao = new Dictionary<int, decimal> { { 9, 1m }, { 8, -2m } }
        },
        new Evento
        {
            Id = 4,
            Nome = "Fechamento das Fronteiras Russas – Favorece Brasil",
            Descricao = "**Rússia** tranca as fronteiras e é dormir com sanções. Armas e petróleo evaporaram do mapa. **Brasil** sorriu com a oportunidade e passou a vender combustível com preço de boutique. Europa chorou, EUA fingiu surpresa.",
            
            Gravidade = 5,
            Categoria = CategoriaEvento.Politico,
            PaisesAfetadosDiretos = new List<int> { 1, 3, 4, 5, 6, 7, 8, 9, 10 },
            ProdutosAfetados = new Dictionary<int, decimal> { { 7, 1.6m }, { 1, 1.3m } },
            PaisesFavorecidos = new Dictionary<int, Dictionary<int, decimal>> { { 2, new Dictionary<int, decimal> { { 1, 1.2m } } } },
            PaisesComViagemBloqueada = new List<int> { 6 },
            RiscoComercial = new Dictionary<int, decimal> { { 6, 0.5m } },
            ModificadorReputacao = new Dictionary<int, decimal> { { 2, 1m }, { 6, -2m } }
        },
        new Evento
        {
            Id = 5,
            Nome = "Greve Geral na Índia – Favorece Brasil",
            Descricao = "**Índia** parou: greve geral de farmácias a gurus do WhatsApp. Vacinas e genéricos desaparecem. Enquanto isso, o **Brasil**, sem saber como, vira fornecedor global de última hora.",
            
            Gravidade = 4,
            Categoria = CategoriaEvento.Politico,
            PaisesAfetadosDiretos = new List<int> { 5 },
            ProdutosAfetados = new Dictionary<int, decimal> { { 4, 1.6m }, { 8, 1.4m } },
            PaisesFavorecidos = new Dictionary<int, Dictionary<int, decimal>> { { 2, new Dictionary<int, decimal> { { 4, 1.2m } } } },
            PaisesComViagemBloqueada = new List<int>(),
            RiscoComercial = new Dictionary<int, decimal> { { 5, 0.5m } },
            ModificadorReputacao = new Dictionary<int, decimal> { { 2, 1m }, { 5, -2m } }
        },
        new Evento
        {
            Id = 6,
            Nome = "Sanções Econômicas à Rússia – Favorece EUA / China / Argentina",
            Descricao = "**Rússia** levou bloco de sanções tão pesado que vira tutorial de como não negociar. Armas russas ficaram raras. **EUA**, **China** e até a **Argentina** se jogaram no mercado com preços levemente criminosos.",
            
            Gravidade = 5,
            Categoria = CategoriaEvento.Politico,
            PaisesAfetadosDiretos = new List<int> { 6 },
            ProdutosAfetados = new Dictionary<int, decimal> { { 7, 1.8m }, { 1, 1.4m } },
            PaisesFavorecidos = new Dictionary<int, Dictionary<int, decimal>> { { 1, new Dictionary<int, decimal> { { 7, 1.3m } } }, { 3, new Dictionary<int, decimal> { { 1, 1.2m } } }, { 8, new Dictionary<int, decimal> { { 7, 1.1m } } } },
            PaisesComViagemBloqueada = new List<int> { 6 },
            RiscoComercial = new Dictionary<int, decimal> { { 6, 0.6m } },
            ModificadorReputacao = new Dictionary<int, decimal> { { 6, -3m }, { 1, 1m }, { 3, 1m } }
        },
        new Evento
        {
            Id = 7,
            Nome = "Incêndio em Fábrica de Explosivos na Alemanha – Favorece Rússia / China",
            Descricao = "Uma faísca e puff! **Alemanha** perde sua maior fábrica de explosivos. **Rússia** e **China** assumiram o mercado como bons vizinhos armamentistas. O mundo armou a festa, a Alemanha só mandou email.",
            
            Gravidade = 3,
            Categoria = CategoriaEvento.Militar,
            PaisesAfetadosDiretos = new List<int> { 4 },
            ProdutosAfetados = new Dictionary<int, decimal> { { 14, 2.0m } },
            PaisesFavorecidos = new Dictionary<int, Dictionary<int, decimal>> { { 6, new Dictionary<int, decimal> { { 14, 1.2m } } }, { 3, new Dictionary<int, decimal> { { 14, 1.1m } } } },
            PaisesComViagemBloqueada = new List<int>(),
            RiscoComercial = new Dictionary<int, decimal> { { 4, 0.3m } },
            ModificadorReputacao = new Dictionary<int, decimal> { { 4, -1m }, { 6, 1m } }
        },
        new Evento
        {
            Id = 8,
            Nome = "Ataque Hacker em Massa – Favorece China",
            Descricao = "**EUA**, **Brasil**, **Alemanha** e **Reino Unido** foram hackeados por adolescentes com fome e tempo livre. Sistemas caíram, chips sumiram. A **China**, que ainda imprime tudo em papel, aproveitou pra vender o dobro do preço.",
            
            Gravidade = 4,
            Categoria = CategoriaEvento.Tecnologico,
            PaisesAfetadosDiretos = new List<int> { 1, 2, 4, 7, 8 },
            ProdutosAfetados = new Dictionary<int, decimal> { { 3, 1.5m } },
            PaisesFavorecidos = new Dictionary<int, Dictionary<int, decimal>> { { 3, new Dictionary<int, decimal> { { 3, 1.2m } } } },
            PaisesComViagemBloqueada = new List<int>(),
            RiscoComercial = new Dictionary<int, decimal> { { 1, 0.3m }, { 4, 0.2m } },
            ModificadorReputacao = new Dictionary<int, decimal> { { 3, 1m }, { 1, -1m } }
        },
        new Evento
        {
            Id = 9,
            Nome = "Legalização da Maconha Medicinal – Favorece Holanda / EUA",
            Descricao = "A **ONU** legaliza a maconha medicinal. **Brasil**, **Rússia** e **Argentina** ficaram perdidos com leis atrasadas e tabus eternos. **Holanda** e **EUA**, que já tinham loja, delivery e cartão fidelidade, viram referência mundial. O produto disparou no mercado lícito e afundou o mercado paralelo.",
            
            Gravidade = 3,
            Categoria = CategoriaEvento.Politico,
            PaisesAfetadosDiretos = new List<int> { 2, 6, 8 },
            ProdutosAfetados = new Dictionary<int, decimal> { { 12, 1.4m } },
            PaisesFavorecidos = new Dictionary<int, Dictionary<int, decimal>> { { 10, new Dictionary<int, decimal> { { 12, 1.3m } } }, { 1, new Dictionary<int, decimal> { { 12, 1.2m } } } },
            PaisesComViagemBloqueada = new List<int>(),
            RiscoComercial = new Dictionary<int, decimal> { { 6, 0.2m } },
            ModificadorReputacao = new Dictionary<int, decimal> { { 10, 2m }, { 2, -1m } }
        },
        new Evento
        {
            Id = 10,
            Nome = "Explosão em Plataforma no Golfo – Favorece EUA / China",
            Descricao = "Uma explosão em plataforma no Golfo tirou **Rússia** e **Holanda** da jogada do petróleo. **EUA** e **China**, sempre com estoque escondido, passaram a vender como se fosse NFT raro.",
            
            Gravidade = 5,
            Categoria = CategoriaEvento.Ambiental,
            PaisesAfetadosDiretos = new List<int> { 10, 6 },
            ProdutosAfetados = new Dictionary<int, decimal> { { 1, 1.9m } },
            PaisesFavorecidos = new Dictionary<int, Dictionary<int, decimal>> { { 1, new Dictionary<int, decimal> { { 1, 1.3m } } }, { 3, new Dictionary<int, decimal> { { 1, 1.2m } } } },
            PaisesComViagemBloqueada = new List<int> { 10 },
            RiscoComercial = new Dictionary<int, decimal> { { 6, 0.4m }, { 10, 0.5m } },
            ModificadorReputacao = new Dictionary<int, decimal> { { 1, 1m }, { 10, -2m } }
        },
        new Evento
        {
            Id = 11,
            Nome = "Protestos Anti-Vacina – Favorece Índia",
            Descricao = "**EUA** e **Brasil** entraram em colapso de lógica: vacinas salvavam vidas, mas as pessoas saíram na rua contra elas. Resultado? Estoques locais viram queimadas e a demanda mundial explode. **Índia**, que fabrica vacina até com fermento, vende tudo com lucro. Produto em alta: **vacinas**. Países em queda: **EUA** e **Brasil** (perda de reputação e aumento de risco).",
            
            Gravidade = 3,
            Categoria = CategoriaEvento.Sanitario,
            PaisesAfetadosDiretos = new List<int> { 1, 2 },
            ProdutosAfetados = new Dictionary<int, decimal> { { 4, 1.6m } },
            PaisesFavorecidos = new Dictionary<int, Dictionary<int, decimal>> { { 5, new Dictionary<int, decimal> { { 4, 1.2m } } } },
            PaisesComViagemBloqueada = new List<int>(),
            RiscoComercial = new Dictionary<int, decimal> { { 1, 0.3m }, { 2, 0.3m } },
            ModificadorReputacao = new Dictionary<int, decimal> { { 5, 1m }, { 2, -1m }, { 1, -1m } }
        },
        new Evento
        {
            Id = 12,
            Nome = "Boom de Carros Elétricos – Favorece Alemanha / China",
            Descricao = "**EUA**, **Brasil** e **Argentina** surtaram com o preço do combustível, e correram atrás de **carros elétricos**. **Alemanha** e **China**, que já produzem carro até com energia da risada, dominaram o mercado. Enquanto isso, **Rússia**, cheirando diesel, perde espaço. Produto em alta: **carros elétricos**.",
            
            Gravidade = 4,
            Categoria = CategoriaEvento.Ambiental,
            PaisesAfetadosDiretos = new List<int> { 1, 2, 8 },
            ProdutosAfetados = new Dictionary<int, decimal> { { 9, 1.5m } },
            PaisesFavorecidos = new Dictionary<int, Dictionary<int, decimal>> { { 4, new Dictionary<int, decimal> { { 9, 1.2m } } }, { 3, new Dictionary<int, decimal> { { 9, 1.2m } } } },
            PaisesComViagemBloqueada = new List<int>(),
            RiscoComercial = new Dictionary<int, decimal>(),
            ModificadorReputacao = new Dictionary<int, decimal> { { 3, 1m }, { 4, 1m } }
        },
        new Evento
        {
            Id = 13,
            Nome = "Censura na Internet Chinesa – Favorece EUA",
            Descricao = "**China** decide apagar a internet. Chips, jogos e até calculadora foram bloqueados. **EUA**, com tecnologia sobrando, viu a demanda por **chips eletrônicos** explodir. Enquanto a censura travava até os GIFs, o dólar entrava sem freio. **China** perde credibilidade e mercado. Produto em alta: **chips**. Ganho total: **EUA**.",
            
            Gravidade = 4,
            Categoria = CategoriaEvento.Tecnologico,
            PaisesAfetadosDiretos = new List<int> { 3 },
            ProdutosAfetados = new Dictionary<int, decimal> { { 3, 1.4m } },
            PaisesFavorecidos = new Dictionary<int, Dictionary<int, decimal>> { { 1, new Dictionary<int, decimal> { { 3, 1.3m } } } },
            PaisesComViagemBloqueada = new List<int>(),
            RiscoComercial = new Dictionary<int, decimal> { { 3, 0.4m } },
            ModificadorReputacao = new Dictionary<int, decimal> { { 1, 1m }, { 3, -1m } }
        },
        new Evento
        {
            Id = 14,
            Nome = "Explosão de Refino no Oriente Médio – Favorece EUA / China",
            Descricao = "Uma explosão paralisou o maior polo de refino no Golfo. O mundo parou. **Petróleo** vira ouro líquido. **EUA** e **China**, com reservas monstruosas, lucraram abastecendo quem fica na seca. **Rússia** e **Arábia** só olharam o mercado virar fumaça – literalmente.",
            
            Gravidade = 5,
            Categoria = CategoriaEvento.Ambiental,
            PaisesAfetadosDiretos = new List<int> { 6, 10 },
            ProdutosAfetados = new Dictionary<int, decimal> { { 1, 1.9m } },
            PaisesFavorecidos = new Dictionary<int, Dictionary<int, decimal>> { { 1, new Dictionary<int, decimal> { { 1, 1.2m } } }, { 3, new Dictionary<int, decimal> { { 1, 1.2m } } } },
            PaisesComViagemBloqueada = new List<int>(),
            RiscoComercial = new Dictionary<int, decimal> { { 6, 0.5m }, { 10, 0.4m } },
            ModificadorReputacao = new Dictionary<int, decimal> { { 1, 1m }, { 10, -2m } }
        },
        new Evento
        {
            Id = 15,
            Nome = "Crise de Opioides nos EUA – Favorece Índia",
            Descricao = "**EUA** travam uma crise de dependência e cortam o fornecimento de **opioides sintéticos**. **Índia**, com controle rígido e produção regulada, vira fornecedora oficial da dor global. **Produto em alta**: Opioides. **País em baixa**: EUA, que perde mercado e reputação.",
            
            Gravidade = 4,
            Categoria = CategoriaEvento.Sanitario,
            PaisesAfetadosDiretos = new List<int> { 1 },
            ProdutosAfetados = new Dictionary<int, decimal> { { 15, 1.5m } },
            PaisesFavorecidos = new Dictionary<int, Dictionary<int, decimal>> { { 5, new Dictionary<int, decimal> { { 15, 1.3m } } } },
            PaisesComViagemBloqueada = new List<int>(),
            RiscoComercial = new Dictionary<int, decimal> { { 1, 0.4m } },
            ModificadorReputacao = new Dictionary<int, decimal> { { 1, -2m }, { 5, 1m } }
        },
        new Evento
        {
            Id = 16,
            Nome = "Conflito no Leste Europeu – Favorece China",
            Descricao = "**Rússia** entrou em conflito na fronteira. **Alemanha**, **Reino Unido** e **EUA** aplicam sanções. **Armas de fogo** e **explosivos** sumiram das rotas oficiais. Com o mundo dividido, **China** vira balcão neutro de exportação bélica com frete grátis.",
            
            Gravidade = 5,
            Categoria = CategoriaEvento.Militar,
            PaisesAfetadosDiretos = new List<int> { 6, 4, 7, 1 },
            ProdutosAfetados = new Dictionary<int, decimal> { { 7, 1.8m }, { 14, 1.6m } },
            PaisesFavorecidos = new Dictionary<int, Dictionary<int, decimal>> { { 3, new Dictionary<int, decimal> { { 7, 1.3m }, { 14, 1.2m } } } },
            PaisesComViagemBloqueada = new List<int> { 6 },
            RiscoComercial = new Dictionary<int, decimal> { { 6, 0.6m } },
            ModificadorReputacao = new Dictionary<int, decimal> { { 6, -2m }, { 3, 1m } }
        },
        new Evento
        {
            Id = 17,
            Nome = "Legalização de Cigarros Alternativos – Favorece Holanda",
            Descricao = "**Holanda** libera geral: **cigarros** alternativos com sabores ganharam status gourmet. Enquanto **Brasil** e **Argentina** tentavam proibir até fumaça de churrasqueira, os holandeses vendiam mentolado por assinatura.",
            
            Gravidade = 2,
            Categoria = CategoriaEvento.Cultural,
            PaisesAfetadosDiretos = new List<int> { 2, 8 },
            ProdutosAfetados = new Dictionary<int, decimal> { { 5, 1.4m } },
            PaisesFavorecidos = new Dictionary<int, Dictionary<int, decimal>> { { 10, new Dictionary<int, decimal> { { 5, 1.3m } } } },
            PaisesComViagemBloqueada = new List<int>(),
            RiscoComercial = new Dictionary<int, decimal> { { 2, 0.2m } },
            ModificadorReputacao = new Dictionary<int, decimal> { { 10, 1m }, { 2, -1m } }
        },
        new Evento
        {
            Id = 18,
            Nome = "Restrição de Anabolizantes na Europa – Favorece Argentina",
            Descricao = "**Alemanha** e **Reino Unido** baniram o uso de **anabolizantes** fora de controle clínico. **Argentina**, onde tudo vira mercado paralelo em 15 minutos, assume o fornecimento fitness underground da Europa. **Produto em alta**: Anabolizantes. **Países punidos**: Alemanha, Reino Unido.",
            
            Gravidade = 3,
            Categoria = CategoriaEvento.Politico,
            PaisesAfetadosDiretos = new List<int> { 4, 7 },
            ProdutosAfetados = new Dictionary<int, decimal> { { 13, 1.5m } },
            PaisesFavorecidos = new Dictionary<int, Dictionary<int, decimal>> { { 8, new Dictionary<int, decimal> { { 13, 1.2m } } } },
            PaisesComViagemBloqueada = new List<int>(),
            RiscoComercial = new Dictionary<int, decimal> { { 7, 0.3m } },
            ModificadorReputacao = new Dictionary<int, decimal> { { 8, 1m }, { 4, -1m } }
        },
        new Evento
        {
            Id = 19,
            Nome = "Roubo em Mina de Ouro na África – Favorece China",
            Descricao = "Uma operação digna de filme: roubam toneladas de **ouro** na **África do Sul**. Com a produção travada, **China** vira exportadora preferida e sobe o preço como quem não quer nada. **Produto em alta**: Ouro. **País em queda**: África do Sul.",
            
            Gravidade = 4,
            Categoria = CategoriaEvento.Militar,
            PaisesAfetadosDiretos = new List<int> { 9 },
            ProdutosAfetados = new Dictionary<int, decimal> { { 10, 1.6m } },
            PaisesFavorecidos = new Dictionary<int, Dictionary<int, decimal>> { { 3, new Dictionary<int, decimal> { { 10, 1.2m } } } },
            PaisesComViagemBloqueada = new List<int>(),
            RiscoComercial = new Dictionary<int, decimal> { { 9, 0.4m } },
            ModificadorReputacao = new Dictionary<int, decimal> { { 9, -1m }, { 3, 1m } }
        },
        new Evento
        {
            Id = 20,
            Nome = "Congresso Mundial Anti-Armas – Favorece Alemanha",
            Descricao = "Um congresso internacional decide restringir comércio de **armas de fogo**. **Rússia** e **EUA** foram limitadas por sanções diplomáticas. **Alemanha**, com indústria controlada, vira exportadora oficial 'certificada pela paz'.",
            
            Gravidade = 3,
            Categoria = CategoriaEvento.Politico,
            PaisesAfetadosDiretos = new List<int> { 1, 6 },
            ProdutosAfetados = new Dictionary<int, decimal> { { 7, 1.5m } },
            PaisesFavorecidos = new Dictionary<int, Dictionary<int, decimal>> { { 4, new Dictionary<int, decimal> { { 7, 1.3m } } } },
            PaisesComViagemBloqueada = new List<int>(),
            RiscoComercial = new Dictionary<int, decimal> { { 6, 0.3m } },
            ModificadorReputacao = new Dictionary<int, decimal> { { 4, 1m }, { 6, -1m } }
        },
        new Evento
        {
            Id = 21,
            Nome = "Saturação do Mercado de Uísque – Favorece Alemanha / Tabaco",
            Descricao = "Após três festas de luxo e uma cúpula do G7 com open bar, o mercado global de **uísque** entra em coma alcoólico. **Alemanha**, sóbria e estratégica, aproveita o vazio e começa a exportar **tabaco em folha** com selo gourmet. **Escócia** e **Holanda** ficam com estoque encalhado e dor de cabeça.",
            
            Gravidade = 3,
            Categoria = CategoriaEvento.Cultural,
            PaisesAfetadosDiretos = new List<int> { 10, 7 },
            ProdutosAfetados = new Dictionary<int, decimal> { { 6, 0.7m } },
            PaisesFavorecidos = new Dictionary<int, Dictionary<int, decimal>> { { 4, new Dictionary<int, decimal> { { 11, 1.3m } } } },
            PaisesComViagemBloqueada = new List<int>(),
            RiscoComercial = new Dictionary<int, decimal>(),
            ModificadorReputacao = new Dictionary<int, decimal> { { 4, 1m }, { 7, -1m } }
        },
        new Evento
        {
            Id = 22,
            Nome = "Legalização Internacional de Anabolizantes – Favorece EUA / Anabolizantes",
            Descricao = "Com o mundo virando uma academia a céu aberto, a **ONU** aprova o uso livre de **anabolizantes**. **EUA**, que já misturam whey no café, lideram a produção e exportação. **Argentina**, antiga fornecedora clandestina, vê o mercado sumir e o lucro virar músculo flácido.",
            
            Gravidade = 4,
            Categoria = CategoriaEvento.Politico,
            PaisesAfetadosDiretos = new List<int> { 8 },
            ProdutosAfetados = new Dictionary<int, decimal> { { 13, 1.7m } },
            PaisesFavorecidos = new Dictionary<int, Dictionary<int, decimal>> { { 1, new Dictionary<int, decimal> { { 13, 1.3m } } } },
            PaisesComViagemBloqueada = new List<int>(),
            RiscoComercial = new Dictionary<int, decimal> { { 8, 0.4m } },
            ModificadorReputacao = new Dictionary<int, decimal> { { 1, 1m }, { 8, -1m } }
        },
        new Evento
        {
            Id = 23,
            Nome = "Embargo a Produtos Químicos Indianos – Favorece Brasil / Medicamentos",
            Descricao = "**Índia** sofre um embargo internacional por violar patentes de **medicamentos controlados**. **Brasil**, que já fabricava o genérico do genérico, entra como novo fornecedor global. Índia perde credibilidade, e o preço das aspirinas passa a concorrer com iPhone.",
            
            Gravidade = 5,
            Categoria = CategoriaEvento.Sanitario,
            PaisesAfetadosDiretos = new List<int> { 5 },
            ProdutosAfetados = new Dictionary<int, decimal> { { 8, 1.6m } },
            PaisesFavorecidos = new Dictionary<int, Dictionary<int, decimal>> { { 2, new Dictionary<int, decimal> { { 8, 1.2m } } } },
            PaisesComViagemBloqueada = new List<int>(),
            RiscoComercial = new Dictionary<int, decimal> { { 5, 0.5m } },
            ModificadorReputacao = new Dictionary<int, decimal> { { 2, 1m }, { 5, -2m } }
        },
        new Evento
        {
            Id = 24,
            Nome = "Legalização Parcial de Armas na Europa – Favorece Alemanha / Armas",
            Descricao = "A **Alemanha** aprova uma lei para uso civil e esportivo de **armas de fogo**. O mercado europeu reage comprando como se fosse pão de queijo. **Rússia** e **China**, que lideravam o contrabando, veem os preços despencarem e os estoques mofarem.",
            
            Gravidade = 3,
            Categoria = CategoriaEvento.Militar,
            PaisesAfetadosDiretos = new List<int> { 3, 6 },
            ProdutosAfetados = new Dictionary<int, decimal> { { 7, 1.5m } },
            PaisesFavorecidos = new Dictionary<int, Dictionary<int, decimal>> { { 4, new Dictionary<int, decimal> { { 7, 1.3m } } } },
            PaisesComViagemBloqueada = new List<int>(),
            RiscoComercial = new Dictionary<int, decimal> { { 3, 0.3m } },
            ModificadorReputacao = new Dictionary<int, decimal> { { 4, 1m }, { 6, -1m } }
        },
        new Evento
        {
            Id = 25,
            Nome = "Crise de Energia Solar – Favorece Rússia / Petróleo",
            Descricao = "Uma tempestade solar frita 70% das placas fotovoltaicas na Europa e Ásia. O sonho verde se apaga. **Rússia**, com seu bom e velho **petróleo**, volta ao jogo como se nada tivesse acontecido. **Alemanha** e **China** apagam a luz e o sorriso.",
            
            Gravidade = 4,
            Categoria = CategoriaEvento.Ambiental,
            PaisesAfetadosDiretos = new List<int> { 4, 3 },
            ProdutosAfetados = new Dictionary<int, decimal> { { 1, 1.5m } },
            PaisesFavorecidos = new Dictionary<int, Dictionary<int, decimal>> { { 6, new Dictionary<int, decimal> { { 1, 1.3m } } } },
            PaisesComViagemBloqueada = new List<int>(),
            RiscoComercial = new Dictionary<int, decimal> { { 3, 0.2m } },
            ModificadorReputacao = new Dictionary<int, decimal> { { 6, 1m }, { 4, -1m } }
        },
        new Evento
        {
            Id = 26,
            Nome = "Tratado Global Anti-Drogas – Favorece China / Chips",
            Descricao = "Um tratado internacional bloqueia o comércio de **cocaína** e **maconha**. **Brasil**, **Argentina** e **Rússia** veem seus mercados paralelos sumirem. **China**, sem drogas e cheia de **chips**, lucra vendendo tecnologia onde antes só tinha entorpecente.",
            
            Gravidade = 5,
            Categoria = CategoriaEvento.Politico,
            PaisesAfetadosDiretos = new List<int> { 2, 6, 8 },
            ProdutosAfetados = new Dictionary<int, decimal> { { 2, 0.5m }, { 12, 0.6m } },
            PaisesFavorecidos = new Dictionary<int, Dictionary<int, decimal>> { { 3, new Dictionary<int, decimal> { { 3, 1.2m } } } },
            PaisesComViagemBloqueada = new List<int>(),
            RiscoComercial = new Dictionary<int, decimal> { { 2, 0.4m } },
            ModificadorReputacao = new Dictionary<int, decimal> { { 3, 1m }, { 2, -2m } }
        },
        new Evento
        {
            Id = 27,
            Nome = "Festival Global do Churrasco – Favorece Brasil / Churrasco",
            Descricao = "Com a explosão do reality 'Churrasco Pelo Mundo', o **Brasil** vira líder em exportação de **carne temperada e estilo de vida**. Enquanto a **Alemanha** proíbe fumaça e a **Índia** proíbe vaca, os brasileiros vendem até carvão aromático.",
            
            Gravidade = 2,
            Categoria = CategoriaEvento.Cultural,
            PaisesAfetadosDiretos = new List<int> { 4, 5 },
            ProdutosAfetados = new Dictionary<int, decimal> { { 15, 1.5m } },
            PaisesFavorecidos = new Dictionary<int, Dictionary<int, decimal>> { { 2, new Dictionary<int, decimal> { { 15, 1.3m } } } },
            PaisesComViagemBloqueada = new List<int>(),
            RiscoComercial = new Dictionary<int, decimal>(),
            ModificadorReputacao = new Dictionary<int, decimal> { { 2, 1m }, { 4, -1m } }
        },
        new Evento
        {
            Id = 28,
            Nome = "Queda no Tráfico de Maconha – Favorece Índia / Produtos Medicinais",
            Descricao = "Com a **maconha medicinal** legalizada globalmente, os preços do mercado paralelo despencam. **Brasil** e **Argentina**, antes reis do cânhamo clandestino, agora lutam para pagar o aluguel do cultivo. **Índia**, que já plantava coisa estranha desde os Vedas, assume o comércio de **extratos medicinais**, agora na moda até em spa europeu.",
            
            Gravidade = 3,
            Categoria = CategoriaEvento.Politico,
            PaisesAfetadosDiretos = new List<int> { 2, 8 },
            ProdutosAfetados = new Dictionary<int, decimal> { { 12, 0.6m } },
            PaisesFavorecidos = new Dictionary<int, Dictionary<int, decimal>> { { 5, new Dictionary<int, decimal> { { 8, 1.3m } } } },
            PaisesComViagemBloqueada = new List<int>(),
            RiscoComercial = new Dictionary<int, decimal>(),
            ModificadorReputacao = new Dictionary<int, decimal>()
        },
        new Evento
        {
            Id = 30,
            Nome = "Desaceleração Verde Global – Favorece Rússia / EUA / Petróleo",
            Descricao = "Após a tempestade solar que fritou as placas fotovoltaicas, os países desenvolvidos pisam no freio verde. **Alemanha** e **China** reduzem incentivos, e **petróleo** volta a ser rei. **Rússia** e **EUA** vendem barril até na Shopee.",
            
            Gravidade = 4,
            Categoria = CategoriaEvento.Ambiental,
            PaisesAfetadosDiretos = new List<int> { 3, 4 },
            ProdutosAfetados = new Dictionary<int, decimal> { { 1, 1.5m } },
            PaisesFavorecidos = new Dictionary<int, Dictionary<int, decimal>> { { 6, new Dictionary<int, decimal> { { 1, 1.2m } } }, { 1, new Dictionary<int, decimal> { { 1, 1.1m } } } },
            PaisesComViagemBloqueada = new List<int>(),
            RiscoComercial = new Dictionary<int, decimal>(),
            ModificadorReputacao = new Dictionary<int, decimal>()
        },
        new Evento
        {
            Id = 32,
            Nome = "Alta Global de Medicamentos – Favorece Brasil / Índia",
            Descricao = "A pandemia não só aumentou o preço das vacinas, como colocou todos os **medicamentos controlados** na lista de desejo da humanidade. **Índia** e **Brasil**, produtores em massa, lucram com até antialérgico sendo vendido como pó mágico. **EUA** sofre com desabastecimento.",
            
            Gravidade = 3,
            Categoria = CategoriaEvento.Sanitario,
            PaisesAfetadosDiretos = new List<int> { 1 },
            ProdutosAfetados = new Dictionary<int, decimal> { { 8, 1.6m } },
            PaisesFavorecidos = new Dictionary<int, Dictionary<int, decimal>> { { 5, new Dictionary<int, decimal> { { 8, 1.2m } } }, { 2, new Dictionary<int, decimal> { { 8, 1.1m } } } },
            PaisesComViagemBloqueada = new List<int>(),
            RiscoComercial = new Dictionary<int, decimal>(),
            ModificadorReputacao = new Dictionary<int, decimal>()
        },
        new Evento
        {
            Id = 34,
            Nome = "Protestos de Atletas Profissionais – Favorece EUA / Anabolizantes",
            Descricao = "Após a Europa proibir **anabolizantes**, os atletas foram às ruas com mais músculo que argumento. **EUA**, vendo oportunidade, legaliza para uso esportivo e começa a exportar com rótulo de suplemento premium. **Alemanha** treina no grito, e perde o ouro no mercado.",
            
            Gravidade = 2,
            Categoria = CategoriaEvento.Cultural,
            PaisesAfetadosDiretos = new List<int> { 4 },
            ProdutosAfetados = new Dictionary<int, decimal> { { 13, 1.4m } },
            PaisesFavorecidos = new Dictionary<int, Dictionary<int, decimal>> { { 1, new Dictionary<int, decimal> { { 13, 1.3m } } } },
            PaisesComViagemBloqueada = new List<int>(),
            RiscoComercial = new Dictionary<int, decimal>(),
            ModificadorReputacao = new Dictionary<int, decimal>()
        },
        new Evento
        {
            Id = 36,
            Nome = "Mercado Negro Reage – Favorece Rússia / China / Armas",
            Descricao = "O tratado contra **armas** empurrou o comércio para o porão. **Rússia** e **China** reativam suas rotas alternativas. Preço sobe, mas risco também. **Alemanha**, agora regulada, perde cliente até pra camelô digital.",
            
            Gravidade = 4,
            Categoria = CategoriaEvento.Militar,
            PaisesAfetadosDiretos = new List<int> { 4 },
            ProdutosAfetados = new Dictionary<int, decimal> { { 7, 1.5m } },
            PaisesFavorecidos = new Dictionary<int, Dictionary<int, decimal>> { { 6, new Dictionary<int, decimal> { { 7, 1.2m } } }, { 3, new Dictionary<int, decimal> { { 7, 1.1m } } } },
            PaisesComViagemBloqueada = new List<int>(),
            RiscoComercial = new Dictionary<int, decimal>(),
            ModificadorReputacao = new Dictionary<int, decimal>()
        }
        };

    // Lista de Encadeamentos
    public static List<EventoEncadeado> ListaDeEncadeamentos = new List<EventoEncadeado>
    {
        new EventoEncadeado
        {
            Id = 1,
            EventoOrigemId = 9,
            Condicao = "Ativo nos últimos 2 turnos",
            EventoResultado = new Evento
            {
                Id = 28,
                Nome = "Queda no Tráfico de Maconha – Favorece Índia / Produtos Medicinais",
                Descricao = "Com a **maconha medicinal** legalizada globalmente, os preços do mercado paralelo despencam. **Brasil** e **Argentina**, antes reis do cânhamo clandestino, agora lutam para pagar o aluguel do cultivo. **Índia**, que já plantava coisa estranha desde os Vedas, assume o comércio de **extratos medicinais**, agora na moda até em spa europeu.",
                
                Gravidade = 3,
                Categoria = CategoriaEvento.Politico,
                PaisesAfetadosDiretos = new List<int> { 2, 8 },
                ProdutosAfetados = new Dictionary<int, decimal> { { 12, 0.6m } },
                PaisesFavorecidos = new Dictionary<int, Dictionary<int, decimal>> { { 5, new Dictionary<int, decimal> { { 8, 1.3m } } } },
                PaisesComViagemBloqueada = new List<int>(),
                RiscoComercial = new Dictionary<int, decimal>(),
                ModificadorReputacao = new Dictionary<int, decimal>()
            }
        },
        new EventoEncadeado
        {
            Id = 2,
            EventoOrigemId = 25,
            Condicao = "Finalizado",
            EventoResultado = new Evento
            {
                Id = 30,
                Nome = "Desaceleração Verde Global – Favorece Rússia / EUA / Petróleo",
                Descricao = "Após a tempestade solar que fritou as placas fotovoltaicas, os países desenvolvidos pisam no freio verde. **Alemanha** e **China** reduzem incentivos, e **petróleo** volta a ser rei. **Rússia** e **EUA** vendem barril até na Shopee.",
                
                Gravidade = 4,
                Categoria = CategoriaEvento.Ambiental,
                PaisesAfetadosDiretos = new List<int> { 3, 4 },
                ProdutosAfetados = new Dictionary<int, decimal> { { 1, 1.5m } },
                PaisesFavorecidos = new Dictionary<int, Dictionary<int, decimal>> { { 6, new Dictionary<int, decimal> { { 1, 1.2m } } }, { 1, new Dictionary<int, decimal> { { 1, 1.1m } } } },
                PaisesComViagemBloqueada = new List<int>(),
                RiscoComercial = new Dictionary<int, decimal>(),
                ModificadorReputacao = new Dictionary<int, decimal>()
            }
        },
        new EventoEncadeado
        {
            Id = 3,
            EventoOrigemId = 1,
            Condicao = "Turno seguinte",
            EventoResultado = new Evento
            {
                Id = 32,
                Nome = "Alta Global de Medicamentos – Favorece Brasil / Índia",
                Descricao = "A pandemia não só aumentou o preço das vacinas, como colocou todos os **medicamentos controlados** na lista de desejo da humanidade. **Índia** e **Brasil**, produtores em massa, lucram com até antialérgico sendo vendido como pó mágico. **EUA** sofre com desabastecimento.",
                
                Gravidade = 3,
                Categoria = CategoriaEvento.Sanitario,
                PaisesAfetadosDiretos = new List<int> { 1 },
                ProdutosAfetados = new Dictionary<int, decimal> { { 8, 1.6m } },
                PaisesFavorecidos = new Dictionary<int, Dictionary<int, decimal>> { { 5, new Dictionary<int, decimal> { { 8, 1.2m } } }, { 2, new Dictionary<int, decimal> { { 8, 1.1m } } } },
                PaisesComViagemBloqueada = new List<int>(),
                RiscoComercial = new Dictionary<int, decimal>(),
                ModificadorReputacao = new Dictionary<int, decimal>()
            }
        },
        new EventoEncadeado
        {
            Id = 4,
            EventoOrigemId = 18,
            Condicao = "Ativo",
            EventoResultado = new Evento
            {
                Id = 34,
                Nome = "Protestos de Atletas Profissionais – Favorece EUA / Anabolizantes",
                Descricao = "Após a Europa proibir **anabolizantes**, os atletas foram às ruas com mais músculo que argumento. **EUA**, vendo oportunidade, legaliza para uso esportivo e começa a exportar com rótulo de suplemento premium. **Alemanha** treina no grito, e perde o ouro no mercado.",
                
                Gravidade = 2,
                Categoria = CategoriaEvento.Cultural,
                PaisesAfetadosDiretos = new List<int> { 4 },
                ProdutosAfetados = new Dictionary<int, decimal> { { 13, 1.4m } },
                PaisesFavorecidos = new Dictionary<int, Dictionary<int, decimal>> { { 1, new Dictionary<int, decimal> { { 13, 1.3m } } } },
                PaisesComViagemBloqueada = new List<int>(),
                RiscoComercial = new Dictionary<int, decimal>(),
                ModificadorReputacao = new Dictionary<int, decimal>()
            }
        },
        new EventoEncadeado
        {
            Id = 5,
            EventoOrigemId = 0,
            Condicao = "Turno seguinte",
            EventoResultado = new Evento
            {
                Id = 36,
                Nome = "Mercado Negro Reage – Favorece Rússia / China / Armas",
                Descricao = "O tratado contra **armas** empurrou o comércio para o porão. **Rússia** e **China** reativam suas rotas alternativas. Preço sobe, mas risco também. **Alemanha**, agora regulada, perde cliente até pra camelô digital.",
                
                Gravidade = 4,
                Categoria = CategoriaEvento.Militar,
                PaisesAfetadosDiretos = new List<int> { 4 },
                ProdutosAfetados = new Dictionary<int, decimal> { { 7, 1.5m } },
                PaisesFavorecidos = new Dictionary<int, Dictionary<int, decimal>> { { 6, new Dictionary<int, decimal> { { 7, 1.2m } } }, { 3, new Dictionary<int, decimal> { { 7, 1.1m } } } },
                PaisesComViagemBloqueada = new List<int>(),
                RiscoComercial = new Dictionary<int, decimal>(),
                ModificadorReputacao = new Dictionary<int, decimal>()
            }
        }



    };
};