﻿using WebApplication2.Context;
using WebApplication2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebApplication2.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class TarefaControllers : ControllerBase
	{

		private readonly organizadorContext _context;
		public TarefaControllers(organizadorContext context)
		{
			_context = context;
		}

		[HttpGet("{id}")]
		public IActionResult ObterPorId(int id)
		{
			// TODO: Buscar o Id no banco utilizando o EF
			// TODO: Validar o tipo de retorno. Se não encontrar a tarefa, retornar NotFound,
			// caso contrário retornar OK com a tarefa encontrada
			var tarefa = _context.Tarefas.Find(id);
			if (tarefa == null)
				return NotFound();
			
			return Ok(tarefa);
		}

		[HttpGet("ObterTodos")]
		public IActionResult ObterTodos()
		{
			var tarefa = _context.Tarefas.ToList();

			if (tarefa == null)
				return NotFound();

			
			return Ok(tarefa);
			// TODO: Buscar todas as tarefas no banco utilizando o EF
		}

		[HttpGet("ObterPorTitulo")]
		public IActionResult ObterPorTitulo(string titulo)
		{
			// TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o titulo recebido por parâmetro
			// Dica: Usar como exemplo o endpoint ObterPorData
			var tarefa = _context.Tarefas.Where(x => x.Titulo == titulo);
			if (tarefa == null)
				return NotFound();

			return Ok(tarefa);
		}

		[HttpGet("ObterPorData")]
		public IActionResult ObterPorData(DateTime data)
		{
			var tarefa = _context.Tarefas.Where(x => x.Data.Date == data.Date);
			return Ok(tarefa);
		}

		[HttpGet("ObterPorStatus")]
		public IActionResult ObterPorStatus(EnumStatusTarefa status)
		{
			// TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o status recebido por parâmetro
			// Dica: Usar como exemplo o endpoint ObterPorData
			var tarefa = _context.Tarefas.Where(x => x.Status == status);
			return Ok(tarefa);
		}

		[HttpPost]
		public IActionResult Criar(Tarefa tarefa)
		{
			if (tarefa.Data == DateTime.MinValue)
				return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

			_context.Add(tarefa);
			_context.SaveChanges();
			// TODO: Adicionar a tarefa recebida no EF e salvar as mudanças (save changes)
			return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
		}

		[HttpPut("{id}")]
		public IActionResult Atualizar(int id, Tarefa tarefa)
		{
			var tarefaBanco = _context.Tarefas.Find(id);

			if (tarefaBanco == null)
				return NotFound();

			if (tarefa.Data == DateTime.MinValue)
				return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });
			
				tarefaBanco.Titulo = tarefa.Titulo;
				tarefaBanco.Descricao = tarefa.Descricao;
				tarefaBanco.Data = tarefa.Data;
				tarefaBanco.Status = tarefa.Status;


			_context.SaveChanges();
			_context.Tarefas.Update(tarefaBanco);
			
			// TODO: Atualizar as informações da variável tarefaBanco com a tarefa recebida via parâmetro
			// TODO: Atualizar a variável tarefaBanco no EF e salvar as mudanças (save changes)
			return Ok(tarefa);	
		}

		[HttpDelete("{id}")]
		public IActionResult Deletar(int id)
		{
			var tarefaBanco = _context.Tarefas.Find(id);

			if (tarefaBanco == null)
				return NotFound();

			_context.Tarefas.Remove(tarefaBanco);
			_context.SaveChanges();
			// TODO: Remover a tarefa encontrada através do EF e salvar as mudanças (save changes)
			return NoContent();
		}
	}
}
