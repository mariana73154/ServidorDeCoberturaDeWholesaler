namespace Client
{

    public class Ficheiro
    {
        public string FileName { get; set; }
        public string Content { get; set; }
        public string Operadora { get; set; }
        public string  FileExtension { get; set; }

        public Ficheiro(string fileName, string content, string operadora, string fileExtension)
        {
            FileName = fileName;
            Content = content;
            Operadora = operadora;
            FileExtension = fileExtension;
        }

        public Ficheiro() {}

    }
}