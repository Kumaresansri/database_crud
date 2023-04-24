﻿using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Data
{
    public class ResumeDbContext:DbContext
    {
        public ResumeDbContext(DbContextOptions<ResumeDbContext> options) : base(options) 
        {
        }
        public virtual DbSet<Applicant> Applicants { get; set; }
        public virtual DbSet<Experience> Experiences { get; set; }
    }
}
