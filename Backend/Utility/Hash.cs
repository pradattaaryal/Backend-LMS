﻿using System;
using System.Security.Cryptography;
using System.Text;

namespace Presentation.Utility
{
    public static class HashUtility
    {
        public static string Hash(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
