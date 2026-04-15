using System;
using System.Collections.Generic;
using System.IO;

public class Aufgabe2
{
    static void Main(string[] args)
    {
        // =============================================
        // HIER PFade + DATEINAMEN ÄNDERN (oder per Kommandozeile)
        // =============================================
        string inputPath = "C:\\Users\\User\\Documents\\Schule\\CHC\\CHC\\aufgaben\\coding1_inputs\\coding2\\coding2\\" + "input_example.txt";
        string outputPath = "C:\\Users\\User\\Documents\\Schule\\CHC\\CHC\\aufgaben\\coding1_inputs\\coding2\\coding2\\" + "output_example2.txt";

        Console.WriteLine($"Input  → {inputPath}");
        Console.WriteLine($"Output → {outputPath}");

        string[] allLines;
        try
        {
            allLines = File.ReadAllLines(inputPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"FEHLER beim Lesen der Input-Datei:\n{ex.Message}");
            return;
        }

        List<string> outputLines = new List<string>();
        int i = 0;

        while (i < allLines.Length)
        {
            // Neues Feld sammeln (aufeinanderfolgende nicht-leere Zeilen)
            List<string> field = new List<string>();
            while (i < allLines.Length && !string.IsNullOrWhiteSpace(allLines[i]))
            {
                field.Add(allLines[i].TrimEnd());
                i++;
            }
            i++; // Leerzeile überspringen

            if (field.Count < 2) continue; // Mindestens 2 Zeilen laut Level 2

            // Alle Zeilen müssen gleich lang sein
            int width = field[0].Length;
            bool valid = true;
            foreach (var row in field)
                if (row.Length != width) valid = false;

            if (!valid || width == 0) continue;

            // Feld verarbeiten
            List<string> newField = SimulateStep(field);

            // Zum Gesamt-Output hinzufügen
            outputLines.AddRange(newField);
            outputLines.Add(""); // Leerzeile zwischen Feldern
        }

        // Letzte Leerzeile entfernen (optional, aber sauber)
        if (outputLines.Count > 0 && string.IsNullOrWhiteSpace(outputLines[^1]))
            outputLines.RemoveAt(outputLines.Count - 1);

        try
        {
            File.WriteAllLines(outputPath, outputLines);
            Console.WriteLine($"✅ {outputLines.Count} Zeilen geschrieben → {outputPath}");
            Console.WriteLine("Level 2 erfolgreich abgeschlossen!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"FEHLER beim Schreiben der Output-Datei:\n{ex.Message}");
        }
    }

    static List<string> SimulateStep(List<string> field)
    {
        int height = field.Count;
        int width = field[0].Length;

        // Grid als mutable Char-Array
        char[][] grid = new char[height][];
        for (int r = 0; r < height; r++)
            grid[r] = field[r].ToCharArray();

        // Alle Murmeln finden + alte Positionen löschen
        List<(int col, int row)> marbles = new List<(int col, int row)>();
        for (int r = 0; r < height; r++)
        {
            for (int c = 0; c < width; c++)
            {
                if (grid[r][c] == 'O')
                {
                    marbles.Add((c, r));
                    grid[r][c] = '.'; // alte Position leeren
                }
            }
        }

        // Jede Murmel bewegen
        foreach (var m in marbles)
        {
            int r = m.row;
            int c = m.col;

            // Regel: immer die Zelle DIREKT DARUNTER anschauen
            char neighbor = (r + 1 < height) ? grid[r + 1][c] : '.';

            int newC = c;
            int newR = r;

            if (neighbor == '.')
            {
                newR = r + 1;           // gerade runter
            }
            else if (neighbor == '\\')
            {
                newC = c + 1;           // Rampe \ → rechts, gleiche Zeile
            }
            else if (neighbor == '/')
            {
                newC = c - 1;           // Rampe / → links, gleiche Zeile
            }

            // Neue Position setzen (laut Beschränkungen immer gültig)
            if (newR < height && newC >= 0 && newC < width)
                grid[newR][newC] = 'O';
        }

        // Zurück in Strings
        List<string> result = new List<string>();
        for (int r = 0; r < height; r++)
            result.Add(new string(grid[r]));

        return result;
    }
}