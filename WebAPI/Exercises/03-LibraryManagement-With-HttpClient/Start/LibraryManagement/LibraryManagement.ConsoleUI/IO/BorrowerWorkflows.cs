using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Services;

namespace LibraryManagement.ConsoleUI.IO
{
    public static class BorrowerWorkflows
    {
        public static void GetAllBorrowers(IBorrowerService service)
        {
            Console.Clear();
            Console.WriteLine("Borrower List");
            Console.WriteLine($"{"ID",-5} {"Name",-32} Email");
            Console.WriteLine(new string('=', 70));
            var result = service.GetAllBorrowers();

            if (result.Ok)
            {
                foreach (var b in result.Data)
                {
                    Console.WriteLine($"{b.BorrowerID,-5} {b.LastName + ", " + b.FirstName,-32} {b.Email}");
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }

            Utilities.AnyKey();
        }

        public static void GetBorrower(IBorrowerService service)
        {
            Console.Clear();
            var email = Utilities.GetRequiredString("Enter borrower email: ");
            var result = service.GetBorrower(email);

            if(result.Ok && result.Data != null)
            {
                Console.WriteLine("\nBorrower Information");
                Console.WriteLine("====================");
                Console.WriteLine($"Id: {result.Data.BorrowerID}");
                Console.WriteLine($"Name: {result.Data.LastName}, {result.Data.FirstName}");
                Console.WriteLine($"Email: {result.Data.Email}");
                Console.WriteLine($"Phone: {result.Data.Phone}");


                if (result.Data.CheckoutLogs.Any(c => c.ReturnDate == null))
                {
                    Console.WriteLine("\nChecked Out Items");
                    Console.WriteLine("=================");

                    Console.WriteLine($"{"Title",-40} {"Type",-10} {"Checkout Date",-15} {"Due Date"}");
                    foreach(var item in result.Data.CheckoutLogs.Where(c=>c.ReturnDate == null))
                    {
                        Console.WriteLine($"{item.Media.Title,-40} {item.Media.MediaType.MediaTypeName,-10} {item.CheckoutDate.ToShortDateString(),-15} {item.DueDate.ToShortDateString()}");
                    }
                }
                else
                {
                    Console.WriteLine("\nNo items checked out.");
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }

            Utilities.AnyKey();
        }

        public static void AddBorrower(IBorrowerService service)
        {
            Console.Clear();
            Console.WriteLine("Add New Borrower");
            Console.WriteLine("====================");

            Borrower newBorrower = new Borrower();

            newBorrower.FirstName = Utilities.GetRequiredString("First Name: ");
            newBorrower.LastName = Utilities.GetRequiredString("Last Name: ");
            newBorrower.Email = Utilities.GetRequiredString("Email: ");
            newBorrower.Phone = Utilities.GetRequiredString("Phone: ");

            var result = service.AddBorrower(newBorrower);

            if(result.Ok)
            {
                Console.WriteLine($"Borrower created with id: {newBorrower.BorrowerID}");
            }
            else
            {
                Console.WriteLine(result.Message);
            }

            Utilities.AnyKey();
        }

        public static void EditBorrower(IBorrowerService service)
        {
            string email;
            Result<Borrower> editResult;

            do
            {
                email = Utilities.GetRequiredString("Enter borrower email: ");
                editResult = service.GetBorrower(email);

                if(editResult.Ok)
                {
                    break;
                }
                Console.WriteLine("Could not find borrower with that email!");
            } while (true);

            Console.WriteLine("\nEdit Borrower (press enter to keep original value)");

            editResult.Data.FirstName = Utilities.GetEditedString($"First Name ({editResult.Data.FirstName}): ", editResult.Data.FirstName);
            editResult.Data.LastName = Utilities.GetEditedString($"Last Name ({editResult.Data.LastName}): ", editResult.Data.LastName);
            editResult.Data.Email = Utilities.GetEditedString($"Email ({editResult.Data.Email}): ", editResult.Data.Email);
            editResult.Data.Phone = Utilities.GetEditedString($"Phone ({editResult.Data.Phone}): ", editResult.Data.Phone);

            var result = service.EditBorrower(editResult.Data);


            if(result.Ok)
            {
                Console.WriteLine("Borrower updated!");
            }
            else
            {
                Console.WriteLine(result.Message);
            }

            Utilities.AnyKey();
        }

        public static void DeleteBorrower(IBorrowerService service)
        {
            string email;
            Result<Borrower> deleteResult;

            do
            {
                email = Utilities.GetRequiredString("Enter borrower email: ");
                deleteResult = service.GetBorrower(email);

                if (deleteResult.Ok)
                {
                    break;
                }
                Console.WriteLine("Could not find borrower with that email!");
            } while (true);

            do
            {
                Console.Write($"Are you sure you want to delete {deleteResult.Data.LastName}, {deleteResult.Data.FirstName} (Y/N)? ");
                string input = Console.ReadLine().ToUpper();

                if(input == "N")
                {
                    return;
                }
                else if (input == "Y")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input, please enter Y or N!");
                    Utilities.AnyKey();
                }
            } while (true);

            var result = service.DeleteBorrower(deleteResult.Data);

            if (result.Ok)
            {
                Console.WriteLine("Borrower deleted!");
            }
            else
            {
                Console.WriteLine(result.Message);
            }

            Utilities.AnyKey();
        }
    }
}
