using PithiaV2.Models;

namespace PithiaV2.Data;

public interface IProfessorRepo
{
    Task SaveChanges();

    Task<Professor> GetProfessorById(int id);
    Task<List<Professor>> GetAllProfessors();
    Task CreateProfessor(Professor professor);

    void DeleteProfessor(Professor professor);

}