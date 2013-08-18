using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GenericDatatables.Core.Domain.Models;

namespace GenericDatatables.Default.Database
{
    public class GymInitializer : DropCreateDatabaseIfModelChanges<GymContext>
    {
        private static readonly string[] FirstNames =
        {
            "Aaron", "Abdul", "Abe", "Abel", "Abraham", "Adam", "Adan", "Adolfo", "Adolph", "Adrian", "Abby", "Abigail",
            "Adele", "Adrian", "Alex", "Jeff", "John", "Jack", "Jim", "Sergio", "Daniel", "Carolina", "David", "Reina",
            "Saul", "Bernard", "Danny", "Dimas", "Yuri", "Ivan", "Laura", "Zachary","Ferdinand","Blair","Fletcher","Amado",
            "Mark","Rene","Jarrod","Robbie","Elliot","Randell","Jon","Ignacio","Davis","Millard","Omer","Guadalupe","Edwardo",
            "Linwood","Edward","Harvey","Claude","Harris","Desmond","Elton","Malcolm","Gerald","Lauren","Miles","Zane","Ross",
            "Boris","Gustavo","Freddie","Zackary","Tommy","Forrest","Ron","Aubrey","Steve","Will","Travis","Delmer","Adam",
            "Chet","Jackie","Kendrick","Lamar"
        };

        private static readonly string[] LastNames =
        {
            "Abbott", "Acosta", "Adams", "Adkins", "Aguilar", "Tapia", "Gutierrez", "Rueda", "Galviz", "Yuli", "Rivera",
            "Mamami", "Saucedo", "Dominguez", "Escobar", "Martin", "Crespo", "Parodi", "Schick", "Demaio", "Paredez",
            "Sim", "Collar", "Merlino","Mayweather", "Scheiber", "Suman", "Coulombe", "Greiner", "Lebrun", "Dunphy", 
            "Reding", "Vanvliet","Bellows", "Selke","Wessels", "Wilbur", "Pineo", "Lasky", "Engle", "Araiza", "Frates", 
            "Hirsh", "Than", "Charters", "Pille","Grinnell", "Rahman", "Puett","Ayoub", "Delong", "Oswalt", "Celis", "Rodman", 
            "Riggs", "Gash", "Rodney", "States", "Jaquez", "Hartwell","Mccrea", "Starcher", "Hagler", "Lasso", "Brossard",
            "Deibler", "Casados"
        };

        private static readonly string[] ExerciseFirstNames =
        {
            "Ab", "Air", "Alternate Leg", "Alternate Incline", "Alternate Leg", "Alternating Cable", 
            "Arm", "Arnold", "Atlas Stone", "Glute", "Glute Ham", "Groin and back", "Gironda", "Olympic", "On your side",
            "One Arm", "One Half", "One Handed", "One Knee", "One Leg", "One-Arm Kettlebell", "One-Arm Open Palm", "One-Arm Overhead", "Overhead",
            "Upright", "Upward", "Upper back", "Front Box", "Front Cable", "Front Cone", "Front Dumbbell", "Front Incline"
        };

        private static readonly string[] ExerciseLastNames =
        {
            "Chins", "Kickback", "Raise", "Stretch", "Crunch", "Machine", "Windmill", "Bike", "Curl", "Touchers",
            "Bound", "Press",
            "Raise", "Row", "Clean", "Trainer", "Stones", "Squat", "Chin-up", "Pulldown", "Triceps extension",
            "Quad Stretch", "Against Wall", "Bench Press", "Dumbbell Preacher Curl",
            "Floor Press", "Lat Pulldown", "Pronated Dumbbell Triceps Extensions", "Locust", "To Chest", "Barbell Squat",
            "Dumbbell Row", "Flat Bench Dumbbell Flye", "High-Pulley Cable Side Bends",
            "Incline Lateral Raise", "Para Press", "Push Press", "Snatch", "Split Jerk", "Split Snatch", "Swings",
            "Bar Row",
            "Kettlebell Clean", "Kettlebell Squats", "Side Laterals", "Deadlift", "Cable Kickback", "Triceps",
            "Back-leg grab"
        };

        protected override void Seed(GymContext context)
        {
            context.Configuration.AutoDetectChangesEnabled = false;
            context.Configuration.ValidateOnSaveEnabled = false;

            int count = 0;
            var gymMembers = GetGymMembers().ToList();
            foreach (var gymMember in gymMembers)
            {
                count ++;
                context.GymMembers.Add(gymMember);
                // recreate the context every 50 records
                if (count%100 == 0)
                {
                    context.SaveChanges();
                    context.Dispose();
                    context = new GymContext();
                    context.Configuration.AutoDetectChangesEnabled = false;
                    context.Configuration.ValidateOnSaveEnabled = false;
                }
            }
            context.SaveChanges();
        }

        private IEnumerable<GymMember> GetGymMembers()
        {
            int minBirthYear    = DateTime.Now.Year - 80;
            int maxBirthYear    = DateTime.Now.Year - 15;
            var random          = new Random();

            foreach (var number in Enumerable.Range(0, 5000))
            {
                int birthYear       = random.Next(minBirthYear, maxBirthYear);
                int birthMonth      = random.Next(1, 13);
                int birthDay        = random.Next(1, DateTime.DaysInMonth(birthYear, birthMonth));
                string firstName    = FirstNames[random.Next(FirstNames.Length)];
                string lastName     = LastNames[random.Next(LastNames.Length)];

                yield return new GymMember
                {
                    AverageExerciseTime     = TimeSpan.FromMinutes(random.NextDouble()*60),
                    FirstName               = firstName,
                    LastName                = lastName,
                    DateOfBirth             = new DateTime(birthYear, birthMonth, birthDay),
                    Weight                  = random.Next(60, 121) + random.NextDouble(),
                    MembershipPrice         = Convert.ToDecimal(random.Next(5, 26) + random.NextDouble()),
                    Excercises              = GetExercises(random).ToList(),
                    CreationDate            = DateTime.Now,
                    CreationUserId          = 0,
                    ModificationDate        = DateTime.Now,
                    ModificationUserId      = 0
                };
            }
        }

        private IEnumerable<Exercise> GetExercises(Random random)
        {
            return Enumerable.Range(0, random.Next(5))
                .Select(number => string.Format("{0} {1}", ExerciseFirstNames[random.Next(ExerciseFirstNames.Length)],ExerciseLastNames[random.Next(ExerciseLastNames.Length)]))
                .Select(name => new Exercise
                {
                    Duration            = new TimeSpan(random.Next(2), random.Next(60), random.Next(60)),
                    Name                = name,
                    CreationDate        = DateTime.Now,
                    CreationUserId      = 0,
                    ModificationDate    = DateTime.Now,
                    ModificationUserId  = 0
                });
        }
    }
}