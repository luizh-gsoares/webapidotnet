using APICatalogo.Infra;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public CategoriasController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    
    [HttpGet("produtos")]
    public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
    {
        return _context.Categorias.Include(p => p.Produtos).ToList();
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<Categoria>> Get()
    {
        return _context.Categorias.ToList();
    }
    
    [HttpGet("{id::int}", Name="ObterCategoria")]
    public ActionResult<Categoria> Get(int id)
    {
        var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);
        if (categoria is null)
            return NotFound("Categoria não encontrada.");

        return categoria;
    }
    
    [HttpPost]
    public ActionResult Post(Categoria categoria)
    {
        if (categoria is null)
            return BadRequest("Houve um erro ao salvar o produto.");
            
        _context.Categorias.Add(categoria);
        _context.SaveChanges();

        return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria);
    }
    
    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Categoria categoria)
    {
        if (id != categoria.CategoriaId)
        {
            return BadRequest();
        }

        _context.Entry(categoria).State = EntityState.Modified;
        _context.SaveChanges();

        return Ok(categoria);
    }
    
    
    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);

        if (categoria == null)
            return NotFound("Categoria não localizado.");
        
        _context.Categorias.Remove(categoria);
        _context.SaveChanges();

        return Ok(categoria);
    }
    
}