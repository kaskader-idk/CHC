using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    public class ReadWrite
    {
        public string ReadFromFile(string path, string fileName)
        {
            string fullPath = Path.Combine(path, fileName);

            if (!File.Exists(fullPath))
                throw new FileNotFoundException("Datei nicht gefunden", fullPath);

            return File.ReadAllText(fullPath);
        }

        public void WriteIntoFile(string path, string fileName, string content)
        {
            string fullPath = Path.Combine(path, fileName);

            // Ordner erstellen, falls er nicht existiert
            Directory.CreateDirectory(path);

            File.WriteAllText(fullPath, content);
        }
    }
}
