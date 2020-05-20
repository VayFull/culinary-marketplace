using Microsoft.EntityFrameworkCore;
using Npgsql;
using Subs.Domain.Entities;
using Subs.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Subs.Tests
{
    public class SharedDatabase:IDisposable
    {
        private static readonly object _lock = new object();
        private static bool _databaseInitialized;

        public SharedDatabase()
        {
            Connection = new NpgsqlConnection(@"Host=localhost;Port=5432;Database=subs;Username=postgres;Password=1234QWER+");
            Seed();
            Connection.Open();
        }

        public DbConnection Connection { get; }

        public SubsDbContext CreateContext(DbTransaction transaction = null)
        {
            var context = new SubsDbContext(new DbContextOptionsBuilder<SubsDbContext>().UseNpgsql(Connection).Options);

            if (transaction != null)
            {
                context.Database.UseTransaction(transaction);
            }

            return context;
        }

        private void Seed()
        {
            lock (_lock)
            {
                if (!_databaseInitialized)
                {
                    using (var context = CreateContext())
                    {
                        context.Database.EnsureDeleted();
                        context.Database.EnsureCreated();

                        var firstBook = new Book { Author = "Повар", FileName = "книга повара.txt", Id = 10, Name = "Книга повара" };
                        var secondBook = new Book { Author = "Повар2", FileName = "книга повара2.txt", Id = 100, Name = "Книга повара2" };

                        var key = new UserKey { Key = "somesuccesskey", Username = "someuser" };

                        context.AddRange(firstBook, secondBook, key);

                        context.SaveChanges();
                    }

                    _databaseInitialized = true;
                }
            }
        }

        public void Dispose() => Connection.Dispose();
    }
}
