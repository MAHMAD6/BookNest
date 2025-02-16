using BookNest.Models;
using BookNest.ViewModels.Components;
using BookNest.Views.Login;
using BookNest.Views.Admin;
using BookNest.Views.User;

namespace BookNest
{
    public partial class App : Application
    {
        public static BaseRepository<User> UserRepo { get; set; } = new();
        public static BaseRepository<Book> BooksRepo { get; set; } = new();
        public static BaseRepository<Author> AuthorsRepo { get; set; } = new();
        public static BaseRepository<Category> CategoriesRepo { get; set; } = new();
        public static BaseRepository<Transaction> TransactionsRepo { get; set; } = new();
        public static BaseRepository<Fine> FinesRepo { get; set; } = new();
        
        public static User? CurrentUser { get; set; }     
        public static User? CurrentAdmin { get; set; }


        public App(BaseRepository<User> userRepo, BaseRepository<Book> booksRepo, BaseRepository<Author> authorsRepo,
            BaseRepository<Category> categoriesRepo, BaseRepository<Transaction> transactionsRepo, BaseRepository<Fine> finesRepo
            )
        {
            InitializeComponent();
            UserRepo = userRepo;
            BooksRepo = booksRepo;
            AuthorsRepo = authorsRepo;
            CategoriesRepo = categoriesRepo;
            FinesRepo =  finesRepo;
            MainPage = new AppShell();
        }
    }
}
