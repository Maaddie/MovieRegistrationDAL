using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MySqlConnector;

namespace MovieRegistrationDAL.Models
{
    public class MovieDAL
    {
        public List<Movie> GetMovies()
        {
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                string sql = "select * from movies";

                connect.Open();

                List<Movie> movie = connect.Query<Movie>(sql).ToList();

                connect.Close();

                return movie;
            }
        }
        public Movie GetMovie(int id)
        {
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                string sql = "select * from movies where ID =" + id;

                connect.Open();

                Movie movie = connect.Query<Movie>(sql).First();

                connect.Close();

                return movie;
            }
        }

        public void RegisterMovie(Movie m)
        {
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                string sql = "insert into movies " +
                   $"values ('{m.ID}', '{m.Title}', '{m.Genre}', '{m.Year}', '{m.Runtime}' )";

                connect.Open();
                connect.Query<Movie>(sql);
                connect.Close();


            }
        }
        public void DeleteMovie(int id)
        {
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                string sql = "delete from Movies where id=" + id;

                connect.Open();
                connect.Query<Movie>(sql);
                connect.Close();
            }
        }

        public List<Movie> SearchMoviesByGenre(string searchbyGenre)
        {
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                string sql = $"select * from movies where Genre like '%{searchbyGenre}%'";

                connect.Open();

                List<Movie> m = connect.Query<Movie>(sql).ToList();

                connect.Close();

                return m;
            }
        }

        public List<Movie> SearchMoviesByTitle(string searchbyTitle)
        {
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                string sql = $"select * from movies where title like '%{searchbyTitle}%'";

                connect.Open();

                List<Movie> m = connect.Query<Movie>(sql).ToList();

                connect.Close();

                return m;
            }
        }

        public void EditMovie(Movie m)
        {
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                string sql = "update movies " +
                    $"set title='{m.Title}', genre='{m.Genre}', `year`='{m.Year}', runtime='{m.Runtime}' " +
                    $"where id={m.ID}";

                connect.Open();
                connect.Query<Movie>(sql);
                connect.Close();
            }
        }


    }
}
