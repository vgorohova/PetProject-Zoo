using Microsoft.EntityFrameworkCore;
using Zoo.Data.Models;

namespace Zoo.Data;

public class ZooService
{
    private readonly ZooContext _context;
    public ZooService(ZooContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AnimalClass>> GetAllAnimalClasses()
    {
        return await _context.AnimalClasses
                .AsNoTracking()
                .OrderBy(ac => ac.ClassTitle)
                .ToListAsync();
    }
    public async Task<IEnumerable<Animal>> GetAllAnimals()
    {
        return await _context.Animals
                .Include(a => a.Classification)
                .AsNoTracking()
                .OrderByDescending(a => a.Id)
                .ToListAsync();
    }

    public async Task<Animal?> GetAnimal(int id)
    {
        return await _context.Animals
                .AsNoTracking()
                .Include(a => a.Classification)
                .SingleOrDefaultAsync(a => a.Id == id);
    }

    public async Task<IEnumerable<Animal>> GetAnimalByClass(string className) 
    {
        var cl = _context.AnimalClasses
                    .SingleOrDefault(a => a.ClassTitle.ToLower() == className);

        if (cl == null)
            return new List<Animal>();
        
        return await _context.Animals
                .AsNoTracking()
                .Include(a => a.Classification)
                .Where(a => a.Classification.Id == cl.Id)
                .OrderByDescending(a => a.Id)
                .ToListAsync();
    }

    public Animal AddAnimal(Animal animal)
    {
        _context.Animals.Add(animal);
        _context.SaveChanges();

        return animal;
    }

    public AnimalClass? GetAnimalClass(int id)
    {
        return  _context.AnimalClasses
                    .SingleOrDefault(a => a.Id == id);
    }

    public Animal UpdateAnimalClass(int id, int classId) 
    {
        Animal? animal = _context.Animals.Find(id);
        AnimalClass? animalClass = _context.AnimalClasses.Find(classId);

        if(animal == null || animalClass == null) 
        {
            throw new InvalidOperationException("Animal or Animal class could not be found");
        }

        animal.Classification = animalClass;
        
        _context.SaveChanges();
        
        return animal;
    }

    public bool DeleteAnimal(int id)
    {
        var animal = _context.Animals.Find(id);
        if (animal == null) return false;

        _context.Animals.Remove(animal);
        _context.SaveChanges();
        
        return true;
    }
}