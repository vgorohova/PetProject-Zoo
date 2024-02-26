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

    public IEnumerable<AnimalClass> GetAllAnimalClasses()
    {
        return _context.AnimalClasses
                .AsNoTracking()
                .OrderBy(ac => ac.ClassTitle)
                .ToArray();
    }
    public IEnumerable<Animal> GetAllAnimals()
    {
        return _context.Animals
                .Include(a => a.Classification)
                .AsNoTracking()
                .OrderByDescending(a => a.Id)
                .ToArray();
    }

    public Animal? GetAnimal(int id)
    {
        return _context.Animals
                .AsNoTracking()
                .Include(a => a.Classification)
                .SingleOrDefault(a => a.Id == id);
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

    public void DeleteAnimal(int id)
    {
        var animal = _context.Animals.Find(id);
        if (animal != null)
        {
            _context.Animals.Remove(animal);
            _context.SaveChanges();
        }
    }
}