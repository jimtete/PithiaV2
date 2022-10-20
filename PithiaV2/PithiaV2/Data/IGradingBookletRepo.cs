using PithiaV2.Models;

namespace PithiaV2.Data;

public interface IGradingBookletRepo
{
    Task SaveChanges();

    Task<GradingBooklet> GetBookletById(int id);
    Task<List<GradingBooklet>> GetAllBooklets();
    Task CreateBooklet(GradingBooklet gradingBooklet);
    Task<GradingBooklet> GetBookletByUserId(int uid);

    void DeleteBooklet(GradingBooklet gradingBooklet);


}