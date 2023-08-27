namespace GrpcServer.Models
{
    public class Domicilio
    {
        public int Num_admin { get; set; }
        public string Municipio { get; set; }
        public string Localidade { get; set; }
        public string Rua { get; set; }
        public int Num_porta { get; set; }
        public string Cod_postal { get; set; }
        public Domicilio()
        {

        }
        public Domicilio(string municipio, string localidade, string rua, int num_porta, string cod_postal, int num_admin ) {
            Num_admin = num_porta;
            Municipio = municipio;
            Localidade = localidade;
            Rua = rua;
            Num_porta = num_porta;
            Cod_postal = cod_postal;
        }

    }
}
