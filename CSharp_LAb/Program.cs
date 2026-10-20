using CSharp_LAb;

while (true)
{
  Console.WriteLine("Quel exo voulez vous lancer ?");
  var choix = Console.ReadLine();

  switch (choix)
  {
    case "1": Exo1.run(); break;
    case "2": Exo2.run(); break;
    case "3": Exo3.Run(); break;
    case "4": Exo4.Run(); break;
    case "q" : return ;
  }
}