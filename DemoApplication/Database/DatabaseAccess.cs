using System;
using System.Collections.Generic;
using System.Text;

namespace DemoApplication
{
    public class DatabaseAccess
    {
        public string PATH_ACCOUNTS = string.Empty;
        public string PATH_PROFILE  = string.Empty;

        public Account GetAccount(int index)
        {
            Account account = null;
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(PATH_ACCOUNTS))
            {
                conn.CreateTable<Account>();
                account = conn.Find<Account>(index);
            }
            return account;
        }

        public List<Account> GetAccounts()
        {
            List<Account> accounts = new List<Account>();
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(PATH_ACCOUNTS))
            {
                conn.CreateTable<Account>();
                accounts = conn.Table<Account>().ToList();
            }
            return accounts;
        }

        public bool UpdateAccount(Account account)
        {
            bool result = false;
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(PATH_ACCOUNTS))
            {
                conn.CreateTable<Account>();
                result = (1 == conn.Update(account));
            }
            return result;
        }

        public bool Add(ref Account account)
        {
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(PATH_ACCOUNTS))
            {
                conn.CreateTable<Account>();
                var numberOfRows = conn.Insert(account);
                return (numberOfRows > 0);
            }
        }
        public bool Delete(Account account)
        {
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(PATH_ACCOUNTS))
            {
                conn.CreateTable<Account>();
                var numberOfRows = conn.Delete<Account>(account);
                return (numberOfRows > 0);
            }
        }
        public bool Delete(int index)
        {
            try
            {
                var items = GetAccounts();
                using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(PATH_ACCOUNTS))
                {
                    conn.CreateTable<Account>();
                    var numberOfRows = conn.Delete<Account>(index);
                    return (numberOfRows > 0);
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void DeleteAll()
        {
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(PATH_ACCOUNTS))
            {
                conn.DropTable<Account>();
            }
        }
    }
}
