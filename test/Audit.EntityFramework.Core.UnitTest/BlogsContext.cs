﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Audit.EntityFramework.Core.UnitTest
{
    public class BlogsContext : AuditDbContext
    {
        public const string CnnString = "data source=localhost;initial catalog=Blogs;integrated security=true;";

        public override bool AuditDisabled { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(CnnString);
            optionsBuilder.EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        public DbSet<BlogAudit> BlogsAudits { get; set; }
        public DbSet<PostAudit> PostsAudits { get; set; }
    }

    public abstract class BaseEntity
    {
        public virtual int Id { get; set; }
    }
    public interface IAuditEntity
    {
        string AuditAction { get; set; }
        DateTime AuditDate { get; set; }
        string AuditUser { get; set; }
        string Exception { get; set; }
    }
    public class Blog : BaseEntity
    {
        public override int Id { get; set; }
        [MaxLength(25)]
        public string Title { get; set; }
        public string BloggerName { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
    public class Post : BaseEntity
    {
        public override int Id { get; set; }
        [MaxLength(20)]
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }
        public string Content { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }

    public class PostAudit : IAuditEntity
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }
        public string Content { get; set; }
        public int BlogId { get; set; }

        [Key]
        public int AuditId { get; set; }
        public string AuditAction { get; set; }
        public DateTime AuditDate { get; set; }
        public string AuditUser { get; set; }
        public string Exception { get; set; }
    }
    public class BlogAudit : IAuditEntity
    {
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string BloggerName { get; set; }
        public virtual ICollection<Post> Posts { get; set; }

        [Key]
        public int AuditId { get; set; }
        public string AuditAction { get; set; }
        public DateTime AuditDate { get; set; }
        public string AuditUser { get; set; }
        public string Exception { get; set; }

    }

}