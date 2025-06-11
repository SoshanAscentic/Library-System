public class Book
{
    public enum BookCategory { Fiction, History, Child }

    private string _title;
    private string _author;
    private int _publicationYear;

    public string Title
    {
        get => _title;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Title cannot be null or empty.", nameof(Title));
            _title = value.Trim();
        }
    }

    public string Author
    {
        get => _author;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Author cannot be null or empty.", nameof(Author));
            _author = value.Trim();
        }
    }

    public int PublicationYear
    {
        get => _publicationYear;
        set
        {
            if (value < 1450 || value > DateTime.Now.Year)
                throw new ArgumentOutOfRangeException(nameof(PublicationYear),
                    $"Publication year must be between 1450 and {DateTime.Now.Year}.");
            _publicationYear = value;
        }
    }

    public BookCategory Category { get; set; }
    public bool IsAvailable { get; set; }

    public Book(string title, string author, int publicationYear, BookCategory category)
    {
        Title = title;              
        Author = author;           
        PublicationYear = publicationYear;  
        Category = category;
        IsAvailable = true;
    }

    public override string ToString()
    {
        return $"Title: {Title}, Author: {Author}, Year: {PublicationYear}, Category: {Category}, Available: {IsAvailable}";
    }
}