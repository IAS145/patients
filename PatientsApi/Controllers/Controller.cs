using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientsApi.Models;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly AppDbContext _context;

    public PatientsController(AppDbContext context)
    {
        _context = context;
    }

    // ✅ POST
    [HttpPost]
    public async Task<IActionResult> Create(CreatePatientDto dto)
    {
        var exists = await _context.Patients.AnyAsync(p =>
            p.DocumentType == dto.DocumentType &&
            p.DocumentNumber == dto.DocumentNumber);

        if (exists)
            return Conflict("Paciente ya existe.");

        var patient = new Patient
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            DocumentType = dto.DocumentType,
            DocumentNumber = dto.DocumentNumber,
            BirthDate = dto.BirthDate,
            Email = dto.Email
        };

        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = patient.Id }, patient);
    }

    // ✅ GET con paginación + filtros
    [HttpGet]
    public async Task<IActionResult> GetAll(
        int page = 1,
        int pageSize = 10,
        string? name = null,
        string? documentNumber = null)
    {
        var query = _context.Patients.AsQueryable();

        if (!string.IsNullOrEmpty(name))
            query = query.Where(p => p.Name.Contains(name));

        if (!string.IsNullOrEmpty(documentNumber))
            query = query.Where(p => p.DocumentNumber.Contains(documentNumber));

        var total = await query.CountAsync();

        var data = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return Ok(new { total, page, pageSize, data });
    }

    // ✅ GET por ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var patient = await _context.Patients.FindAsync(id);

        if (patient == null)
            return NotFound();

        return Ok(patient);
    }

    // ✅ PUT (actualización parcial)
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdatePatientDto dto)
    {
        var patient = await _context.Patients.FindAsync(id);

        if (patient == null)
            return NotFound();

        // Validar duplicado si cambia documento
        if (dto.DocumentType != null || dto.DocumentNumber != null)
        {
            var newType = dto.DocumentType ?? patient.DocumentType;
            var newNumber = dto.DocumentNumber ?? patient.DocumentNumber;

            var exists = await _context.Patients.AnyAsync(p =>
                p.Id != id &&
                p.DocumentType == newType &&
                p.DocumentNumber == newNumber);

            if (exists)
                return Conflict("Documento duplicado.");
        }

        if (dto.Name != null) patient.Name = dto.Name;
        if (dto.DocumentType != null) patient.DocumentType = dto.DocumentType;
        if (dto.DocumentNumber != null) patient.DocumentNumber = dto.DocumentNumber;
        if (dto.BirthDate.HasValue) patient.BirthDate = dto.BirthDate.Value;
        if (dto.Email != null) patient.Email = dto.Email;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // ✅ DELETE
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var patient = await _context.Patients.FindAsync(id);

        if (patient == null)
            return NotFound();

        _context.Patients.Remove(patient);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}