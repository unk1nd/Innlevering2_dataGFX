using System;

namespace Oblig2_sphere
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Sphere game = new Sphere())
            {
                game.Run();
            }
        }
    }
#endif
}

