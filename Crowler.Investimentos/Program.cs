using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using Crowler.Investimentos.Domain;
using Crowler.Investimentos.Infra;
using CsvHelper;

namespace Crowler.Investimentos
{
    class Program
    {
        static void Main(string[] args)
        {
            var codigosFundos = new string[] {"XPML11", "VISC11", "FIGS11", "MALL11", "OUJP11", "BRCR11", "FLMA11", "PVBI11", "MCCI11",
                "MXRF11", "KNRI11", "BCFF11", "TGAR11", "RBRP11", "LVBI11", "GGRC11", "GTWR11", "HGLG11", "XPLG11", "ALZR11", "IRDM11"};

            var watch = Stopwatch.StartNew();

            var fundos = new List<FundoInvestimento>();

            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);

            Parallel.ForEach(codigosFundos, (codigo) =>
            {
                var address = $"https://statusinvest.com.br/fundos-imobiliarios/{codigo}";
                var document = context.OpenAsync(address).Result;
                var fundo = new FundoInvestimento();
                fundo.Codigo = document.QuerySelector("#main-header > div > div > div:nth-child(1) > div > ol > li:nth-child(3) > a > span").TextContentToString();
                fundo.Nome = document.QuerySelector("#main-header > div > div > div:nth-child(1) > h1 > small").TextContentToString();
                fundo.Segmento = document.QuerySelector("#fund-section > div > div > div.card.bg-main-gd-h.white-text.rounded > div > div:nth-child(1) > div > div > div > a > strong").TextContentToString();
                fundo.Preco = document.QuerySelector("#main-2 > div.container.pb-7 > div.top-info.d-flex.flex-wrap.justify-between.mb-3.mb-md-5 > div.info.special.w-100.w-md-33.w-lg-20 > div > div:nth-child(1) > strong").TextContentToDouble();
                fundo.PercentualDY12Meses = document.QuerySelector("#main-2 > div.container.pb-7 > div.top-info.d-flex.flex-wrap.justify-between.mb-3.mb-md-5 > div:nth-child(4) > div > div:nth-child(1) > strong").TextContentToDouble();
                fundo.QuantidadeQuotas = document.QuerySelector("#main-2 > div.container.pb-7 > div:nth-child(5) > div > div:nth-child(6) > div > div:nth-child(2) > span.sub-value").TextContentToInteger();
                fundo.ValorMercado = document.QuerySelector("#main-2 > div.container.pb-7 > div:nth-child(5) > div > div:nth-child(2) > div > div:nth-child(2) > span.sub-value").TextContentToDouble();
                fundo.PatrimonioLiquido = document.QuerySelector("#main-2 > div.container.pb-7 > div:nth-child(5) > div > div:nth-child(1) > div > div:nth-child(2) > span.sub-value").TextContentToDouble();
                fundo.ValorDividendoPorAcao = document.QuerySelector("#dy-info > div > div.d-flex.align-items-center > strong").TextContentToDouble();
                fundo.PercentualDYMes = document.QuerySelector("#dy-info > div > div:nth-child(2) > div:nth-child(1) > div:nth-child(1) > div > b").TextContentToDouble();

                fundos.Add(fundo);
            });

            watch.Stop();

            Console.WriteLine("Tempo gasto: " + watch.ElapsedMilliseconds + " ms");

            var datetime = DateTime.Now.ToString("ddMMyyyyHmm");
            using (var writer = new StreamWriter($"../data/fundos-{datetime}.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.Configuration.RegisterClassMap<CsvFundoInvestimentoMapping>();
                csv.WriteRecords(fundos);
            }
        }
    }
}
