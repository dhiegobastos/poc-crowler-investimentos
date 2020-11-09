using System;
using Crowler.Investimentos.Domain;
using CsvHelper.Configuration;

namespace Crowler.Investimentos.Infra
{
    public class CsvFundoInvestimentoMapping : ClassMap<FundoInvestimento>
    {
        public CsvFundoInvestimentoMapping()
        {
            Map(m => m.Segmento).Index(0).Name("Segmento");
            Map(m => m.Nome).Index(1).Name("Nome");
            Map(m => m.Codigo).Index(2).Name("Sigla");
            Map(m => m.QuantidadeQuotaMagicNumber).Index(3).Name("Magic Number (Qtd)");
            Map(m => m.ValorTotalMagicNumber).Index(4).Name("Magic Number (R$)");
            Map(m => m.Preco).Index(5).Name("Preço");
            Map(m => m.PVP).Index(6).Name("P/VP");
            Map(m => m.PercentualDY12Meses).Index(7).Name("DY (Últimos 12M)");
            Map(m => m.QuantidadeQuotas).Index(8).Name("Quantidade Quotas");
            Map(m => m.ValorMercado).Index(9).Name("Valor de Mercado (R$)");
            Map(m => m.PatrimonioLiquido).Index(10).Name("Patrimonio Líquido (R$)");
            Map(m => m.ValorDividendoPorAcao).Index(11).Name("Dividendo por ação (R$)");
            Map(m => m.PercentualDYMes).Index(12).Name("Dividend Yield (último mês)");
        }
    }
}