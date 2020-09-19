using System;
using System.IO;

namespace JuntaArquivos
{
    public class Program
    {
        public const string PASTA_ARQUIVO = @"C:\Test\arquivos\";
        private static StreamWriter sw = null;

        public static void Main(string[] args)
        {
            var dir = new DirectoryInfo(PASTA_ARQUIVO);
            string pastaNovoArquivo = Path.Combine(PASTA_ARQUIVO, "Agrupados");
            string NomeArquivoLeitura = "Leitura_GrupoA" + DateTime.Now.Year.ToString()  + DateTime.Now.Month.ToString("D2") +  DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff").Replace("/", "_").Replace(" ", "_").Replace(":", "") ;

            if (!Directory.Exists(pastaNovoArquivo))
                Directory.CreateDirectory(pastaNovoArquivo);

            string misuraFileLongName = Path.Combine(pastaNovoArquivo, NomeArquivoLeitura + ".txt");

            sw = File.CreateText(misuraFileLongName);
            sw.NewLine = "\n";
            sw.Flush();

            int cont = 1;

            foreach (var arquivo in dir.GetFiles())
            {
                string textoArquivo = File.ReadAllText(arquivo.FullName);

                FileInfo fileInfo = new FileInfo(misuraFileLongName);

                int Arq10mb = 10000000;
                long Arq1gb = 1000000000000;

                if (fileInfo.Length >= Arq1gb)
                {
                    sw.Flush();
                    sw.Close();
                    sw.Dispose();

                    misuraFileLongName = Path.Combine(pastaNovoArquivo, NomeArquivoLeitura + cont + ".txt");
                    cont++;

                    sw = File.CreateText(misuraFileLongName);
                    sw.NewLine = "\n";
                    sw.Flush();
                }

                sw.Write(textoArquivo);
                sw.Flush();
            }

            sw.Flush();
            sw.Close();
            sw.Dispose();
        }
    }
}
