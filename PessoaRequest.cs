using System;

namespace WebApiPessoa
{
    public class PessoaRequest
    {
        public string Nome { get; set; }

        public DateTime DataNascimento {  get; set; }
        
        public decimal Altura { get; set; }

        public decimal Peso { get; set; }   
        public decimal Salario { get; set; }

        public decimal Saldo { get; set; }
    }
}
