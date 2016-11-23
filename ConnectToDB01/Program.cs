using System;
using System.Data;
using System.Data.SqlClient;

namespace ConnectToDB01
{

    class Program
    {

        private static string connectionString =
            "Server=ealdb1.eal.local; Database=ejl81_db; User Id=ejl81_usr; Password=Baz1nga81;";
        static void Main(string[] args)
        {

            Program program = new ConnectToDB01.Program();
            program.Run();
        }

        private void Run()
        {

            bool running = true;
            try
            {
                do
                {
                    int input = Menu();
                    switch (input)
                    {
                        case 1: InsertOwner(); break;
                        case 2: InsertPet(); break;
                        case 3: InsertBreed(); break;
                        case 4: GetAllOwners(); break;
                        case 5: GetAllPets(); break;
                        case 6: GetAllBreeds(); break;
                        case 7: SearchByLastName(); break;
                        case 8: SearchByEmailFirstName(); break;
                        case 9: OwnersPets(); break;
                        case 10: running = false; break;
                    }
                    Console.Clear();
                } while (running);
            }
            catch (SqlException e)
            {
                Console.WriteLine("UPS " + e.Message);
                Console.ReadKey();
            }

        }

        private int Menu()
        {

            Console.WriteLine("Commands:\n1) Insert new owner\n2) Insert new pet\n3) Insert new breed\n" +
                                          "4) Get all owners\n5) Get all pets\n6) Get all breeds\n7) Search owners by last name\n" +
                                          "8) Search owners by email and first name\n9) Get information about owners by ID\n" +
                                          "10) End program\n");
            Console.WriteLine("Please input your command");
            string input = Console.ReadLine();
            Console.Clear();
            int x = Convert.ToInt32(input);
            return x;
        }

        private void OwnersPets()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmdOwnersPets = new SqlCommand("GetInformation", con);
                cmdOwnersPets.CommandType = CommandType.StoredProcedure;
                string input = GetInput("OwnerID to search for");
                cmdOwnersPets.Parameters.Add(new SqlParameter("OwnerID", input));

