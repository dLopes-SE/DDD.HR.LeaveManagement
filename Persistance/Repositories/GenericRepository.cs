﻿using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistance.DataBaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
  public class GenericRepository<T>(HrDbContext context) : IGenericRepository<T> where T : class
  {
    protected readonly HrDbContext _context = context;

    public async Task CreateAsync(T entity)
    {
      await _context.AddAsync(entity);
      await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
      _context.Remove(entity);
      await _context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync()
    {
      return await _context.Set<T>().ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
      return await _context.Set<T>().FindAsync(id);
    }

    public async Task UpdateAsync(T entity)
    {
      _context.Entry(entity).State = EntityState.Modified;
      await _context.SaveChangesAsync();
    }
  }
}