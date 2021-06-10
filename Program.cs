/*
*@ Sung Kim
* Assignment3 (late submission)
* I did 3 extra points: adding 2 extra patterns, and using Console.SetCursorPosition()
*/

using System;
using static System.Console;
using System.Threading;
using NLog;

namespace Assignment3
{
     class Program
    {
        const int Dead = 0;             // Using a grid of 0's and 1's will help us count
        const int Alive = 1;            //   count neighbors efficiently in the Life program.
        const int GridSizeX = 25;
        const int GridSizeY = 25;
        const int CosmicRayPercent = 4;

        static string AliveString = " * ";
        static string DeadString = "   ";
        static readonly Logger logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            logger.Info("=== Starting Program ===");

            // default setting
            bool interactiveMode = true;
            bool rPentominoMode = true;
            bool thurnerbirdMode = false;
            bool blockGliderMode = false;
            bool cosmicRays = false;
            int fillPercentage = 20;
            int finalGeneration = -1;  //when it's silent mode, finalGeneration = 50 / interactiveMode = -1
            

            logger.Info("--Parsing command line arguments--");
            if (args.Length > 0)    //1st argument
            {
                string mode = args[0].ToLower();
                if (mode.StartsWith('s'))
                {
                    interactiveMode = false;
                    finalGeneration = 50;
                }
                else if (mode.StartsWith("i"))
                {
                    interactiveMode = true;
                    finalGeneration = -1; 
                }
                else
                {
                    WriteLine("Error: Invalid argument. Please type 'interactive' or 'silent'. ");
                }
            }

            if (args.Length > 1)    // 2nd argument
            {
                string initialGrid = args[1].ToLower();
                if (initialGrid.StartsWith("r"))        // Initialized with R-Pentamino pattern (default)
                    rPentominoMode = true;
                else if (initialGrid.StartsWith("t"))   // Initialized with Thunderbird pattern (extra points)                
                {
                    rPentominoMode = false;
                    thurnerbirdMode = true;
                }
                else if (initialGrid.StartsWith("b"))  // Initialized with Glider pattern (extra points)                                
                {
                    rPentominoMode = false;
                    blockGliderMode = true;
                }
                else
                {
                    rPentominoMode = false;
                    try 
                    {
                        fillPercentage = int.Parse(initialGrid);
                        if (fillPercentage < 0 || fillPercentage > 100)
                            throw new FormatException();
                    }
                    catch (FormatException)
                    {
                        WriteLine("Error: Second argument invalid. Please use 'r-pentomino' or a random-fill percentage (0-100).");
                        return;
                    }
                    catch (OverflowException)
                    {
                        WriteLine("Error: Second argument invalid. Please use 'r-pentomino' or a random-fill percentage (0-100).");
                        return;
                    }
                }
               
            }
            if (args.Length > 2)    //3rd argument
            {
                try
                {
                    finalGeneration = int.Parse(args[2]);
                    if ( finalGeneration < -1 || finalGeneration > int.MaxValue)
                        throw new FormatException(); 
                }
                catch (FormatException)
                {
                    WriteLine("Error: Invalid value for third argument. Please enter number of generation to run (or -1 to run 'forever')");
                    return;
                }
            }

            logger.Info("... argument parsing complete.");
            logger.Debug($"interactiveMode: {interactiveMode}, useRPentomino: {rPentominoMode}, fillPercentage: {fillPercentage}, finalGeneration: {finalGeneration}");

        //=======================================================================================//

        int generation = 0;
        int[,] currentGrid = new int[GridSizeX, GridSizeY];
        
        string uiMode ="";
        string startMode = $"Random ({fillPercentage}% filled)";

        if(interactiveMode)
        {
            uiMode = "Interactive Mode (press F, R, or Q)";
        }
        else
            uiMode = "Silent Mode";

        if (rPentominoMode) startMode = "R-Pentomino";
        if (thurnerbirdMode) startMode = "Thunder bird";
        if (blockGliderMode) startMode = "Block and Glider";