                SqlDataReader reader = cmdOwnersPets.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string ownerName = reader["OwnerName"].ToString();
                        string petName = reader["PetName"].ToString();
                        string petType = reader["PetType"].ToString();
                        string petBreed = reader["PetBreed"].ToString();
                        string averageLifeExpectancy = reader["AverageLifeExpectancy"].ToString();
                        Console.WriteLine(ownerName + " " + petName + " " + petType + " " + petBreed + " " + averageLifeExpectancy);
                    }
                    Console.ReadKey();
                }
            }
        }

        private void SearchByEmailFirstName()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmdSearchByEmailFirstName = new SqlCommand("GetOwnersByEmail", con);
                cmdSearchByEmailFirstName.CommandType = CommandType.StoredProcedure;
                string input = GetInput("OwnerEmail to search for");
                cmdSearchByEmailFirstName.Parameters.Add(new SqlParameter("OwnerEmail", input));
                input = GetInput("OwnerFirstName to search for");
                cmdSearchByEmailFirstName.Parameters.Add(new SqlParameter("OwnerFirstName", input));

                SqlDataReader reader = cmdSearchByEmailFirstName.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string id = reader["OwnerID"].ToString();
                        string firstName = reader["OwnerFirstName"].ToString();
                        string lastName = reader["OwnerLastName"].ToString();
                        string phoneNumber = reader["OwnerPhoneNumber"].ToString();
                        string email = reader["OwnerEmail"].ToString();
                        Console.WriteLine(id + " " + firstName + " " + lastName + " " + phoneNumber + " " + email);
                    }
                    Console.ReadKey();
                }
            }
        }

        private void GetAllBreeds()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmdGetAllBreeds = new SqlCommand("GetBreeds", con);
                cmdGetAllBreeds.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmdGetAllBreeds.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string breedName = reader["BreedName"].ToString();
                        string minWeight = reader["MinWeight"].ToString();
                        string maxWeight = reader["MaxWeight"].ToString();
                        string averageLifeExpectancy = reader["AverageLifeExpectancy"].ToString();
                        Console.WriteLine(breedName + " " + minWeight + " " + maxWeight + " " + averageLifeExpectancy);
                    }
                    Console.ReadKey();
                }
            }
        }

        private void GetAllOwners()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmdGetAllOwners = new SqlCommand("GetOwners", con);
                cmdGetAllOwners.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmdGetAllOwners.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string id = reader["OwnerID"].ToString();
                        string OwnerLastName = reader["OwnerLastName"].ToString();
                        string OwnerFirstName = reader["OwnerFirstName"].ToString();
                        string OwnerPhoneNumber = reader["OwnerPhoneNumber"].ToString();
                        string OwnerEmail = reader["OwnerEmail"].ToString();
                        Console.WriteLine(id + " " + OwnerLastName + " " + OwnerFirstName + " " + OwnerPhoneNumber + " " + OwnerEmail);
                    }
                    Console.ReadKey();
                }
            }
        }

        private void GetAllPets()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmdGetAllPets = new SqlCommand("GetPets", con);
                cmdGetAllPets.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmdGetAllPets.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string id = reader["PetID"].ToString();
                        string petName = reader["PetName"].ToString();
                        string petType = reader["PetType"].ToString();
                        string petBreed = reader["PetBreed"].ToString();
                        string petDateOfBirth = reader["PetDateOfBirth"].ToString();
                        string petWeight = reader["PetWeight"].ToString();
                        string ownerID = reader["OwnerID"].ToString();
                        Console.WriteLine(id + " " + petName + " " + petType + " " + petBreed + " " + petDateOfBirth +
                                          " " + petWeight + " " + ownerID);
                    }
                    Console.ReadKey();
                }
            }
        }

        private void InsertBreed()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmdBreed = new SqlCommand("InsertBreed", con);
                cmdBreed.CommandType = CommandType.StoredProcedure;
                string input;
                input = GetInput("BreedName");
                cmdBreed.Parameters.Add(new SqlParameter("BreedName", input));
                input = GetInput("MinWeight");
                cmdBreed.Parameters.Add(new SqlParameter("MinWeight", input));
                input = GetInput("MaxWeight");
                cmdBreed.Parameters.Add(new SqlParameter("MaxWeight", input));
                input = GetInput("AverageLifeExpectancy");
                cmdBreed.Parameters.Add(new SqlParameter("AverageLifeExpectancy", input));

                cmdBreed.ExecuteNonQuery();
            }
            Console.WriteLine("Breed Added");
        }

        private void InsertPet()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmdPet = new SqlCommand("InsertPet", con);
                cmdPet.CommandType = CommandType.StoredProcedure;
                string input;
                input = GetInput("PetID");
                cmdPet.Parameters.Add(new SqlParameter("PetID", input));
                input = GetInput("PetName");
                cmdPet.Parameters.Add(new SqlParameter("PetName", input));
                input = GetInput("PetType");
                cmdPet.Parameters.Add(new SqlParameter("PetType", input));
                input = GetInput("PetBreed");
                cmdPet.Parameters.Add(new SqlParameter("PetBreed", input));
                input = GetInput("PetDateOfBirth");
                cmdPet.Parameters.Add(new SqlParameter("PetDateOfBirth", input));
                input = GetInput("PetWeight");
                cmdPet.Parameters.Add(new SqlParameter("PetWeight", input));
                input = GetInput("OwnerID");
                cmdPet.Parameters.Add(new SqlParameter("OwnerID", input));

                cmdPet.ExecuteNonQuery();
            }
            Console.WriteLine("Pet Added");
        }

        private void SearchByLastName()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmdSearchLastName = new SqlCommand("GetOwnersByLastName", con);
                cmdSearchLastName.CommandType = CommandType.StoredProcedure;
                string input = GetInput("OwnerLastName to search for");
                cmdSearchLastName.Parameters.Add(new SqlParameter("OwnerLastName", input));

                SqlDataReader reader = cmdSearchLastName.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string id = reader["OwnerID"].ToString();
                        string firstName = reader["OwnerFirstName"].ToString();
                        string lastName = reader["OwnerLastName"].ToString();
                        string phoneNumber = reader["OwnerPhoneNumber"].ToString();
                        string email = reader["OwnerEmail"].ToString();
                        Console.WriteLine(id + " " + firstName + " " + lastName + " " + phoneNumber + " " + email);
                        Console.ReadKey();
                    }
                }
            }
        }

        private void InsertOwner()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmdOwner = new SqlCommand("InsertOwner", con);
                cmdOwner.CommandType = CommandType.StoredProcedure;
                string input = GetInput("OwnerID");
                cmdOwner.Parameters.Add(new SqlParameter("OwnerID", input));
                input = GetInput("OwnerLastName");
                cmdOwner.Parameters.Add(new SqlParameter("OwnerLastName", input));
                input = GetInput("OwnerFirstName");
                cmdOwner.Parameters.Add(new SqlParameter("OwnerFirstName", input));
                input = GetInput("OwnerPhoneNumber");
                cmdOwner.Parameters.Add(new SqlParameter("OwnerPhoneNumber", input));
                input = GetInput("OwnerEmail");
                cmdOwner.Parameters.Add(new SqlParameter("OwnerEmail", input));

                cmdOwner.ExecuteNonQuery();
            }
            Console.WriteLine("Owner Added");
        }

        private string GetInput(string info)
        {
            Console.WriteLine("Please input the " + info);
            string input = Console.ReadLine();
            return input;
        }
    }
}