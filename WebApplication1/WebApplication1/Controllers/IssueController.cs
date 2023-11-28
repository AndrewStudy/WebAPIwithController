using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("prideauto.api/[controller]")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        private readonly IssueContext _context;

        public IssueController(IssueContext context) => _context = context;

        [HttpGet]
        public async Task<IEnumerable<Issue>> GetIssues()
            => await _context.Issues.ToListAsync();

        [HttpGet("id")]
        [ProducesResponseType(typeof(Issue), StatusCodes.Status200OK)]
        [ProducesResponseType( StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var issue = await _context.Issues.FindAsync(id);
            return issue == null ? NotFound() : Ok(issue);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Issue), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(Issue issue)
        {
            await _context.Issues.AddAsync(issue);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new {id = issue.Id} , issue);
        }

        [HttpPut("{id}")]
        [ProducesResponseType( StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Issue), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update(int id, Issue issue)
        {
            if (id != issue.Id) return BadRequest();

            _context.Entry(issue).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Issue), StatusCodes.Status204NoContent)]

        public async Task<IActionResult> Delete(int id)
        {
            var issueToDelete = await _context.Issues.FindAsync(id);
            if (id != issueToDelete.Id) return NotFound();

            _context.Issues.Remove(issueToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