        string runDuration = "";
        if(finalGeneration == -1)
        {
            runDuration = "until 'Q' is pressed.";
        }
        else
            runDuration = $"for {finalGeneration} generations";
        
        string cosmicRayMessage = "";
        if(cosmicRays)
        {
            cosmicRayMessage = " with Cosmic Rays";
        }
        else cosmicRayMessage = "";

  
        logger.Info($"{uiMode}");
        logger.Info($"Starting with: {startMode}");
        logger.Info($"Running {runDuration}");


        WriteLine("Conway's Game of Life:");
        WriteLine("=================================================");
        WriteLine($"  {uiMode}{cosmicRayMessage}");
        WriteLine($"  Starting with: {startMode}");
        WriteLine($"  Running {runDuration}");

        if (rPentominoMode)
            InitialRPentominoGrid(currentGrid);
        else if (thurnerbirdMode)   //extra point
            InitialThunderbird(currentGrid);
        else if (blockGliderMode)        //extra point
            InitialBlockGlider(currentGrid);
        else
            RandomFilledGrid(currentGrid, fillPercentage);

        bool done = false;
            while (!done)
            {
                if (generation == finalGeneration)
                    break;
                if (interactiveMode)
                    ShowGeneration(currentGrid, generation, cosmicRayMessage);

                logger.Info($"Generation: {generation}  aliveCount: {AliveCellCount(currentGrid)}");

                if (interactiveMode && Console.KeyAvailable)
                {
                    ConsoleKey key = Console.ReadKey(true).Key;
                    logger.Info($"{key} pressed!");

                    switch (key)
                    {
                        case ConsoleKey.Q:
                            done = true;
                            break;

                        case ConsoleKey.F:
                            RandomFilledGrid(currentGrid, fillPercentage);
                            generation = 0;
                            ShowGeneration(currentGrid, generation, cosmicRayMessage);
                            break;
                        
                        case ConsoleKey.R:
                            InitialRPentominoGrid(currentGrid);
                            generation = 0;
                            ShowGeneration(currentGrid, generation, cosmicRayMessage);
                            break;
                        
                        // Extra credit for adding Thunderbird pattern
                        case ConsoleKey.T:
                            InitialThunderbird(currentGrid);
                            generation = 0;
                            ShowGeneration(currentGrid, generation, cosmicRayMessage);
                            break;

                        // Extra credit for adding Thunderbird pattern
                        case ConsoleKey.B:
                            InitialBlockGlider(currentGrid);
                            generation = 0;
                            ShowGeneration(currentGrid, generation, cosmicRayMessage);
                            break; 

                        default:
                            break;
                    }
                }

                currentGrid = CalculateNextGeneration(currentGrid);
                
                if (cosmicRays)
                {
                    Random rng = new Random();
                    if (rng.Next() % 100 < CosmicRayPercent)
                    {
                        int x = rng.Next() % GridSizeX;
                        int y = rng.Next() % GridSizeY;

                        if (currentGrid[x,y] == Alive)
                        
                            currentGrid[x,y] = Dead;
                        
                        else    
                            currentGrid[x,y] = Alive;
                    }
                }
                generation++;
            }

            // Display final generation (even in silent mode)
            ShowGeneration(currentGrid, generation, cosmicRayMessage);
            logger.Info($"Final Generation: {generation}  aliveCount: {AliveCellCount(currentGrid)}");

            logger.Info("=== Ending Program ===");
        }

        //===========================   Methods  ========================================================//

        //R-PentominoGrid (not sure if this is right way)
        static void InitialRPentominoGrid(int[,] grid)
        {
            logger.Info("--- Filling grid with R-Pentomino pattern");
            int x,y;
            for (x = 0; x < GridSizeX; x++)
                for (y = 0; y < GridSizeY; y++)
                    grid[x,y] = 0;
            x = GridSizeX / 2;
            y = GridSizeY / 2;
            grid[x,y-1] = Alive;
            grid[x+1,y-1] = Alive;
            grid[x,y] = Alive;
            grid[x-1,y] = Alive;
            grid[x,y+1] = Alive;
            
            //hard coded
            // grid[11,13] = Alive;
            // grid[11,12] = Alive;
            // grid[11,11] = Alive;
            // grid[10,12] = Alive;
            // grid[12,11] = Alive;
        }

