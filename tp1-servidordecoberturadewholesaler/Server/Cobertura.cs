using System.Data.SqlClient;

namespace Server
{
    public class Cobertura
    {
        public Ficheiro File { get; set; }
        public string Municipio { get; set; }
        public string Localidade { get; set; }
        public string  Rua { get; set; }
        public int NumPorta { get; set; }
        public string CodPostal { get; set; }
        public string Owner { get; set; }

        public string Operadora { get; set; }=null;
        public Cobertura(Ficheiro file, string municipio, string localidade, string rua, int numPorta, string codPostal, string owner)
        {
            File = file;
            Municipio = municipio;
            Localidade = localidade;
            Rua = rua;
            NumPorta = numPorta;
            CodPostal = codPostal;
            Owner = owner;
        }

        public Cobertura(string municipio, string localidade, string rua, int numPorta, string codPostal, string owner)
        {
            Municipio = municipio;
            Localidade = localidade;
            Rua = rua;
            NumPorta = numPorta;
            CodPostal = codPostal;
            Owner = owner;
        }
        public Cobertura(string operadora, string municipio, string localidade, string rua, int numPorta, string codPostal, string owner)
        {
            Municipio = municipio;
            Localidade = localidade;
            Rua = rua;
            NumPorta = numPorta;
            CodPostal = codPostal;
            Owner = owner;
            Operadora = operadora;
        }


        public Cobertura() { }


    }
}