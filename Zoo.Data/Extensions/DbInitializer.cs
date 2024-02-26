using Npgsql.Replication;
using Zoo.Data.Models;

namespace Zoo.Data;

public static class DbInitializer
{
    public static void Initialize(ZooContext context)
    {
        if (context.Animals.Any()){
            return;
        }

        var tiger = new AnimalClass{ ClassTitle = "Tiger" };
        var pig = new AnimalClass { ClassTitle = "Pig" };
        var owl = new AnimalClass { ClassTitle = "Owl" };
        var frog = new AnimalClass {  ClassTitle = "Frog" };

        var animals = new Animal[] {
            new() {
                NickName = "TigerRRR",
                Classification = tiger,
                Title = "Bengal Tiger",
                Sound = "RRRR",
                Photo = "tiger1.jpg"
            },
            new() {
                NickName = "TigerMeau",
                Classification = tiger,
                Title = "Siberian Tiger",
                Sound = "RRRR",
                Photo = "tiger2.jpg"
            },
            new() {
                NickName = "PigHrHr",
                Classification = pig,
                Title = "Domestic Pig",
                Sound = "HrHr",
                Photo = "pig1.jpg"
            },
            new() {
                NickName = "Hedvig",
                Classification = owl,
                Title = "Snowy owl",
                Sound = "UhUh",
                Photo = "owl1.jpg"
            },
            new() {
                NickName = "FrogKvaKva",
                Classification = owl,
                Title = "Glass Frog",
                Sound = "KvaKva",
                Photo = "frog1.jpg"
            },
        };

        // context.AnimalClasses.AddRange([tiger, pig, owl, frog]);
        context.Animals.AddRange(animals);
        
        context.SaveChanges();
    }
}