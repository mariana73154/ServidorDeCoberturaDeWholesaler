using System.Globalization;

namespace GrpcServer.Models
{
    public class Cobertura
    {
        public string Operadora { get; set; }
        public int Num_admin { get; set; }
        public string N_Owner { get; set; }
        public string Estado { get; set; }
        public string Modalidade { get; set; }

        public Cobertura(string operadora, int num_admin, string n_Owner, string estado, string modalidade)
        {
            Operadora = operadora;
            Num_admin = num_admin;
            N_Owner = n_Owner;
            Estado = estado;
            Modalidade = modalidade;
        }

    }
}
