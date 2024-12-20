﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
    }
    public class AuthorDto
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
    }
}
