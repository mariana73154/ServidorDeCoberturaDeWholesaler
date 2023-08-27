namespace Server
{

    public class Municipio
    {
        public string Name { get; set; }
        public int MunicipioClassification { get; set; } 
        public int  Sobreposicao { get; set; } 

        public  Municipio(string name)
        {
            Name = name;
            MunicipioClassification = 0;
            Sobreposicao = 0;
        }

        public Municipio() {}
    }
}