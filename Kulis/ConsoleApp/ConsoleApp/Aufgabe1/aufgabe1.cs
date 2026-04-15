using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main2(string[] args)
    {
        // =============================================
        // HIER KANNST DU DIE PFade UND DATEINAMEN ÄNDERN
        // =============================================
        string inputPath = "C:\\Users\\User\\Documents\\Schule\\CHC\\CHC\\aufgaben\\coding1_inputs\\coding1\\" + "input1.txt";   // ← ändern oder per Argument
        string outputPath = "C:\\Users\\User\\Documents\\Schule\\CHC\\CHC\\aufgaben\\coding1_inputs\\coding1\\" + "output1.txt";  // ← ändern oder per Argument

        Console.WriteLine($"Input  → {inputPath}");
        Console.WriteLine($"Output → {outputPath}");

        string[] lines;
        try
        {
            lines = File.ReadAllLines(inputPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"FEHLER beim Lesen der Input-Datei:\n{ex.Message}");
            return;
        }

        List<string> results = new List<string>();
        int i = 0;

        while (i < lines.Length)
        {
            if (string.IsNullOrWhiteSpace(lines[i]))
            {
                i++;
                continue;
            }

            string row0 = lines[i].TrimEnd();
            i++;
            if (i >= lines.Length) break;
            string row1 = lines[i].TrimEnd();
            i++;

            if (row0.Length != row1.Length || row0.Length == 0) continue;

            (int col, int row) marble = FindMarble(row0, row1);
            (int newCol, int newRow) newPos = SimulateStep(marble.col, marble.row, row0, row1);

            results.Add($"{newPos.newCol},{newPos.newRow}");
        }

        try
        {
            File.WriteAllLines(outputPath, results);
            Console.WriteLine($"✅ {results.Count} Felder verarbeitet → {outputPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"FEHLER beim Schreiben:\n{ex.Message}");
        }
    }

    static (int col, int row) FindMarble(string row0, string row1)
    {
        for (int c = 0; c < row0.Length; c++)
        {
            if (row0[c] == 'O') return (c, 0);
            if (row1[c] == 'O') return (c, 1);
        }
        throw new InvalidOperationException("Keine Murmel gefunden!");
    }

    static (int newCol, int newRow) SimulateStep(int col, int row, string row0, string row1)
    {
        // Zelle in der anderen Zeile (Nachbarzelle)
        int otherRow = 1 - row;
        char neighbor = (otherRow == 0 ? row0[col] : row1[col]);

        if (neighbor == '.')
        {
            // Regel 1: gerade runter (gleiche Spalte, Zeilenwechsel)
            return (col, otherRow);
        }
        else if (neighbor == '\\')
        {
            // Regel 2: Rampe \ → rechts rutschen (Spalte +1, gleiche Zeile)
            return (col + 1, row);
        }
        else if (neighbor == '/')
        {
            // Spiegelverkehrt: Rampe / → links rutschen (Spalte -1, gleiche Zeile)
            return (col - 1, row);
        }

        // Laut Aufgabenstellung trifft immer eine Regel zu
        return (col, row); // Fallback (sollte nie passieren)
    }
}