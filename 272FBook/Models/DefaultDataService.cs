using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace RandomCourseFBook.Models
{
    public class DefaultDataService
    {
        private String ConnectionString;
        public static List<MarkRange> markRanges = null;

        public DefaultDataService() {
            ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            markRanges = new List<MarkRange>();
            markRanges.Add(new MarkRange { Symbol = "F", MinOfRange = 0, MaxOfRange = 59 });
            markRanges.Add(new MarkRange { Symbol = "D", MinOfRange = 60, MaxOfRange = 64 });
            markRanges.Add(new MarkRange { Symbol = "D+", MinOfRange = 65, MaxOfRange = 69 });
            markRanges.Add(new MarkRange { Symbol = "C", MinOfRange = 70, MaxOfRange = 74 });
            markRanges.Add(new MarkRange { Symbol = "C+", MinOfRange = 75, MaxOfRange = 79 });
            markRanges.Add(new MarkRange { Symbol = "B", MinOfRange = 80, MaxOfRange = 84 });
            markRanges.Add(new MarkRange { Symbol = "B+", MinOfRange = 85, MaxOfRange = 89 });
            markRanges.Add(new MarkRange { Symbol = "A", MinOfRange = 90, MaxOfRange = 94 });
            markRanges.Add(new MarkRange { Symbol = "A+", MinOfRange = 95, MaxOfRange = 100 });
        }

        public List<Student> getAllStudentsByMarkRange(int min, int max)
        {
            List<Student> students = new List<Student>();
            //TODO: get all students whose grade falls between min and max (inclusive)
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT ID, FirstName, Surname, Sex, Grade FROM Students WHERE Grade >= @min AND Grade <= @max";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@min", min);
                    command.Parameters.AddWithValue("@max", max);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Student student = new Student
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                FirstName = reader["FirstName"].ToString(),
                                Surname = reader["LastName"].ToString(),
                                Sex = reader["Gender"].ToString(),
                                Grade = Convert.ToInt32(reader["Grade"])
                            };
                            students.Add(student);
                        }
                    }
                }
            }
                return students;
        }


        public List<Student> getAllStudentsBySexAndMarkRange(String sex, int min, int max)
        {
            List<Student> students = new List<Student>();
            //TODO: get all students whose grade falls between min and max (inclusive) and whose recorded sex matches the method's first argument
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT ID, FirstName, LastName, Gender, Grade FROM Students WHERE Gender = @sex AND Grade >= @min AND Grade <= @max";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@sex", sex);
                    command.Parameters.AddWithValue("@min", min);
                    command.Parameters.AddWithValue("@max", max);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Student student = new Student
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                FirstName = reader["FirstName"].ToString(),
                                Surname = reader["LastName"].ToString(),
                                Sex = reader["Gender"].ToString(),
                                Grade = Convert.ToInt32(reader["Grade"])
                            };
                            students.Add(student);
                        }
                    }
                }
            }
            return students;
        }

        public List<Image> getAllImages() {
            List<Image> images = new List<Image>();
            //TODO: Retrieve all the information associated with the images
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT ID, StudentID, ImageData FROM Images";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Image image = new Image
                            {
                                StudentID = Convert.ToInt32(reader["StudentID"]),
                                ImageRaw = reader["ImageData"].ToString()
                            };
                            images.Add(image);
                        }
                    }
                }
            }

            return images;
        }
    }
}