using Library_System.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_System.Handlers
{
    public class MenuHandler
    {
        private readonly IBookService bookService;
        private readonly IMemberService memberService;
        private readonly IBorrowingService borrowingService;
        private readonly IConsoleService console;
        private readonly IInputService input;

        public MenuHandler(IBookService bookService, IMemberService memberService,
            IBorrowingService borrowingService, IConsoleService console, IInputService input)
        {
            this.bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
            this.memberService = memberService ?? throw new ArgumentNullException(nameof(memberService));
            this.borrowingService = borrowingService ?? throw new ArgumentNullException(nameof(borrowingService));
            this.console = console ?? throw new ArgumentNullException(nameof(console));
            this.input = input ?? throw new ArgumentNullException(nameof(input));
        }

        public void Run()
        {
            var currentMember = AuthenticateUser();
            if (currentMember == null) return;

            ShowWelcome(currentMember);
            ShowMainMenu(currentMember);
        }

        private Member? AuthenticateUser()
        {
            while (true)
            {
                ShowAuthMenu();
                var choice = input.GetIntegerInputInRange("Choose option (1-2): ", 1, 2);

                if (!choice.HasValue) continue;

                var member = choice.Value switch
                {
                    1 => SignUpUser(),
                    2 => LoginUser(),
                    _ => null
                };

                if (member != null) return member;
            }
        }

        private void ShowAuthMenu()
        {
            console.WriteLine("\n=== Welcome to Library System ===");
            console.WriteLine("1. Sign up as new member");
            console.WriteLine("2. Login with Member ID");
        }

        private Member? SignUpUser()
        {
            var name = input.GetNonEmptyInput("Enter your name: ");
            if (name == null) return null;

            console.WriteLine("\nSelect member type:");
            console.WriteLine("0 - Regular Member");
            console.WriteLine("1 - Minor Staff");
            console.WriteLine("2 - Management Staff");

            var memberType = input.GetIntegerInputInRange("Choose type (0-2): ", 0, 2);
            if (!memberType.HasValue) return null;

            try
            {
                var newMember = memberService.AddMember(name, memberType.Value);
                console.WriteLine($"Registration successful! Your Member ID is: {newMember.MemberID}");
                return newMember;
            }
            catch (Exception ex)
            {
                console.WriteLine($"Registration failed: {ex.Message}");
                return null;
            }
        }

        private Member? LoginUser()
        {
            var memberId = input.GetIntegerInput("Enter your Member ID: ");
            if (!memberId.HasValue) return null;

            var member = memberService.GetMemberById(memberId.Value);
            if (member != null)
            {
                console.WriteLine($"Welcome back, {member.Name}!");
                return member;
            }

            console.WriteLine("Member not found. Please sign up first.");
            return null;
        }

        private void ShowWelcome(Member member)
        {
            console.Clear();
            console.WriteLine($"=== Welcome, {member.Name}! ===");
            console.WriteLine($"Member Type: {member.GetMemberType()}");
        }

        private void ShowMainMenu(Member member)
        {
            while (true)
            {
                try
                {
                    console.WriteLine("\n=== Main Menu ===");
                    console.WriteLine("1. Add Book");
                    console.WriteLine("2. Remove Book");
                    console.WriteLine("3. Borrow Book");
                    console.WriteLine("4. Return Book");
                    console.WriteLine("5. View All Books");
                    console.WriteLine("6. View All Members");
                    console.WriteLine("0. Exit");

                    var choice = input.GetIntegerInputInRange("Choose option (0-6): ", 0, 6);
                    if (!choice.HasValue) continue;

                    if (choice.Value == 0)
                    {
                        console.WriteLine("Thank you for using the Library System!");
                        break;
                    }

                    HandleMenuChoice(choice.Value, member);
                }
                catch (Exception ex)
                {
                    console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        private void HandleMenuChoice(int choice, Member member)
        {
            switch (choice)
            {
                case 1: HandleAddBook(member); break;
                case 2: HandleRemoveBook(member); break;
                case 3: HandleBorrowBook(member); break;
                case 4: HandleReturnBook(member); break;
                case 5: HandleViewBooks(member); break;
                case 6: HandleViewMembers(member); break;
            }
        }

        private void HandleAddBook(Member member)
        {
            if (!member.CanManageBooks())
            {
                console.WriteLine("Access denied. You don't have permission to add books.");
                return;
            }

            var title = input.GetNonEmptyInput("Book Title: ");
            if (title == null) return;

            var author = input.GetNonEmptyInput("Author: ");
            if (author == null) return;

            var year = input.GetIntegerInput("Publication Year: ");
            if (!year.HasValue) return;

            console.WriteLine("Category (0-Fiction, 1-History, 2-Child): ");
            var category = input.GetIntegerInputInRange("", 0, 2);
            if (!category.HasValue) return;

            try
            {
                var book = new Book(title, author, year.Value, (Book.BookCategory)category.Value);
                bookService.AddBook(book);
                console.WriteLine("Book added successfully!");
            }
            catch (Exception ex)
            {
                console.WriteLine($"Failed to add book: {ex.Message}");
            }
        }

        private void HandleRemoveBook(Member member)
        {
            if (!member.CanManageBooks())
            {
                console.WriteLine("Access denied. You don't have permission to remove books.");
                return;
            }

            var title = input.GetNonEmptyInput("Book Title to remove: ");
            if (title == null) return;

            var year = input.GetIntegerInput("Publication Year: ");
            if (!year.HasValue) return;

            try
            {
                bookService.RemoveBook(title, year.Value);
                console.WriteLine("Book removed successfully!");
            }
            catch (Exception ex)
            {
                console.WriteLine($"Failed to remove book: {ex.Message}");
            }
        }

        private void HandleBorrowBook(Member member)
        {
            if (!member.CanBorrowBooks())
            {
                console.WriteLine("Access denied. You cannot borrow books.");
                return;
            }

            var title = input.GetNonEmptyInput("Book Title to borrow: ");
            if (title == null) return;

            var year = input.GetIntegerInput("Publication Year: ");
            if (!year.HasValue) return;

            try
            {
                borrowingService.BorrowBook(title, year.Value, member.MemberID);
                console.WriteLine("Book borrowed successfully!");
            }
            catch (Exception ex)
            {
                console.WriteLine($"Failed to borrow book: {ex.Message}");
            }
        }

        private void HandleReturnBook(Member member)
        {
            var title = input.GetNonEmptyInput("Book Title to return: ");
            if (title == null) return;

            var year = input.GetIntegerInput("Publication Year: ");
            if (!year.HasValue) return;

            try
            {
                borrowingService.ReturnBook(title, year.Value, member.MemberID);
                console.WriteLine("Book returned successfully!");
            }
            catch (Exception ex)
            {
                console.WriteLine($"Failed to return book: {ex.Message}");
            }
        }

        private void HandleViewBooks(Member member)
        {
            if (!member.CanViewBooks())
            {
                console.WriteLine("Access denied. You cannot view books.");
                return;
            }

            bookService.DisplayBooks();
        }

        private void HandleViewMembers(Member member)
        {
            if (!member.CanViewMembers())
            {
                console.WriteLine("Access denied. You cannot view members.");
                return;
            }

            memberService.DisplayMembers();
        }
    }
}
