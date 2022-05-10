using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup
{
    public static class Actors
    {
        public static void AddActors(this MovieStoreDbContext context)
        {
             context.Actors.AddRange(
                     new Actor
                     {
                          Name = "Elisabeth",
                          Surname = "Moss"
                          
                     },
                     new Actor
                     {
                          Name = "Madeline",   
                          Surname = "Brewer"
                     },
                     new Actor
                     {
                          Name = "Mala",
                          Surname = "Emde"
                     },
                     new Actor
                     {
                           Name = "Frances",
                           Surname = "McDormand"
                     },
                     new Actor
                     {
                          Name = "Jamie",
                          Surname = "Foxx"
                     },
                     new Actor
                     {
                          Name = "Anton",
                          Surname = "Spieker"
                     },
                     new Actor
                     {
                        Name = "Linda",
                        Surname = "May"
                     }
                );
        }     
    }
}