using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlaylistAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Please provide a Playlist file path and a report file path");
            } else
            {
                string PlaylistFile = args[0];
                string ReportFile = args[1];
                string[] AllLines;

                try
                {
                    AllLines = File.ReadAllLines(PlaylistFile);
                }
                catch
                {
                    Console.WriteLine("Error reading playlist File");
                    Console.ReadLine();
                    return ;
                }
                AllLines = AllLines.Skip(1).ToArray();
                var Songs = from line in AllLines
                                   let data = line.Split('\t')
                                   select new Song()
                                   {

                                       Name = data[0],
                                       Artist = data[1],
                                       Album = data[2],
                                       Genre = data[3],
                                       Size = Int64.Parse(data[4]),
                                       Time = int.Parse(data[5]),
                                       Year = int.Parse(data[6]),
                                       Plays = int.Parse(data[7]),
                                   };
                var SongsWith200PlusPlays = from song in Songs where song.Plays >= 200 select song;
                var SongsAlternative = from song in Songs where song.Genre == "Alternative" select song;
                var SongsRap = from song in Songs where song.Genre == "Hip-Hop/Rap" select song;
                var Fishbowl = from song in Songs where song.Album == "Welcome to the Fishbowl" select song;
                var OldSongs = from song in Songs where song.Year < 1970 select song;
                var LongName = from song in Songs where song.Name.Length > 85 select song.Name;
                var LongestSong = from song in Songs where song.Time == Songs.Max(time => time.Time) select song;
                string report = "";

                report += "Songs that received 200 or more plays:\n";
                foreach (Song c in SongsWith200PlusPlays)
                {
                    report += c.ToString() + " \n";
                }
                
                int alt = 0;
                foreach (Song c in SongsAlternative)
                {
                    alt++;
                }
                report += "\nNumber of Alternative songs: " + alt + "\n";
                int rap = 0;
                foreach (Song c in SongsRap)
                {
                    rap++; ;
                }
                report += "\nNumber of Hip-Hop/Rap songs: " + rap + "\n";

                report += "\nSongs from the album Welcome to the Fishbowl: \n";
                foreach (Song c in Fishbowl)
                {
                    report += c.ToString() + " \n";
                }
                report += "\nSongs from before 1970:\n";
                foreach (Song c in OldSongs)
                {
                    report += c.ToString() + " \n";
                }
                report += "\nSong names longer than 85 characters:\n";
                foreach (string c in LongName)
                {
                    report += c + " \n";
                }
                report += "\nLongest song:\n";
                foreach (Song c in LongestSong)
                {
                    report += c.ToString() + " \n";
                }
                try
                {
                    System.IO.File.WriteAllText(@ReportFile, report);
                }
                catch
                {
                    Console.WriteLine("Report file cannot be opened or writen to.");
                }


            }
            Console.ReadLine();
        }
    }

    public class Song
    {
        public string Name;
        public string Artist;
        public string Album;
        public string Genre;
        public Int64 Size;
        public int Time;
        public int Year;
        public int Plays;
        public Song()
        {

        }
        override public string ToString()
        {
            return String.Format("Name: {0}, Artist: {1}, Album: {2}, Genre: {3}, Size: {4}, Time: {5}, Year: {6}, Plays: {7}", Name, Artist, Album, Genre, Size, Time, Year, Plays);
        }
    }
}
