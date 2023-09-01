using Livro.Data;
using Livro.Models;
using Microsoft.AspNetCore.Mvc;

namespace Livro.Controllers
{
    [ApiController]

    public class LivroControllers : ControllerBase
    {
        [HttpGet("/")]
        public IActionResult Get([FromServices]AppDbContext context)
        {
            return Ok(context.Livros.ToList());
        }

        [HttpGet("/{Id:int}")]
        public IActionResult GetById([FromServices]AppDbContext context,[FromRoute] int Id)
        {
            var livro = context.Livros.Find(Id);
            if (livro is null) return NotFound();

            return Ok(livro);
        }

        [HttpPost("/")]
        public IActionResult Post([FromServices]AppDbContext context,[FromBody] LivroMd UmLivro)
        {
            context.Livros.Add(UmLivro);
            context.SaveChanges();

            return Created($"/{UmLivro.Id}",UmLivro);
        }

         [HttpPut("/{id:int}")]
        public IActionResult put([FromServices]AppDbContext context,[FromRoute] int Id,[FromBody] LivroMd umLivro){
                  var livro = context.Livros.Find(Id);
                  if (livro is null) return NotFound();

                  livro.Titulo = umLivro.Titulo;
                  livro.Editora = umLivro.Editora;
                  livro.AnoPublic = umLivro.AnoPublic;
                  livro.Autor = umLivro.Autor;
                  livro.quantidade = umLivro.quantidade;

                  context.SaveChanges();
                  return Ok(livro);
        }

        [HttpDelete("/{Id:int}")]
        public IActionResult Delete([FromServices]AppDbContext context,[FromRoute] int Id){
             var livro = context.Livros.Find(Id);
                  if (livro is null) return NotFound();
            context.Livros.Remove(livro);
            context.SaveChanges();
            return Ok(livro);
        }

        [HttpGet("/{Editora:}")]
        public IActionResult GetByEditora([FromServices]AppDbContext context,[FromRoute] string Editora){
            var livro = context.Livros.Where(x => x.Editora == Editora);
                 if (livro is null) return NotFound();
            return Ok(livro);
        }

        [HttpPut("/Adicionar/{Id:int}")]
        public IActionResult Adicionar([FromServices]AppDbContext context,[FromRoute] int Id,[FromBody] LivroMd Quantidade)
        {
            var livro = context.Livros.Find(Id);
                if (livro is null) return NotFound();
            livro.quantidade += Quantidade.quantidade;
            context.SaveChanges();
            return Ok(livro.quantidade);
        }
        
        [HttpPut("/Retirar/{Id:int}")]
        public IActionResult Retirar([FromServices]AppDbContext context,[FromRoute] int Id,[FromBody] LivroMd Quantidade)
        {
            var livro = context.Livros.Find(Id);
                if (livro is null) return NotFound();
            livro.quantidade -= Quantidade.quantidade;
            context.SaveChanges();
            return Ok(livro.quantidade);
        }
    }
}