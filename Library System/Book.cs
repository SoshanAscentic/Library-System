namespace LibrarySystem
{
    internal class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }

        private int publicationYear;
        public int PublicationYear
        {
            get { return publicationYear; }
            set
            {

                if (value >= 1450 && value <= DateTime.Now.Year)
                {
                    publicationYear = value;

                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Publication year must be between 1450 and the current year.");
                }
            }
        }
        private string Catagory { get; set; }
        private bool IsAvailable { get; set; }

        public Book(string title, string author, int publicationYear, string catagory)
        {
            Title = title;
            Author = author;
            this.publicationYear = publicationYear;
            Catagory = catagory;
            IsAvailable = true; // Default to available
        }

        
    }


}