using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPessoa.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {
     
        public PessoaController(ILogger<PessoaController> logger)
        {
        }

        ///<summary>
        /// Rota responsável por realizar processamento de dados de uma pessoa - Calcula idade, calcula imc, caclulca inss, realiza
        /// conversão de saldo para dolar.
        /// </summary>
        /// <returns>Retorna os dados processados da pessoa</returns>
        /// <response code="200">Returna os dados processados com sucesso</response>
        /// <response code="400"></response>

        [HttpPost]
        public PessoaResponse ProcessarInformacoesPessoais([FromBody] PessoaRequest request)
        {
            var anoAtual = DateTime.Now.Year;
            var idade = anoAtual - request.DataNascimento.Year;

            var imc = Math.Round(request.Peso / (request.Altura * request.Altura), 2);
            var classificacao = "";

            if (imc < (decimal)18.5)
            {
                classificacao = "abaixo do peso ideal";
            }
            else if (imc >= (decimal)18.5 && imc <= (decimal)24.99)
            {
                classificacao = "Peso Normal";
            }
            else if (imc >= (decimal)25 && imc <= (decimal)29.99)
            {
                classificacao = "Pré-Obesidade";
            }
            else if (imc >= (decimal)30 && imc <= (decimal)34.99)
            {
                classificacao = "Obesidade Grau I";
            }
            else if (imc >= (decimal)35 && imc <= (decimal)39.99)
            {
                classificacao = "Obsesidade Grau II";
            }
            else
            {
                classificacao = "Obesidade Grau III";
            }

            var aliquota = 7.5;

            if (request.Salario <= 1212)
            {
                aliquota = 7.5;
            }
            else if (request.Salario >= (decimal)1212.01 && request.Salario <= (decimal)2427.35)
            {
                aliquota = 9;
            }
            else if (request.Salario >= (decimal)2427.36 && request.Salario <= (decimal)3641.03)
            {
                aliquota = 12;
            }
            else
            {
                aliquota = 14;
            }

            var inss = (request.Salario * (decimal)aliquota) / 100;
            var salarioLiquido = request.Salario - inss;

            var dolar = (decimal)5.14;
            var saldoDolar = Math.Round(request.Saldo / dolar, 2);

            var resposta = new PessoaResponse();
            resposta.SaldoDolar = (double)saldoDolar;
            resposta.Aliquota = aliquota;
            resposta.SalarioLiquido = (double)salarioLiquido;
            resposta.Classificacao = classificacao;
            resposta.Idade = idade;
            resposta.Imc = imc;
            resposta.Inss = (double)inss;
            resposta.Nome = request.Nome;

            return resposta;

        }
    }
}
