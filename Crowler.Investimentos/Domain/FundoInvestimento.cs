using System;

namespace Crowler.Investimentos.Domain
{
    public class FundoInvestimento
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Segmento { get; set; }
        public double Preco { get; set; }
        public double PVP => ValorMercado / PatrimonioLiquido;
        public double PercentualDY12Meses { get; set; }
        public int QuantidadeQuotas { get; set; }
        public double ValorMercado { get; set; }
        public double PatrimonioLiquido { get; set; }
        public double ValorDividendoPorAcao { get; set; }
        public double PercentualDYMes { get; set; }
        public int QuantidadeQuotaMagicNumber => (int)(Preco / ValorDividendoPorAcao);
        public double ValorTotalMagicNumber => QuantidadeQuotaMagicNumber * Preco;
    }
}