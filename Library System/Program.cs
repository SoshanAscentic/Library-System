using System;

namespace Library_System
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ILibrary library = new Library();
            Member? loggedInMember = null;

            while (loggedInMember == null)
            {
                Console.WriteLine("Welcome to the Library System!");
                Console.WriteLine("1. Sign up as a new member");
                Console.WriteLine("2. Login with Member ID");
                Console.Write("Please choose an option (1 or 2): ");
                string? input = Console.ReadLine();

                if (input == "1")
                {
                    Console.Write("Enter your name: ");
                    string? name = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(name))
                    {
                        Console.WriteLine("Name cannot be empty.");
                        continue;
                    }

                    Console.WriteLine("Select member type: 0 - Member, 1 - Staff, 2 - Management");
                    string? typeInputStr = Console.ReadLine();

                    if (!int.TryParse(typeInputStr, out int typeInput) || typeInput < 0 || typeInput > 2)
                    {
                        Console.WriteLine("Invalid member type.");
                        continue;
                    }

                    Member.MemberType memberType = (Member.MemberType)typeInput;
                    Member newMember = library.AddMember(name, memberType);
                    loggedInMember = newMember;

                    Console.WriteLine($"Successfully signed up! Your Member ID is: {newMember.MemberID}");
                }
                else if (input == "2")
                {
                    Console.Write("Enter your Member ID: ");
                    string? memberIdStr = Console.ReadLine();

                    if (!int.TryParse(memberIdStr, out int enteredId))
                    {
                        Console.WriteLine("Invalid input.");
                        continue;
                    }

                    // Check if member exists
                    var existingMember = ((Library)library).GetMemberById(enteredId);
                    if (existingMember != null)
                    {
                        loggedInMember = existingMember;
                        Console.WriteLine($"Welcome back, {existingMember.Name}!");
                    }
                    else
                    {
                        Console.WriteLine("Member not found. Please sign up first.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid option.");
                }
            }

            Console.Clear();
            Console.WriteLine($"Welcome, {loggedInMember.Name}! Type: {loggedInMember.Type}");

            while (true)
            {
                Console.WriteLine("\n--- Library Dashboard ---");
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. Remove Book");
                Console.WriteLine("3. Borrow Book");
                Console.WriteLine("4. Return Book");
                if (loggedInMember.Type != Member.MemberType.Staff)
                {
                    Console.WriteLine("5. Display Books");
                }
                if (loggedInMember.Type == Member.MemberType.Management || loggedInMember.Type == Member.MemberType.Member)
                {
                    Console.WriteLine("6. Display Members");
                }
                Console.WriteLine("0. Exit");

                Console.Write("Choose an option: ");
                string? option = Console.ReadLine();

                try
                {
                    switch (option)
                    {
                        case "1":
                            Console.Write("Title: ");
                            string? title = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(title))
                            {
                                Console.WriteLine("Title cannot be empty.");
                                break;
                            }

                            Console.Write("Author: ");
                            string? author = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(author))
                            {
                                Console.WriteLine("Author cannot be empty.");
                                break;
                            }

                            Console.Write("Publication Year: ");
                            string? pubYearStr = Console.ReadLine();
                            if (!int.TryParse(pubYearStr, out int pubYear))
                            {
                                Console.WriteLine("Invalid publication year.");
                                break;
                            }

                            Console.WriteLine("Category (0 - Fiction, 1 - History, 2 - Child): ");
                            string? categoryStr = Console.ReadLine();
                            if (!int.TryParse(categoryStr, out int categoryInt) || categoryInt < 0 || categoryInt > 2)
                            {
                                Console.WriteLine("Invalid category.");
                                break;
                            }

                            Book.BookCategory category = (Book.BookCategory)categoryInt;
                            Book book = new Book(title, author, pubYear, category);
                            library.AddBook(book);
                            Console.WriteLine("Book added successfully.");
                            break;

                        case "2":
                            Console.Write("Enter title of the book to remove: ");
                            string? removeTitle = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(removeTitle))
                            {
                                Console.WriteLine("Title cannot be empty.");
                                break;
                            }

                            Console.Write("Enter publication year: ");
                            string? removeYearStr = Console.ReadLine();
                            if (!int.TryParse(removeYearStr, out int removeYear))
                            {
                                Console.WriteLine("Invalid publication year.");
                                break;
                            }

                            library.RemoveBook(removeTitle, removeYear);
                            Console.WriteLine("Book removed successfully.");
                            break;

                        case "3":
                            Console.Write("Enter title of the book to borrow: ");
                            string? borrowTitle = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(borrowTitle))
                            {
                                Console.WriteLine("Title cannot be empty.");
                                break;
                            }

                            Console.Write("Enter publication year: ");
                            string? borrowYearStr = Console.ReadLine();
                            if (!int.TryParse(borrowYearStr, out int borrowYear))
                            {
                                Console.WriteLine("Invalid publication year.");
                                break;
                            }

                            library.BorrowBook(borrowTitle, borrowYear, loggedInMember.MemberID);
                            Console.WriteLine("Book borrowed successfully.");
                            break;

                        case "4":
                            Console.Write("Enter title of the book to return: ");
                            string? returnTitle = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(returnTitle))
                            {
                                Console.WriteLine("Title cannot be empty.");
                                break;
                            }

                            Console.Write("Enter publication year: ");
                            string? returnYearStr = Console.ReadLine();
                            if (!int.TryParse(returnYearStr, out int returnYear))
                            {
                                Console.WriteLine("Invalid publication year.");
                                break;
                            }

                            library.ReturnBook(returnTitle, returnYear, loggedInMember.MemberID);
                            Console.WriteLine("Book returned successfully.");
                            break;

                        case "5":
                            if (loggedInMember.Type != Member.MemberType.Staff)
                            {
                                library.DisplayBooks();
                            }
                            else
                            {
                                Console.WriteLine("Access denied. Staff cannot view books.");
                            }
                            break;

                        case "6":
                            if (loggedInMember.Type == Member.MemberType.Management || loggedInMember.Type == Member.MemberType.Member)
                            {
                                library.DisplayMembers();
                            }
                            else
                            {
                                Console.WriteLine("Access denied. Only Members and Management can view members.");
                            }
                            break;

                        case "0":
                            Console.WriteLine("Exiting...");
                            return;

                        default:
                            Console.WriteLine("Invalid option.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}
