namespace Library_System.Models
{
    public class Book
    {

        public enum BookCategory
        {
            Fiction,
            History,
            Child
        }
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
        public BookCategory Category { get; set; }
        public bool IsAvailable { get; set; }

        public Book(string title, string author, int publicationYear, BookCategory category)
        {
            Title = title;
            Author = author;
            this.publicationYear = publicationYear;
            Category = category;
            IsAvailable = true; // Default to available
        }
    }
}