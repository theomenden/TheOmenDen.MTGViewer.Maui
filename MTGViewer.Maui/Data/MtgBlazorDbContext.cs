using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MTGViewer.Maui.Data.Scryfall.Models;

namespace MTGViewer.Maui.Data;
public sealed class MtgBlazorDbContext: DbContext
{
    public DbSet<ScryfallCard> ScryfallCards { get; set; } 

    public MtgBlazorDbContext(DbContextOptions<MtgBlazorDbContext> options)
    :base(options)
    {
        SQLitePCL.Batteries_V2.Init();
    }
}
