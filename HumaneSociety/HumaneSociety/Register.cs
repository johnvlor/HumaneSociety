using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    class Register
    {
        private DataClassesDataContext db;

        public Register()
        {
            db = new DataClassesDataContext();
        }

        public void CollectAdoptionFee(Adopter newAdopters, Animal newAnimals)
        {
            var animals =
                from a in db.Animals
                join c in db.Adopters on a.Adopter_Id equals c.Adopter_Id
                where (a.Animal_Id == newAnimals.Animal_Id && c.Adopter_Id == newAdopters.Adopter_Id)
                select a;

            if (!animals.Any())
            {
                Console.WriteLine("This record does not exist.");
            }
            else
            {
                foreach (var a in animals)
                {
                    a.Adopter.Paid_Amount = a.AdoptionFee.Adoption_Fee;
                    Console.WriteLine("Adoption Fee Paid: ${0}", a.Adopter.Paid_Amount);
                }
            }
            db.SubmitChanges();
        }
    }
}
