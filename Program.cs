using System.Collections.Generic;

namespace AdventOfCode2024Day5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "input.txt");

            List<(int x, int y)> rules = new();
            List<List<int>> invalidLines = new();

            int result = 0;
            int result2 = 0;

            using (StreamReader sr = new StreamReader(path))
            {
                string line = sr.ReadLine();

                // Obtenir, pour chaque ligne de la premiere partie de l'input, l'ordre X et l'ordre Y (les chiffres X étant toujours imprimés avant les chiffres Y)
                while (line.Contains("|"))
                {
                    string[] numbers = line.Split('|').ToArray();
                    rules.Add((int.Parse(numbers[0]),int.Parse(numbers[1])));
                    line = sr.ReadLine();
                }

                while (line != null && string.IsNullOrWhiteSpace(line))
                {
                    line = sr.ReadLine();
                }

                while (line != null && line.Contains(","))
                {
                    List<int> numberToVerify = line.Split(",").Select(int.Parse).ToList();

                    List<(int x, int y)> applicablerules = new();


                    bool isValableLine = true;

                    foreach (var rule in rules)
                    {
                        // A chaque nouvelle ligne de cette premiere partie, l'ordre précédent doit être respecté
                        if (numberToVerify.Contains(rule.x) && numberToVerify.Contains(rule.y))
                        {
                            applicablerules.Add(rule);
                        }
                    }

                    foreach (var rule in applicablerules)
                    {
                        // pour la seconde partie de l'input, vérifier quelles lignes respectent les regles de l'ordre ainsi créé.
                        // vérifier la présence d'un chiffre de la liste à un autre par rrapport à son index (et non les valeurs)
                        int indexX = numberToVerify.IndexOf(rule.x);
                        int indexY = numberToVerify.IndexOf(rule.y);

                        if (indexX >= indexY)
                        {
                            isValableLine = false;
                            invalidLines.Add(numberToVerify);
                            break;
                        }                        
                    }

                    if (isValableLine)
                    {
                        // Pour chaque ligne respectant l'ordre, ajouter la valeur du numéro central
                        result = result + numberToVerify[numberToVerify.Count / 2];
                    }

                    line = sr.ReadLine();
                }

                Console.WriteLine("Premiere partie : " + result.ToString());

                // seconde partie : corriger les lignes restantes
                foreach (var list in invalidLines)
                {
                    bool isValableLine = false;
                    do
                    {
                        isValableLine = true;
                    
                        foreach (var rule in rules)
                        {
                            if (list.Contains(rule.x) && list.Contains(rule.y))
                            {
                                int indexX = list.IndexOf(rule.x);
                                int indexY = list.IndexOf(rule.y);

                                if (indexX > indexY)
                                {
                                    list.RemoveAt(indexX);
                                    list.Insert(indexY, rule.x);
                                    isValableLine = false;
                                }
                            }                            
                        } 
                    }
                    while (!isValableLine);

                    result2 += list[list.Count / 2];
                }
                Console.WriteLine("Deuxieme partie : " + result2.ToString());
            }
        }
    }
}
