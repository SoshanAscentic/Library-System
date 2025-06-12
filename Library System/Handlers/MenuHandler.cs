using Library_System.Interfaces;
using Library_System.Models;
using Library_System.Models.Members;
using System;

namespace Library_System.Handlers
{
    public class MenuHandler
    {
        private readonly IBookService bookService;
        private readonly IMemberService memberService;
        private readonly IBorrowingService borrowingService;
        private readonly IConsoleService console;
        private readonly IInputService input;
        private readonly IAuthenticationService authService;
        
        private Member? currentUser;

        public MenuHandler(
            IBookService bookService,
            IMemberService memberService,
            IBorrowingService borrowingService,
            IConsoleService console,
            IInputService input,
            IAuthenticationService authService)
        {
            this.bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
            this.memberService = memberService ?? throw new ArgumentNullException(nameof(memberService));
            this.borrowingService = borrowingService ?? throw new ArgumentNullException(nameof(borrowingService));
            this.console = console ?? throw new ArgumentNullException(nameof(console));
            this.input = input ?? throw new ArgumentNullException(nameof(input));
            this.authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        public void Run()
        {
            console.WriteLine("=== Welcome to the Library Management System ===");
            
            while (true)
            {
                if (currentUser == null)
                {
                    ShowAuthenticationMenu();
                }
                else
                {
                    ShowMainMenu();
                }
            }
        }

        private void ShowAuthenticationMenu()
        {
            console.WriteLine("\n=== Authentication Menu ===");
            console.WriteLine("1. Login");
            console.WriteLine("2. Sign Up");
            console.WriteLine("3. Exit");
            console.Write("Choose an option: ");

            var choice = input.GetIntegerInputInRange("", 1, 3);
            if (choice == null) return;

            switch (choice)
            {
                case 1:
                    currentUser = authService.Login();
                    break;
                case 2:
                    currentUser = authService.SignUp();
                    break;
                case 3:
                    console.WriteLine("Thank you for using the Library Management System!");
                    Environment.Exit(0);
                    break;
            }
        }

        private void ShowMainMenu()
        {
            console.WriteLine($"\n=== Main Menu - Welcome {currentUser!.Name} ({currentUser.GetMemberType()}) ===");
            
            int optionNumber = 1;
            
            // Show options based on user permissions
            if (currentUser.CanViewBooks())
            {
                console.WriteLine($"{optionNumber++}. View All Books");
            }
            
            if (currentUser.CanBorrowBooks())
            {
                console.WriteLine($"{optionNumber++}. Borrow Book");
                console.WriteLine($"{optionNumber++}. Return Book");
            }
            
            if (currentUser.CanViewMembers())
            {
                console.WriteLine($"{optionNumber++}. View All Members");
            }
            
            if (currentUser.CanManageBooks())
            {
                console.WriteLine($"{optionNumber++}. Add Book");
                console.WriteLine($"{optionNumber++}. Remove Book");
            }
            
            console.WriteLine($"{optionNumber++}. View My Profile");
            console.WriteLine($"{optionNumber}. Logout");

            var choice = input.GetIntegerInputInRange("Choose an option: ", 1, optionNumber);
            if (choice == null) return;

            ProcessMainMenuChoice(choice.Value);
        }

        private void ProcessMainMenuChoice(int choice)
        {
            int currentOption = 1;

            if (currentUser!.CanViewBooks() && choice == currentOption++)
            {
                ViewAllBooks();
                return;
            }

            if (currentUser.CanBorrowBooks())
            {
                if (choice == currentOption++)
                {
                    BorrowBook();
                    return;
                }
                if (choice == currentOption++)
                {
                    ReturnBook();
                    return;
                }
            }

            if (currentUser.CanViewMembers() && choice == currentOption++)
            {
                ViewAllMembers();
                return;
            }

            if (currentUser.CanManageBooks())
            {
                if (choice == currentOption++)
                {
                    AddBook();
                    return;
                }
                if (choice == currentOption++)
                {
                    RemoveBook();
                    return;
                }
            }

            if (choice == currentOption++)
            {
                ViewMyProfile();
                return;
            }

            if (choice == currentOption)
            {
                console.WriteLine("Logged out successfully!");
                currentUser = null;
            }
        }

        private void ViewAllBooks()
        {
            console.WriteLine("\n=== All Books ===");
            bookService.DisplayBooks();
            WaitForKeyPress();
        }

        private void BorrowBook()
        {
            console.WriteLine("\n=== Borrow Book ===");
            
            var title = input.GetNonEmptyInput("Enter book title: ");
            if (title == null) return;

            var year = input.GetIntegerInput("Enter publication year: ");
            if (year == null) return;

            try
            {
                borrowingService.BorrowBook(title, year.Value, currentUser!.MemberID);
                console.WriteLine("Book borrowed successfully!");
            }
            catch (Exception ex)
            {
                console.WriteLine($"Error: {ex.Message}");
            }
            
            WaitForKeyPress();
        }

        private void ReturnBook()
        {
            console.WriteLine("\n=== Return Book ===");
            
            var title = input.GetNonEmptyInput("Enter book title: ");
            if (title == null) return;

            var year = input.GetIntegerInput("Enter publication year: ");
            if (year == null) return;

            try
            {
                borrowingService.ReturnBook(title, year.Value, currentUser!.MemberID);
                console.WriteLine("Book returned successfully!");
            }
            catch (Exception ex)
            {
                console.WriteLine($"Error: {ex.Message}");
            }
            
            WaitForKeyPress();
        }

        private void ViewAllMembers()
        {
            console.WriteLine("\n=== All Members ===");
            memberService.DisplayMembers();
            WaitForKeyPress();
        }

        private void AddBook()
        {
            console.WriteLine("\n=== Add Book ===");
            
            var title = input.GetNonEmptyInput("Enter book title: ");
            if (title == null) return;

            var author = input.GetNonEmptyInput("Enter book author: ");
            if (author == null) return;

            var year = input.GetIntegerInput("Enter publication year: ");
            if (year == null) return;

            console.WriteLine("Select book category:");
            console.WriteLine("0. Fiction");
            console.WriteLine("1. History");
            console.WriteLine("2. Child");

            var categoryChoice = input.GetIntegerInputInRange("Enter category (0-2): ", 0, 2);
            if (categoryChoice == null) return;

            try
            {
                var category = (Book.BookCategory)categoryChoice.Value;
                var book = new Book(title, author, year.Value, category);
                bookService.AddBook(book);
                console.WriteLine("Book added successfully!");
            }
            catch (Exception ex)
            {
                console.WriteLine($"Error: {ex.Message}");
            }
            
            WaitForKeyPress();
        }

        private void RemoveBook()
        {
            console.WriteLine("\n=== Remove Book ===");
            
            var title = input.GetNonEmptyInput("Enter book title: ");
            if (title == null) return;

            var year = input.GetIntegerInput("Enter publication year: ");
            if (year == null) return;

            try
            {
                bookService.RemoveBook(title, year.Value);
                console.WriteLine("Book removed successfully!");
            }
            catch (Exception ex)
            {
                console.WriteLine($"Error: {ex.Message}");
            }
            
            WaitForKeyPress();
        }

        private void ViewMyProfile()
        {
            console.WriteLine("\n=== My Profile ===");
            console.WriteLine(currentUser!.ToString());
            console.WriteLine($"Permissions:");
            console.WriteLine($"- Can Borrow Books: {currentUser.CanBorrowBooks()}");
            console.WriteLine($"- Can View Books: {currentUser.CanViewBooks()}");
            console.WriteLine($"- Can View Members: {currentUser.CanViewMembers()}");
            console.WriteLine($"- Can Manage Books: {currentUser.CanManageBooks()}");
            WaitForKeyPress();
        }

        private void WaitForKeyPress()
        {
            console.WriteLine("\nPress any key to continue...");
            console.ReadLine();
        }
    }
}