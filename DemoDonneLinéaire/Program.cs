namespace DemoDonneLinéaire
{
    public class Program
    {
        public class Personne
        {
            public int Age { get; set; }
            public Personne(int pAge)
            {
                Age = pAge;
            }
            public Personne(Personne copeie ): this(copeie.Age)
            {
                
            }
        }

        static void Main(string[] args)
        {
            int x = 5;
            int y = x;

            x = 10;
            Console.WriteLine($"X (= 10) : {x} | Y : {y}");

            Personne eric = new Personne(20);
            Personne isabelle = eric;
            Personne johanne = new Personne(eric);
            eric.Age = 32;

            Console.WriteLine($"Âge d'Éric : {eric.Age} | âge d'Isabelle : {isabelle.Age}" +
                $"| âge johanne : {johanne.Age}");

            Console.ReadKey();
        }
    }
}
