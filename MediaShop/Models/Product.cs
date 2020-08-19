using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediaShop.Models
{
    public class Product
    {
        [NotMapped]
        public static int idCounter { get; set; }
        [Key]
        public int id { get; set; }
        [Column]
        public string name { get; set; }
        [Column]
        public double price { get; set; }
        [Column]
        public int stock { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public ProductType productType { get; set; }

        // unitsSold lagras inte i databasen, utan beräknas istället dynamiskt i StatsForm klassen baserat
        // på produkter från alla kvitton.
        [NotMapped]
        public int unitsSold { get; set; }

        public Product()
        {
            // Här skapas ett unikt id för varje produkt med hjälp av en static int (idCounter).
            idCounter++;
            //this.id = idCounter;
            // Varje ny produkt måste ha minst 1 stock (lagerstatus).
            this.stock = 1;
        }

        public enum ProductType
        {
            BOOK,
            CD,
            DVD,
            GAME,
            OTHER
        }


        public override string ToString()
        {
            string s = "";
            s += name + " (Id: " + id + ") " + "[" + productType + "]\n";
            s += price + " SEK\n";
            s += stock + " in stock\n";
            s += unitsSold + " units sold\n";
            return s;
        }
    }
}
