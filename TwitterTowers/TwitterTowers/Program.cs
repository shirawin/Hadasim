using System;

namespace TwitterTowers
{
    class Program
    {
        static void RectangularTower()
        {
            Console.Write("Enter the length of the tower: ");
            int length = int.Parse(Console.ReadLine());
            Console.Write("Enter the width of the tower: ");
            int width = int.Parse(Console.ReadLine());

            if (length == width)
            {
                Console.WriteLine("This is a square");
                Console.WriteLine("The perimeter of the tower is " + (2 * (length + width)));
            }
            else if (Math.Abs(length - width) > 5)
            {
                Console.WriteLine("The area of the tower is " + (length * width));
            }
            else
            {
                Console.WriteLine("The perimeter of the tower is " + (2 * (length + width)));
            }
        }

        static void TriangularTower()
        {
            Console.Write("Enter the height of the tower: ");
            int height = int.Parse(Console.ReadLine());
            Console.Write("Enter the width of the tower: ");
            int width = int.Parse(Console.ReadLine());
            int spaces, stars,lines, cnt;
            //Check if the width is odd and shorter than twice the height
            if (width % 2 == 1 && width < height * 2)
            {
                cnt = 0;
                for (int i = 3; i < width; i += 2)
                {
                    cnt++;
                }
                stars = 3;
                spaces = (width / 2);
                string spaceString = "";
                //Print the first line.
                for (int i = 0; i < spaces; i++)
                {
                    spaceString += " ";
                }
                Console.WriteLine(spaceString + "*");
                // Checking if there are an equal number of rows of the same width.
                if ((height - 2) % cnt == 0)
                {
                    lines = (height - 2) / cnt;
                    for (int i = 2; i < height; i++)
                    {
                        //Print the spaces
                        for (int j = 1; j < spaces; j++)
                        {
                            Console.Write(" ");
                        }
                        //Print the stars.
                        for (int j = 1; j <= stars; j++)
                        {
                            Console.Write("*");
                        }
                        Console.WriteLine();
                        lines--;
                        if (lines==0)
                        {
                            stars += 2;
                            spaces--;
                        }

                    }
                }
                else
                {
                    int rest = (height - 2) % cnt; //The rest.
                    lines = (height - 2) / cnt; // The number of groups.
                    bool first = true;
                    for (int i = 2; i < height; i++)
                    {
                        //Print the spaces
                        for (int j = 1; j < spaces; j++)
                        {
                            Console.Write(" ");
                        }
                        //Print the stars.
                        for (int j = 0; j < stars; j++)
                        {
                            Console.Write("*");
                        }
                        Console.WriteLine();
                        lines--;
                        if (lines == 0)
                        {
                            if (first)
                            {
                                first = !first;
                                lines = rest;
                            }
                            else
                            {
                                stars += 2;
                                lines = (height - 2) / cnt;
                                spaces--;
                            }
                        }

                    }
                }
                //Print the last line.
                for (int i = 0; i < width; i++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("The triangle cannot be printed.");
            }
        }

        static void Main()
        {
            while (true)
            {
                Console.WriteLine("1. Rectangular Tower");
                Console.WriteLine("2. Triangular Tower");
                Console.WriteLine("3. Exit");
                Console.Write("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine());
                if (choice == 1)
                {
                    RectangularTower();
                }
                else if (choice == 2)
                {
                    Console.WriteLine("1. Calculate perimeter");
                    Console.WriteLine("2. Print triangle");
                    Console.Write("Enter your choice: ");
                    int subChoice = int.Parse(Console.ReadLine());
                    if (subChoice == 1)
                    {
                        Console.Write("Enter the height of the triangle: ");
                        int height = int.Parse(Console.ReadLine());
                        Console.Write("Enter the base of the triangle: ");
                        int @base = int.Parse(Console.ReadLine());
                        Console.WriteLine("The perimeter of the triangle is " + (@base + 2 * height));
                    }
                    else if (subChoice == 2)
                    {
                        TriangularTower();
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice");
                    }
                }
                else if (choice == 3)
                {
                    Console.WriteLine("Exiting...");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice");
                }
            }
        }
    }
}
