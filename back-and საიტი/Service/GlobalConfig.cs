using Models;
using Service.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class GlobalConfig
    {
        public static string ConnectionString { get; } = "Server=DESKTOP-34GOSNV\\SQLEXPRESS;Database=Music_Project;Trusted_Connection=True;TrustServerCertificate=True";
        public static IDatabaseConnection Database { get; set; }
        public static void ChooseDatabase(DatabaseType databaseType)
        {
            switch (databaseType)
            {
                case DatabaseType.Sql:
                    Database = new SqlServerDatabaseConnection();
                    break;
            }
        }
        public static void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                string salt = GenerateRandomSalt();
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password + salt);
                byte[] hash = sha256.ComputeHash(passwordBytes);

                passwordHash = Convert.ToBase64String(hash);
                passwordSalt = salt;
            }
        }

        public static string GenerateRandomSalt()
        {
            byte[] randomBytes = new byte[32];
            using (var range = new RNGCryptoServiceProvider())
            {
                range.GetBytes(randomBytes);
            }

            return Convert.ToBase64String(randomBytes);
        }

        public static bool VerifyPassword(string password, string passwordHash, string passwordSalt)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password + passwordSalt);
                byte[] hash = sha256.ComputeHash(passwordBytes);
                string computedHash = Convert.ToBase64String(hash);

                return passwordHash == computedHash;
            }
        }
    }
}
