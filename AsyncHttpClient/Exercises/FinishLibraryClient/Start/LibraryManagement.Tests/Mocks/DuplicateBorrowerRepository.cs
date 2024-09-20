using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Tests.Mocks
{
    public class DuplicateBorrowerRepository : IBorrowerRepository
    {
        public void Add(Borrower borrower)
        {
            throw new NotImplementedException();
        }

        public void Delete(Borrower borrower)
        {
            throw new NotImplementedException();
        }

        public List<Borrower> GetAll()
        {
            throw new NotImplementedException();
        }

        public Borrower? GetByEmail(string email)
        {
            return new Borrower
            {
                BorrowerID = 1,
                FirstName = "Test",
                LastName = "Borrower",
                Email = "test@example.com",
                Phone = "55555555555"
            };
        }

        public Borrower? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Borrower borrower)
        {
            throw new NotImplementedException();
        }
    }
}