        // Thunderbrid Grid 
        static void InitialThunderbird(int[,] grid)
        {
          logger.Info("--- Filling grid with Thunder bird pattern");
            int x,y;
            for (x = 0; x < GridSizeX; x++)
                for (y = 0; y < GridSizeY; y++)
                    grid[x,y] = 0;
            x = GridSizeX / 2;
            y = GridSizeY / 2;
            grid[x-1,y-2] = Alive;
            grid[x,y-2] = Alive;
            grid[x+1,y-2] = Alive;
            grid[x,y] = Alive;
            grid[x,y+1] = Alive;
            grid[x,y+2] = Alive;   
        }

        static void InitialBlockGlider(int[,] grid)
        {
            logger.Info("--- Filling grid with Glider pattern");
            int x,y;
            for (x = 0; x < GridSizeX; x++)
                for (y = 0; y < GridSizeY; y++)
                    grid[x,y] = 0;
            x = GridSizeX / 2;
            y = GridSizeY / 2;
            grid[x-1,y-2] = Alive;
            grid[x-2,y-2] = Alive;
            grid[x-2,y-1] = Alive;
            grid[x,y] = Alive;
            grid[x,y-1] = Alive;
            grid[x+1,y] = Alive;
        }

        static void ShowGeneration(int[,] grid, int generation, string optionalMessage = "")
        {  
            WriteLine($"Generation #{generation}");
            WriteLine($"+{PrintDashes(GridSizeY)}+");
             
            for (int y = 0; y < GridSizeY; y++)
            {
                string s = "|";
                string cell = "";
                for (int x = 0; x < GridSizeX; x++)
                {
                    if (grid[x,y] == Alive)
                        cell = AliveString;
                    else
                        cell = DeadString;

                    s += cell;
                }
                s += "|";
                WriteLine(s);
            }
            WriteLine($"+{PrintDashes(GridSizeX)}+");
            Thread.Sleep(500);
            Console.SetCursorPosition(0,4); // extra point

        }

        static string PrintDashes(int number)
        {
            return new string('-', number * AliveString.Length);
        }

        static int AliveCellCount(int[,] grid)
        {
            int count = 0;
            for (int x=0; x < GridSizeX; x++)
                for (int y=0; y < GridSizeY; y++)
                    count += grid[x,y];
            return count;
        }
        
        static int[,] CalculateNextGeneration(int[,] grid)
        {
            int[,] nextGrid = new int[GridSizeX, GridSizeY];
            
            for (int x = 1; x < GridSizeX -1; x++)
            {
                for (int y = 1; y < GridSizeY -1; y++)
                {
                    int neighbors = grid[x-1,y-1] + grid[x,y-1] + grid[x+1,y-1] +
                                    grid[x-1,y] +                 grid[x+1,y] +
                                    grid[x-1,y+1] + grid[x,y+1] + grid[x+1,y+1];

                    if (grid[x,y] == Alive)
                    {
                        if (neighbors == 2 || neighbors == 3)
                            nextGrid[x,y] = Alive;
                            
                        else nextGrid[x,y] = Dead;
                    }
                    else
                    {
                        if (neighbors == 3)
                            nextGrid[x,y] = Alive;
                            
                        else nextGrid[x,y] = Dead;
                    }    
                }
            }
            return nextGrid;
        }

        static void RandomFilledGrid(int[,] grid, int fillPercentage)
        {
            logger.Info($"--- Random grid has been printed --- ({fillPercentage}%)");
            Random ran = new Random();

            for (int x=0; x < GridSizeX; x++)
                for (int y=0; y < GridSizeY; y++)

                    if (ran.Next() % 100 < fillPercentage)
                       grid[x,y] = Alive;
                    else    
                        grid[x,y] = Dead;
        }
        
    }
}
