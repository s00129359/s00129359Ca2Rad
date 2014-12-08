using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ca2.Models
{
    //initialise database
    internal class MovieDbInitialiser : DropCreateDatabaseAlways<NewDatabase0>
    {
        //seed the datase
        protected override void Seed(NewDatabase0 context)
        {
            // list of actors var
            var actorsWow = new List<Actor>()
            {
                new Actor() {actorFName = "Orlando", actorSName = "Bloom", actorSex = sex.male, placeOfBirth = "United Kingdom", actorAge = 37},
                new Actor() {actorFName = "Pierce ", actorSName = "Brosnan", actorSex = sex.male, placeOfBirth = "Ireland", actorAge = 61},
                new Actor() {actorFName = "Arnold", actorSName = "schwarzenegger", actorSex = sex.male, placeOfBirth = "Austria", actorAge = 67}
            };
            //movie list var
            var movieWow = new List<Movie>()
            {
                new Movie() {movieName = "Wow The Movie", 
                    movieRuntime = 153, 
                    movieShortDescription = "This is Impressive", 
                    movieGenre = genre.action,
                    actorsInMovie = actorsWow                   
                }
            };
            //save movie
            movieWow.ForEach(mov => context.Movie.Add(mov));
            context.SaveChanges();
            //actors list var
            var actorsHello = new List<Actor>()
            {
                new Actor() {actorFName = "Brad", actorSName = "Pitt", actorSex = sex.male, placeOfBirth = "USA", actorAge = 50},
                new Actor() {actorFName = "Leonardo", actorSName = "Dicaprio", actorSex = sex.male, placeOfBirth = "USA", actorAge = 40}             
            };
            //movie list var
            var movies = new List<Movie>()
            {
                new Movie() {movieName = "Hello", 
                    movieRuntime = 123, 
                    movieShortDescription = "A friendly man saying hello", 
                    movieGenre = genre.comedy,
                    actorsInMovie =  actorsHello                  
                }
            };
            //save movie
            movies.ForEach(mov => context.Movie.Add(mov));
            context.SaveChanges();
            //actors list var
            var greatActors = new List<Actor>()
            { 
                new Actor() {actorFName = "Angelina", actorSName = "Jolie", actorSex = sex.female, placeOfBirth = "USA", actorAge = 39},
                new Actor() {actorFName = "Eva ", actorSName = "Green", actorSex = sex.female, placeOfBirth = "France", actorAge = 35},
                new Actor() {actorFName = "Natalie ", actorSName = "Portman", actorSex = sex.female, placeOfBirth = "Isreal", actorAge = 34},
            };

            //movie list var
            var movieGreat = new List<Movie>()
            {
                new Movie() {movieName = "Great Movies", 
                    movieRuntime = 53, 
                    movieShortDescription = "This is great", 
                    movieGenre = genre.romance,
                    actorsInMovie = greatActors                                 
                }
            };
            movieGreat.ForEach(mov => context.Movie.Add(mov));
            context.SaveChanges();

            //concatonate var
            var concat = actorsHello.Concat(greatActors);
            var movieNew = new List<Movie>()
            {
                new Movie() {movieName = "New Movie", 
                    movieRuntime = 253, 
                    movieShortDescription = "This is new", 
                    movieGenre = genre.childern,
                    actorsInMovie = concat.ToList()                                 
                }
            };
            movieNew.ForEach(mov => context.Movie.Add(mov));
            context.SaveChanges();

            //concatonate 2 vars
            var concat2 = actorsHello.Concat(actorsWow);
            
            var movieFinal = new List<Movie>()
            {
                new Movie() {movieName = "Final Movie", 
                    movieRuntime = 133, 
                    movieShortDescription = "This is Final", 
                    movieGenre = genre.horror,
                    actorsInMovie = concat2.ToList()                                 
                }
            };
            movieFinal.ForEach(mov => context.Movie.Add(mov));
            context.SaveChanges();

        }//end seed
    }//end initiser 

    public class NewDatabase0 : DbContext
    {
        public DbSet<Movie> Movie { get; set; }
        public DbSet<Actor> Actor { get; set; }
        public DbSet<ActorScreenName> ScreenName { get; set; }
        public NewDatabase0()
            : base("NewDatabase0")
        { }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Movie>()
        //        .HasMany(m => m.actorsInMovie)
        //        .WithMany(m => m.moviesActorIn)
        //        .Map(mA =>
        //        {
        //            mA.ToTable("ScreenName");
        //            mA.MapLeftKey("MovieId");
        //            mA.MapRightKey("ActorId");
        //        });
        //    base.OnModelCreating(modelBuilder);
        //}

        
    }

    public class ActorScreenName
    {
        [Key, Column(Order = 0)]    // Left key
        public int movieId { get; set; }
        [Key, Column(Order = 1)]        // Right key
        public int actorId { get; set; }
        //both make Primary key
        public string ScreenName { get; set; }

        //many to many
        public virtual Movie Movie { get; set; }
        public virtual Actor Actor { get; set; }
    }//end ActorScreenName class

    public class Movie
    {
        [Key]
        public int movieId { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Name must be longer.", MinimumLength = 4)]
        [Display(Name = "Movie Name")]
        public string movieName { get; set; }
        [Required]
        public double movieRuntime { get; set; }
        public genre movieGenre { get; set; }
        [Required]
        [StringLength(75, ErrorMessage = "Desc should be short.", MinimumLength = 7)]
        [Display(Name = "Movie Desc")]
        public string movieShortDescription { get; set; }


       // public virtual Actor Actor { get; set; }
        //collection of actors that act in each movie
        public virtual ICollection<Actor> actorsInMovie { get; set; }

    }//end movie class

    public enum genre
    {
        action, horror, comedy,
        romance, childern, scary
    }//end enum for genre of movie

    public class Actor
    {
        [Key]
        public int actorId { get; set; }
        [Required]
        [StringLength(25, ErrorMessage = "Name must be longer.", MinimumLength = 2)]
        [Display(Name = "Actor first Name")]
        public string actorFName { get; set; }
        [Required]
        [StringLength(25, ErrorMessage = "Name must be longer.", MinimumLength = 2)]
        [Display(Name = "Actor second Name")]
        public string actorSName { get; set; }
        [Required]
        public int actorAge { get; set; }
        public sex actorSex { get; set; }
        public string placeOfBirth { get; set; }

       // public virtual Movie Movie { get; set; }
        //list of movies each actor acts in
        public virtual ICollection<Movie> moviesActorIn { get; set; }
    }//end Actor class

    public enum sex { male, female }//enum for actor gender
}