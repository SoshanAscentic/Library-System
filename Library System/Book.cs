namespace LibrarySystem
{
    internal class Book
    {
        private String title;
        private String author;
        private int publicationYear;
        private String catagory;
        private bool isAvailable;

        public Book(String title, String author, int publicationYear, String catagory)
        {
            this.title = title;
            this.author = author;
            this.publicationYear = publicationYear;
            this.catagory = catagory;
            this.isAvailable = true; // Default to available
        }

        public String Title
        {
            get { return title; }
            set { title = value; }
        }

        public String Author
        {
            get { return author; }
            set { author = value; }
        }

        public int PublicationYear
        {
            get { return publicationYear; }
            set
            {

                if (value > 1450 && value > DateTime.Now.Year)
                {
                    publicationYear = value;

                }
                else
                {
                    Console.WriteLine("Enter a valid year of publication");
                }
            }
        }

        public String Catagory
        {
            get { return catagory; }
            set { catagory = value; }
        }

        public bool IsAvailable
        {
            get { return isAvailable; }
            set { isAvailable = value; }
        }
    }
}