﻿namespace MediaShop.Models
{
    public class Product
    {
        public static int idCounter { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public int stock { get; set; }
        public ProductType productType { get; set; }
        // unitsSold lagras inte i databasen, utan beräknas istället dynamiskt i StatsForm klassen baserat
        // på produkter från alla kvitton.
        public int unitsSold { get; set; }

        public Product()
        {
            // Här skapas ett unikt id för varje produkt med hjälp av en static int (idCounter).
            idCounter++;
            this.id = idCounter;
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
